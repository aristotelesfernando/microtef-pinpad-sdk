using Pinpad.Sdk.Model.Utilities;
using System;

namespace Pinpad.Sdk.Model
{
	/// <summary>
	/// Pinpad keyboard interface tool.
	/// </summary>
	public interface IPinpadKeyboard
	{
        // Properties
        /// <summary>
        /// It contains methods to select data through the pinpad.
        /// </summary>
        IDataPicker DataPicker { get; set; }

        // Methods
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
		/// <summary>
		/// Gets a decimal amount.
		/// The amount shall be typed in the followed format: 
		///    - "1,99"
		///    - "0,00"
		/// Containing always at least 4 chars.
		/// </summary>
		/// <param name="currency">Amount currency, i. e. R$, US$.</param>
		/// <returns>The amount if a valid amount was typed. Null if: timeout, user cancelled, amount was typed on an invalid format (example: 1.7,2).</returns>
		Nullable<decimal> GetAmount (AmountCurrencyCode currency);
        /// <summary>
        /// Get function keys. Doesn't read numeric keys.
        /// Only works for devices with ABECS 2.0.
        /// </summary>
        /// <returns>The pressed key code.</returns>
        PinpadKeyCode VerifyKeyPressing();

    }
}
