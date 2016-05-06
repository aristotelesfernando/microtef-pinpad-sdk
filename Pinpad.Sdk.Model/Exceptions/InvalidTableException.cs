using System;

namespace Pinpad.Sdk.Exceptions
{
	/// <summary>
	/// Thrown when an invalid pinpad table is detected. Verify which of the pinpad tables you are trying to inject into the pinpad.
	/// </summary>
	public class InvalidTableException : Exception
	{
		/// <summary>
		/// Default constructor.
		/// </summary>
		/// <param name="message">Invalid table detected message.</param>
		/// <param name="innerException">Inner exception, if any.</param>
		public InvalidTableException(string message = null, Exception innerException = null) 
			: base (message, innerException) {  }
	}
}
