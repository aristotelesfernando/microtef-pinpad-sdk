using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PinPadSDK.Enums;

namespace PinPadSDK.Exceptions {
    /// <summary>
    /// Exception for unknown PinPadKey value
    /// </summary>
    public class UnknownPinPadKeyException : Exception {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="inner">The exception that is the cause of the current exception, or a null reference if no inner exception is specified.</param>
        public UnknownPinPadKeyException(string message = null, Exception inner = null)
            : base(message, inner) {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="key">PinPadKey</param>
        public UnknownPinPadKeyException(PinPadKey key)
            : this("Unknown PinPadKeys: " + key.ToString( ), null) {
        }
    }
}
