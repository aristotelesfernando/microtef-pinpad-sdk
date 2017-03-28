using Pinpad.Sdk.Properties;
using System;

namespace Pinpad.Sdk.Events 
{
	/// <summary>
	/// Event args for when the PinPad receives a Notification message
	/// </summary>
	public sealed class PinpadNotificationEventArgs : EventArgs
	{
		/// <summary>
		/// PinPad message
		/// </summary>
		public SimpleMessage Message { get; private set; }

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="message">PinPad message</param>
		public PinpadNotificationEventArgs(SimpleMessage message)
		{
			this.Message = message;
		}
	}
}
