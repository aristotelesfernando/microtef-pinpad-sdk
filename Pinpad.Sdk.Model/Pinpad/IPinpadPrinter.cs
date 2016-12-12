namespace Pinpad.Sdk.Model
{
    // TODO: Doc
    public interface IPinpadPrinter
    {
        IPinpadPrinter AddLogo();
        IPinpadPrinter AddQrCode(PrinterAlignmentCode alignment, string qrCodeMessage);
        IPinpadPrinter AppendLine(PrinterAlignmentCode alignment,
            PrinterFontSize fontSize, string text, params object[] args);
        bool Print();
    }
}
