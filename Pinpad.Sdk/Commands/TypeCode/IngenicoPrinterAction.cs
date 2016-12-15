namespace Pinpad.Sdk.Commands.TypeCode
{
    /// <summary>
    /// Type of the action the printer should perform.
    /// </summary>
    public enum IngenicoPrinterAction
    {
        /// <summary>
        /// Invalid type of action.
        /// </summary>
        Undefined   = 0,
        /// <summary>
        /// Start printing.
        /// </summary>
        Start       = 1,
        /// <summary>
        /// Finish printing.
        /// </summary>
        End         = 2,
        /// <summary>
        /// Print text.
        /// </summary>
        PrintText   = 3,
        /// <summary>
        /// Print image.
        /// </summary>
        PrintImage  = 4,
        /// <summary>
        /// Skip one or more lines.
        /// </summary>
        SkipLine    = 5,
        /// <summary>
        /// Print QR code.
        /// </summary>
        PrintQrCode = 6
    }
}
