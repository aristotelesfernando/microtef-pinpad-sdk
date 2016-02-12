using PinPadSDK.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PinPadSDK.Commands.Stone.PrtActionData {
    /// <summary>
    /// Controller for PRT append string response action
    /// </summary>
    public class PrtAppendStringResponseData : BasePrtResponseData {
        /// <summary>
        /// Constructor
        /// </summary>
        public PrtAppendStringResponseData() {
        }

        /// <summary>
        /// Response Event
        /// </summary>
        public override PrtAction PRT_ACTION {
            get {
                return PrtAction.AppendString;
            }
        }
    }
}
