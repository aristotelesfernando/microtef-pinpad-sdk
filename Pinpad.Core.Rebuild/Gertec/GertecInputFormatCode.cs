namespace Pinpad.Core.Rebuild.Gertec
{
	/// <summary>
	/// Formatting in case of a numeric input.
	/// </summary>
	public enum GertecNumberFormatCode
	{
		/// <summary>
		/// Undefined. Default value.
		/// </summary>
		Undefined = 0,
		/// <summary>
		/// The command will not ask for numbers to be typed.
		/// </summary>
		None = 1,
		/// <summary>
		/// The command will ask for decimals to be typed.
		/// </summary>
		Decimal = 2,
		/// <summary>
		/// The command will ask for binaries to be typed.
		/// </summary>
		Binary = 3,
		/// <summary>
		/// The command will ask for hexadecimals to be typed.
		/// </summary>
		Hexadecimal = 5
	}

	/// <summary>
	/// Formatting in care of a text input.
	/// </summary>
	public enum GertecTextFormatCode
	{
		/// <summary>
		/// Undefined. Default value.
		/// </summary>
		Undefined = 0,
		/// <summary>
		/// The command will not ask for numbers to be typed.
		/// </summary>
		None = 1,
		/// <summary>
		/// The command will ask for text in capital letters to be typed.
		/// </summary>
		AlphaCapitals = 2,
		/// <summary>
		/// The command will ask for lowercase text to be typed.
		/// </summary>
		AlphaNonCapitals = 3,
		/// <summary>
		/// The command will ask for symbols to be typed.
		/// </summary>
		Symbols = 4
	}
}
