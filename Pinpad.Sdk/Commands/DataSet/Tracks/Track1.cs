﻿using Pinpad.Sdk.PinpadProperties.Refactor;
using Pinpad.Sdk.PinpadProperties.Refactor.Formatter;
using Pinpad.Sdk.PinpadProperties.Refactor.Parser;
using Pinpad.Sdk.PinpadProperties.Refactor.Property;
using System;

namespace Pinpad.Sdk.Commands 
{
	/// <summary>
	/// Controller for Track1 data
	/// </summary>
	public class Track1 : BaseTrack 
	{
		/// <summary>
		/// Separator used in the track data
		/// </summary>
		protected override string FieldSeparator { get { return "^"; } }
        /// <summary>
		/// Format Code
		/// </summary>
		public TextProperty<Nullable<char>> FormatCode { get; private set; }
        /// <summary>
        /// Primary Account Number
        /// </summary>
        public TextProperty<string> PAN { get; private set; }
        /// <summary>
        /// Cardholder name
        /// </summary>
        public TextProperty<string> Name { get; private set; }
        /// <summary>
        /// Card Expiration Date
        /// </summary>
        public TextProperty<Nullable<DateTime>> ExpirationDate { get; private set; }
        /// <summary>
        /// Card Service Code
        /// </summary>
        public TextProperty<ServiceCode> ServiceCode { get; private set; }
        /// <summary>
        /// Discretionary Data
        /// </summary>
        public TextProperty<string> DiscretionaryData { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public Track1() 
		{
			this.FormatCode = new TextProperty<Nullable<char>>("FormatCode", false, 
                StringFormatter.CharStringFormatter, StringParser.CharStringParser);
			this.PAN = new TextProperty<string>("PAN", false, this.StringWithSeparatorStringFormatter, 
                this.StringWithSeparatorStringParser);
			this.Name = new TextProperty<string>("Name", false, this.StringWithSeparatorStringFormatter, 
                this.StringWithSeparatorStringParser);
			this.ExpirationDate = new TextProperty<Nullable<DateTime>>("ExpirationDate", true, 
                this.YearAndMonthStringFormatter, this.YearAndMonthStringParser, FieldSeparator);
			this.ServiceCode = new TextProperty<ServiceCode>("ServiceCode", false, 
                PinpadProperties.Refactor.ServiceCode.StringFormatter, 
                PinpadProperties.Refactor.ServiceCode.StringParser, FieldSeparator);
			this.DiscretionaryData = new TextProperty<string>("DiscretionaryData", true, 
                StringFormatter.StringStringFormatter, StringParser.StringStringParser);

			this.AddProperty(this.FormatCode);
			this.AddProperty(this.PAN);
			this.AddProperty(this.Name);
			this.AddProperty(this.ExpirationDate);
			this.AddProperty(this.ServiceCode);
			this.AddProperty(this.DiscretionaryData);
		}
	}
}
