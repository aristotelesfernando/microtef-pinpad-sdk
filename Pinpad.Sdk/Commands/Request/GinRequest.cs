using Pinpad.Sdk.PinpadProperties.Refactor.Formatter;
using Pinpad.Sdk.PinpadProperties.Refactor.Parser;
using Pinpad.Sdk.PinpadProperties.Refactor.Property;
using System;

namespace Pinpad.Sdk.Commands
{
    /// <summary>
    /// GIN request
    /// </summary>
    internal sealed class GinRequest : PinpadProperties.Refactor.BaseCommand 
	{
		// Members
		/// <summary>
		/// Name of the command
		/// </summary>
		public override string CommandName { get { return "GIN"; } }
		/// <summary>
		/// Length of the first region of the command
		/// </summary>
		public RegionProperty CMD_LEN1 { get; private set; }
		/// <summary>
		/// Acquirer index to get information from or 0 to get PinPad information
		/// </summary>
		public FixedLengthProperty<Nullable<int>> GIN_ACQIDX { get; private set; }
		
		// Constructor
		/// <summary>
		/// Constructor
		/// </summary>
		public GinRequest() 
		{
			this.CMD_LEN1 = new RegionProperty("CMD_LEN1", 3);
			this.GIN_ACQIDX = new FixedLengthProperty<Nullable<int>>("GIN_ACQIDX", 2, false, 
                StringFormatter.IntegerStringFormatter, StringParser.IntegerStringParser);

			this.StartRegion(this.CMD_LEN1);
			{
				this.AddProperty(this.GIN_ACQIDX);
			}
			this.EndLastRegion();
		}
	}
}
