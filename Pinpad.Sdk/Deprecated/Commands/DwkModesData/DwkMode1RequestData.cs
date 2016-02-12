using PinPadSDK.Property;
using PinPadSDK.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StonePortableUtils;

namespace PinPadSDK.Commands.DwkModesData {
    /// <summary>
    /// Controller for DWK Mode 1 request
    /// </summary>
    public class DwkMode1RequestData : BaseDwkRequestData {
        /// <summary>
        /// Constructor
        /// </summary>
        public DwkMode1RequestData() {
            this.DWK_METHOD = new SimpleProperty<CryptographyMethod>("DWK_METHOD", false, CryptographyMethod.StringFormatter, CryptographyMethod.StringParser);
            this.DWK_MKIDX = new PinPadFixedLengthPropertyController<int?>("DWK_MKIDX", 2, false, DefaultStringFormatter.IntegerStringFormatter, DefaultStringParser.IntegerStringParser);
            this.DWK_WKPAN = new PinPadFixedLengthPropertyController<HexadecimalData>("DWK_WKPAN", 32, false, DefaultStringFormatter.HexadecimalRightPaddingStringFormatter, DefaultStringParser.HexadecimalRightPaddingStringParser);

            this.AddProperty(this.DWK_METHOD);
            this.AddProperty(this.DWK_MKIDX);
            this.AddProperty(this.DWK_WKPAN);
        }

        /// <summary>
        /// Encryption Mode
        /// </summary>
        public override DwkMode DWK_MODE {
            get {
                return DwkMode.Mode1;
            }
        }

        /// <summary>
        /// Encryption method
        /// Only MasterKey/WorkingKey for Key Management Method is supported
        /// </summary>
        public SimpleProperty<CryptographyMethod> DWK_METHOD { get; private set; }

        /// <summary>
        /// Master Key index to use
        /// </summary>
        public PinPadFixedLengthPropertyController<Nullable<int>> DWK_MKIDX { get; private set; }

        /// <summary>
        /// Encrypted Working Key Pan by Master Key.
        /// If used DES only the first 16 chars (8 bytes) are used
        /// </summary>
        public PinPadFixedLengthPropertyController<HexadecimalData> DWK_WKPAN { get; private set; }
    }
}
