using System;
using System.Text;
using System.Linq;

namespace Pinpad.Sdk.Model
{
    /// <summary>
    /// EMV data required by the application.
    /// </summary>
    internal enum EmvTagCode
    {
        /// <summary>
        /// Transaction Currency Code.
        /// </summary>
        TransactionCurrencyCode = 0x5F2A,
        /// <summary>
        /// Application Interchange Profile:
        /// Indicates the capabilities of the card to support specific functions in the application.
        /// </summary>
        ApplicationInterchangeProfile = 0x82,
        /// <summary>
        /// Terminal Verification Results:
        /// Status of the different functions as seen from the terminal.
        /// </summary>
        TerminalVerificationResults = 0x95,
        /// <summary>
        /// Transaction Date:
        /// Local date that the transaction was authorised formatted as YYMMDD.
        /// </summary>
        TransactionDate = 0x9A,
        /// <summary>
        /// Transaction Type:
        /// Indicates the type of financial transaction, represented by the first two digits of ISO 8583:1987 Processing Code.
        /// </summary>
        TransactionType = 0x9C,
        /// <summary>
        /// Authorised (Numeric), Authorised amount of the transaction (excluding adjustments).
        /// </summary>
        Amount = 0x9F02,
        /// <summary>
        /// Issuer Application Data:
        /// Contains proprietary application data for transmission to the issuer in an online transaction. 
        /// </summary>
        IssuerApplicationData = 0x9F10,
        /// <summary>
        /// Terminal Country Code.
        /// </summary>
        TerminalCountryCode = 0x9F1A,
        /// <summary>
        /// Interface Device (IFD) Serial Number. Component serial number set by provider.
        /// </summary>
        SerialNumber = 0x9F1E,
        /// <summary>
        /// Application Cryptogram.
        /// </summary>
        ApplicationCryptogram = 0x9F26,
        /// <summary>
        /// Terminal Capabilities.
        /// </summary>
        TerminalCapabilities = 0x9F33,
        /// <summary>
        /// Application Transaction Counter (ATC).
        /// </summary>
        ApplicationTransactionCounter = 0x9F36,
        /// <summary>
        /// Unpredictable Number:
        /// Value to provide variability and uniqueness to the generation of a cryptogram.
        /// </summary>
        UnpredictableNumber = 0x9F37,
        /// <summary>
        /// Cryptogram Information Data:
        /// Indicates the type of cryptogram and the actions to be performed by the terminal.
        /// </summary>
        CryptogramInformationData = 0x9F27,
        /// <summary>
        /// Cardholder Verification Method (CVM) Results:
        /// Indicates the results of the last CVM performed.
        /// </summary>
        CardholderVerificationMethodResults = 0x9F34,
        /// <summary>
        /// Indicates the whole cardholder name when greater than 26 characters using the same 
        /// coding convention as in ISO 7813.
        /// </summary>
        CardholderNameExtended = 0x9F0B
    }

	/// <summary>
	/// Responsible for know all EMV tags this application supports.
	/// </summary>
    public class EmvTag
    {
		/// <summary>
		/// Get's all EMV tags supported and needed to perform EMV operations.
		/// </summary>
		/// <returns>EMV tags concatenated.</returns>
        public static string GetEmvTagsRequired()
        {
            StringBuilder strCodes = new StringBuilder();
            EmvTagCode[] codes = Enum.GetValues(typeof(EmvTagCode))
                                     .Cast<EmvTagCode>()
                                     .ToArray();

            foreach (EmvTagCode currCode in codes)
            {
                string curHexCode = ((int)currCode).ToString("x");

                strCodes.Append(curHexCode);
            }

            return strCodes.ToString();
        }
    }
}
