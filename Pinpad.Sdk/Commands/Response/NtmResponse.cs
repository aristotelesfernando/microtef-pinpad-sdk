using Pinpad.Sdk.PinpadProperties.Refactor.Formatter;
using Pinpad.Sdk.PinpadProperties.Refactor.Property;

namespace Pinpad.Sdk.Commands 
{
	/// <summary>
	/// NTM response
	/// </summary>
	internal class NtmResponse : BaseResponse 
	{
		// Members
		/// <summary>
		/// Name of the command
		/// </summary>
		public override string CommandName { get { return "NTM"; } }
		/// <summary>
		/// Length of the first region of the command
		/// </summary>
		public RegionProperty RSP_LEN1 { get; private set; }
		/// <summary>
		/// Message that was displayed in the PinPad screen
		/// </summary>
		public TextProperty<SimpleMessageProperty> NTM_MSG { get; private set; }
        /// <summary>
        /// Is this a blocking command?
        /// </summary>
        public override bool IsBlockingCommand { get { return true; } }

        // Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public NtmResponse() 
		{
			this.RSP_LEN1 = new RegionProperty("RSP_LEN1", 3);
			this.NTM_MSG = new TextProperty<SimpleMessageProperty>("NTM_MSG", false, 
                StringFormatter.PropertyControllerStringFormatter, SimpleMessageProperty.CustomStringParser, null, 
                new SimpleMessageProperty());

			this.StartRegion(this.RSP_LEN1);
			{
				this.AddProperty(this.NTM_MSG);
			}
			this.EndLastRegion();
		}
	}
}
