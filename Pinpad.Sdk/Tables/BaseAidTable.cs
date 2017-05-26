using Pinpad.Sdk.Commands;
using Pinpad.Sdk.Model;
using Pinpad.Sdk.PinpadProperties.Refactor.Formatter;
using Pinpad.Sdk.PinpadProperties.Refactor.Parser;
using Pinpad.Sdk.PinpadProperties.Refactor.Property;
using System;

namespace Pinpad.Sdk
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
		public FixedLengthProperty<Nullable<int>> T1_APPTYPE { get; private set; }
		/// <summary>
		/// Default label for the application, in case the card doesn't have one
		/// Obsolete and unused since EMV 4.3.
		/// 16 chars long.
		/// </summary>
		public FixedLengthProperty<string> T1_DEFLABEL { get; private set; }
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
		private FixedLengthProperty<ApplicationType> _T1_ICCSTD { get; set; }

		// Constructor
		/// <summary>
		/// Constructor that sets default values.
		/// </summary>
		public BaseAidTable() 
		{
			this.T1_AID = new VariableLengthProperty<HexadecimalData>("T1_AID", 2, 32, 1.0f / 2, true, false, 
                StringFormatter.HexadecimalStringFormatter, StringParser.HexadecimalStringParser);
			this.T1_APPTYPE = new FixedLengthProperty<int?>("T1_APPTYPE", 2, false, 
                StringFormatter.IntegerStringFormatter, StringParser.IntegerStringParser);
			this.T1_DEFLABEL = new FixedLengthProperty<string>("T1_DEFLABEL", 16, false, 
                StringFormatter.StringStringFormatter, StringParser.StringStringParser);
			this._T1_ICCSTD = new FixedLengthProperty<ApplicationType>("T1_ICCSTD", 2, false, 
                StringFormatter.EnumStringFormatter<ApplicationType>, 
                StringParser.EnumStringParser<ApplicationType>, null, this.T1_ICCSTD);

			// PinPadBaseTableController starts the region and doesn't close
			{
				this.AddProperty(this.T1_AID);
				this.AddProperty(this.T1_APPTYPE);
				this.AddProperty(this.T1_DEFLABEL);
				this.AddProperty(this._T1_ICCSTD);
			}
			// PinPadBaseTableController starts the region and doesn't close
		}

	}
}
