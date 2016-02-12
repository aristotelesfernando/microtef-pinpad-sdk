using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PinPadSDK.Enums {
    /// <summary>
    /// Enumerator for cryptography methods
    /// </summary>
    public enum CryptographyMode {
        /// <summary>
        /// Null
        /// </summary>
        Undefined = 0,
        
        /// <summary>
        /// DES
        /// </summary>
        DataEncryptionStandard,

        /// <summary>
        /// TDES
        /// </summary>
        TripleDataEncryptionStandard
    }
}
