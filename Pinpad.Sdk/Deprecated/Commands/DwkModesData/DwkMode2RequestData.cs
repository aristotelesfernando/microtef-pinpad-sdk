using PinPadSDK.Property;
using PinPadSDK.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StonePortableUtils;

namespace PinPadSDK.Commands.DwkModesData {
    /// <summary>
    /// Controller for DWK Mode 2 request
    /// </summary>
    public class DwkMode2RequestData : BaseDwkRequestData {
        /// <summary>
        /// Constructor
        /// </summary>
        public DwkMode2RequestData() {
            this.DWK_RSAMOD = new PinPadFixedLengthPropertyController<HexadecimalData>("DWK_RSAMOD", 256, false, DefaultStringFormatter.HexadecimalStringFormatter, DefaultStringParser.HexadecimalStringParser);
            this.DWK_RSAEXP = new PinPadFixedLengthPropertyController<HexadecimalData>("DWK_RSAEXP", 6, false, DefaultStringFormatter.HexadecimalStringFormatter, DefaultStringParser.HexadecimalStringParser);

            this.AddProperty(this.DWK_RSAMOD);
            this.AddProperty(this.DWK_RSAEXP);
        }

        /// <summary>
        /// Encryption Mode
        /// </summary>
        public override DwkMode DWK_MODE {
            get {
                return DwkMode.Mode2;
            }
        }

        /// <summary>
        /// Modulus of the RSA Public Key created by the eletronic payment system
        /// </summary>
        public PinPadFixedLengthPropertyController<HexadecimalData> DWK_RSAMOD { get; private set; }

        /// <summary>
        /// Expoent of the RSA Public Key created by the eletronic payment system
        /// </summary>
        public PinPadFixedLengthPropertyController<HexadecimalData> DWK_RSAEXP { get; private set; }
    }
}
