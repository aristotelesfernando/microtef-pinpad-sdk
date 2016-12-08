namespace Pinpad.Sdk.Model
{
    public interface IPinpadPrinter
    {
        IPinpadPrinter AddImage(string imagePath);
        IPinpadPrinter AddQrCode(string imagePath);
        IPinpadPrinter AppendLine(PrinterAlignmentCode alignment,
            PrinterFontSize fontSize, string text, params object[] args);
        bool Print();
    }
}
