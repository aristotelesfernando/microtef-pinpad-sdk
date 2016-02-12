using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PinPadSDK.Enums {
    /// <summary>
    /// Enumerator for Interchanges
    /// </summary>
    public enum InterchangeType {
        /// <summary>
        /// Null
        /// </summary>
        Undefined = 0,

        /// <summary>
        /// International card
        /// </summary>
        International,

        /// <summary>
        /// National card
        /// </summary>
        National,

        /// <summary>
        /// Private card
        /// </summary>
        Private,

        /// <summary>
        /// Test card
        /// </summary>
        Test
    }
}
