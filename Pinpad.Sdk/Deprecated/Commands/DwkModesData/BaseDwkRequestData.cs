using PinPadSDK.Property;
using PinPadSDK.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PinPadSDK.Commands.DwkModesData {
    /// <summary>
    /// Base DWK request
    /// </summary>
    public class BaseDwkRequestData : BaseProperty {
        /// <summary>
        /// Constructor
        /// </summary>
        public BaseDwkRequestData() {
            this._DWK_MODE = new PinPadFixedLengthPropertyController<DwkMode>("DWK_MODE", 1, false, DefaultStringFormatter.EnumStringFormatter<DwkMode>, DefaultStringParser.EnumStringParser<DwkMode>, null, this.DWK_MODE);

            this.AddProperty(this._DWK_MODE);
        }

        /// <summary>
        /// Encryption Mode
        /// </summary>
        public virtual DwkMode DWK_MODE {
            get {
                if (this._DWK_MODE != null) {
                    return this._DWK_MODE.Value;
                }
                else {
                    return DwkMode.Undefined;
                }
            }
        }

        private PinPadFixedLengthPropertyController<DwkMode> _DWK_MODE { get; set; }
    }
}
