using Pinpad.Core.Properties;
using System;

namespace Pinpad.Core.Events 
{
	/// <summary>
	/// Event args for when the PinPad receives a Notification message
	/// </summary>
	public class PinpadNotificationEventArgs : EventArgs
	{
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="message">PinPad message</param>
		public PinpadNotificationEventArgs(SimpleMessage message)
		{
			this.Message = message;
		}

		/// <summary>
		/// PinPad message
		/// </summary>
		public SimpleMessage Message { get; private set; }
	}
}
