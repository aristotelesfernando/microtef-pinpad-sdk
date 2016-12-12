using Pinpad.Sdk.Commands.TypeCode;
using Pinpad.Sdk.Model;

namespace Pinpad.Sdk.Commands.DataSet
{
    /// <summary>
    /// Item to be printed by a pinpad thermal printer.
    /// </summary>
    internal sealed class PrinterItem
    {
        /// <summary>
        /// Printing type, e.g. image, QR code, text.
        /// </summary>
        public IngenicoPrinterAction Type { get; set; }
        /// <summary>
        /// Text to be printed.
        /// </summary>
        public string Text { get; set; }
        /// <summary>
        /// Text alignment.
        /// </summary>
        public PrinterAlignmentCode Alignment { get; set; }
        /// <summary>
        /// Text font size.
        /// </summary>
        public PrinterFontSize FontSize { get; set; }
        /// <summary>
        /// Steps to be skipped in blank.
        /// Useful at the end of the receipt, because of the gap between the thermal 
        /// printer and the accessory to cut the paper.
        /// </summary>
        public int StepsToSkip { get; set; }
    }
}
