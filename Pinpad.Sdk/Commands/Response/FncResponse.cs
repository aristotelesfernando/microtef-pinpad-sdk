using Pinpad.Sdk.Properties;

namespace Pinpad.Sdk.Commands
{
    /// <summary>
    /// FNC response
    /// </summary>
    public class FncResponse : BaseResponse
	{
        /// <summary>
        /// Constructor
        /// </summary>
        public FncResponse()
		{
            this.RSP_LEN1 = new RegionProperty("RSP_LEN1", 3);
            this.FNC_DECISION = new PinpadFixedLengthProperty<OnlineTransactionStatus>("FNC_DECISION", 1, false, DefaultStringFormatter.EnumStringFormatter<OnlineTransactionStatus>, DefaultStringParser.EnumStringParser<OnlineTransactionStatus>);
            this.FNC_EMVDAT = new VariableLengthProperty<HexadecimalData>("FNC_EMVDAT", 3, 512, 1.0f / 2, false, true, DefaultStringFormatter.HexadecimalStringFormatter, DefaultStringParser.HexadecimalStringParser);
            this.FNC_ISR = new VariableLengthProperty<HexadecimalData>("FNC_ISR", 2, 100, 1.0f / 2, false, true, DefaultStringFormatter.HexadecimalStringFormatter, DefaultStringParser.HexadecimalStringParser);
            this.FNC_ACQRD = new VariableLengthProperty<string>("FNC_ACQRD", 3, 0, 1.0f, false, true, DefaultStringFormatter.StringStringFormatter, DefaultStringParser.StringStringParser, string.Empty);

            this.StartRegion(this.RSP_LEN1);
            {
                this.AddProperty(this.FNC_DECISION);
                this.AddProperty(this.FNC_EMVDAT);
                this.AddProperty(this.FNC_ISR);
                this.AddProperty(this.FNC_ACQRD);
            }
            this.EndLastRegion();
        }

        /// <summary>
        /// Is this a blocking command?
        /// </summary>
        public override bool IsBlockingCommand {
            get { return false; }
        }

        /// <summary>
        /// Name of the command
        /// </summary>
        public override string CommandName { get { return "FNC"; } }

        /// <summary>
        /// Length of the first region of the command
        /// </summary>
        public RegionProperty RSP_LEN1 { get; private set; }

        /// <summary>
        /// Transaction status decision
        /// </summary>
        public PinpadFixedLengthProperty<OnlineTransactionStatus> FNC_DECISION { get; private set; }

        /// <summary>
        /// EMV data to send to card issues at Tag/Length/Value format
        /// </summary>
        public VariableLengthProperty<HexadecimalData> FNC_EMVDAT { get; private set; }

        /// <summary>
        /// Issuer Script Result
        /// </summary>
        public VariableLengthProperty<HexadecimalData> FNC_ISR { get; private set; }

        /// <summary>
        /// Acquirer Specific returned data
        /// </summary>
        public VariableLengthProperty<string> FNC_ACQRD { get; private set; }
    }
}
