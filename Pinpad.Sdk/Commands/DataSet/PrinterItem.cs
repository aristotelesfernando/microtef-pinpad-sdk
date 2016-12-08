using Pinpad.Sdk.Commands.TypeCode;
using Pinpad.Sdk.Model;

namespace Pinpad.Sdk.Commands.DataSet
{
    // TODO: Documentar.
    internal sealed class PrinterItem
    {
        public IngenicoPrinterAction Type { get; set; }
        public string Text { get; set; }
        public PrinterAlignmentCode Alignment { get; set; }
        public PrinterFontSize FontSize { get; set; }
        public string QrCodeMessage { get; set; }
        public byte [] Image { get; set; }
        public int StepsToSkip { get; set; }
    }
}
