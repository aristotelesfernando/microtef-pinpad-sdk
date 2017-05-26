using Pinpad.Sdk.PinpadProperties.Refactor.Formatter;
using Pinpad.Sdk.PinpadProperties.Refactor.Parser;
using Pinpad.Sdk.PinpadProperties.Refactor.Property;

namespace Pinpad.Sdk.Commands
{
    /// <summary>
    /// DEX request
    /// </summary>
    internal sealed class DexRequest : PinpadProperties.Refactor.BaseCommand
	{
		// Members
		/// <summary>
		/// Name of the command
		/// </summary>
		public override string CommandName { get { return "DEX"; } }
		/// <summary>
		/// Length of the first region of the command
		/// </summary>
		public RegionProperty CMD_LEN1 { get; private set; }
		/// <summary>
		/// Message to be displayed at the PinPad screen
		/// There is no control of screen height or width
		/// </summary>
		public VariableLengthProperty<MultilineMessageProperty> DEX_MSG { get; private set; }

		// Constructor
		/// <summary>
		/// Constructor.
		/// </summary>
		public DexRequest ()
		{
			this.CMD_LEN1 = new RegionProperty("CMD_LEN1", 3);
			this.DEX_MSG = new VariableLengthProperty<MultilineMessageProperty>("DEX_MSG", 3, 160, 1.0f, false, false, 
                StringFormatter.PropertyControllerStringFormatter, StringParser.PropertyControllerStringParser<MultilineMessageProperty>, 
                null, new MultilineMessageProperty());

			this.StartRegion(this.CMD_LEN1);
			{
				this.AddProperty(this.DEX_MSG);
			}
			this.EndLastRegion();
		}
	}
}
