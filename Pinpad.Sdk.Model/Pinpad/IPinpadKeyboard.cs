namespace Pinpad.Sdk.Model
{
	/// <summary>
	/// Pinpad keyboard interface tool.
	/// </summary>
	public interface IPinpadKeyboard
	{
		/// <summary>
		/// Gets the next Key pressed at the Pinpad with the default, safe, method.
		/// Does not retrieve numeric keys.
		/// </summary>
		/// <returns>PinpadKey or Undefined on failure.</returns>
		PinpadKeyCode GetKey();
		/// <summary>
		/// Gets a numeric input from pinpad keyboard.
		/// </summary>
		/// <param name="firstLine">First line label.</param>
		/// <param name="secondLine">Second line label.</param>
		/// <param name="minimumLength">Minimum input size.</param>
		/// <param name="maximumLength">Maximum input size.</param>
		/// <param name="timeOut">Time out.</param>
		/// <returns>Input from the keyboard. Null if nothing was received, whether of timeout or cancellation.</returns>
		string GetNumericInput (FirstLineLabelCode firstLine, SecondLineLabelCode secondLine, int minimumLength, int maximumLength, int timeOut);
		string GetText (KeyboardNumberFormat numericInput, KeyboardTextFormat textInput, FirstLineLabelCode firstLine, SecondLineLabelCode secondLine, int minimumLength, int maximumLength, int timeOut);
	}
}
