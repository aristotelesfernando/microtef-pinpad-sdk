using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PinPadSDK.Exceptions {
    /// <summary>
    /// Exception for failure parsing a PinPadProperty
    /// </summary>
    public class PropertyParseException : PinPadException {

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="inner">The exception that is the cause of the current exception, or a null reference if no inner exception is specified.</param>
        /// <param name="commandString">The command string sent to the Command</param>
        /// <param name="propertyName">The property name that failed to parse</param>
        /// <param name="propertyString">The command substring sent to the StringParser</param>
        public PropertyParseException(string commandString = null, string propertyName = null, string propertyString = null, string message = null, Exception inner = null)
            : base(null, null, message, inner) {
            this.CommandString = commandString;
            this.PropertyName = propertyName;
            this.PropertyString = propertyString;
        }

        /// <summary>
        /// The command string sent to the Command
        /// </summary>
        public string CommandString { get; private set; }

        /// <summary>
        /// The property name that failed to parse
        /// </summary>
        public string PropertyName { get; private set; }

        /// <summary>
        /// The command substring sent to the StringParser
        /// </summary>
        public string PropertyString { get; private set; }
    }
}
