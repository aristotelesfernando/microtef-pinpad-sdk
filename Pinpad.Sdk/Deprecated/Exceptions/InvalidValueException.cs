using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PinPadSDK.Exceptions {
    /// <summary>
    /// Exception for when a value is not supported
    /// </summary>
    public class InvalidValueException : PinPadException {
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="message">Exception Message</param>
        /// <param name="inner">Inner Exception</param>
        public InvalidValueException(string message = null, Exception inner = null) : base(null, null, message, inner) { }
    }
}
