using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PinPadSDK.Exceptions {
    /// <summary>
    /// Exception for when the Checksum has failed consecutively for over 3 times
    /// </summary>
    public class InvalidChecksumException : PinPadException {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="inner">The exception that is the cause of the current exception, or a null reference if no inner exception is specified.</param>
        /// <param name="requestString">Request string sent to the PinPad</param>
        /// <param name="responseString">Response string received from the PinPad</param>
        public InvalidChecksumException(string requestString, string responseString, string message = null, Exception inner = null) : base(requestString, responseString, message, inner) { }
    }
}
