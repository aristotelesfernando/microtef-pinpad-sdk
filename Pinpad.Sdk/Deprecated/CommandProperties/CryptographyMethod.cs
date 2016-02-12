using PinPadSDK.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PinPadSDK.Property;

namespace PinPadSDK.Property {
    /// <summary>
    /// Controller for cryptography method
    /// </summary>
    internal class CryptographyMethod {
        /// <summary>
        /// Constructor
        /// </summary>
        internal CryptographyMethod() {
        }

        /// <summary>
        /// Constructor with initial value
        /// </summary>
        /// <param name="keyManagement">Online Pin Cryptography Key Management Mode</param>
        /// <param name="cryptography">Online Pin Cryptography Mode</param>
        internal CryptographyMethod(KeyManagementMode keyManagement, CryptographyMode cryptography)
        : this(){
            this.KeyManagement = keyManagement;
            this.Cryptography = cryptography;
        }

        /// <summary>
        /// PinPadCryptographyMethodController string formatter
        /// </summary>
        /// <param name="obj">value to convert</param>
        /// <returns>Value of the property as string or InvalidCastException for invalid values</returns>
        internal static string StringFormatter(CryptographyMethod obj) {
            if (obj.KeyManagement == KeyManagementMode.Undefined || obj.Cryptography == CryptographyMode.Undefined) {
                throw new InvalidCastException("KeyManagementMethod and CryptographyMethod cannot be undefined.");
            }
            int value = (((int)obj.KeyManagement) - 1) * 2;
            value += ((int)obj.Cryptography) - 1;
            return value.ToString();
        }

        /// <summary>
        /// PinPadCryptographyMethodController string parser
        /// </summary>
        /// <param name="reader">string reader</param>
        /// <returns>PinPadCryptographyMethodController</returns>
        internal static CryptographyMethod StringParser(StringReader reader) {
            int intValue = DefaultStringParser.IntegerStringParser(reader, 1).Value;

            int keyManagementMethodValue = (intValue / 2) + 1;
            KeyManagementMode keyManagementMethod = (KeyManagementMode)keyManagementMethodValue;

            int cryptographyMethodValue = (intValue % 2) + 1;
            CryptographyMode cryptographyMethod = (CryptographyMode)cryptographyMethodValue;

            CryptographyMethod value = new CryptographyMethod(keyManagementMethod, cryptographyMethod);
            return value;
        }

        /// <summary>
        /// Cryptography Key Management Method
        /// </summary>
        internal KeyManagementMode KeyManagement { get; set; }

        /// <summary>
        /// Cryptography Method
        /// </summary>
        internal CryptographyMode Cryptography { get; set; }
    }
}
