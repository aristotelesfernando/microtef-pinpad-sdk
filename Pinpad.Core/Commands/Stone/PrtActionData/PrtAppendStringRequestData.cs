using PinPadSDK.Property;
using PinPadSDK.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PinPadSDK.Commands.Stone.PrtActionData {
    /// <summary>
    /// Controller for PRT append string request action
    /// </summary>
    public class PrtAppendStringRequestData : BasePrtRequestData {
        /// <summary>
        /// Constructor
        /// </summary>
        public PrtAppendStringRequestData() {
            this.PRT_SIZE = new PinPadFixedLengthPropertyController<PrtStringSize>("PRT_SIZE", 1, false, DefaultStringFormatter.EnumStringFormatter<PrtStringSize>, DefaultStringParser.EnumStringParser<PrtStringSize>);
            this.PRT_ALIGNMENT = new PinPadFixedLengthPropertyController<PrtAlignment>("PRT_ALIGNMENT", 1, false, DefaultStringFormatter.EnumStringFormatter<PrtAlignment>, DefaultStringParser.EnumStringParser<PrtAlignment>);
            this.PRT_MSG = new VariableLengthProperty<string>("PRT_MSG", 3, 512, 1.0f, false, false, DefaultStringFormatter.StringStringFormatter, DefaultStringParser.StringStringParser);

            this.AddProperty(this.PRT_SIZE);
            this.AddProperty(this.PRT_ALIGNMENT);
            this.AddProperty(this.PRT_MSG);
        }

        /// <summary>
        /// Response Event
        /// </summary>
        public override PrtAction PRT_ACTION {
            get {
                return PrtAction.AppendString;
            }
        }

        /// <summary>
        /// Size of the font to use
        /// </summary>
        public PinPadFixedLengthPropertyController<PrtStringSize> PRT_SIZE { get; private set; }

        /// <summary>
        /// Message alignment
        /// </summary>
        public PinPadFixedLengthPropertyController<PrtAlignment> PRT_ALIGNMENT { get; private set; }

        /// <summary>
        /// Message to append
        /// </summary>
        public VariableLengthProperty<string> PRT_MSG { get; private set; }
    }
}
