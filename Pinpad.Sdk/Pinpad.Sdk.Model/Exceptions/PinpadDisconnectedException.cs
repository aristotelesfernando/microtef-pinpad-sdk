using System;

namespace Pinpad.Sdk.Exceptions
{
	public class PinpadDisconnectedException : Exception
	{
		public PinpadDisconnectedException(string message = null, Exception inner = null)
			: base(message, inner) { }
	}
}
