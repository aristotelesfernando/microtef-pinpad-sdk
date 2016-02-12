using PinPadSDK.Property;
using PinPadSDK.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PinPadSDK.Commands.Stone {
    /// <summary>
    /// GKE request
    /// </summary>
    public class GkeRequest : BaseStoneRequest {
        /// <summary>
        /// Constructor
        /// </summary>
        public GkeRequest() {
            this.CMD_LEN1 = new RegionProperty("CMD_LEN1", 3);
            this.GKE_ACTION = new PinPadFixedLengthPropertyController<GkeAction>("GKE_ACTION", 1, false, DefaultStringFormatter.EnumStringFormatter<GkeAction>, DefaultStringParser.EnumStringParser<GkeAction>);

            this.StartRegion(this.CMD_LEN1);
            {
                this.AddProperty(this.GKE_ACTION);
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
        public override string CommandName { get { return "GKE"; } }

        /// <summary>
        /// Length of the first region of the command
        /// </summary>
        public RegionProperty CMD_LEN1 { get; private set; }

        /// <summary>
        /// Action to perform
        /// </summary>
        public PinPadFixedLengthPropertyController<GkeAction> GKE_ACTION { get; private set; }
    }
}
