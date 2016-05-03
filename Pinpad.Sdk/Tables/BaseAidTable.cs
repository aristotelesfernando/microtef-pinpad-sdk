using Pinpad.Sdk.Commands;
using Pinpad.Sdk.Model;
using Pinpad.Sdk.Properties;
using Pinpad.Sdk.TypeCode;
using Pinpad.Sdk.Utilities;
using System;

namespace Pinpad.Sdk.Tables
{
	/// <summary>
	/// Application IDentifier table record
	/// </summary>
	public class BaseAidTable : BaseTable 
	{
		// Members
		/// <summary>
		/// Table Identifier
		/// </summary>
		public override EmvTableType TAB_ID { get { return EmvTableType.Aid; } }
		/// <summary>
		/// Application IDentifier
		/// </summary>
		public VariableLengthProperty<HexadecimalData> T1_AID { get; private set; }
		/// <summary>
		/// Application Type
		/// Free to use but commonly used as 1 for credit and 2 for debit
		/// </summary>
		public PinpadFixedLengthProperty<Nullable<int>> T1_APPTYPE { get; private set; }
		/// <summary>
		/// Default label for the application, in case the card doesn't have one
		/// Obsolete and unused since EMV 4.3.
		/// 16 chars long.
		/// </summary>
		public PinpadFixedLengthProperty<string> T1_DEFLABEL { get; private set; }
		/// <summary>
		/// Application standard
		/// </summary>
		public virtual ApplicationType T1_ICCSTD
		{
			get
			{
				if (_T1_ICCSTD == null) { return ApplicationType.Undefined; }
				else { return _T1_ICCSTD.Value; }
			}
		}
		/// <summary>
		/// EMV. Fixed "03".
		/// </summary>
		private PinpadFixedLengthProperty<ApplicationType> _T1_ICCSTD { get; set; }

		// Constructor
		/// <summary>
		/// Constructor that sets default values.
		/// </summary>
		public BaseAidTable() 
		{
			this.T1_AID = new VariableLengthProperty<HexadecimalData>("T1_AID", 2, 32, 1.0f / 2, true, false, DefaultStringFormatter.HexadecimalStringFormatter, DefaultStringParser.HexadecimalStringParser);
			this.T1_APPTYPE = new PinpadFixedLengthProperty<int?>("T1_APPTYPE", 2, false, DefaultStringFormatter.IntegerStringFormatter, DefaultStringParser.IntegerStringParser);
			this.T1_DEFLABEL = new PinpadFixedLengthProperty<string>("T1_DEFLABEL", 16, false, DefaultStringFormatter.StringStringFormatter, DefaultStringParser.StringStringParser);
			this._T1_ICCSTD = new PinpadFixedLengthProperty<ApplicationType>("T1_ICCSTD", 2, false, DefaultStringFormatter.EnumStringFormatter<ApplicationType>, DefaultStringParser.EnumStringParser<ApplicationType>, null, this.T1_ICCSTD);

			// PinPadBaseTableController starts the region and doesn't close
			{
				AddProperty(this.T1_AID);
				AddProperty(this.T1_APPTYPE);
				AddProperty(this.T1_DEFLABEL);
				AddProperty(this._T1_ICCSTD);
			}
			// PinPadBaseTableController starts the region and doesn't close
		}

	}
}
