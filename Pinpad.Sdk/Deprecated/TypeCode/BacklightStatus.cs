using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PinPadSDK.Enums {
    /// <summary>
    /// Enumerator for Backlight status
    /// Since undefined is 0 every value will be the actual code plus 1
    /// </summary>
    public enum BacklightStatus {
        /// <summary>
        /// Null
        /// </summary>
        Undefined = 0,

        /// <summary>
        /// Always off
        /// </summary>
        AlwaysOff,

        /// <summary>
        /// On for some time
        /// </summary>
        TimedOn,

        /// <summary>
        /// Always on
        /// </summary>
        AlwaysOn
    }
}
