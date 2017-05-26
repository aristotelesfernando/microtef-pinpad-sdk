using Pinpad.Sdk.PinpadProperties.Refactor.Formatter;
using Pinpad.Sdk.PinpadProperties.Refactor.Parser;
using Pinpad.Sdk.PinpadProperties.Refactor.Property;

namespace Pinpad.Sdk.Commands 
{
	/// <summary>
	/// GTS response
	/// </summary>
	internal class GtsResponse : BaseResponse 
	{
        /// <summary>
		/// Is this a blocking command?
		/// </summary>
		public override bool IsBlockingCommand { get { return false; } }
        /// <summary>
        /// Name of the command
        /// </summary>
        public override string CommandName { get { return "GTS"; } }
        /// <summary>
        /// Length of the first region of the command
        /// </summary>
        public RegionProperty RSP_LEN1 { get; private set; }

        /// <summary>
        /// Version of the EMV table from the requested Acquirer index
        /// If there was no table loaded for the requested acquirer index this field will be filled with zeros
        /// If the table was loaded without the generic index (0) and the requested index was generic this field will be filled with zeros
        /// </summary>
        public FixedLengthProperty<string> GTS_TABVER { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public GtsResponse() 
		{
			this.RSP_LEN1 = new RegionProperty("RSP_LEN1", 3);
			this.GTS_TABVER = new FixedLengthProperty<string>("GTS_TABVER", 10, false, 
                StringFormatter.StringStringFormatter, StringParser.StringStringParser);

			this.StartRegion(this.RSP_LEN1);
			{
				this.AddProperty(this.GTS_TABVER);
			}
			this.EndLastRegion();
		}
	}
}
