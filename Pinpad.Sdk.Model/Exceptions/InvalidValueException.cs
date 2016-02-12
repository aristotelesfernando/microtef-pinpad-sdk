using System;

/*
 * 
 * DEPRECATED.
 * MUST BE REFACTORED.
 * 
 */

namespace Pinpad.Sdk.Model.Exceptions 
{
    /// <summary>
    /// Exception for when a value is not supported
    /// </summary>
    public class InvalidValueException : PinpadException 
	{
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="message">Exception Message</param>
        /// <param name="inner">Inner Exception</param>
        public InvalidValueException(string message = null, Exception inner = null) : base(null, null, message, inner) { }
    }
}
