using Pinpad.Sdk.Properties;

namespace Pinpad.Sdk.Commands 
{
    /// <summary>
    /// Controller for GIN response for GIN request with ACQIDX 00
    /// </summary>
    internal class GinResponse : BaseResponse
	{
		// Members
        /// <summary>
        /// If it's a blocking command.
        /// </summary>
        public override bool IsBlockingCommand 
		{
            get { return false; }
        }
        /// <summary>
        /// Name of the command
        /// </summary>
        public override string CommandName { get { return "GIN"; } }
        /// <summary>
        /// Length of the first region of the command
        /// </summary>
        public RegionProperty RSP_LEN1 { get; private set; }
        /// <summary>
        /// Manufacturer Name
        /// </summary>
        public PinpadFixedLengthProperty<string> GIN_MNAME { get; private set; }
        /// <summary>
        /// Model and hardware version
        /// </summary>
        public PinpadFixedLengthProperty<string> GIN_MODEL { get; private set; }
        /// <summary>
        /// Contactless indicator
        /// If 'C' is present contactless is supported
        /// </summary>
        public PinpadFixedLengthProperty<string> GIN_CTLSUP { get; private set; }
        /// <summary>
        /// Basic software or Operational system versions, without a defined format
        /// </summary>
        public PinpadFixedLengthProperty<string> GIN_SOVER { get; private set; }
        /// <summary>
        /// Specification version at format "V.VV" or "VVVA" where A is the alphanumeric identifier, example: "108a"
        /// </summary>
        public PinpadFixedLengthProperty<string> GIN_SPECVER { get; private set; }
        /// <summary>
        /// Manufactured Version at format "VVV.VV YYMMDD"
        /// </summary>
        public PinpadFixedLengthProperty<string> GIN_MANVER { get; private set; }
        /// <summary>
        /// Serial number
        /// </summary>
        public PinpadFixedLengthProperty<string> GIN_SERNUM { get; private set; }

        // Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public GinResponse() 
		{
            this.RSP_LEN1 = new RegionProperty("RSP_LEN1", 3);
            this.GIN_MNAME = new PinpadFixedLengthProperty<string>("GIN_MNAME", 20, false, 
                DefaultStringFormatter.StringStringFormatter, DefaultStringParser.StringStringParser);
            this.GIN_MODEL = new PinpadFixedLengthProperty<string>("GIN_MODEL", 19, false, 
                DefaultStringFormatter.StringStringFormatter, DefaultStringParser.StringStringParser);
            this.GIN_CTLSUP = new PinpadFixedLengthProperty<string>("GIN_CTLSUP", 1, false, 
                DefaultStringFormatter.StringStringFormatter, DefaultStringParser.StringStringParser);
            this.GIN_SOVER = new PinpadFixedLengthProperty<string>("GIN_SOVER", 20, false, 
                DefaultStringFormatter.StringStringFormatter, DefaultStringParser.StringStringParser);
            this.GIN_SPECVER = new PinpadFixedLengthProperty<string>("GIN_SPECVER", 4, false, 
                DefaultStringFormatter.StringStringFormatter, DefaultStringParser.StringStringParser);
            this.GIN_MANVER = new PinpadFixedLengthProperty<string>("GIN_MANVER", 16, false, 
                DefaultStringFormatter.StringStringFormatter, DefaultStringParser.StringStringParser);
            this.GIN_SERNUM = new PinpadFixedLengthProperty<string>("GIN_SERNUM", 20, false, 
                DefaultStringFormatter.StringStringFormatter, DefaultStringParser.StringStringParser);

            this.StartRegion(this.RSP_LEN1);
            {
                this.AddProperty(this.GIN_MNAME);
                this.AddProperty(this.GIN_MODEL);
                this.AddProperty(this.GIN_CTLSUP);
                this.AddProperty(this.GIN_SOVER);
                this.AddProperty(this.GIN_SPECVER);
                this.AddProperty(this.GIN_MANVER);
                this.AddProperty(this.GIN_SERNUM);
            }
            this.EndLastRegion();
        }
    }
}
