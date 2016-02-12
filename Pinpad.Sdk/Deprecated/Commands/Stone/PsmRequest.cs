using PinPadSDK.Property;
using PinPadSDK.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PinPadSDK.Commands.Stone {
    /// <summary>
    /// Power Saving Mode Request
    /// </summary>
    public class PsmRequest : BaseStoneRequest {
        /// <summary>
        /// Constructor
        /// </summary>
        public PsmRequest() {
            this.CMD_LEN1 = new RegionProperty("CMD_LEN1", 3);
            this.PSM_LIGHTSTATUS = new PinPadFixedLengthPropertyController<BacklightStatus>("PSM_LIGHTSTATUS", 1, false, DefaultStringFormatter.EnumStringFormatter<BacklightStatus>, DefaultStringParser.EnumStringParser<BacklightStatus>);
            this.PSM_SLEEPTIME = new PinPadFixedLengthPropertyController<int?>("PSM_SLEEPTIME", 3, false, DefaultStringFormatter.IntegerStringFormatter, DefaultStringParser.IntegerStringParser);

            this.StartRegion(this.CMD_LEN1);
            {
                this.AddProperty(this.PSM_LIGHTSTATUS);
                this.AddProperty(this.PSM_SLEEPTIME);
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
        public override string CommandName { get { return "PSM"; } }

        /// <summary>
        /// Length of the first region of the command
        /// </summary>
        public RegionProperty CMD_LEN1 { get; private set; }

        /// <summary>
        /// Screen backlight status
        /// </summary>
        public PinPadFixedLengthPropertyController<BacklightStatus> PSM_LIGHTSTATUS { get; private set; }

        /// <summary>
        /// Sleep time in miliseconds between work cycles, directly affects terminal's FPS
        /// </summary>
        public PinPadFixedLengthPropertyController<Nullable<int>> PSM_SLEEPTIME { get; private set; }
    }
}
