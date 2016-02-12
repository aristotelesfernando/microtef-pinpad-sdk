using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PinPadSDK.Enums {
    /// <summary>
    /// Enumerator for PRT actions
    /// Since undefined is 0 every value will be the actual code plus 1
    /// </summary>
    public enum PrtAction {
        /// <summary>
        /// Null
        /// </summary>
        Undefined = 0,

        /// <summary>
        /// Clears the buffer and prepare to print
        /// </summary>
        Begin,

        /// <summary>
        /// Print the data in the buffer and then clears the buffer
        /// </summary>
        End,

        /// <summary>
        /// Append a string to the print buffer
        /// </summary>
        AppendString,

        /// <summary>
        /// Append a image to the print buffer
        /// </summary>
        AppendImage,

        /// <summary>
        /// Adds to the vertifcal offset of the next buffer append
        /// </summary>
        Step
    }
}
