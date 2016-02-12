using PinPadSDK.Property;
using PinPadSDK.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PinPadSDK.Commands.Stone.PrtActionData {
    /// <summary>
    /// Controller for PRT append image request action
    /// </summary>
    public class PrtAppendImageRequestData : BasePrtRequestData {
        /// <summary>
        /// Constructor
        /// </summary>
        public PrtAppendImageRequestData() {
            this.PRT_PADDING = new PinPadFixedLengthPropertyController<int?>("PRT_PADDING", 4, false, DefaultStringFormatter.IntegerStringFormatter, DefaultStringParser.IntegerStringParser);
            this.PRT_FILENAME = new VariableLengthProperty<string>("PRT_FILENAME", 3, 15, 1.0f, false, false, DefaultStringFormatter.StringStringFormatter, DefaultStringParser.StringStringParser);

            this.AddProperty(this.PRT_PADDING);
            this.AddProperty(this.PRT_FILENAME);
        }

        /// <summary>
        /// Response Event
        /// </summary>
        public override PrtAction PRT_ACTION {
            get {
                return PrtAction.AppendImage;
            }
        }

        /// <summary>
        /// Horizontal padding of the image
        /// </summary>
        public PinPadFixedLengthPropertyController<Nullable<int>> PRT_PADDING { get; private set; }

        /// <summary>
        /// Name of the image file to append
        /// </summary>
        public VariableLengthProperty<string> PRT_FILENAME { get; private set; }
    }
}
