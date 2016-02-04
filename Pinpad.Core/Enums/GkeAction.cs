using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PinPadSDK.Enums {
    /// <summary>
    /// Enumerator for GKE Actions
    /// Since undefined is 0 every value will be the actual code plus 1
    /// </summary>
    public enum GkeAction {
        /// <summary>
        /// Null
        /// </summary>
        Undefined = 0,

        /// <summary>
        /// Reads the next key in the buffer or waits for input
        /// </summary>
        GKE_ReadKey = 1,

        /// <summary>
        /// Clears the key buffer
        /// </summary>
        GKE_ClearBuffer = 2,

        /// <summary>
        /// Reads the next key in the buffer or waits for input without the keyboard lights
        /// </summary>
        GKE_ReadKeyNoLight = 3,
    }
}
