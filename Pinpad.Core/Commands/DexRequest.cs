using PinPadSDK.Property;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PinPadSDK.Commands {
    /// <summary>
    /// DEX request
    /// </summary>
    public class DexRequest : BaseCommand {
        /// <summary>
        /// Constructor
        /// </summary>
        public DexRequest() {
            this.CMD_LEN1 = new RegionProperty("CMD_LEN1", 3);
            this.DEX_MSG = new VariableLengthProperty<MultilineMessage>("DEX_MSG", 3, 160, 1.0f, false, false, DefaultStringFormatter.PropertyControllerStringFormatter, DefaultStringParser.PropertyControllerStringParser<MultilineMessage>, null, new MultilineMessage());

            this.StartRegion(this.CMD_LEN1);
            {
                this.AddProperty(this.DEX_MSG);
            }
            this.EndLastRegion();
        }

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
        public VariableLengthProperty<MultilineMessage> DEX_MSG { get; private set; }
    }
}
