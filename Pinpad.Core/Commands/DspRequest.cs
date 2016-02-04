using PinPadSDK.Property;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PinPadSDK.Commands {
    /// <summary>
    /// DSP request
    /// </summary>
    public class DspRequest : BaseCommand {
        /// <summary>
        /// Constructor
        /// </summary>
        public DspRequest() {
            this.CMD_LEN1 = new RegionProperty("CMD_LEN1", 3);
            this.DSP_MSG = new SimpleProperty<SimpleMessage>("DSP_MSG", false, DefaultStringFormatter.PropertyControllerStringFormatter, SimpleMessage.StringParser, null, new SimpleMessage());

            this.StartRegion(this.CMD_LEN1);
            {
                this.AddProperty(this.DSP_MSG);
            }
            this.EndLastRegion();
        }

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
    }
}
