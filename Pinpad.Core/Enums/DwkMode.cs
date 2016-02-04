using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PinPadSDK.Enums {
    /// <summary>
    /// Enumerator for DWK modes
    /// Since undefined is 0 every value will be the actual code plus 1
    /// </summary>
    public enum DwkMode {
        /// <summary>
        /// Null
        /// </summary>
        Undefined = 0,

        /// <summary>
        /// Mode1 of DWK command
        /// Uses MK/WK index to encrypt
        /// </summary>
        Mode1 = 1,

        /// <summary>
        /// Mode2 of DWK command
        /// Uses RSA Public Key to encrypt
        /// </summary>
        Mode2 = 2,
    }
}
