using System;
namespace Pinpad.Sdk.Model.Exceptions
{
	/// <summary>
	/// Exception for when the PinPad's Stone Version is below the command's minimum or when attempting to send a Stone command to a PinPad wihtout Stone App
	/// </summary>
	public class StoneVersionMismatchException : PinpadException 
	{
		/// <summary>
		/// Error reason.
		/// </summary>
		public string Reason { get; set; }

		/// <summary>
		/// Creates the exception.
		/// </summary>
		/// <param name="reason">Exception reason.</param>
		/// <param name="message">Exception Message</param>
		/// <param name="inner">Inner Exception</param>
		public StoneVersionMismatchException(string reason = null, string message = null, Exception inner = null) : base(null, null, message, inner) 
		{
			this.Reason = reason;
		}
	}
}
