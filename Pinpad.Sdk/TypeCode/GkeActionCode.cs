namespace Pinpad.Sdk.TypeCode
{
	/// <summary>
	/// Enumerator for GKE Actions
	/// Since undefined is 0 every value will be the actual code plus 1
	/// </summary>
	public enum GkeActionCode
	{
		/// <summary>
		/// Null
		/// </summary>
		Undefined = 0,

		/// <summary>
		/// Reads the next key in the buffer or waits for input
		/// </summary>
		ReadKey = 1,

		/// <summary>
		/// Clears the key buffer
		/// </summary>
		ClearBuffer = 2,

		/// <summary>
		/// Reads the next key in the buffer or waits for input without the keyboard lights
		/// </summary>
		ReadKeyNoLight = 3
	}
}
