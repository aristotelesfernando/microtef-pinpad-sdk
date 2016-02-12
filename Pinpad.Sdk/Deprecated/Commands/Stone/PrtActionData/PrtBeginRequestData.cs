using PinPadSDK.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PinPadSDK.Commands.Stone.PrtActionData {
    /// <summary>
    /// Controller for PRT begin request action
    /// </summary>
    public class PrtBeginRequestData : BasePrtRequestData {
        /// <summary>
        /// Constructor
        /// </summary>
        public PrtBeginRequestData() {
        }

        /// <summary>
        /// Response Event
        /// </summary>
        public override PrtAction PRT_ACTION {
            get {
                return PrtAction.Begin;
            }
        }
    }
}
