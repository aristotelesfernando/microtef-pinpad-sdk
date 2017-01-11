using Pinpad.Sdk.Model;
using System.Collections.ObjectModel;
using Pinpad.Sdk.Commands.DataSet;
using Pinpad.Sdk.Commands.Request;
using Pinpad.Sdk.Commands.TypeCode;
using Pinpad.Sdk.Commands.Response;

namespace Pinpad.Sdk.Pinpad
{
    /// <summary>
    /// Responsible for representate pinpad thermal printer.
    /// Only available for Ingenico iWL250 pinpad.
    /// </summary>
    public sealed class IngenicoPinpadPrinter : IPinpadPrinter
    {
        /// <summary>
        /// Indicates wether the thermal printer is supported or not.
        /// </summary>
        public bool IsSupported
        {
            get
            {
                if (this.PinpadInformation.ManufacturerName.Contains("INGENICO") == true
                    && this.PinpadInformation.Model.Contains("iWL250") == true)
                {
                    return true;
                }

                return false;
            }
        }
        /// <summary>
        /// Responsible for logical communication with pinpad.
        /// </summary>
        private PinpadCommunication Communication { get; set; }
        /// <summary>
        /// Information about the pinpad connected.
        /// </summary>
        private IPinpadInfos PinpadInformation { get; set; }
        /// <summary>
        /// Printer buffer.
        /// </summary>
        internal Collection<PrinterItem> ItemsToPrint { get; private set; }
        
        /// <summary>
        /// Creates an instance of <see cref="IngenicoPinpadPrinter"/> with all it's properties.
        /// </summary>
        /// <param name="communication">Pinpad communication service.</param>
        /// <param name="infos">Real time pinpad information.</param>
        public IngenicoPinpadPrinter(PinpadCommunication communication, IPinpadInfos infos)
        {
            this.Communication = communication;
            this.PinpadInformation = infos;
            this.ItemsToPrint = new Collection<PrinterItem>();
        }

        /// <summary>
        /// Add Stone logotype to the printer buffer.
        /// </summary>
        /// <returns>Itself.</returns>
        public IPinpadPrinter AddLogo ()
        {
            // If image does not exist in pinpad memory:
            if (this.VerifyIfLogoExists() == false)
            {
                // Loads the image into the memory:
                this.ReloadImage();
            }

            PrinterItem newQrCode = new PrinterItem
            {
                Type = IngenicoPrinterAction.PrintImage
            };

            this.ItemsToPrint.Add(newQrCode);

            return this;
        }
        /// <summary>
        /// Add QR code to the printer buffer.
        /// </summary>
        /// <param name="alignment">QR code alignment.</param>
        /// <param name="qrCodeMessage">QR code message.</param>
        /// <returns>Itself.</returns>
        public IPinpadPrinter AddQrCode(PrinterAlignmentCode alignment, 
            string qrCodeMessage)
        {
            PrinterItem newQrCode = new PrinterItem
            {
                Type = IngenicoPrinterAction.PrintQrCode,
                FontSize = PrinterFontSize.Big,
                Alignment = alignment,
                Text = qrCodeMessage
            };

            this.ItemsToPrint.Add(newQrCode);

            return this;
        }
        /// <summary>
        /// Add line of text to the printer buffer.
        /// </summary>
        /// <param name="alignment">Text alignment.</param>
        /// <param name="fontSize">Font size.</param>
        /// <param name="text">Text to print.</param>
        /// <param name="args">Arguments to the text to print.</param>
        /// <returns>Itself.</returns>
        public IPinpadPrinter AppendLine (PrinterAlignmentCode alignment, 
            PrinterFontSize fontSize, string text, params object[] args)
        {
            PrinterItem newLine = new PrinterItem
            {
                Type = IngenicoPrinterAction.PrintText,
                Text = this.GetNormalizedText(string.Format(text, args)),
                Alignment = alignment,
                FontSize = fontSize
            };
            this.ItemsToPrint.Add(newLine);

            return this;
        }

