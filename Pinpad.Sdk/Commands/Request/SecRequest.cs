using Pinpad.Sdk.Properties;
using Pinpad.Sdk.Utilities;
using System;

namespace Pinpad.Sdk.Commands
{
    /// <summary>
    /// SEC request
    /// </summary>
    public class SecRequest : BaseStoneRequest
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public SecRequest()
        {
            this.CMD_LEN1 = new RegionProperty("CMD_LEN1", 3);
            this.SEC_ACQIDX = new PinpadFixedLengthProperty<Nullable<int>>("SEC_ACQIDX", 2, false, DefaultStringFormatter.IntegerStringFormatter, DefaultStringParser.IntegerStringParser);
            this.SEC_CMDBLK = new VariableLengthProperty<HexadecimalData>("SEC_CMDBLK", 3, 999, 1.0f / 8, false, false, DefaultStringFormatter.HexadecimalStringFormatter, DefaultStringParser.HexadecimalStringParser);

            this.StartRegion(this.CMD_LEN1);
            {
                this.AddProperty(this.SEC_ACQIDX);
                this.AddProperty(this.SEC_CMDBLK);
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
        public override string CommandName { get { return "SEC"; } }

        /// <summary>
        /// Length of the first region of the command
        /// </summary>
        public RegionProperty CMD_LEN1 { get; private set; }

        /// <summary>
        /// Acquirer index for the cryptogram
        /// </summary>
        public PinpadFixedLengthProperty<Nullable<int>> SEC_ACQIDX { get; private set; }

        /// <summary>
        /// Encrypted command block
        /// </summary>
        public VariableLengthProperty<HexadecimalData> SEC_CMDBLK { get; private set; }
    }
}
