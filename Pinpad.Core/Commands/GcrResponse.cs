using Pinpad.Core.Properties;
using Pinpad.Core.Tracks;
using Pinpad.Core.TypeCode;
using System;

/* WARNING!
 * 
 * DEPRECATED.
 * MUST BE REFACTORED.
 * 
 */

namespace Pinpad.Core.Commands 
{
	/// <summary>
	/// GCR response
	/// </summary>
	public class GcrResponse : BaseResponse 
	{
		/// <summary>
		/// Constructor
		/// </summary>
		public GcrResponse() 
		{
			this.RSP_LEN1 = new RegionProperty("RSP_LEN1", 3);
			this.GCR_CARDTYPE = new PinpadFixedLengthProperty<ApplicationType>("GCR_CARDTYPE", 2, false, DefaultStringFormatter.EnumStringFormatter<ApplicationType>, DefaultStringParser.EnumStringParser<ApplicationType>);
			this.GCR_STATCHIP = new PinpadFixedLengthProperty<FallbackStatus>("GCR_STATCHIP", 1, false, DefaultStringFormatter.EnumStringFormatter<FallbackStatus>, DefaultStringParser.EnumStringParser<FallbackStatus>);
			this.GCR_APPTYPE = new PinpadFixedLengthProperty<int?>("GCR_APPTYPE", 2, false, DefaultStringFormatter.IntegerStringFormatter, DefaultStringParser.IntegerStringParser);
			this.GCR_ACQIDX = new PinpadFixedLengthProperty<int?>("GCR_ACQIDX", 2, false, DefaultStringFormatter.IntegerStringFormatter, DefaultStringParser.IntegerStringParser);
			this.GCR_RECIDX = new PinpadFixedLengthProperty<int?>("GCR_RECIDX", 2, false, DefaultStringFormatter.IntegerStringFormatter, DefaultStringParser.IntegerStringParser);
			this.GCR_TRK1 = new VariableLengthProperty<Track1>("GCR_TRK1", 2, 76, 1.0f, true, true, DefaultStringFormatter.PropertyControllerStringFormatter, DefaultStringParser.PropertyControllerStringParser<Track1>, String.Empty.PadLeft(76));
			this.GCR_TRK2 = new VariableLengthProperty<Track2>("GCR_TRK2", 2, 37, 1.0f, true, true, DefaultStringFormatter.PropertyControllerStringFormatter, DefaultStringParser.PropertyControllerStringParser<Track2>, String.Empty.PadLeft(37));
			this.GCR_TRK3 = new VariableLengthProperty<string>("GCR_TRK3", 3, 104, 1.0f, true, true, DefaultStringFormatter.StringStringFormatter, DefaultStringParser.StringStringParser, String.Empty.PadLeft(104));
			this.GCR_PAN = new VariableLengthProperty<string>("GCR_PAN", 2, 19, 1.0f, true, true, DefaultStringFormatter.StringStringFormatter, DefaultStringParser.StringStringParser,"");
			this.GCR_PANSEQNO = new PinpadFixedLengthProperty<int?>("GCR_PANSEQNO", 2, false, DefaultStringFormatter.IntegerStringFormatter, DefaultStringParser.IntegerStringParser);
			this.GCR_APPLABEL = new PinpadFixedLengthProperty<string>("GCR_APPLABEL", 16, false, DefaultStringFormatter.LeftPaddingWithSpacesStringFormatter, DefaultStringParser.StringStringParser);
			this.GCR_SRVCODE = new SimpleProperty<ServiceCode>("GCR_SRVCODE", false, ServiceCode.StringFormatter, ServiceCode.StringParser, String.Empty.PadLeft(3));
			this.GCR_CHNAME = new PinpadFixedLengthProperty<string>("GCR_CHNAME", 26, false, DefaultStringFormatter.LeftPaddingWithSpacesStringFormatter, DefaultStringParser.StringStringParser);
			this.GCR_CARDEXP = new SimpleProperty<Nullable<DateTime>>("GCR_CARDEXP", true, DefaultStringFormatter.DateStringFormatter, DefaultStringParser.DateStringParser, "000000");
			this.GCR_RUF1 = new PinpadFixedLengthProperty<string>("GCR_RUF1", 29, false, DefaultStringFormatter.LeftPaddingWithSpacesStringFormatter, DefaultStringParser.StringStringParser, null, String.Empty.PadLeft(29));
			this.GCR_ISSCNTRY = new PinpadFixedLengthProperty<int?>("GCR_ISSCNTRY", 3, false, DefaultStringFormatter.IntegerStringFormatter, DefaultStringParser.IntegerStringParser);
			this.GCR_ACQRD = new VariableLengthProperty<string>("GCR_ACQRD", 3, 66, 1.0f, false, true, DefaultStringFormatter.StringStringFormatter, DefaultStringParser.StringStringParser);

			this.StartRegion(this.RSP_LEN1);
			{
				this.AddProperty(this.GCR_CARDTYPE);
				this.AddProperty(this.GCR_STATCHIP);
				this.AddProperty(this.GCR_APPTYPE);
				this.AddProperty(this.GCR_ACQIDX);
				this.AddProperty(this.GCR_RECIDX);
				this.AddProperty(this.GCR_TRK1);
				this.AddProperty(this.GCR_TRK2);
				this.AddProperty(this.GCR_TRK3);
				this.AddProperty(this.GCR_PAN);
				this.AddProperty(this.GCR_PANSEQNO);
				this.AddProperty(this.GCR_APPLABEL);
				this.AddProperty(this.GCR_SRVCODE);
				this.AddProperty(this.GCR_CHNAME);
				this.AddProperty(this.GCR_CARDEXP);
				this.AddProperty(this.GCR_RUF1);
				this.AddProperty(this.GCR_ISSCNTRY);
				this.AddProperty(this.GCR_ACQRD);
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
		public override string CommandName { get { return "GCR"; } }

		/// <summary>
		/// Length of the first region of the command
		/// </summary>
		public RegionProperty RSP_LEN1 { get; private set; }

		/// <summary>
		/// Card input method
		/// This does not mean the card does not support other input methods
		/// </summary>
		public PinpadFixedLengthProperty<ApplicationType> GCR_CARDTYPE { get; private set; }

		/// <summary>
		/// Status of the last ICC read
		/// This data is used when the card was inputted as Magnetic Stripe and it indicates presence of EMV chip
		/// </summary>
		public PinpadFixedLengthProperty<FallbackStatus> GCR_STATCHIP { get; private set; }

		/// <summary>
		/// AID table selected registry's Application Type
		/// </summary>
		public PinpadFixedLengthProperty<Nullable<int>> GCR_APPTYPE{ get; private set; }

		/// <summary>
		/// AID table selected registry's Acquirer Index
		/// </summary>
		public PinpadFixedLengthProperty<Nullable<int>> GCR_ACQIDX { get; private set; }

		/// <summary>
		/// AID table selected registry's Record Index
		/// </summary>
		public PinpadFixedLengthProperty<Nullable<int>> GCR_RECIDX{ get; private set; }

		/// <summary>
		/// Card Track 1
		/// </summary>
		public VariableLengthProperty<Track1> GCR_TRK1 { get; private set; }

		/// <summary>
		/// Card Track 2
		/// </summary>
		public VariableLengthProperty<Track2> GCR_TRK2 { get; private set; }

		/// <summary>
		/// Card Track 3
		/// </summary>
		public VariableLengthProperty<string> GCR_TRK3 { get; private set; }

		/// <summary>
		/// Card PAN
		/// Not available for Magnetic Stripe cards, check for Tracks data in those cases
		/// </summary>
		public VariableLengthProperty<string> GCR_PAN { get; private set; }

		/// <summary>
		/// Card Application PAN sequence number
		/// </summary>
		public PinpadFixedLengthProperty<Nullable<int>> GCR_PANSEQNO { get; private set; }

		/// <summary>
		/// Card Application Label, left aligned
		/// </summary>
		public PinpadFixedLengthProperty<string> GCR_APPLABEL { get; private set; }

		/// <summary>
		/// Service Code
		/// </summary>
		public SimpleProperty<ServiceCode> GCR_SRVCODE { get; private set; }

		/// <summary>
		/// Cardholder name
		/// </summary>
		public PinpadFixedLengthProperty<string> GCR_CHNAME { get; private set; }

		/// <summary>
		/// Card Expiration Date
		/// </summary>
		public SimpleProperty<Nullable<DateTime>> GCR_CARDEXP { get; private set; }

		/// <summary>
		/// Reserved for Future Use (Reservado para Uso Futuro)
		/// </summary>
		public PinpadFixedLengthProperty<string> GCR_RUF1 { get; private set; }

		/// <summary>
		/// Card Issuer Country Code
		/// </summary>
		public PinpadFixedLengthProperty<Nullable<int>> GCR_ISSCNTRY { get; private set; }

		/// <summary>
		/// Acquirer Specific Extra Data
		/// </summary>
		public VariableLengthProperty<string> GCR_ACQRD { get; private set; }
	}
}
