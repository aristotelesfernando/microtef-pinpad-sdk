using Pinpad.Sdk.PinpadProperties.Refactor.Command;
using Pinpad.Sdk.PinpadProperties.Refactor.Formatter;
using Pinpad.Sdk.PinpadProperties.Refactor.Parser;
using Pinpad.Sdk.PinpadProperties.Refactor.Property;
using System;

namespace Pinpad.Sdk.Commands
{
	/// <summary>
	/// TLI request
	/// </summary>
	internal sealed class TliRequest : BaseCommand
	{
        /// <summary>
		/// Name of the command
		/// </summary>
		public override string CommandName { get { return "TLI"; } }
        /// <summary>
        /// Length of the first region of the command
        /// </summary>
        public RegionProperty CMD_LEN1 { get; private set; }
        /// <summary>
        /// EMV Table acquirer index to update
        /// To change all acquirers use the generic index (0)
        /// </summary>
        public FixedLengthProperty<Nullable<int>> TLI_ACQIDX { get; private set; }
        /// <summary>
        /// EMV Table version to load
        /// </summary>
        public FixedLengthProperty<string> TLI_TABVER { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public TliRequest() 
			: base (new AbecsContext())
		{
			this.CMD_LEN1 = new RegionProperty("CMD_LEN1", 3);
			this.TLI_ACQIDX = new FixedLengthProperty<Nullable<int>>("TLI_ACQIDX", 2, false, 
                StringFormatter.IntegerStringFormatter, StringParser.IntegerStringParser);
			this.TLI_TABVER = new FixedLengthProperty<string>("TLI_TABVER", 10, false, 
                StringFormatter.StringStringFormatter, StringParser.StringStringParser);

			this.StartRegion(this.CMD_LEN1);
			{
				this.AddProperty(this.TLI_ACQIDX);
				this.AddProperty(this.TLI_TABVER);
			}
			this.EndLastRegion();
		}
	}
}
