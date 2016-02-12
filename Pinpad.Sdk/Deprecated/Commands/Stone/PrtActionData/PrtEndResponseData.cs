using PinPadSDK.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PinPadSDK.Commands.Stone.PrtActionData {
    /// <summary>
    /// Controller for PRT end response action
    /// </summary>
    public class PrtEndResponseData : BasePrtResponseData {
        /// <summary>
        /// Constructor
        /// </summary>
        public PrtEndResponseData() {
        }

        /// <summary>
        /// Response Event
        /// </summary>
        public override PrtAction PRT_ACTION {
            get {
                return PrtAction.End;
            }
        }
    }
}
