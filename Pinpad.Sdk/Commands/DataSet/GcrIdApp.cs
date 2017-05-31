using Pinpad.Sdk.PinpadProperties.Refactor.Formatter;
using Pinpad.Sdk.PinpadProperties.Refactor.Parser;
using Pinpad.Sdk.PinpadProperties.Refactor.Property;
using System;

namespace Pinpad.Sdk.Commands
{
	/// <summary>
	/// Controller for GCR PinPad command request Id App entry
	/// </summary>
	public sealed class GcrIdApp : PinpadProperties.Refactor.Property.BaseProperty 
	{
        /// <summary>
		/// Aid Table Acquirer Index
		/// </summary>
		public FixedLengthProperty<Nullable<int>> TAB_ACQ { get; private set; }
        /// <summary>
        /// Aid Table Record Index
        /// </summary>
        public FixedLengthProperty<Nullable<int>> TAB_RECIDX { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public GcrIdApp() 
		{
			this.TAB_ACQ = new FixedLengthProperty<Nullable<int>>("TAB_ACQ", 2, false, 
                StringFormatter.IntegerStringFormatter, StringParser.IntegerStringParser);
			this.TAB_RECIDX = new FixedLengthProperty<Nullable<int>>("TAB_RECIDX", 2, false, 
                StringFormatter.IntegerStringFormatter, StringParser.IntegerStringParser);

			this.AddProperty(this.TAB_ACQ);
			this.AddProperty(this.TAB_RECIDX);
		}
		/// <summary>
		/// Constructor with initial value
		/// </summary>
		/// <param name="acquirer">Aid Table Acquirer Index</param>
		/// <param name="record">Aid Table Record Index</param>
		public GcrIdApp(int acquirer, int record)
			: this()
        {
			this.TAB_ACQ.Value = acquirer;
			this.TAB_RECIDX.Value = record;
		}
	}
}
