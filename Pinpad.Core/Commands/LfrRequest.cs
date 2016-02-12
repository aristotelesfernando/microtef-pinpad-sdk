using Pinpad.Core.Properties;
using Pinpad.Core.Utilities;

namespace Pinpad.Core.Commands {
    /// <summary>
    /// LFR request
    /// </summary>
    public class LfrRequest : BaseStoneRequest {
        /// <summary>
        /// Constructor
        /// </summary>
        public LfrRequest() {
            this.CMD_LEN1 = new RegionProperty("CMD_LEN1", 3);
            this.LFR_DATA = new VariableLengthProperty<HexadecimalData>("LFR_DATA", 3, 999, 1.0f / 2, false, false, DefaultStringFormatter.HexadecimalStringFormatter, DefaultStringParser.HexadecimalStringParser);

            this.StartRegion(this.CMD_LEN1);
            {
                this.AddProperty(this.LFR_DATA);
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
        public override string CommandName { get { return "LFR"; } }

        /// <summary>
        /// Length of the first region of the command
        /// </summary>
        public RegionProperty CMD_LEN1 { get; private set; }

        /// <summary>
        /// Data to append to file
        /// </summary>
        public VariableLengthProperty<HexadecimalData> LFR_DATA { get; private set; }
    }
}
