using PinPadSDK.Property;
using PinPadSDK.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PinPadSDK.Commands.CkeEventsData {
    /// <summary>
    /// CKE KeyPress event response
    /// Happens when a PinPad key is pressed when specified by the CKE request
    /// </summary>
    public class CkeKeyPressResponseData : BaseCkeResponseData {
        /// <summary>
        /// Constructor
        /// </summary>
        public CkeKeyPressResponseData() {
            this.CKE_KEYCODE = new PinPadFixedLengthPropertyController<PinPadKey>("CKE_KEYCODE", 2, false, DefaultStringFormatter.EnumStringFormatter<PinPadKey>, DefaultStringParser.EnumStringParser<PinPadKey>);

            this.AddProperty(this.CKE_KEYCODE);
        }

        /// <summary>
        /// Response Event
        /// </summary>
        public override CkeEvent CKE_EVENT {
            get { return CkeEvent.KeyPress; }
        }

        /// <summary>
        /// Key that was pressed
        /// </summary>
        public PinPadFixedLengthPropertyController<PinPadKey> CKE_KEYCODE { get; private set; }
    }
}
