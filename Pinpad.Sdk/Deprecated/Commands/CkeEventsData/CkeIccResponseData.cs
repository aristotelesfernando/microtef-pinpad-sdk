using PinPadSDK.Property;
using PinPadSDK.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PinPadSDK.Commands.CkeEventsData {
    /// <summary>
    /// CKE ICC status report event response
    /// Happens when ICC status match the expected ICC status specified by the CKE request
    /// </summary>
    public class CkeIccResponseData : BaseCkeResponseData {
        /// <summary>
        /// Constructor
        /// </summary>
        public CkeIccResponseData() {
            this.CKE_ICCSTAT = new SimpleProperty<bool?>("CKE_ICCSTAT", false, DefaultStringFormatter.BooleanStringFormatter, DefaultStringParser.BooleanStringParser);

            this.AddProperty(this.CKE_ICCSTAT);
        }

        /// <summary>
        /// Response Event
        /// </summary>
        public override CkeEvent CKE_EVENT {
            get { return CkeEvent.IccStatusChanged; }
        }

        /// <summary>
        /// Wether the ICC is present or not. True means the ICC is present.
        /// </summary>
        public SimpleProperty<Nullable<bool>> CKE_ICCSTAT { get; private set; }
    }
}
