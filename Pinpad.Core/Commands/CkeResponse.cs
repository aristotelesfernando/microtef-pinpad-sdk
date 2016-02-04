using PinPadSDK.Commands.CkeEventsData;
using PinPadSDK.Property;
using PinPadSDK.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PinPadSDK.Commands {
    /// <summary>
    /// CKE response
    /// </summary>
    public class CkeResponse : BaseResponse {
        /// <summary>
        /// Constructor
        /// </summary>
        public CkeResponse() {
            this.RSP_LEN1 = new RegionProperty("RSP_LEN1", 3);
            this.CKE_EVENTDATA = new SimpleProperty<BaseCkeResponseData>("CKE_EVENTDATA", false, DefaultStringFormatter.PropertyControllerStringFormatter, CkeResponse.EventDataStringParser);

            this.StartRegion(this.RSP_LEN1);
            {
                this.AddProperty(this.CKE_EVENTDATA);
            }
            this.EndLastRegion();
        }

        /// <summary>
        /// Is this a blocking command?
        /// </summary>
        public override bool IsBlockingCommand {
            get { return true; }
        }

        /// <summary>
        /// Name of the command
        /// </summary>
        public override string CommandName { get { return "CKE"; } }

        /// <summary>
        /// Length of the first region of the command
        /// </summary>
        public RegionProperty RSP_LEN1 { get; private set; }

        /// <summary>
        /// Response Event
        /// </summary>
        public CkeEvent CKE_EVENT {
            get {
                if (CKE_EVENTDATA.HasValue == true) {
                    return CKE_EVENTDATA.Value.CKE_EVENT;
                }
                else {
                    return CkeEvent.Undefined;
                }
            }
        }

        /// <summary>
        /// Response Event Data
        /// </summary>
        public SimpleProperty<BaseCkeResponseData> CKE_EVENTDATA { get; private set; }

        /// <summary>
        /// Gets or Sets the event data as MagneticStripe
        /// </summary>
        public CkeMagneticStripeResponseData MagneticStripeData {
            get {
                return CKE_EVENTDATA.Value as CkeMagneticStripeResponseData;
            }
            set {
                CKE_EVENTDATA.Value = value;
            }
        }

        /// <summary>
        /// Gets or Sets the event data as Key Press
        /// </summary>
        public CkeKeyPressResponseData KeyPressData {
            get {
                return CKE_EVENTDATA.Value as CkeKeyPressResponseData; }
            set {
                CKE_EVENTDATA.Value = value;
            }
        }

        /// <summary>
        /// Gets or Sets the event data as Icc
        /// </summary>
        public CkeIccResponseData IccData {
            get {
                return CKE_EVENTDATA.Value as CkeIccResponseData;
            }
            set {
                CKE_EVENTDATA.Value = value;
            }
        }

        /// <summary>
        /// Gets or Sets the event data as Contactless
        /// </summary>
        public CkeContactlessResponseData CtlsData {
            get {
                return CKE_EVENTDATA.Value as CkeContactlessResponseData;
            }
            set {
                CKE_EVENTDATA.Value = value;
            }
        }

        /// <summary>
        /// Parses a Cke Event Data
        /// </summary>
        /// <param name="reader">string reader</param>
        /// <returns>PinPadCkeBaseResponseController</returns>
        private static BaseCkeResponseData EventDataStringParser(StringReader reader) {
            string commandString = reader.ReadString(reader.Remaining);

            BaseCkeResponseData value = new BaseCkeResponseData();
            value.CommandString = commandString;
            switch (value.CKE_EVENT) {
                case CkeEvent.KeyPress:
                    value = new CkeKeyPressResponseData();
                    break;

                case CkeEvent.MagneticStripeCardPassed:
                    value = new CkeMagneticStripeResponseData();
                    break;

                case CkeEvent.IccStatusChanged:
                    value = new CkeIccResponseData();
                    break;

                case CkeEvent.CtlsUpdate:
                    value = new CkeContactlessResponseData();
                    break;

                default:
                    throw new InvalidOperationException("Attempt to parse unknown event: " + value.CKE_EVENT);
            }
            value.CommandString = commandString;
            return value;
        }
    }
}
