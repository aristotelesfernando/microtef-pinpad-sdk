using PinPadSDK.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PinPadSDK.Commands.Stone.PrtActionData {
    /// <summary>
    /// Controller for PRT append image response action
    /// </summary>
    public class PrtAppendImageResponseData : BasePrtResponseData {
        /// <summary>
        /// Constructor
        /// </summary>
        public PrtAppendImageResponseData() {
        }

        /// <summary>
        /// Response Event
        /// </summary>
        public override PrtAction PRT_ACTION {
            get {
                return PrtAction.AppendImage;
            }
        }
    }
}
