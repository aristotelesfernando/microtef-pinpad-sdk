using PinPadSDK.Property;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PinPadSDK.Commands.Stone {
    /// <summary>
    /// DSI request
    /// </summary>
    public class DsiRequest : BaseStoneRequest {
        /// <summary>
        /// Constructor
        /// </summary>
        public DsiRequest() {
            this.CMD_LEN1 = new RegionProperty("CMD_LEN1", 3);
            this.DSI_IMGNAME = new VariableLengthProperty<string>("DSI_IMGNAME", 3, 15, 1.0f, false, false, DefaultStringFormatter.StringStringFormatter, DefaultStringParser.StringStringParser);

            this.StartRegion(this.CMD_LEN1);
            {
                this.AddProperty(this.DSI_IMGNAME);
            }
            this.EndLastRegion();
        }

        /// <summary>
        /// Minimum stone version required for the request
        /// </summary>
        public override int MinimumStoneVersion { get { return 1; } }

        /// <summary>
        /// Name of the command
        /// </summary>
        public override string CommandName { get { return "DSI"; } }

        /// <summary>
        /// Length of the first region of the command
        /// </summary>
        public RegionProperty CMD_LEN1 { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public VariableLengthProperty<string> DSI_IMGNAME { get; private set; }
    }
}
