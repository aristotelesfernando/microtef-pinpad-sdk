using Pinpad.Sdk.PinpadProperties.Refactor.Command;
using Pinpad.Sdk.PinpadProperties.Refactor.Formatter;
using Pinpad.Sdk.PinpadProperties.Refactor.Property;

namespace Pinpad.Sdk.Commands
{
    /// <summary>
    /// CLO request
    /// </summary>
    internal sealed class CloRequest : BaseCommand
	{
        /// <summary>
		/// Name of the command
		/// </summary>
		public override string CommandName { get { return "CLO"; } }
        /// <summary>
        /// Length of the first region of the command
        /// </summary>
        public RegionProperty CMD_LEN1 { get; private set; }
        /// <summary>
        /// Message to be left at the PinPad screen after closing the connection
        /// </summary>
        public TextProperty<SimpleMessageProperty> CLO_MSG { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public CloRequest ()
		{
			this.CMD_LEN1 = new RegionProperty("CMD_LEN1", 3);
			this.CLO_MSG = new TextProperty<SimpleMessageProperty>("CLO_MSG", false, 
                StringFormatter.PropertyControllerStringFormatter, SimpleMessageProperty.CustomStringParser, 
                null, new SimpleMessageProperty());

			this.StartRegion(this.CMD_LEN1);
			{
				this.AddProperty(this.CLO_MSG);
			}
			this.EndLastRegion();
		}
	}
}
