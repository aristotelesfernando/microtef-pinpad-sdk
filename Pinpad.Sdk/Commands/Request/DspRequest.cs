using Pinpad.Sdk.Properties;

/* WARNING!
 * 
 * DEPRECATED.
 * MUST BE REFACTORED.
 * 
 */

namespace Pinpad.Sdk.Commands 
{
	/// <summary>
	/// DSP request
	/// </summary>
	public class DspRequest : BaseCommand 
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
		public SimpleProperty<SimpleMessage> DSP_MSG { get; private set; }
		
		// Constructor
		/// <summary>
		/// Constructor
		/// </summary>
		public DspRequest() 
		{
			this.CMD_LEN1 = new RegionProperty("CMD_LEN1", 3);
			this.DSP_MSG = new SimpleProperty<SimpleMessage>("DSP_MSG", false, DefaultStringFormatter.PropertyControllerStringFormatter, SimpleMessage.StringParser, null, new SimpleMessage());

			this.StartRegion(this.CMD_LEN1);
			{
				this.AddProperty(this.DSP_MSG);
			}
			this.EndLastRegion();
		}
	}
}
