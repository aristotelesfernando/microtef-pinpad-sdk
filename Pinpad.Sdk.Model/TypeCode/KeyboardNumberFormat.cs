namespace Pinpad.Sdk.Model
{
	/// <summary>
	/// Formatting in case of a numeric input.
	/// </summary>
	public enum KeyboardNumberFormat
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
}
