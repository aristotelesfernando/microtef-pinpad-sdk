using PinPadSDK.Property;
using PinPadSDK.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PinPadSDK.Commands.CkeEventsData {
    /// <summary>
    /// Base CKE 
    /// </summary>
    public class BaseCkeResponseData : BaseProperty {
        /// <summary>
        /// Constructor
        /// </summary>
        public BaseCkeResponseData() {
            this._CKE_EVENT = new PinPadFixedLengthPropertyController<CkeEvent>("CKE_EVENT", 1, false, DefaultStringFormatter.EnumStringFormatter<CkeEvent>, DefaultStringParser.EnumStringParser<CkeEvent>, null, this.CKE_EVENT);

            this.AddProperty(this._CKE_EVENT);
        }

        /// <summary>
        /// Response Event
        /// </summary>
        public virtual CkeEvent CKE_EVENT {
            get {
                if (this._CKE_EVENT != null) {
                    return this._CKE_EVENT.Value;
                }
                else {
                    return CkeEvent.Undefined;
                }
            }
        }

        private PinPadFixedLengthPropertyController<CkeEvent> _CKE_EVENT { get; set; }
    }
}
