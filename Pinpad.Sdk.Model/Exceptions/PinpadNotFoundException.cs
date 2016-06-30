using System;

namespace Pinpad.Sdk.Model.Exceptions
{
	/// <summary>
	/// Indicates that none pinpads were found on the machine.
	/// </summary>
	public class PinpadNotFoundException : Exception
	{
		/// <summary>
		/// Port that the pinpad should be connected (if any).
		/// </summary>
		public string PinpadPortName { get; private set; }

		/// <summary>
		/// Set's <see cref="PinpadNotFoundException"/> message to "None pinpad found.".
		/// </summary>
		public PinpadNotFoundException ()
			: base("None pinpad found.")
		{ }
		/// <summary>
		/// Set's <see cref="PinpadNotFoundException"/> to <paramref name="message"/>.
		/// </summary>
		/// <param name="message">Exception message.</param>
		/// <param name="pinpadPortName">Port that the pinpad should be connected (if any).</param>
		public PinpadNotFoundException (string message, string pinpadPortName)
			: base(message)
		{
			this.PinpadPortName = pinpadPortName;
		}
	}
}
