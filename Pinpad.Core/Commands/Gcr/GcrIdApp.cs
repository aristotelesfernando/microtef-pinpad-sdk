using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PinPadSDK.Property;

namespace PinPadSDK.Commands.Gcr {
    /// <summary>
    /// Controller for GCR PinPad command request Id App entry
    /// </summary>
    public class GcrIdApp : BaseProperty {
        /// <summary>
        /// Constructor
        /// </summary>
        public GcrIdApp() {
            this.TAB_ACQ = new PinPadFixedLengthPropertyController<Nullable<int>>("TAB_ACQ", 2, false, DefaultStringFormatter.IntegerStringFormatter, DefaultStringParser.IntegerStringParser);
            this.TAB_RECIDX = new PinPadFixedLengthPropertyController<Nullable<int>>("TAB_RECIDX", 2, false, DefaultStringFormatter.IntegerStringFormatter, DefaultStringParser.IntegerStringParser);

            this.AddProperty(this.TAB_ACQ);
            this.AddProperty(this.TAB_RECIDX);
        }

        /// <summary>
        /// Constructor with initial value
        /// </summary>
        /// <param name="acquirer">Aid Table Acquirer Index</param>
        /// <param name="record">Aid Table Record Index</param>
        public GcrIdApp(int acquirer, int record)
            : this() {
            this.TAB_ACQ.Value = acquirer;
            this.TAB_RECIDX.Value = record;
        }

        /// <summary>
        /// Aid Table Acquirer Index
        /// </summary>
        public PinPadFixedLengthPropertyController<Nullable<int>> TAB_ACQ { get; private set; }

        /// <summary>
        /// Aid Table Record Index
        /// </summary>
        public PinPadFixedLengthPropertyController<Nullable<int>> TAB_RECIDX { get; private set; }
    }
}
