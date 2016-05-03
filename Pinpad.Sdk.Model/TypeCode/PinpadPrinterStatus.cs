namespace Pinpad.Sdk.Model 
{
	/// <summary>
	/// Enumerator for Printer status
	/// Since undefined is 0 every value will be the actual code plus 1
	/// </summary>
	public enum PinpadPrinterStatus 
	{
		/// <summary>
		/// Null
		/// </summary>
		Undefined = 0,
		/// <summary>
		/// Printer is ready to receive data to print
		/// </summary>
		Ready = 1,
		/// <summary>
		/// Printer is busy
		/// </summary>
		Busy = 2,
		/// <summary>
		/// Printer out of paper
		/// </summary>
		OutOfPaper = 3,
		/// <summary>
		/// Error with data to print
		/// </summary>
		InvalidFormat = 4,
		/// <summary>
		/// Generic printer error
		/// </summary>
		PrinterError = 5,
		/// <summary>
		/// Printer overheating
		/// </summary>
		Overheating = 9,
		/// <summary>
		/// Still printing
		/// </summary>
		UnfinishedPrinting = 241,
		/// <summary>
		/// PinPad lacks the font files
		/// </summary>
		LackOfFont = 253,
		/// <summary>
		/// Information to print too long
		/// </summary>
		PackageTooLong = 255,
	}
}
