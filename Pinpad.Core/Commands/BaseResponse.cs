using PinPadSDK.Property;
using PinPadSDK.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PinPadSDK.Commands {
    /// <summary>
    /// PinPad response command
    /// </summary>
    public abstract class BaseResponse : BaseCommand {
        /// <summary>
        /// Constructor
        /// </summary>
        public BaseResponse( ) {
            this.RSP_STAT = new PinPadFixedLengthPropertyController<ResponseStatus>("RSP_STAT", 3, false, DefaultStringFormatter.EnumStringFormatter<ResponseStatus>, DefaultStringParser.EnumStringParser<ResponseStatus>);

            this.AddProperty(this.RSP_STAT);
        }

        /// <summary>
        /// Is this a blocking command?
        /// Blocking commands depend on external factors and therefore are not garanteed to respond within 10 seconds.
        /// </summary>
        public abstract bool IsBlockingCommand { get; }

        /// <summary>
        /// Command response code
        /// </summary>
        public PinPadFixedLengthPropertyController<ResponseStatus> RSP_STAT { get; private set; }

        /// <summary>
        /// Does the property makes the other properties be ignored?
        /// </summary>
        /// <param name="property">Property</param>
        /// <returns>boolean</returns>
        protected override bool IsPropertyFinal(IProperty property) {
            //If the RSP_STAT is not ST_OK, there is no response data
            if (property == this.RSP_STAT) {
                if (this.RSP_STAT.Value != ResponseStatus.ST_OK) {
                    return true;
                }
                else {
                    return false;
                }
            }
            return base.IsPropertyFinal(property);
        }

    }
}
