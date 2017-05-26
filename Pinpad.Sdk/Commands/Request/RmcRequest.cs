using Pinpad.Sdk.PinpadProperties.Refactor.Formatter;
using Pinpad.Sdk.PinpadProperties.Refactor.Property;

namespace Pinpad.Sdk.Commands 
{
	/// <summary>
	/// Remove Card Request
	/// </summary>
	internal sealed class RmcRequest : PinpadProperties.Refactor.BaseCommand 
	{
        /// <summary>
		/// Name of the command
		/// </summary>
		public override string CommandName { get { return "RMC"; } }
        /// <summary>
        /// Length of the first region of the command
        /// </summary>
        public RegionProperty CMD_LEN1 { get; private set; }
        /// <summary>
        /// Message to be displayed along with remove card message while a card is inserted and left on screen after the card was removed.
        /// This message will remain on screen after the card is removed and even if it wasn't present to begin with.
        /// </summary>
        public TextProperty<SimpleMessageProperty> RMC_MSG { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public RmcRequest() 
		{
			this.CMD_LEN1 = new RegionProperty("CMD_LEN1", 3);
			this.RMC_MSG = new TextProperty<SimpleMessageProperty>("RMC_MSG", false, 
                StringFormatter.PropertyControllerStringFormatter, SimpleMessageProperty.CustomStringParser, null, 
                new SimpleMessageProperty());

			this.StartRegion(this.CMD_LEN1);
			{
				this.AddProperty(this.RMC_MSG);
			}
			this.EndLastRegion();
		}
	}
}
