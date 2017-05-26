using Pinpad.Sdk.PinpadProperties.Refactor;
using Pinpad.Sdk.PinpadProperties.Refactor.Formatter;
using Pinpad.Sdk.PinpadProperties.Refactor.Parser;
using Pinpad.Sdk.PinpadProperties.Refactor.Property;
using System;

namespace Pinpad.Sdk.Commands 
{
	/// <summary>
	/// GCR response
	/// </summary>
	internal class GcrResponse : BaseResponse 
	{
        /// <summary>
		/// Is this a blocking command?
		/// </summary>
		public override bool IsBlockingCommand
        {
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
        public FixedLengthProperty<ApplicationType> GCR_CARDTYPE { get; private set; }
        /// <summary>
        /// Status of the last ICC read
        /// This data is used when the card was inputted as Magnetic Stripe and it indicates presence of EMV chip
        /// </summary>
        public FixedLengthProperty<FallbackStatus> GCR_STATCHIP { get; private set; }
        /// <summary>
        /// AID table selected registry's Application Type
        /// </summary>
        public FixedLengthProperty<Nullable<int>> GCR_APPTYPE { get; private set; }
        /// <summary>
        /// AID table selected registry's Acquirer Index
        /// </summary>
        public FixedLengthProperty<Nullable<int>> GCR_ACQIDX { get; private set; }
        /// <summary>
        /// AID table selected registry's Record Index
        /// </summary>
        public FixedLengthProperty<Nullable<int>> GCR_RECIDX { get; private set; }
        /// <summary>
        /// Card Track 1
        /// </summary>
        public VariableLengthProperty<string> GCR_TRK1 { get; private set; }
        /// <summary>
        /// Card Track 2
        /// </summary>
        public VariableLengthProperty<string> GCR_TRK2 { get; private set; }
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
        public FixedLengthProperty<Nullable<int>> GCR_PANSEQNO { get; private set; }
        /// <summary>
        /// Card Application Label, left aligned
        /// </summary>
        public FixedLengthProperty<string> GCR_APPLABEL { get; private set; }
        /// <summary>
        /// Service Code
        /// </summary>
        public TextProperty<ServiceCode> GCR_SRVCODE { get; private set; }
        /// <summary>
        /// Cardholder name
        /// </summary>
        public FixedLengthProperty<string> GCR_CHNAME { get; private set; }
        /// <summary>
        /// Card Expiration Date
        /// </summary>
        public TextProperty<Nullable<DateTime>> GCR_CARDEXP { get; private set; }
        /// <summary>
        /// Reserved for Future Use (Reservado para Uso Futuro)
        /// </summary>
        public FixedLengthProperty<string> GCR_RUF1 { get; private set; }
        /// <summary>
        /// Card Issuer Country Code
        /// </summary>
        public FixedLengthProperty<Nullable<int>> GCR_ISSCNTRY { get; private set; }
        /// <summary>
        /// Acquirer Specific Extra Data
        /// </summary>
        public VariableLengthProperty<string> GCR_ACQRD { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public GcrResponse() 
		{
			this.RSP_LEN1 = new RegionProperty("RSP_LEN1", 3);
			this.GCR_CARDTYPE = new FixedLengthProperty<ApplicationType>("GCR_CARDTYPE", 2, false, 
                StringFormatter.EnumStringFormatter<ApplicationType>, StringParser.EnumStringParser<ApplicationType>);
			this.GCR_STATCHIP = new FixedLengthProperty<FallbackStatus>("GCR_STATCHIP", 1, false, 
                StringFormatter.EnumStringFormatter<FallbackStatus>, StringParser.EnumStringParser<FallbackStatus>);
			this.GCR_APPTYPE = new FixedLengthProperty<int?>("GCR_APPTYPE", 2, false, 
                StringFormatter.IntegerStringFormatter, StringParser.IntegerStringParser);
			this.GCR_ACQIDX = new FixedLengthProperty<int?>("GCR_ACQIDX", 2, false, 
                StringFormatter.IntegerStringFormatter, StringParser.IntegerStringParser);
			this.GCR_RECIDX = new FixedLengthProperty<int?>("GCR_RECIDX", 2, false, 
                StringFormatter.IntegerStringFormatter, StringParser.IntegerStringParser);
			this.GCR_TRK1 = new VariableLengthProperty<string>("GCR_TRK1", 2, 76, 1.0f, true, true, 
                StringFormatter.StringStringFormatter, StringParser.StringStringParser, String.Empty.PadLeft(76));
			this.GCR_TRK2 = new VariableLengthProperty<string>("GCR_TRK2", 2, 37, 1.0f, true, true, 
                StringFormatter.StringStringFormatter, StringParser.StringStringParser, String.Empty.PadLeft(37));
			this.GCR_TRK3 = new VariableLengthProperty<string>("GCR_TRK3", 3, 104, 1.0f, true, true, 
                StringFormatter.StringStringFormatter, StringParser.StringStringParser, String.Empty.PadLeft(104));
			this.GCR_PAN = new VariableLengthProperty<string>("GCR_PAN", 2, 19, 1.0f, true, true, 
                StringFormatter.StringStringFormatter, StringParser.StringStringParser,"");
			this.GCR_PANSEQNO = new FixedLengthProperty<int?>("GCR_PANSEQNO", 2, true, 
                StringFormatter.IntegerStringFormatter, StringParser.IntegerStringParser);
			this.GCR_APPLABEL = new FixedLengthProperty<string>("GCR_APPLABEL", 16, false, 
                StringFormatter.LeftPaddingWithSpacesStringFormatter, StringParser.StringStringParser);
			this.GCR_SRVCODE = new TextProperty<ServiceCode>("GCR_SRVCODE", false, ServiceCode.StringFormatter, 
                ServiceCode.StringParser, String.Empty.PadLeft(3));
			this.GCR_CHNAME = new FixedLengthProperty<string>("GCR_CHNAME", 26, false, 
                StringFormatter.LeftPaddingWithSpacesStringFormatter, StringParser.StringStringParser);
			this.GCR_CARDEXP = new TextProperty<Nullable<DateTime>>("GCR_CARDEXP", true, 
                StringFormatter.DateStringFormatter, StringParser.DateStringParser, "000000");
			this.GCR_RUF1 = new FixedLengthProperty<string>("GCR_RUF1", 29, false, 
                StringFormatter.LeftPaddingWithSpacesStringFormatter, StringParser.StringStringParser, null, String.Empty.PadLeft(29));
			this.GCR_ISSCNTRY = new FixedLengthProperty<int?>("GCR_ISSCNTRY", 3, false, 
                StringFormatter.IntegerStringFormatter, StringParser.IntegerStringParser);
			this.GCR_ACQRD = new VariableLengthProperty<string>("GCR_ACQRD", 3, 66, 1.0f, false, true, 
                StringFormatter.StringStringFormatter, StringParser.StringStringParser);

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
	}
}
