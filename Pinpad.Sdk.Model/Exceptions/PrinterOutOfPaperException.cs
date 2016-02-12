using Pinpad.Sdk.Model.TypeCode;

namespace Pinpad.Sdk.Model.Exceptions 
{
	/// <summary>
	/// Exception for Printer out of paper
	/// </summary>
	public class PrinterOutOfPaperException : PrinterException 
	{
		/// <summary>
		/// Printer Status
		/// </summary>
		public const PinpadPrinterStatus PrinterErrorCode = PinpadPrinterStatus.OutOfPaper;

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="message">The message that describes the error.</param>
		/// <param name="inner">The exception that is the cause of the current exception, or a null reference if no inner exception is specified.</param>
		public PrinterOutOfPaperException(string message = null, System.Exception inner = null) : base(PrinterErrorCode, message, inner) { }
	}
}
