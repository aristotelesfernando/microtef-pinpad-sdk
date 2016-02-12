using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PinPadSDK.Enums {
    /// <summary>
    /// Enumerator for PRT string alignment
    /// Since undefined is 0 every value will be the actual code plus 1
    /// </summary>
    public enum PrtAlignment {
        /// <summary>
        /// Null
        /// </summary>
        Undefined = 0,

        /// <summary>
        /// Left alignment
        /// </summary>
        Left,

        /// <summary>
        /// Center alignment
        /// </summary>
        Center,

        /// <summary>
        /// Right alignment
        /// </summary>
        Right
    }
}
