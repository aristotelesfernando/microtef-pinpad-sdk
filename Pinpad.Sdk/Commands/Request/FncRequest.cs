using Pinpad.Sdk.PinpadProperties.Refactor.Formatter;
using Pinpad.Sdk.PinpadProperties.Refactor.Parser;
using Pinpad.Sdk.PinpadProperties.Refactor.Property;
using System;

namespace Pinpad.Sdk.Commands
{
    /// <summary>
    /// FNC request
    /// </summary>
    internal sealed class FncRequest : PinpadProperties.Refactor.BaseCommand
	{
        /// <summary>
        /// Constructor
        /// </summary>
        public FncRequest()
		{
            this.CMD_LEN1 = new RegionProperty("CMD_LEN1", 3);
            this.FNC_COMMST = new FixedLengthProperty<AcquirerCommunicationStatus>("FNC_COMMST", 1, false, 
                StringFormatter.EnumStringFormatter<AcquirerCommunicationStatus>, StringParser.EnumStringParser<AcquirerCommunicationStatus>);
            this.FNC_ISSMODE = new FixedLengthProperty<int?>("FNC_ISSMODE", 1, false, 
                StringFormatter.IntegerStringFormatter, StringParser.IntegerStringParser, null, 0);
            this.FNC_ARC = new FixedLengthProperty<string>("FNC_ARC", 2, false, 
                StringFormatter.StringStringFormatter, StringParser.StringStringParser);
            this.FNC_ISSDAT = new VariableLengthProperty<HexadecimalData>("FNC_ISSDAT", 3, 512, 1.0f / 2, false, 
                false, StringFormatter.HexadecimalStringFormatter, StringParser.HexadecimalStringParser);
            this.FNC_ACQPR = new VariableLengthProperty<string>("FNC_ACQPR", 3, 0, 1.0f, false, true, 
                StringFormatter.StringStringFormatter, StringParser.StringStringParser, String.Empty);
            this.CMD_LEN2 = new RegionProperty("CMD_LEN2", 3);
            this.FNC_TAGS = new VariableLengthProperty<HexadecimalData>("FNC_TAGS", 3, 256, 1.0f / 2, false, 
                true, StringFormatter.HexadecimalStringFormatter, StringParser.HexadecimalStringParser);

            this.StartRegion(this.CMD_LEN1);
            {
                this.AddProperty(this.FNC_COMMST);
                this.AddProperty(this.FNC_ISSMODE);
                this.AddProperty(this.FNC_ARC);
                this.AddProperty(this.FNC_ISSDAT);
                this.AddProperty(this.FNC_ACQPR);
            }
            this.EndLastRegion();
            this.StartRegion(this.CMD_LEN2);
            {
                this.AddProperty(this.FNC_TAGS);
            }
            this.EndLastRegion();
        }

        /// <summary>
        /// Name of the command
        /// </summary>
        public override string CommandName { get { return "FNC"; } }

        /// <summary>
        /// Length of the first region of the command
        /// </summary>
        public RegionProperty CMD_LEN1 { get; private set; }

        /// <summary>
        /// Status of communication with acquirer network
        /// </summary>
        public FixedLengthProperty<AcquirerCommunicationStatus> FNC_COMMST { get; private set; }

        /// <summary>
        /// Card Issuer Type, fixed 0
        /// </summary>
        public FixedLengthProperty<Nullable<int>> FNC_ISSMODE { get; private set; }

        /// <summary>
        /// Authorization Response Code
        /// </summary>
        public FixedLengthProperty<string> FNC_ARC { get; private set; }

        /// <summary>
        /// EMV data received from the card issuer with Tag/Length/Value format
        /// </summary>
        public VariableLengthProperty<HexadecimalData> FNC_ISSDAT { get; private set; }

        /// <summary>
        /// Acquirer specific data, not used
        /// </summary>
        public VariableLengthProperty<string> FNC_ACQPR { get; private set; }

        /// <summary>
        /// Length of the second region of the command
        /// </summary>
        public RegionProperty CMD_LEN2 { get; private set; }

        /// <summary>
        /// EMV tags list to be returned in the command response
        /// </summary>
        public VariableLengthProperty<HexadecimalData> FNC_TAGS { get; private set; }
    }
}
