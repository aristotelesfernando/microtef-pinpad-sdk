using Pinpad.Sdk.PinpadProperties.Refactor;
using Pinpad.Sdk.PinpadProperties.Refactor.Formatter;
using Pinpad.Sdk.PinpadProperties.Refactor.Parser;
using Pinpad.Sdk.PinpadProperties.Refactor.Property;
using System;

namespace Pinpad.Sdk.Commands 
{
	/// <summary>
	/// GOC request
	/// </summary>
	internal sealed class GocRequest : PinpadProperties.Refactor.BaseCommand 
	{
        /// <summary>
		/// Name of the command
		/// </summary>
		public override string CommandName { get { return "GOC"; } }
        /// <summary>
        /// Length of the first region of the command
        /// </summary>
        public RegionProperty CMD_LEN1 { get; private set; }
        /// <summary>
        /// Amount of the transaction
        /// </summary>
        public FixedLengthProperty<Nullable<long>> GOC_AMOUNT { get; private set; }
        /// <summary>
        /// Amount of cashback of the transaction
        /// </summary>
        public FixedLengthProperty<Nullable<long>> GOC_CASHBACK { get; private set; }
        /// <summary>
        /// Is the PAN in the Black List?
        /// </summary>
        public TextProperty<Nullable<bool>> GOC_EXCLIST { get; private set; }
        /// <summary>
        /// Should force online connection?
        /// </summary>
        public TextProperty<Nullable<bool>> GOC_CONNECT { get; private set; }
        /// <summary>
        /// Amount of cashback of the transaction
        /// </summary>
        public FixedLengthProperty<Nullable<int>> GOC_RUF1 { get; private set; }
        /// <summary>
        /// Online pin cryptography method
        /// </summary>
        public TextProperty<CryptographyMethod> GOC_METHOD { get; private set; }
        /// <summary>
        /// Online pin cryptography method key index
        /// </summary>
        public FixedLengthProperty<Nullable<int>> GOC_KEYIDX { get; private set; }
        /// <summary>
        /// Encrypted Working Key with the specified Master Key Index
        /// Hexadecimal
        /// Ignored by the PinPad if using DUKPT as key management method
        /// </summary>
        public FixedLengthProperty<HexadecimalData> GOC_WKENC { get; private set; }
        /// <summary>
        /// Should EMV perform risk management?
        /// </summary>
        public TextProperty<Nullable<bool>> GOC_RISKMAN { get; private set; }
        /// <summary>
        /// Terminal Floor Limit in cents
        /// Hexadecimal
        /// </summary>
        public FixedLengthProperty<HexadecimalData> GOC_FLRLIMIT { get; private set; }
        /// <summary>
        /// Target Percentage to be used for Biased Random Selection
        /// </summary>
        public FixedLengthProperty<Nullable<int>> GOC_TPBRS { get; private set; }
        /// <summary>
        /// Threshold Value for Biased Random Selection in cents
        /// Hexadecimal
        /// </summary>
        public FixedLengthProperty<HexadecimalData> GOC_TVBRS { get; private set; }
        /// <summary>
        /// Maximum Target Percentage to be used for Biased Random Selection
        /// </summary>
        public FixedLengthProperty<Nullable<int>> GOC_MTPBRS { get; private set; }
        /// <summary>
        /// Acquirer specific parameters
        /// </summary>
        public VariableLengthProperty<string> GOC_ACQPR { get; private set; }
        /// <summary>
        /// Length of the second region of the command
        /// </summary>
        public RegionProperty CMD_LEN2 { get; private set; }
        /// <summary>
        /// First EMV tags list identifying EMV data to be returned at GOC_EMVDAT
        /// </summary>
        public VariableLengthProperty<HexadecimalData> GOC_TAGS1 { get; private set; }
        /// <summary>
        /// Length of the third region of the command
        /// </summary>
        public RegionProperty CMD_LEN3 { get; private set; }
        /// <summary>
        /// Second EMV tags list identifying EMV data to be returned at GOC_EMVDAT
        /// Adds to GOC_TAGS1 list
        /// </summary>
        public VariableLengthProperty<HexadecimalData> GOC_TAGS2 { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public GocRequest() 
		{
			this.CMD_LEN1 = new RegionProperty("CMD_LEN1", 3);
			this.GOC_AMOUNT = new FixedLengthProperty<long?>("GOC_AMOUNT", 12, false, 
                StringFormatter.LongIntegerStringFormatter, StringParser.LongIntegerStringParser);
			this.GOC_CASHBACK = new FixedLengthProperty<long?>("GOC_CASHBACK", 12, false, 
                StringFormatter.LongIntegerStringFormatter, StringParser.LongIntegerStringParser);
			this.GOC_EXCLIST = new TextProperty<bool?>("GOC_EXCLIST", false, 
                StringFormatter.BooleanStringFormatter, StringParser.BooleanStringParser);
			this.GOC_CONNECT = new TextProperty<bool?>("GOC_CONNECT", false, 
                StringFormatter.BooleanStringFormatter, StringParser.BooleanStringParser);
			this.GOC_RUF1 = new FixedLengthProperty<int?>("GOC_RUF1", 1, false, 
                StringFormatter.IntegerStringFormatter, StringParser.IntegerStringParser, null, 0);
			this.GOC_METHOD = new TextProperty<CryptographyMethod>("GOC_METHOD", false, 
                CryptographyMethod.StringFormatter, CryptographyMethod.CustomStringParser);
			this.GOC_KEYIDX = new FixedLengthProperty<int?>("GOC_KEYIDX", 2, false, 
                StringFormatter.IntegerStringFormatter, StringParser.IntegerStringParser);
			this.GOC_WKENC = new FixedLengthProperty<HexadecimalData>("GOC_WKENC", 32, false, 
                StringFormatter.HexadecimalStringFormatter, StringParser.HexadecimalStringParser);
			this.GOC_RISKMAN = new TextProperty<bool?>("GOC_RISKMAN", false, 
                StringFormatter.BooleanStringFormatter, StringParser.BooleanStringParser);
			this.GOC_FLRLIMIT = new FixedLengthProperty<HexadecimalData>("GOC_FLRLIMIT", 8, false, 
                StringFormatter.HexadecimalStringFormatter, StringParser.HexadecimalStringParser);
			this.GOC_TPBRS = new FixedLengthProperty<int?>("GOC_TPBRS", 2, false, 
                StringFormatter.IntegerStringFormatter, StringParser.IntegerStringParser);
			this.GOC_TVBRS = new FixedLengthProperty<HexadecimalData>("GOC_TVBRS", 8, false, 
                StringFormatter.HexadecimalStringFormatter, StringParser.HexadecimalStringParser);
			this.GOC_MTPBRS = new FixedLengthProperty<int?>("GOC_MTPBRS", 2, false, 
                StringFormatter.IntegerStringFormatter, StringParser.IntegerStringParser);
			this.GOC_ACQPR = new VariableLengthProperty<string>("GOC_ACQPR", 3, 32, 1.0f, false, true, 
                StringFormatter.StringStringFormatter, StringParser.StringStringParser);
			this.CMD_LEN2 = new RegionProperty("CMD_LEN2", 3);
			this.GOC_TAGS1 = new VariableLengthProperty<HexadecimalData>("GOC_TAGS1", 3, 256, 1.0f / 2, false, 
                false, StringFormatter.HexadecimalStringFormatter, StringParser.HexadecimalStringParser);
			this.CMD_LEN3 = new RegionProperty("CMD_LEN3", 3);
			this.GOC_TAGS2 = new VariableLengthProperty<HexadecimalData>("GOC_TAGS2", 3, 256, 1.0f / 2, false, 
                false, StringFormatter.HexadecimalStringFormatter, StringParser.HexadecimalStringParser);

			this.StartRegion(this.CMD_LEN1);
			{
				this.AddProperty(this.GOC_AMOUNT);
				this.AddProperty(this.GOC_CASHBACK);
				this.AddProperty(this.GOC_EXCLIST);
				this.AddProperty(this.GOC_CONNECT);
				this.AddProperty(this.GOC_RUF1);
				this.AddProperty(this.GOC_METHOD);
				this.AddProperty(this.GOC_KEYIDX);
				this.AddProperty(this.GOC_WKENC);
				this.AddProperty(this.GOC_RISKMAN);
				this.AddProperty(this.GOC_FLRLIMIT);
				this.AddProperty(this.GOC_TPBRS);
				this.AddProperty(this.GOC_TVBRS);
				this.AddProperty(this.GOC_MTPBRS);
				this.AddProperty(this.GOC_ACQPR);
			}
			this.EndLastRegion();
			this.StartRegion(this.CMD_LEN2);
			{
				this.AddProperty(this.GOC_TAGS1);
			}
			this.EndLastRegion();
			this.StartRegion(this.CMD_LEN3);
			{
				this.AddProperty(this.GOC_TAGS2);
			}
			this.EndLastRegion();
		}
	}
}
