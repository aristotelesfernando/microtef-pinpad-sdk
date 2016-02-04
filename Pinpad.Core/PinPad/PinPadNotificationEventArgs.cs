using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PinPadSDK.Property;

namespace PinPadSDK.PinPad {
    /// <summary>
    /// Event args for when the PinPad receives a Notification message
    /// </summary>
    public class PinPadNotificationEventArgs : EventArgs {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="message">PinPad message</param>
        public PinPadNotificationEventArgs(SimpleMessage message) {
            this.Message = message;
        }

        /// <summary>
        /// PinPad message
        /// </summary>
        public SimpleMessage Message { get; private set; }
    }
}
