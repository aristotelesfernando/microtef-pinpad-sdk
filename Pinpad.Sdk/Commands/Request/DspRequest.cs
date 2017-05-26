using Pinpad.Sdk.PinpadProperties.Refactor.Formatter;
using Pinpad.Sdk.PinpadProperties.Refactor.Property;

namespace Pinpad.Sdk.Commands 
{
	/// <summary>
	/// DSP request
	/// </summary>
	internal sealed class DspRequest : PinpadProperties.Refactor.BaseCommand 
	{
		// Members
		/// <summary>
		/// Name of the command
		/// </summary>
		public override string CommandName { get { return "DSP"; } }
		/// <summary>
		/// Length of the first region of the command
		/// </summary>
		public RegionProperty CMD_LEN1 { get; private set; }
		/// <summary>
		/// Message to display
		/// </summary>
		public TextProperty<SimpleMessageProperty> DSP_MSG { get; private set; }
		
		// Constructor
		/// <summary>
		/// Constructor
		/// </summary>
		public DspRequest() 
		{
			this.CMD_LEN1 = new RegionProperty("CMD_LEN1", 3);
			this.DSP_MSG = new TextProperty<SimpleMessageProperty>("DSP_MSG", false, 
                StringFormatter.PropertyControllerStringFormatter, SimpleMessageProperty.CustomStringParser, 
                null, new SimpleMessageProperty());

			this.StartRegion(this.CMD_LEN1);
			{
				this.AddProperty(this.DSP_MSG);
			}
			this.EndLastRegion();
		}
	}
}
