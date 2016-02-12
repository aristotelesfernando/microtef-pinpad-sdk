using PinPadSDK.Property;
using StonePortableUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PinPadSDK.Commands {
    /// <summary>
    /// GPN response
    /// </summary>
    public class GpnResponse : BaseResponse {
        /// <summary>
        /// Constructor
        /// </summary>
        public GpnResponse() {
            this.RSP_LEN1 = new RegionProperty("RSP_LEN1", 3);
            this.GPN_PINBLK = new PinPadFixedLengthPropertyController<HexadecimalData>("GPN_PINBLK", 16, false, DefaultStringFormatter.HexadecimalStringFormatter, DefaultStringParser.HexadecimalStringParser);
            this.GPN_KSN = new PinPadFixedLengthPropertyController<HexadecimalData>("GPN_KSN", 20, false, DefaultStringFormatter.HexadecimalStringFormatter, DefaultStringParser.HexadecimalStringParser);

            this.StartRegion(this.RSP_LEN1);
            {
                this.AddProperty(this.GPN_PINBLK);
                this.AddProperty(this.GPN_KSN);
            }
            this.EndLastRegion();
        }

        /// <summary>
        /// Is this a blocking command?
        /// </summary>
        public override bool IsBlockingCommand {
            get { return true; }
        }

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
        public PinPadFixedLengthPropertyController<HexadecimalData> GPN_PINBLK { get; private set; }

        /// <summary>
        /// Key Serial Number is DUKPT was used, for MK/WK this field is filled with zeros
        /// </summary>
        public PinPadFixedLengthPropertyController<HexadecimalData> GPN_KSN { get; private set; }
    }
}
