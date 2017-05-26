using Pinpad.Sdk.PinpadProperties.Refactor.Property;
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
		public SimpleMessageProperty Message { get; private set; }

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="message">PinPad message</param>
		public PinpadNotificationEventArgs(SimpleMessageProperty message)
		{
			this.Message = message;
		}
	}
}
