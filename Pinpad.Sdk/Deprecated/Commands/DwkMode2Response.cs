using PinPadSDK.Property;
using StonePortableUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PinPadSDK.Commands {
    /// <summary>
    /// Controller for DWK Mode2 response
    /// </summary>
    public class DwkMode2Response : BaseResponse {
        /// <summary>
        /// Constructor
        /// </summary>
        public DwkMode2Response() {
            this.RSP_LEN1 = new RegionProperty("RSP_LEN1", 3);
            this.DWK_CRYPT = new PinPadFixedLengthPropertyController<HexadecimalData>("DWK_CRYPT", 256, false, DefaultStringFormatter.HexadecimalStringFormatter, DefaultStringParser.HexadecimalStringParser);

            this.StartRegion(this.RSP_LEN1);
            {
                this.AddProperty(this.DWK_CRYPT);
            }
            this.EndLastRegion();
        }

        /// <summary>
        /// Is this a blocking command?
        /// </summary>
        public override bool IsBlockingCommand {
            get { return false; }
        }

        /// <summary>
        /// Name of the command
        /// </summary>
        public override string CommandName { get { return "DWK"; } }

        /// <summary>
        /// Length of the first region of the command
        /// </summary>
        public RegionProperty RSP_LEN1 { get; private set; }

        /// <summary>
        /// RSA cryptogram containing a random WorkingKey for Pan
        /// </summary>
        public PinPadFixedLengthPropertyController<HexadecimalData> DWK_CRYPT { get; private set; }
    }
}
