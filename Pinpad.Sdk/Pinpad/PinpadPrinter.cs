using Pinpad.Sdk.Model;
using System.Collections.ObjectModel;
using Pinpad.Sdk.Commands.DataSet;
using Pinpad.Sdk.Commands.Request;
using Pinpad.Sdk.Commands.TypeCode;

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

        public IPinpadPrinter AddImage (string imagePath)
        {
            // TODO: Adicionar imagem ao buffer!
            LfiRequest request = new LfiRequest();
            request.LFI_FILENAME.Value = imagePath;

            bool status = this.Communication.SendRequestAndVerifyResponseCode(request);

            return this;
        }
        public IPinpadPrinter AddQrCode(PrinterAlignmentCode alignment, string qrCodeMessage)
        {
            PrinterItem newQrCode = new PrinterItem
            {
                Type = IngenicoPrinterAction.PrintQrCode,
                FontSize = PrinterFontSize.Big,
                Alignment = alignment,
                QrCodeMessage = qrCodeMessage
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
            this.ItemsToPrint.Add(new PrinterItem
            {
                Type = IngenicoPrinterAction.SkipLine,
                StepsToSkip = 8
            });

            // Add begin printing:
            this.ItemsToPrint.Insert(0, new PrinterItem { Type = IngenicoPrinterAction.Start });

            // Add finish printing:
            this.ItemsToPrint.Add(new PrinterItem { Type = IngenicoPrinterAction.End });

            foreach (PrinterItem item in this.ItemsToPrint)
            {
                PrtRequest request = new PrtRequest();
                
                switch (item.Type)
                {
                    case IngenicoPrinterAction.Start:
                        request.PRT_Action.Value = IngenicoPrinterAction.Start;
                        break;
                    case IngenicoPrinterAction.PrintImage:
                        // TODO: Falta implementar.
                        break;
                    case IngenicoPrinterAction.PrintQrCode:
                        // TODO: Falta implementar.
                        request.PRT_Action.Value = IngenicoPrinterAction.PrintQrCode;
                        request.PRT_Size.Value = item.FontSize;
                        request.PRT_Alignment.Value = item.Alignment;
                        request.PRT_Horizontal.Value = 1;
                        request.PRT_DATA.Value = item.QrCodeMessage;
                        request.PRT_DATA.Value = request.PRT_DATA.Value.PadRight(512, ' ');
                        break;
                    case IngenicoPrinterAction.PrintText:
                        request.PRT_Action.Value = IngenicoPrinterAction.PrintText;
                        request.PRT_Alignment.Value = item.Alignment;
                        request.PRT_Size.Value = item.FontSize;
                        request.PRT_DATA.Value = item.Text;
                        break;
                    case IngenicoPrinterAction.SkipLine:
                        request.PRT_Action.Value = IngenicoPrinterAction.SkipLine;
                        request.PRT_Steps.Value = item.StepsToSkip;
                        break;
                    case IngenicoPrinterAction.End:
                        request.PRT_Action.Value = IngenicoPrinterAction.End;
                        break;
                }

                // TODO: Verificar retorno?
                bool status = this.Communication.SendRequestAndVerifyResponseCode(request);

                if (status == false)
                {
                    return false;
                }
            }

            this.ItemsToPrint.Clear();

            return true;
        }
    }
}
