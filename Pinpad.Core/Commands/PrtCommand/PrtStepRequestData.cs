using Pinpad.Core.Properties;
using Pinpad.Core.TypeCode;
using System;

namespace Pinpad.Core.Commands
{
    /// <summary>
    /// Controller for PRT vertical step request action
    /// </summary>
    public class PrtStepRequestData : BasePrtRequestData {
        /// <summary>
        /// Constructor
        /// </summary>
        public PrtStepRequestData() {
            this.PRT_STEPS = new PinpadFixedLengthProperty<int?>("PRT_STEPS", 4, false, DefaultStringFormatter.IntegerStringFormatter, DefaultStringParser.IntegerStringParser);

            this.AddProperty(this.PRT_STEPS);
        }

        /// <summary>
        /// Response Event
        /// </summary>
        public override PrinterActionCode PRT_ACTION {
            get {
                return PrinterActionCode.Step;
            }
        }

        /// <summary>
        /// Adds to the vertical offset of the next print buffer append
        /// </summary>
        public PinpadFixedLengthProperty<Nullable<int>> PRT_STEPS { get; private set; }
    }
}
