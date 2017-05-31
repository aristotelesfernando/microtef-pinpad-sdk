using Pinpad.Sdk.PinpadProperties.Refactor.Formatter;
using Pinpad.Sdk.PinpadProperties.Refactor.Parser;
using Pinpad.Sdk.PinpadProperties.Refactor.Property;

namespace Pinpad.Sdk.Commands
{
    /// <summary>
    /// GPN response
    /// </summary>
    internal class GpnResponse : BaseResponse
    {
        /// <summary>
        /// Is this a blocking command?
        /// </summary>
        public override bool IsBlockingCommand { get { return true; } }
        /// <summary>
        /// Name of the command
        /// </summary>
        public override string CommandName { get { return "GPN"; } }
        /// <summary>
        /// Length of the first region of the command
        /// </summary>
        public RegionProperty RSP_LEN1 { get; private set; }
        /// <summary>
        /// Encrypted Pin Block containing the data from the GPN request concatenated
        /// </summary>
        public FixedLengthProperty<HexadecimalData> GPN_PINBLK { get; private set; }
        /// <summary>
        /// Key Serial Number is DUKPT was used, for MK/WK this field is filled with zeros
        /// </summary>
        public FixedLengthProperty<HexadecimalData> GPN_KSN { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public GpnResponse()
        {
            this.RSP_LEN1 = new RegionProperty("RSP_LEN1", 3);
            this.GPN_PINBLK = new FixedLengthProperty<HexadecimalData>("GPN_PINBLK", 16, false, 
                StringFormatter.HexadecimalStringFormatter, StringParser.HexadecimalStringParser);
            this.GPN_KSN = new FixedLengthProperty<HexadecimalData>("GPN_KSN", 20, false, 
                StringFormatter.HexadecimalStringFormatter, StringParser.HexadecimalStringParser);

            this.StartRegion(this.RSP_LEN1);
            {
                this.AddProperty(this.GPN_PINBLK);
                this.AddProperty(this.GPN_KSN);
            }
            this.EndLastRegion();
        }
    }
}
