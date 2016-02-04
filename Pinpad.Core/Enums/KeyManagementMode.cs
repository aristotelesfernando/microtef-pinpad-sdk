using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PinPadSDK.Enums {
    /// <summary>
    /// Enumerator for key management methods
    /// </summary>
    public enum KeyManagementMode {
        /// <summary>
        /// Null
        /// </summary>
        Undefined = 0,

        /// <summary>
        /// MK/WK
        /// </summary>
        MasterAndWorkingKeys,

        /// <summary>
        /// DUKPT
        /// </summary>
        DerivedUniqueKeyPerTransaction
    }
}
