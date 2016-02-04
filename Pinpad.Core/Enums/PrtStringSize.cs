using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PinPadSDK.Enums {
    /// <summary>
    /// Enumerator for PRT string font sizes
    /// Since undefined is 0 every value will be the actual code plus 1
    /// </summary>
    public enum PrtStringSize {
        /// <summary>
        /// Null
        /// </summary>
        Undefined = 0,

        /// <summary>
        /// Small string
        /// </summary>
        Small,

        /// <summary>
        /// Reserved for Future Use
        /// </summary>
        RFU1,

        /// <summary>
        /// Medium string
        /// </summary>
        Medium,

        /// <summary>
        /// Reserved for Future Use
        /// </summary>
        RFU2,

        /// <summary>
        /// Reserved for Future Use
        /// </summary>
        RFU3,

        /// <summary>
        /// Reserved for Future Use
        /// </summary>
        RFU4,

        /// <summary>
        /// Big string
        /// </summary>
        Big,

        /// <summary>
        /// Reserved for Future Use
        /// </summary>
        RFU5,
    }
}
