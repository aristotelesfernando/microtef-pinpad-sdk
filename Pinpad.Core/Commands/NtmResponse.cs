using Pinpad.Core.Properties;

/* WARNING!
 * 
 * DEPRECATED.
 * MUST BE REFACTORED.
 * 
 */

namespace Pinpad.Core.Commands 
{
    /// <summary>
    /// NTM response
    /// </summary>
    public class NtmResponse : BaseResponse 
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
        public SimpleProperty<SimpleMessage> NTM_MSG { get; private set; }
        
		// Constructor
		/// <summary>
        /// Constructor
        /// </summary>
        public NtmResponse() 
		{
            this.RSP_LEN1 = new RegionProperty("RSP_LEN1", 3);
            this.NTM_MSG = new SimpleProperty<SimpleMessage>("NTM_MSG", false, DefaultStringFormatter.PropertyControllerStringFormatter, SimpleMessage.StringParser, null, new SimpleMessage());

            this.StartRegion(this.RSP_LEN1);
            {
                this.AddProperty(this.NTM_MSG);
            }
            this.EndLastRegion();
        }

		// Method
        /// <summary>
        /// Is this a blocking command?
        /// </summary>
        public override bool IsBlockingCommand {
            get { return true; }
        }
    }
}