        /// <summary>
        /// Append an empty line.
        /// </summary>
        /// <returns>Itself.</returns>
        public IPinpadPrinter AppendLine()
        {
            this.ItemsToPrint.Add(new PrinterItem
            {
                Type = IngenicoPrinterAction.SkipLine,
                StepsToSkip = 4
            });

            return this;
        }
        /// <summary>
        /// Add line separator.
        /// </summary>
        /// <returns>Itself.</returns>
        public IPinpadPrinter AddSeparator()
        {
            return this.AppendLine(PrinterAlignmentCode.Center, PrinterFontSize.Big, 
                "________________________");
        }
        /// <summary>
        /// Print all content in printer buffer.
        /// </summary>
        /// <returns>
        /// Whether the printing was successful or not. Also, returns false
        /// if the pinpad has no printer or the printer is not supported by this
        /// application.
        /// </returns>
        public bool Print ()
        {
            // If pinpad has no printer, or is not supported by this application,
            // this method must return false.
            if (this.IsSupported == false)
            {
                return false;
            }

            // Add command to begin printing:
            this.ItemsToPrint.Insert(0, new PrinterItem { Type = IngenicoPrinterAction.Start });

            // Add command to skip line at the end of the receipt:
            this.ItemsToPrint.Add(new PrinterItem
            {
                Type = IngenicoPrinterAction.SkipLine,
                StepsToSkip = 8
            });

            // Add command to finish printing:
            this.ItemsToPrint.Add(new PrinterItem { Type = IngenicoPrinterAction.End });

            foreach (PrinterItem item in this.ItemsToPrint)
            {
                // Get request based on the configured item:
                PrtRequest request = this.GetRequestByType(item);

                if (request == null)
                {
                    return false;
                }

                // Send the print request to the pinpad and verify whether it was
                // accepted or not:
                bool status = this.Communication.SendRequestAndVerifyResponseCode(request);

                if (status == false)
                {
                    return false;
                }
            }

            // Clear list of items to print:
            this.ItemsToPrint.Clear();

            return true;
        }

        /// <summary>
        /// Creates a printing request based on the action, that is,
        /// the <see cref="IngenicoPrinterAction"/>.
        /// </summary>
        /// <param name="item">Item to print.</param>
        /// <returns>Request to print something through thermal printer.</returns>
        private PrtRequest GetRequestByType(PrinterItem item)
        {
            switch (item.Type)
            {
                case IngenicoPrinterAction.Start:
                    return this.CreateRequestToStartPrinting();

                case IngenicoPrinterAction.PrintImage:
                    return this.CreateRequestToPrintLogo();

                case IngenicoPrinterAction.PrintQrCode:
                    return this.CreateRequestToPrintQrCode(item);
                    
                case IngenicoPrinterAction.PrintText:
                    return this.CreateRequestToPrintText(item);
                    
                case IngenicoPrinterAction.SkipLine:
                    return this.CreateRequestToSkipLine(item);
                    
                case IngenicoPrinterAction.End:
                    return this.CreateRequestToFinishPrinting();
            }

            return null;
        }

