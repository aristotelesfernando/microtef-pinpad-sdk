namespace Pinpad.Sdk.Model 
{
	/// <summary>
	/// Enumerator for PRT string font sizes
	/// Since undefined is 0 every value will be the actual code plus 1
	/// </summary>
	public enum PrinterStringSize 
	{
		/// <summary>
		/// Null
		/// </summary>
		Undefined = 0,
		/// <summary>
		/// Small string
		/// </summary>
		Small,
		/// <summary>
		/// Reserved for Future Use
		/// </summary>
		RFU1,
		/// <summary>
		/// Medium string
		/// </summary>
		Medium,
		/// <summary>
		/// Reserved for Future Use
		/// </summary>
		RFU2,
		/// <summary>
		/// Reserved for Future Use
		/// </summary>
		RFU3,
		/// <summary>
		/// Reserved for Future Use
		/// </summary>
		RFU4,
		/// <summary>
		/// Big string
		/// </summary>
		Big,
		/// <summary>
		/// Reserved for Future Use
		/// </summary>
		RFU5
	}
}
