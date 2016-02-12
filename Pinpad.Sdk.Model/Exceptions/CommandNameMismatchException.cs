﻿namespace Pinpad.Sdk.Model.Exceptions 
{
    /// <summary>
    /// Exception for when attempting to parse a command string into the wrong object
    /// </summary>
    public class CommandNameMismatchException : PinpadException 
	{

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="inner">The exception that is the cause of the current exception, or a null reference if no inner exception is specified.</param>
        public CommandNameMismatchException(string message = null, System.Exception inner = null)
            : base(null, null, message, inner) {  }
    }
}
