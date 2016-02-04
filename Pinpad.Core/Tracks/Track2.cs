using PinPadSDK.Property;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace PinPadSDK.Controllers.Tracks {
    /// <summary>
    /// Controller for Track2 data
    /// </summary>
    public class Track2 : BaseTrack {
        /// <summary>
        /// Separator used in the track data
        /// </summary>
        protected override string FieldSeparator { get { return "="; } }

        /// <summary>
        /// Constructor
        /// </summary>
        public Track2() {
            this.PAN = new SimpleProperty<string>("PAN", false, this.StringWithSeparatorStringFormatter, this.StringWithSeparatorStringParser);
            this.ExpirationDate = new SimpleProperty<Nullable<DateTime>>("ExpirationDate", true, this.YearAndMonthStringFormatter, this.YearAndMonthStringParser, FieldSeparator);
            this.ServiceCode = new SimpleProperty<ServiceCode>("ServiceCode", false, Property.ServiceCode.StringFormatter, Property.ServiceCode.StringParser, FieldSeparator);
            this.DiscretionaryData = new SimpleProperty<string>("DiscretionaryData", true, DefaultStringFormatter.StringStringFormatter, DefaultStringParser.StringStringParser);

            this.AddProperty(this.PAN);
            this.AddProperty(this.ExpirationDate);
            this.AddProperty(this.ServiceCode);
            this.AddProperty(this.DiscretionaryData);
        }

        /// <summary>
        /// Primary Account Number
        /// </summary>
        public SimpleProperty<string> PAN { get; private set; }

        /// <summary>
        /// Card Expiration Date
        /// </summary>
        public SimpleProperty<Nullable<DateTime>> ExpirationDate { get; private set; }

        /// <summary>
        /// Card Service Code
        /// </summary>
        public SimpleProperty<ServiceCode> ServiceCode { get; private set; }

        /// <summary>
        /// Discretionary Data
        /// </summary>
        public SimpleProperty<string> DiscretionaryData { get; private set; }
    }
}
