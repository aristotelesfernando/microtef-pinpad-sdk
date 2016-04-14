using Pinpad.Sdk.Properties;
using System;

namespace Pinpad.Sdk.Tracks 
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
		/// Constructor
		/// </summary>
		public Track1() 
		{
			this.FormatCode = new SimpleProperty<Nullable<char>>("FormatCode", false, DefaultStringFormatter.CharStringFormatter, DefaultStringParser.CharStringParser);
			this.PAN = new SimpleProperty<string>("PAN", false, this.StringWithSeparatorStringFormatter, this.StringWithSeparatorStringParser);
			this.Name = new SimpleProperty<string>("Name", false, this.StringWithSeparatorStringFormatter, this.StringWithSeparatorStringParser);
			this.ExpirationDate = new SimpleProperty<Nullable<DateTime>>("ExpirationDate", true, this.YearAndMonthStringFormatter, this.YearAndMonthStringParser, FieldSeparator);
			this.ServiceCode = new SimpleProperty<ServiceCode>("ServiceCode", false, Properties.ServiceCode.StringFormatter, Properties.ServiceCode.StringParser, FieldSeparator);
			this.DiscretionaryData = new SimpleProperty<string>("DiscretionaryData", true, DefaultStringFormatter.StringStringFormatter, DefaultStringParser.StringStringParser);

			this.AddProperty(this.FormatCode);
			this.AddProperty(this.PAN);
			this.AddProperty(this.Name);
			this.AddProperty(this.ExpirationDate);
			this.AddProperty(this.ServiceCode);
			this.AddProperty(this.DiscretionaryData);
		}

		/// <summary>
		/// Format Code
		/// </summary>
		public SimpleProperty<Nullable<char>> FormatCode { get; private set; }

		/// <summary>
		/// Primary Account Number
		/// </summary>
		public SimpleProperty<string> PAN { get; private set; }

		/// <summary>
		/// Cardholder name
		/// </summary>
		public SimpleProperty<string> Name { get; private set; }

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
