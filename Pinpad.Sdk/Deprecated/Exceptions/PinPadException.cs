using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PinPadSDK.Exceptions {
    /// <summary>
    /// Exception for PinPad communication
    /// </summary>
    public class PinPadException : Exception {
        /// <summary>
        /// Request string sent to the PinPad
        /// </summary>
        public string RequestString { get; private set; }

        /// <summary>
        /// Response string received from the PinPad
        /// </summary>
        public string ResponseString { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="inner">The exception that is the cause of the current exception, or a null reference if no inner exception is specified.</param>
        /// <param name="requestString">Request string sent to the PinPad</param>
        /// <param name="responseString">Response string received from the PinPad</param>
        public PinPadException(string requestString, string responseString, string message = null, Exception inner = null)
            : base(message, inner) {
            this.RequestString = requestString;
            this.ResponseString = responseString;
        }
    }
}
