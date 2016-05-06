using System;

namespace Pinpad.Sdk.Exceptions
{
	/// <summary>
	/// Thrown when detect that the pinpad is no longer connected.
	/// </summary>
	public class PinpadDisconnectedException : Exception
	{
		/// <summary>
		/// Default constructor.
		/// </summary>
		/// <param name="message">Pinpad disconnected message.</param>
		/// <param name="inner">Inner exception, if any.</param>
		public PinpadDisconnectedException(string message = null, Exception inner = null)
			: base(message, inner) { }
	}
}
