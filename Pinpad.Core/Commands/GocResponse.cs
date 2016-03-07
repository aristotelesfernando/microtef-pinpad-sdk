using Pinpad.Core.Properties;
using Pinpad.Core.TypeCode;
using Pinpad.Core.Utilities;
using System;

namespace Pinpad.Core.Commands 
{
	/// <summary>
	/// GOC response
	/// </summary>
	public class GocResponse : BaseResponse 
	{
		/// <summary>
		/// Constructor
		/// </summary>
		public GocResponse() 
		{
			this.RSP_LEN1 = new RegionProperty("RSP_LEN1", 3);
			this.GOC_DECISION = new PinpadFixedLengthProperty<OfflineTransactionStatus>("GOC_DECISION", 1, false, DefaultStringFormatter.EnumStringFormatter<OfflineTransactionStatus>, DefaultStringParser.EnumStringParser<OfflineTransactionStatus>);
			this.GOC_SIGNAT = new SimpleProperty<bool?>("GOC_SIGNAT", false, DefaultStringFormatter.BooleanStringFormatter, DefaultStringParser.BooleanStringParser);
			this.GOC_PINOFF = new SimpleProperty<bool?>("GOC_PINOFF", false, DefaultStringFormatter.BooleanStringFormatter, DefaultStringParser.BooleanStringParser);
			this.GOC_ERRPINOFF = new PinpadFixedLengthProperty<int?>("GOC_ERRPINOFF", 1, false, DefaultStringFormatter.IntegerStringFormatter, DefaultStringParser.IntegerStringParser);
			this.GOC_PBLOCKED = new SimpleProperty<bool?>("GOC_PBLOCKED", false, DefaultStringFormatter.BooleanStringFormatter, DefaultStringParser.BooleanStringParser);
			this.GOC_PINONL = new SimpleProperty<bool?>("GOC_PINONL", false, DefaultStringFormatter.BooleanStringFormatter, DefaultStringParser.BooleanStringParser);
			this.GOC_PINBLK = new PinpadFixedLengthProperty<HexadecimalData>("GOC_PINBLK", 16, false, DefaultStringFormatter.HexadecimalStringFormatter, DefaultStringParser.HexadecimalStringParser);
			this.GOC_KSN = new PinpadFixedLengthProperty<HexadecimalData>("GOC_KSN", 20, false, DefaultStringFormatter.HexadecimalStringFormatter, DefaultStringParser.HexadecimalStringParser);
			this.GOC_EMVDAT = new VariableLengthProperty<HexadecimalData>("GOC_EMVDAT", 3, 512, 1.0f / 2, false, false, DefaultStringFormatter.HexadecimalStringFormatter, DefaultStringParser.HexadecimalStringParser);
			this.GOC_ACQRD = new VariableLengthProperty<string>("GOC_ACQRD", 3, 0, 1.0f, false, true, DefaultStringFormatter.StringStringFormatter, DefaultStringParser.StringStringParser, String.Empty);

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
		public PinpadFixedLengthProperty<OfflineTransactionStatus> GOC_DECISION { get; private set; }

		/// <summary>
		/// Should Paper signature be obtained?
		/// </summary>
		public SimpleProperty<Nullable<bool>> GOC_SIGNAT { get; private set; }

		/// <summary>
		/// Was the pin verified offline?
		/// </summary>
		public SimpleProperty<Nullable<bool>> GOC_PINOFF { get; private set; }

		/// <summary>
		/// Amount of invalid offline pin presentations this transaction
		/// </summary>
		public PinpadFixedLengthProperty<Nullable<int>> GOC_ERRPINOFF { get; private set; }

		/// <summary>
		/// Was the offline pin blocked at the last invalid presentation in this transaction?
		/// </summary>
		public SimpleProperty<Nullable<bool>> GOC_PBLOCKED { get; private set; }

		/// <summary>
		/// Was the Pin captured for online verification?
		/// If false GOC_PINBLK and GOC_KSN should be ignored.
		/// </summary>
		public SimpleProperty<Nullable<bool>> GOC_PINONL { get; private set; }

		/// <summary>
		/// Encrypted Pin Block
		/// </summary>
		public PinpadFixedLengthProperty<HexadecimalData> GOC_PINBLK { get; private set; }

		/// <summary>
		/// Key Serial Number of the Encrypted Pin, only if DUKPT was used as Key Management Method, otherwise this field is filled with zeros
		/// </summary>
		public PinpadFixedLengthProperty<HexadecimalData> GOC_KSN { get; private set; }

		/// <summary>
		/// EMV data as Tag/Length/Value as requested from GOC_TAGS1 and GOC_TAGS2 if found and respecting requested order
		/// </summary>
		public VariableLengthProperty<HexadecimalData> GOC_EMVDAT { get; private set; }

		/// <summary>
		/// Acquirer specific parameters, always empty
		/// </summary>
		public VariableLengthProperty<string> GOC_ACQRD { get; private set; }
	}
}
