using Pinpad.Sdk.PinpadProperties.Refactor.Formatter;
using Pinpad.Sdk.PinpadProperties.Refactor.Parser;
using Pinpad.Sdk.PinpadProperties.Refactor.Property;
using System;

namespace Pinpad.Sdk.Commands 
{
	/// <summary>
	/// GOC response
	/// </summary>
	internal class GocResponse : BaseResponse 
	{
        /// <summary>
		/// Is this a blocking command?
		/// </summary>
		public override bool IsBlockingCommand { get { return true; } }

        /// <summary>
        /// Name of the command
        /// </summary>
        public override string CommandName { get { return "GOC"; } }

        /// <summary>
        /// Length of the first region of the command
        /// </summary>
        public RegionProperty RSP_LEN1 { get; private set; }

        /// <summary>
        /// Offline transaction status
        /// </summary>
        public FixedLengthProperty<OfflineTransactionStatus> GOC_DECISION { get; private set; }

        /// <summary>
        /// Should Paper signature be obtained?
        /// </summary>
        public TextProperty<Nullable<bool>> GOC_SIGNAT { get; private set; }

        /// <summary>
        /// Was the pin verified offline?
        /// </summary>
        public TextProperty<Nullable<bool>> GOC_PINOFF { get; private set; }

        /// <summary>
        /// Amount of invalid offline pin presentations this transaction
        /// </summary>
        public FixedLengthProperty<Nullable<int>> GOC_ERRPINOFF { get; private set; }

        /// <summary>
        /// Was the offline pin blocked at the last invalid presentation in this transaction?
        /// </summary>
        public TextProperty<Nullable<bool>> GOC_PBLOCKED { get; private set; }

        /// <summary>
        /// Was the Pin captured for online verification?
        /// If false GOC_PINBLK and GOC_KSN should be ignored.
        /// </summary>
        public TextProperty<Nullable<bool>> GOC_PINONL { get; private set; }

        /// <summary>
        /// Encrypted Pin Block
        /// </summary>
        public FixedLengthProperty<HexadecimalData> GOC_PINBLK { get; private set; }

        /// <summary>
        /// Key Serial Number of the Encrypted Pin, only if DUKPT was used as Key Management Method, otherwise this field is filled with zeros
        /// </summary>
        public FixedLengthProperty<HexadecimalData> GOC_KSN { get; private set; }

        /// <summary>
        /// EMV data as Tag/Length/Value as requested from GOC_TAGS1 and GOC_TAGS2 if found and respecting requested order
        /// </summary>
        public VariableLengthProperty<HexadecimalData> GOC_EMVDAT { get; private set; }

        /// <summary>
        /// Acquirer specific parameters, always empty
        /// </summary>
        public VariableLengthProperty<string> GOC_ACQRD { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public GocResponse() 
		{
			this.RSP_LEN1 = new RegionProperty("RSP_LEN1", 3);
			this.GOC_DECISION = new FixedLengthProperty<OfflineTransactionStatus>("GOC_DECISION", 1, false, 
                StringFormatter.EnumStringFormatter<OfflineTransactionStatus>, 
                StringParser.EnumStringParser<OfflineTransactionStatus>);
			this.GOC_SIGNAT = new TextProperty<bool?>("GOC_SIGNAT", false, 
                StringFormatter.BooleanStringFormatter, StringParser.BooleanStringParser);
			this.GOC_PINOFF = new TextProperty<bool?>("GOC_PINOFF", false, 
                StringFormatter.BooleanStringFormatter, StringParser.BooleanStringParser);
			this.GOC_ERRPINOFF = new FixedLengthProperty<int?>("GOC_ERRPINOFF", 1, false, 
                StringFormatter.IntegerStringFormatter, StringParser.IntegerStringParser);
			this.GOC_PBLOCKED = new TextProperty<bool?>("GOC_PBLOCKED", false, 
                StringFormatter.BooleanStringFormatter, StringParser.BooleanStringParser);
			this.GOC_PINONL = new TextProperty<bool?>("GOC_PINONL", false, 
                StringFormatter.BooleanStringFormatter, StringParser.BooleanStringParser);
			this.GOC_PINBLK = new FixedLengthProperty<HexadecimalData>("GOC_PINBLK", 16, false, 
                StringFormatter.HexadecimalStringFormatter, StringParser.HexadecimalStringParser);
			this.GOC_KSN = new FixedLengthProperty<HexadecimalData>("GOC_KSN", 20, false, 
                StringFormatter.HexadecimalStringFormatter, StringParser.HexadecimalStringParser);
			this.GOC_EMVDAT = new VariableLengthProperty<HexadecimalData>("GOC_EMVDAT", 3, 512, 1.0f / 2, false, 
                false, StringFormatter.HexadecimalStringFormatter, StringParser.HexadecimalStringParser);
			this.GOC_ACQRD = new VariableLengthProperty<string>("GOC_ACQRD", 3, 0, 1.0f, false, true, 
                StringFormatter.StringStringFormatter, StringParser.StringStringParser, String.Empty);

			this.StartRegion(this.RSP_LEN1);
			{
				this.AddProperty(this.GOC_DECISION);
				this.AddProperty(this.GOC_SIGNAT);
				this.AddProperty(this.GOC_PINOFF);
				this.AddProperty(this.GOC_ERRPINOFF);
				this.AddProperty(this.GOC_PBLOCKED);
				this.AddProperty(this.GOC_PINONL);
				this.AddProperty(this.GOC_PINBLK);
				this.AddProperty(this.GOC_KSN);
				this.AddProperty(this.GOC_EMVDAT);
				this.AddProperty(this.GOC_ACQRD);
			}
			this.EndLastRegion();
		}
	}
}
