using PinPadSDK.Property;
using PinPadSDK.Controllers.Tracks;
using PinPadSDK.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PinPadSDK.Commands.CkeEventsData {
    /// <summary>
    /// CKE Magnetic Stripe card passed event response
    /// Happens when a Magnetic Stripe card is passed when specified by the CKE request
    /// </summary>
    public class CkeMagneticStripeResponseData : BaseCkeResponseData {
        /// <summary>
        /// Constructor
        /// </summary>
        public CkeMagneticStripeResponseData() {
            this.CKE_TRK1 = new VariableLengthProperty<Track1>("CKE_TRK1", 2, 76, 1.0f, true, true, DefaultStringFormatter.PropertyControllerStringFormatter, DefaultStringParser.PropertyControllerStringParser<Track1>, String.Empty);
            this.CKE_TRK2 = new VariableLengthProperty<Track2>("CKE_TRK2", 2, 37, 1.0f, true, true, DefaultStringFormatter.PropertyControllerStringFormatter, DefaultStringParser.PropertyControllerStringParser<Track2>, String.Empty);
            this.CKE_TRK3 = new VariableLengthProperty<string>("CKE_TRK3", 3, 104, 1.0f, true, true, DefaultStringFormatter.StringStringFormatter, DefaultStringParser.StringStringParser);

            this.AddProperty(this.CKE_TRK1);
            this.AddProperty(this.CKE_TRK2);
            this.AddProperty(this.CKE_TRK3);
        }

        /// <summary>
        /// Response Event
        /// </summary>
        public override CkeEvent CKE_EVENT {
            get { return CkeEvent.MagneticStripeCardPassed; }
        }

        /// <summary>
        /// Card Track 1
        /// </summary>
        public VariableLengthProperty<Track1> CKE_TRK1 { get; private set; }

        /// <summary>
        /// Card Track 2
        /// </summary>
        public VariableLengthProperty<Track2> CKE_TRK2 { get; private set; }

        /// <summary>
        /// Card Track 3
        /// </summary>
        public VariableLengthProperty<string> CKE_TRK3 { get; private set; }
    }
}
