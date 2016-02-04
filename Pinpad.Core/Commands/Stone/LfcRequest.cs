using PinPadSDK.Property;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PinPadSDK.Commands.Stone {
    /// <summary>
    /// LFC request
    /// </summary>
    public class LfcRequest : BaseStoneRequest {
        /// <summary>
        /// Constructor
        /// </summary>
        public LfcRequest() {
            this.CMD_LEN1 = new RegionProperty("CMD_LEN1", 3);
            this.LFC_FILENAME = new VariableLengthProperty<string>("LFC_FILENAME", 3, 15, 1.0f, false, false, DefaultStringFormatter.StringStringFormatter, DefaultStringParser.StringStringParser);

            this.StartRegion(this.CMD_LEN1);
            {
                this.AddProperty(this.LFC_FILENAME);
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
        public override string CommandName { get { return "LFC"; } }

        /// <summary>
        /// Length of the first region of the command
        /// </summary>
        public RegionProperty CMD_LEN1 { get; private set; }

        /// <summary>
        /// File name to check
        /// </summary>
        public VariableLengthProperty<string> LFC_FILENAME { get; private set; }
    }
}
