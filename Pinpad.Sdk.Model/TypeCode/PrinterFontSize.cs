namespace Pinpad.Sdk.Model 
{
	/// <summary>
	/// Enumerator for PRT string font sizes
	/// Since undefined is 0 every value will be the actual code plus 1
	/// </summary>
	public enum PrinterFontSize 
	{
		/// <summary>
		/// Null
		/// </summary>
		Undefined = 0,
		/// <summary>
		/// Small string
		/// </summary>
		Small = 2,
		/// <summary>
		/// Medium string
		/// </summary>
		Medium = 3,
		/// <summary>
		/// Big string
		/// </summary>
		Big = 4
	}
}
