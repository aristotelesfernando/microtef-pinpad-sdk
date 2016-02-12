using PinPadSDK.Property;
using PinPadSDK.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PinPadSDK.Commands.Stone.PrtActionData {
    /// <summary>
    /// Controller for PRT vertical step request action
    /// </summary>
    public class PrtStepRequestData : BasePrtRequestData {
        /// <summary>
        /// Constructor
        /// </summary>
        public PrtStepRequestData() {
            this.PRT_STEPS = new PinPadFixedLengthPropertyController<int?>("PRT_STEPS", 4, false, DefaultStringFormatter.IntegerStringFormatter, DefaultStringParser.IntegerStringParser);

            this.AddProperty(this.PRT_STEPS);
        }

        /// <summary>
        /// Response Event
        /// </summary>
        public override PrtAction PRT_ACTION {
            get {
                return PrtAction.Step;
            }
        }

        /// <summary>
        /// Adds to the vertical offset of the next print buffer append
        /// </summary>
        public PinPadFixedLengthPropertyController<Nullable<int>> PRT_STEPS { get; private set; }
    }
}
