using Pinpad.Sdk.Model.TypeCode;
namespace Pinpad.Sdk.Model
{
    /// <summary>
    /// Application Identifier.
    /// </summary>
    public class PinpadAid : BaseTableEntry
    {
        // Constants
        /// <summary>
        /// Generic TCC value.
        /// </summary>
        public const string DEFAULT_TCC = "R";
		/// <summary>
		/// Terminal capabilities exact size.
		/// </summary>
        public const int TERMINAL_CAPABILITIES_LENGTH = 6;
		/// <summary>
		/// Additional capabilities exact size.
		/// </summary>
        public const int ADDITIONAL_TERMINAL_CAPABILITIES_LENGTH = 10;
		/// <summary>
		/// Stone application number into the pinpad.
		/// </summary>
        public const int STONE_ACQUIRER_NUMBER = 8;

        // Members
        /// <summary>
        /// Acquirer identifier. Responsible for the AID table (in ABECS, Stone is represented by "8").
        /// </summary>
        public int AcquirerNumber { get; set; }
        /// <summary>
        /// Application Identifier (AID).
        /// AID – Application.
        /// </summary>
        public string ApplicationId { get; set; }
        /// <summary>
        /// Brand identification in BrandTable.
        /// </summary>
        public int TefBrandId { get; set; }
        /// <summary>
        /// Application name.
        /// Preferred mnemonic associated with the AID.
        /// </summary>
        public string ApplicationName { get; set; }
        /// <summary>
        /// AID field length.
        /// </summary>
        public int AidLength { get; set; }
        /// <summary>
        /// Indicates the priority of a given application or group of applications in a directory
        /// </summary>
        public string Priority { get; set; }
        /// <summary>
        /// Target Percentage to be Used for Random Selection
        /// </summary>
        public string TargetPercentage { get; set; }
        /// <summary>
        /// Maximum Target Percentage to be used for Biased Random Selection:
        /// </summary>
        public string MaximumTargetPercentage { get; set; }
        /// <summary>
        /// Indicates the floor limit in the terminal in conjunction with the AID.
        /// In case of Brazil, represented at centavo.
        /// </summary>
        public string FloorLimit { get; set; }
        public string Threshold { get; set; }
        /// <summary>
        /// Terminal Action Code – Denial.
        /// - This specifies the terminal's conditions to reject a transaction.
        /// - Specifies the acquirer’s conditions that cause the denial of a transaction without attempt to go online
        /// </summary>
        public string TerminalActionCodeDenial { get; set; }
        /// <summary>
        /// Terminal Action Code – Default.
        /// Specifies the acquirer’s conditions that cause a transaction to be rejected if it might have been approved online, but the terminal is unable to process the transaction online
        /// </summary>
        public string TerminalActionCodeDefault { get; set; }
        /// <summary>
        /// Terminal Action Code – Online.
        /// - Specifies the acquirer’s conditions that cause a transaction to be transmitted online
        /// - This specifies the terminals's conditions to approve a transaction online.
        /// </summary>
        public string TerminalActionCodeOnline { get; set; }
        /// <summary>
        /// Default Dynamic Data Authentication Data Object List
        /// 
        /// The DDOL is a list of data that the card requires if the DDA (Dynamic Data Authentication of card and terminal data to verify that the card application and data are genuine.)
        /// method is used during Data Authentication, and the terminal may also store a default DDOL that can be used if the DDOL is not present in the data from the card.
        /// The terminal uses the DOL processing rules to format the requested data and then sends it to the card in the Internal Authenticate command.
        /// </summary>
        public string DefaultDynamicDataObjectList { get; set; }
        /// <summary>
        /// Default Transaction Certificate Data Object List:
        /// The TDOL is a list of data that the terminal must use as the input when it needs to calculate a transaction cryptogram hash, 
        /// and the terminal may also store a default TDOL that can be used if the TDOL is not present in the data from the card. 
        /// The terminal uses the DOL (Data Object List) processing rules to format the requested data, but this is only required 
        /// if the transaction cryptogram hash is required by either of the CDOL.
        /// </summary>
        public string DefaultTransactionDataObjectList { get; set; }
        /// <summary>
        /// Application version number. (TAG 9F09)
        /// Version number assigned by the payment system for the application.
        /// </summary>
        public string ApplicationVersion { get; set; }
        /// <summary>
        /// Recorder index in this table (from "01" to "99").
        /// </summary>
        public string AidIndex { get; set; }
        /// <summary>
        /// Application type (from "01" to "98").
        /// </summary>
        public string ApplicationType { get; set; }
        /// <summary>
        /// Terminal country code. (TAG 9F1A)
        /// Indicates the country of the terminal, represented according to ISO 3166
        /// </summary>
        public string TerminalCountryCode { get; set; }
        /// <summary>
        /// Transaction Currency Code. (TAG 5F36)
        /// Indicates the currency in which the account is managed according to ISO 4217
        /// </summary>
        public string TerminalCurrencyCode { get; set; }
        /// <summary>
        /// Transaction Currency Exponent. (TAG 5F36)
        /// Indicates the implied position of the decimal point from the right of the amount represented according to ISO 4217.
        /// </summary>
        public string TerminalCurrencyExponent { get; set; }
        /// <summary>
        /// Additional Terminal Capabilities. (TAG 9F40)
        /// Indicates the data input and output capabilities of the terminal
        /// </summary>
        public string AdditionalTerminalCapabilities { get; set; }
        /// <summary>
        /// Terminal Type. (TAG 9F35)
        /// Indicates the environment of the terminal, its communications capability, and its operational control
        /// </summary>
        public string TerminalType { get; set; }
        /// <summary>
        /// Indicated an action to Contactless Module if reset ("0") transaction amount.
        /// - false: unsupported;
        /// - true: support, but only online.
        /// </summary>
        public bool ContactlessZeroAmount { get; set; }
        /// <summary>
        /// Contactless mode.
        /// Terminal capability to the referred AID, in case it's located in a CTLS.
        /// </summary>
        public PinpadContactlessMode ContactlessMode { get; set; }
        /// <summary>
        /// Terminal/Reader Contactless Transaction Limit.
        /// </summary>
        public string ContactlessTransactionLimit { get; set; }
        /// <summary>
        /// Terminal/Reader Contactless Floor Limit.
        /// </summary>
        public string ContactlessFloorLimit { get; set; }
        /// <summary>
        /// Terminal/Reader CVM (Cardholder Verification Method) Required Limit 
        /// </summary>
        public string ContactlessCvmLimit { get; set; }
        /// <summary>
        /// Contactless Application Version. (TAG 9F6D)
        /// PayPass Mag Stripe Application Version Number (Terminal).
        /// </summary>
        public string ContactlessApplicationVersion { get; set; }
        /// <summary>
        /// Merchant Identifier. (TAG 9F16)
        /// When concatenated with the Acquirer Identifier, uniquely identifies a given merchant
        /// </summary>
        public int MerchantId { get; set; }
        /// <summary>
        /// Terminal Identification. (TAG 9F1C)
        /// When concatenated with the Acquirer Identifier, uniquely identifies a given merchant.
        /// </summary>
        public int TerminalId { get; set; }
        /// <summary>
        /// Merchant Category Code. (TAG 9F15)
        /// Classifies the type of business being done by the merchant, represented according to ISO 8583:1993 for Card Acceptor Business Code.
        /// </summary>
        public int MerchantCategoryCode { get; set; }
        /// <summary>
        /// Terminal Capabilities. (TAG 9F33)
        /// Indicates:
        ///     - the card data input,
        ///     - CVM: Carholder Verification Method
        ///     - security capabilities of the terminal.
        /// </summary>
        public string TerminalCapabilities { get; set; }
        public string TCC { get { return DEFAULT_TCC; } }
    }
}
