using Pinpad.Sdk.PinpadProperties.Refactor.Command;
using Pinpad.Sdk.PinpadProperties.Refactor.Formatter;
using Pinpad.Sdk.PinpadProperties.Refactor.Parser;
using Pinpad.Sdk.PinpadProperties.Refactor.Property;
using System;

namespace Pinpad.Sdk.Commands 
{
	/// <summary>
	/// GTS request
	/// </summary>
	internal sealed class GtsRequest : BaseCommand 
	{
		// Members
		/// <summary>
		/// Name of the command
		/// </summary>
		public override string CommandName { get { return "GTS"; } }
		/// <summary>
		/// Length of the first region of the command
		/// </summary>
		public RegionProperty CMD_LEN1 { get; private set; }
		/// <summary>
		/// EMV Table Acquirer Index to request version or 0 when using a single table for all acquirers
		/// </summary>
		public FixedLengthProperty<Nullable<int>> GTS_ACQIDX { get; private set; }
		
		// Constructor
		/// <summary>
		/// Constructor
		/// </summary>
		public GtsRequest() 
		{
			this.CMD_LEN1 = new RegionProperty("CMD_LEN1", 3);
			this.GTS_ACQIDX = new FixedLengthProperty<Nullable<int>>("GTS_ACQIDX", 2, false, 
                StringFormatter.IntegerStringFormatter, StringParser.IntegerStringParser);

			this.StartRegion(this.CMD_LEN1);
			{
				this.AddProperty(this.GTS_ACQIDX);
			}
			this.EndLastRegion();
		}
	}
}
