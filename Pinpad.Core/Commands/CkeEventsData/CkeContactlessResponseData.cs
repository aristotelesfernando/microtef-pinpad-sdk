using PinPadSDK.Property;
using PinPadSDK.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PinPadSDK.Commands.CkeEventsData {
    /// <summary>
    /// CTLS status report event response
    /// Happens when either a CTLS device was detected or timed out after 2 minutes if specified by the CKE request
    /// </summary>
    public class CkeContactlessResponseData : BaseCkeResponseData {
        /// <summary>
        /// Constructor
        /// </summary>
        public CkeContactlessResponseData() {
            this.CKE_CTLSSTAT = new SimpleProperty<bool?>("CKE_CTLSSTAT", false, DefaultStringFormatter.BooleanStringFormatter, DefaultStringParser.BooleanStringParser);

            this.AddProperty(this.CKE_CTLSSTAT);
        }

        /// <summary>
        /// Response Event
        /// </summary>
        public override CkeEvent CKE_EVENT {
            get { return CkeEvent.CtlsUpdate; }
        }

        /// <summary>
        /// Wether a CTLS device was detected within the time limit of 2 minutes
        /// True means a device was detected
        /// False means the 2 minute timeout was reached
        /// </summary>
        public SimpleProperty<Nullable<bool>> CKE_CTLSSTAT { get; private set; }
    }
}
