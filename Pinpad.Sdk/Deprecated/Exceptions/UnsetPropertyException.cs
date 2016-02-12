using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PinPadSDK.Exceptions {
    /// <summary>
    /// Exception for when a property is not optional and has the data default value
    /// </summary>
    public class UnsetPropertyException : PinPadException {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="inner">The exception that is the cause of the current exception, or a null reference if no inner exception is specified.</param>
        public UnsetPropertyException(string message = null, Exception inner = null)
            : base(null, null, message, inner) {
        }
    }
}
