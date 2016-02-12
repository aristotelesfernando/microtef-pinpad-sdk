using Pinpad.Sdk.Model.TypeCode;
using System;

namespace Pinpad.Sdk.Model.Exceptions 
{
    /// <summary>
    /// Exception for printer busy
    /// </summary>
    public class PrinterBusyException : PrinterException 
	{
        /// <summary>
        /// Printer Status
        /// </summary>
        public const PinpadPrinterStatus PrinterErrorCode = PinpadPrinterStatus.Busy;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="inner">The exception that is the cause of the current exception, or a null reference if no inner exception is specified.</param>
        public PrinterBusyException(string message = null, Exception inner = null) : base(PrinterErrorCode, message, inner) { }
    }
}
