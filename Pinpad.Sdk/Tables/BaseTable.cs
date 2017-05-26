using Pinpad.Sdk.Model;
using Pinpad.Sdk.PinpadProperties.Refactor.Formatter;
using Pinpad.Sdk.PinpadProperties.Refactor.Parser;
using Pinpad.Sdk.PinpadProperties.Refactor.Property;
using System;

namespace Pinpad.Sdk
{
    /// <summary>
    /// Base PinPad table
    /// </summary>
    public class BaseTable : PinpadProperties.Refactor.Property.BaseProperty 
	{
        private FixedLengthProperty<EmvTableType> _TAB_ID { get; set; }

        /// <summary>
        /// EMV Table acquirer index
        /// </summary>
        public FixedLengthProperty<Nullable<int>> TAB_ACQ { get; private set; }
        /// <summary>
        /// EMV Table record index
        /// Each acquirer has it's own list, so you can have TAB_ACQ = 1 and TAB_RECIDX = 1 while using TAB_ACQ = 2 and TAB_RECIDX = 1 without conflict
        /// </summary>
        public FixedLengthProperty<Nullable<int>> TAB_RECIDX { get; private set; }
        
        /// <summary>
        /// Constructor
        /// Starts the region TAB_LEN and does not close it
        /// </summary>
        public BaseTable() 
		{
			this.TAB_LEN = new RegionProperty("TAB_LEN", 3, true);
			this._TAB_ID = new FixedLengthProperty<EmvTableType>("TAB_ID", 1, false, 
                StringFormatter.EnumStringFormatter<EmvTableType>, StringParser.EnumStringParser<EmvTableType>, 
                null, this.TAB_ID);
			this.TAB_ACQ = new FixedLengthProperty<int?>("TAB_ACQ", 2, false, 
                StringFormatter.IntegerStringFormatter, StringParser.IntegerStringParser);
			this.TAB_RECIDX = new FixedLengthProperty<int?>("TAB_RECIDX", 2, false, 
                StringFormatter.IntegerStringFormatter, StringParser.IntegerStringParser);

			this.StartRegion(this.TAB_LEN);
			{
				this.AddProperty(this._TAB_ID);
				this.AddProperty(this.TAB_ACQ);
				this.AddProperty(this.TAB_RECIDX);
			}
		}

		/// <summary>
		/// Length of the table
		/// </summary>
		public RegionProperty TAB_LEN { get; private set; }

		/// <summary>
		/// Table Identifier
		/// </summary>
		public virtual EmvTableType TAB_ID {
			get {
				if (this._TAB_ID == null) {
					return EmvTableType.Undefined;
				}
				else {
					return _TAB_ID.Value;
				}
			}
		}
	}
}
