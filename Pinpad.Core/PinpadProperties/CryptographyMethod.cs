using Pinpad.Core.TypeCode;
using System;

namespace Pinpad.Core.Properties
{
    /// <summary>
    /// Controller for cryptography method
    /// </summary>
    public class CryptographyMethod {
        /// <summary>
        /// Constructor
        /// </summary>
        public CryptographyMethod() {
        }

        /// <summary>
        /// Constructor with initial value
        /// </summary>
        /// <param name="keyManagement">Online Pin Cryptography Key Management Mode</param>
        /// <param name="cryptography">Online Pin Cryptography Mode</param>
        public CryptographyMethod(KeyManagementMode keyManagement, CryptographyMode cryptography)
        : this(){
            this.KeyManagement = keyManagement;
            this.Cryptography = cryptography;
        }

        /// <summary>
        /// PinPadCryptographyMethodController string formatter
        /// </summary>
        /// <param name="obj">value to convert</param>
        /// <returns>Value of the property as string or InvalidCastException for invalid values</returns>
        public static string StringFormatter(CryptographyMethod obj) {
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
        public static CryptographyMethod StringParser(StringReader reader) {
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
        public KeyManagementMode KeyManagement { get; set; }

        /// <summary>
        /// Cryptography Method
        /// </summary>
        public CryptographyMode Cryptography { get; set; }
    }
}
