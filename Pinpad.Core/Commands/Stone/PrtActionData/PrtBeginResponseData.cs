using PinPadSDK.Property;
using PinPadSDK.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PinPadSDK.Commands.Stone.PrtActionData {
    /// <summary>
    /// Controller for PRT begin response action
    /// </summary>
    public class PrtBeginResponseData : BasePrtResponseData {
        /// <summary>
        /// Constructor
        /// </summary>
        public PrtBeginResponseData() {
            this.PRT_STATUS = new PinPadFixedLengthPropertyController<PinPadPrinterStatus>("PRT_STATUS", 3, false, DefaultStringFormatter.EnumStringFormatter<PinPadPrinterStatus>, DefaultStringParser.EnumStringParser<PinPadPrinterStatus>);
           
            this.AddProperty(this.PRT_STATUS);
        }

        /// <summary>
        /// Response Event
        /// </summary>
        public override PrtAction PRT_ACTION {
            get {
                return PrtAction.Begin;
            }
        }

        /// <summary>
        /// Adds to the vertical offset of the next print buffer append
        /// </summary>
        public PinPadFixedLengthPropertyController<PinPadPrinterStatus> PRT_STATUS { get; private set; }
    }
}