        /// <summary>
        /// Creates a <see cref="PrtRequest"/> configured to notificate the thermal printer
        /// that a printing operation is about to begin.
        /// </summary>
        /// <returns>The printing request.</returns>
        private PrtRequest CreateRequestToStartPrinting()
        {
            PrtRequest startRequest = new PrtRequest();

            // Setup request to start printing:
            startRequest.PRT_Action.Value = IngenicoPrinterAction.Start;

            return startRequest;
        }
        /// <summary>
        /// Creates a <see cref="PrtRequest"/> configured with Stone logotype to print.
        /// </summary>
        /// <returns>The image printing request.</returns>
        private PrtRequest CreateRequestToPrintLogo()
        {
            PrtRequest printImageRequest = new PrtRequest();

            // Setup request to print an image:
            printImageRequest.PRT_Action.Value = IngenicoPrinterAction.PrintImage;
            printImageRequest.PRT_Horizontal.Value = 1;
            printImageRequest.PRT_DATA.Value = PrinterLogo.FileName;
            
            return printImageRequest;
        }
        /// <summary>
        /// Creates a <see cref="PrtRequest"/> configured with a QR code to print.
        /// </summary>
        /// <param name="item">Item with QR code information.</param>
        /// <returns>The QR code printing request.</returns>
        private PrtRequest CreateRequestToPrintQrCode(PrinterItem item)
        {
            PrtRequest printQrCodeRequest = new PrtRequest();

            // Setup request to print a QR code:
            printQrCodeRequest.PRT_Action.Value = IngenicoPrinterAction.PrintQrCode;
            printQrCodeRequest.PRT_Size.Value = item.FontSize;
            printQrCodeRequest.PRT_Alignment.Value = item.Alignment;
            printQrCodeRequest.PRT_Horizontal.Value = 1;
            printQrCodeRequest.PRT_DATA.Value = item.Text;
            printQrCodeRequest.PRT_DATA.Value = printQrCodeRequest
                .PRT_DATA.Value.PadRight(512, ' ');

            return printQrCodeRequest;
        }
        /// <summary>
        /// Creates a <see cref="PrtRequest"/> configured with a text to print.
        /// </summary>
        /// <param name="item">Item with text to print information.</param>
        /// <returns>The text printing request.</returns>
        private PrtRequest CreateRequestToPrintText(PrinterItem item)
        {
            PrtRequest printTextRequest = new PrtRequest();

            // Setup request to print text:
            printTextRequest.PRT_Action.Value = IngenicoPrinterAction.PrintText;
            printTextRequest.PRT_Alignment.Value = item.Alignment;
            printTextRequest.PRT_Size.Value = item.FontSize;
            printTextRequest.PRT_DATA.Value = item.Text;

            return printTextRequest;
        }
        /// <summary>
        /// Creates a <see cref="PrtRequest"/> configured to notificate the thermal printer
        /// that all items have been sent, and the printer can already beging to print.
        /// </summary>
        /// <returns>The finish printing request.</returns>
        private PrtRequest CreateRequestToFinishPrinting()
        {
            PrtRequest finishPrintingRequest = new PrtRequest();

            // Setup request to finish pinting:
            finishPrintingRequest.PRT_Action.Value = IngenicoPrinterAction.End;

            return finishPrintingRequest;
        }
        /// <summary>
        /// Creates a <see cref="PrtRequest"/> configured with skip lines.
        /// </summary>
        /// <param name="item">Item with skip configuration.</param>
        /// <returns>The skip line request.</returns>
        private PrtRequest CreateRequestToSkipLine(PrinterItem item)
        {
            PrtRequest skipLineRequest = new PrtRequest();

            // Setup request to skip line:
            skipLineRequest.PRT_Action.Value = IngenicoPrinterAction.SkipLine;
            skipLineRequest.PRT_Steps.Value = item.StepsToSkip;

            return skipLineRequest;
        }

        /// <summary>
        /// Remove special characters and replace them with their equivalent.
        /// </summary>
        /// <param name="textToNormalize">Text to be normalized.</param>
        /// <returns>The normalized equivalent text.</returns>
        private string GetNormalizedText(string textToNormalize)
        {
            // Remove special characters with their equivalent:
            byte[] bytes = System.Text.Encoding.GetEncoding("iso-8859-8").GetBytes(textToNormalize);
            return System.Text.Encoding.UTF8.GetString(bytes, 0, bytes.Length);
        }
        /// <summary>
        /// Verify if Stone logo exist in pinpad memory.
        /// </summary>
        /// <returns>Return if the image exist or not.</returns>
        private bool VerifyIfLogoExists()
        {
            // Verify if pinpad is supported:
            if (this.IsSupported == false) { return true; }

            // Verify if image exists in pinpad memory!

            // Setup LFC request:
            LfcRequest lfcRequest = new LfcRequest();
            lfcRequest.LFC_FILENAME.Value = PrinterLogo.FileName;

            // Send request and receive response:
            LfcResponse  lfcResponse = this.Communication
                .SendRequestAndReceiveResponse<LfcResponse>(lfcRequest);

            // Verify whether image exists:
            if (lfcResponse.LFC_EXISTS.Value.HasValue == true)
            {
                return lfcResponse.LFC_EXISTS.Value.Value;
            }

            return false;
        }
        /// <summary>
        /// Load Stone logotype into pinpad memory.
        /// </summary>
        private void ReloadImage()
        {
            bool status;

            // Initialize to send image:
            LfiRequest request = new LfiRequest();
            request.LFI_FILENAME.Value = PrinterLogo.FileName;
            status = this.Communication.SendRequestAndVerifyResponseCode(request);

            // Reload image into pinpad memory:
            string[] imageLines = PrinterLogo.BmpFile.Split('-');

            foreach (string imageLine in imageLines)
            {
                LfrRequest lfr = new LfrRequest();
                lfr.LFR_Data.Value = imageLine;
                status = this.Communication.SendRequestAndVerifyResponseCode(lfr);
            }

            // Finish sending image:
            status = this.Communication.SendRequestAndVerifyResponseCode(new LfeRequest());
        }
    }
}
