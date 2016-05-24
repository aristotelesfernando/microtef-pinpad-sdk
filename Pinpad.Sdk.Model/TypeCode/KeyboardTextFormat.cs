namespace Pinpad.Sdk.Model
{
	/// <summary>
	/// Formatting in care of a text input.
	/// </summary>
	public enum KeyboardTextFormat
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
		Symbols = 5
	}
}
