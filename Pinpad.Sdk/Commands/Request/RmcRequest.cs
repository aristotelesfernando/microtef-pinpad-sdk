using Pinpad.Sdk.Properties;

namespace Pinpad.Sdk.Commands 
{
	/// <summary>
	/// Remove Card Request
	/// </summary>
	public class RmcRequest : BaseCommand 
	{
		/// <summary>
		/// Constructor
		/// </summary>
		public RmcRequest() 
		{
			this.CMD_LEN1 = new RegionProperty("CMD_LEN1", 3);
			this.RMC_MSG = new SimpleProperty<SimpleMessage>("RMC_MSG", false, DefaultStringFormatter.PropertyControllerStringFormatter, SimpleMessage.StringParser, null, new SimpleMessage());

			this.StartRegion(this.CMD_LEN1);
			{
				this.AddProperty(this.RMC_MSG);
			}
			this.EndLastRegion();
		}

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
		public SimpleProperty<SimpleMessage> RMC_MSG { get; private set; }
	}
}
