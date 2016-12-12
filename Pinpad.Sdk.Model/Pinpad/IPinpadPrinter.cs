namespace Pinpad.Sdk.Model
{
    /// <summary>
    /// Responsible for representate pinpad thermal printer.
    /// Only available for Ingenico iWL250 pinpad.
    /// </summary>
    public interface IPinpadPrinter
    {
        /// <summary>
        /// Add Stone logotype to the printer buffer.
        /// </summary>
        /// <returns>Itself.</returns>
        IPinpadPrinter AddLogo();
        /// <summary>
        /// Add QR code to the printer buffer.
        /// </summary>
        /// <param name="alignment">QR code alignment.</param>
        /// <param name="qrCodeMessage">QR code message.</param>
        /// <returns>Itself.</returns>
        IPinpadPrinter AddQrCode(PrinterAlignmentCode alignment, string qrCodeMessage);
        /// <summary>
        /// Add line of text to the printer buffer.
        /// </summary>
        /// <param name="alignment">Text alignment.</param>
        /// <param name="fontSize">Font size.</param>
        /// <param name="text">Text to print.</param>
        /// <param name="args">Arguments to the text to print.</param>
        /// <returns>Itself.</returns>
        IPinpadPrinter AppendLine(PrinterAlignmentCode alignment,
            PrinterFontSize fontSize, string text, params object[] args);
        /// <summary>
        /// Print all content in printer buffer.
        /// </summary>
        /// <returns>Whether the printing was successful or not.</returns>
        bool Print();
    }
}
