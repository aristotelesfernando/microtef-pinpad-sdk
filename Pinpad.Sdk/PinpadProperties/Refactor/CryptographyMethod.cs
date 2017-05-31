using Pinpad.Sdk.Commands;
using Pinpad.Sdk.Model.TypeCode;
using Pinpad.Sdk.PinpadProperties.Refactor.Parser;
using System;

namespace Pinpad.Sdk.PinpadProperties.Refactor
{
    /// <summary>
	/// Controller for cryptography methods.
	/// </summary>
	public sealed class CryptographyMethod
    {
        /// <summary>
        /// Cryptography Key Management Method
        /// </summary>
        public KeyManagementMode KeyManagement { get; set; }
        /// <summary>
        /// Cryptography Method
        /// </summary>
        public CryptographyMode Cryptography { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public CryptographyMethod() { }

        /// <summary>
        /// Constructor with initial value.
        /// </summary>
        /// <param name="keyManagement">Online Pin Cryptography Key Management Mode.</param>
        /// <param name="cryptography">Online Pin Cryptography Mode.</param>
        public CryptographyMethod(KeyManagementMode keyManagement, CryptographyMode cryptography)
        : this()
        {
            this.KeyManagement = keyManagement;
            this.Cryptography = cryptography;
        }
        /// <summary>
        /// PinPadCryptographyMethodController string formatter.
        /// </summary>
        /// <param name="obj">Value to convert.</param>
        /// <returns>Value of the property as string or InvalidCastException for invalid values.</returns>
        public static string StringFormatter(CryptographyMethod obj)
        {
            // Validates parameter value:
            if (obj.KeyManagement == KeyManagementMode.Undefined || obj.Cryptography == CryptographyMode.Undefined)
            {
                throw new InvalidCastException("KeyManagementMethod and CryptographyMethod cannot be undefined.");
            }

            // Converts to string:
            int value = (((int)obj.KeyManagement) - 1) * 2;
            value += ((int)obj.Cryptography) - 1;

            return value.ToString();
        }
        /// <summary>
        /// PinPadCryptographyMethodController string parser.
        /// </summary>
        /// <param name="reader">String reader.</param>
        /// <returns>PinPadCryptographyMethodController</returns>
        public static CryptographyMethod CustomStringParser(StringReader reader)
        {
            int intValue = StringParser.IntegerStringParser(reader, 1).Value;

            int keyManagementMethodValue = (intValue / 2) + 1;
            KeyManagementMode keyManagementMethod = (KeyManagementMode)keyManagementMethodValue;

            int cryptographyMethodValue = (intValue % 2) + 1;
            CryptographyMode cryptographyMethod = (CryptographyMode)cryptographyMethodValue;

            CryptographyMethod value = new CryptographyMethod(keyManagementMethod, cryptographyMethod);
            return value;
        }
    }
}
