using PinPadSDK.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PinPadSDK.Commands.Stone.PrtActionData {
    /// <summary>
    /// Controller for PRT step response action
    /// </summary>
    public class PrtStepResponseData : BasePrtResponseData {
        /// <summary>
        /// Constructor
        /// </summary>
        public PrtStepResponseData() {
        }

        /// <summary>
        /// Response Event
        /// </summary>
        public override PrtAction PRT_ACTION {
            get {
                return PrtAction.Step;
            }
        }
    }
}
