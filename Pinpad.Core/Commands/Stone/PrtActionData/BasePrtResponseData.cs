using PinPadSDK.Property;
using PinPadSDK.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PinPadSDK.Commands.Stone.PrtActionData {
    /// <summary>
    /// Base Prt Response Action
    /// </summary>
    public class BasePrtResponseData : BaseProperty {
        /// <summary>
        /// Constructor
        /// </summary>
        public BasePrtResponseData() {
            this._PRT_ACTION = new PinPadFixedLengthPropertyController<PrtAction>("PRT_ACTION", 1, false, DefaultStringFormatter.EnumStringFormatter<PrtAction>, DefaultStringParser.EnumStringParser<PrtAction>, null, this.PRT_ACTION);

            this.AddProperty(this._PRT_ACTION);
        }

        /// <summary>
        /// Action
        /// </summary>
        public virtual PrtAction PRT_ACTION {
            get {
                if (this._PRT_ACTION != null) {
                    return this._PRT_ACTION.Value;
                }
                else {
                    return PrtAction.Undefined;
                }
            }
        }

        private PinPadFixedLengthPropertyController<PrtAction> _PRT_ACTION { get; set; }
    }
}
