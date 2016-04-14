using Pinpad.Sdk.Properties;
using Pinpad.Sdk.Utilities;

namespace Pinpad.Sdk.Commands
{
    /// <summary>
    /// SEC response
    /// </summary>
    public class SecResponse : BaseResponse
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public SecResponse()
        {
            this.RSP_LEN1 = new RegionProperty("RSP_LEN1", 4);
            this.SEC_CMDBLK = new VariableLengthProperty<HexadecimalData>("SEC_CMDBLK", 3, 999, 1.0f, false, false, DefaultStringFormatter.HexadecimalStringFormatter, DefaultStringParser.HexadecimalStringParser);

            this.StartRegion(this.RSP_LEN1);
            {
                this.AddProperty(this.SEC_CMDBLK);
            }
            this.EndLastRegion();
        }

        /// <summary>
        /// Is this a blocking command?
        /// Since this command contain a blocking command it's considered a blocking command
        /// </summary>
        public override bool IsBlockingCommand { get { return true; } }

        /// <summary>
        /// Name of the command
        /// </summary>
        public override string CommandName { get { return "SEC"; } }

        /// <summary>
        /// Length of the first region of the command
        /// </summary>
        public RegionProperty RSP_LEN1 { get; private set; }

        /// <summary>
        /// Encrypted command block
        /// </summary>
        public VariableLengthProperty<HexadecimalData> SEC_CMDBLK { get; private set; }
    }
}
