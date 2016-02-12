using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PinPadSDK.Enums;

namespace PinPadSDK.Exceptions {
    /// <summary>
    /// Exception for PinPad printer
    /// </summary>
    public class PrinterException : PinPadException {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="printerErrorCode">Printer Status</param>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="inner">The exception that is the cause of the current exception, or a null reference if no inner exception is specified.</param>
        public PrinterException(PinPadPrinterStatus printerErrorCode, string message = null, Exception inner = null) : base(null, null, message, inner) { this.printerErrorCode = printerErrorCode; }

        /// <summary>
        /// Printer Status
        /// </summary>
        public PinPadPrinterStatus printerErrorCode { get; private set; }
    }
}
