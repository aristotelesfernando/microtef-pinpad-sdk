using Pinpad.Sdk.Model;
using System.Collections.ObjectModel;
using Pinpad.Sdk.Commands.DataSet;
using Pinpad.Sdk.Commands.Request;
using Pinpad.Sdk.Commands.TypeCode;
using Pinpad.Sdk.Commands;
using Pinpad.Sdk.Commands.Response;

namespace Pinpad.Sdk.Pinpad
{
    // TODO: Documentar.
    public sealed class PinpadPrinter : IPinpadPrinter
    {
        private PinpadCommunication Communication { get; set; }
        private Collection<PrinterItem> ItemsToPrint { get; set; }

        public PinpadPrinter(PinpadCommunication communication)
        {
            this.Communication = communication;
            this.ItemsToPrint = new Collection<PrinterItem>();
        }

        public IPinpadPrinter AddLogo ()
        {
            PrinterItem newQrCode = new PrinterItem
            {
                Type = IngenicoPrinterAction.PrintImage
            };

            this.ItemsToPrint.Add(newQrCode);

            return this;
        }
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
        public IPinpadPrinter AppendLine (PrinterAlignmentCode alignment, 
            PrinterFontSize fontSize, string text, params object[] args)
        {
            PrinterItem newLine = new PrinterItem
            {
                Type = IngenicoPrinterAction.PrintText,
                Text = string.Format(text, args),
                Alignment = alignment,
                FontSize = fontSize
            };

            this.ItemsToPrint.Add(newLine);

            return this;
        }

        public bool Print ()
        {
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

        private PrtRequest GetRequestByType(PrinterItem item)
        {
            switch (item.Type)
            {
                case IngenicoPrinterAction.Start:
                    return this.CreateRequestToStartPrinting();

                case IngenicoPrinterAction.PrintImage:
                    return this.CreateRequestToPrintImage(item);

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

        // Methods to create a printing request:
        private PrtRequest CreateRequestToStartPrinting()
        {
            PrtRequest startRequest = new PrtRequest();

            // Setup request to start printing:
            startRequest.PRT_Action.Value = IngenicoPrinterAction.Start;

            return startRequest;
        }
        private PrtRequest CreateRequestToPrintImage(PrinterItem item)
        {
            PrtRequest printImageRequest = new PrtRequest();

            // Setup request to print an image:
            printImageRequest.PRT_Action.Value = IngenicoPrinterAction.PrintImage;
            printImageRequest.PRT_Horizontal.Value = 1;
            printImageRequest.PRT_DATA.Value = PrinterLogo.FileName;

            // If image does not exist in pinpad memory:
            if (this.VerifyIfLogoExists() == true)
            {
                // Loads the image into the memory:
                this.ReloadImage();
            }

            return printImageRequest;
        }
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
        private PrtRequest CreateRequestToFinishPrinting()
        {
            PrtRequest finishPrintingRequest = new PrtRequest();

            // Setup request to finish pinting:
            finishPrintingRequest.PRT_Action.Value = IngenicoPrinterAction.End;

            return finishPrintingRequest;
        }
        private PrtRequest CreateRequestToSkipLine(PrinterItem item)
        {
            PrtRequest skipLineRequest = new PrtRequest();

            // Setup request to skip line:
            skipLineRequest.PRT_Action.Value = IngenicoPrinterAction.SkipLine;
            skipLineRequest.PRT_Steps.Value = item.StepsToSkip;

            return skipLineRequest;
        }

        private bool VerifyIfLogoExists()
        {
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
                lfr.LFR_Data.Value.Add(new HexadecimalData(imageLine));
                status = this.Communication.SendRequestAndVerifyResponseCode(lfr);
            }

            // Finish sending image:
            status = this.Communication.SendRequestAndVerifyResponseCode(new LfeRequest());
        }
    }
}
