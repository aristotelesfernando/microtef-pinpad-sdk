using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PinPadSDK.Exceptions {
    /// <summary>
    /// Exception for when a property value length does not match what was expected
    /// </summary>
    public class LenghtMismatchException : PinPadException {
        /// <summary>
        /// Constrcutor
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="inner">The exception that is the cause of the current exception, or a null reference if no inner exception is specified.</param>
        public LenghtMismatchException(string message = null, Exception inner = null) : base(null, null, message, inner) { }
    }
}
