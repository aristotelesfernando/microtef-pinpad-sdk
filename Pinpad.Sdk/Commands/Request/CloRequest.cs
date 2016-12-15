using Pinpad.Sdk.Properties;

namespace Pinpad.Sdk.Commands
{
	/// <summary>
	/// CLO request
	/// </summary>
	internal sealed class CloRequest : BaseCommand
	{
		/// <summary>
		/// Constructor
		/// </summary>
		public CloRequest ()
		{
			this.CMD_LEN1 = new RegionProperty("CMD_LEN1", 3);
			this.CLO_MSG = new SimpleProperty<SimpleMessage>("CLO_MSG", false, DefaultStringFormatter.PropertyControllerStringFormatter, SimpleMessage.StringParser, null, new SimpleMessage());

			this.StartRegion(this.CMD_LEN1);
			{
				this.AddProperty(this.CLO_MSG);
			}
			this.EndLastRegion();
		}

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
		public SimpleProperty<SimpleMessage> CLO_MSG { get; private set; }
	}
}
