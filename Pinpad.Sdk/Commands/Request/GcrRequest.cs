using Pinpad.Sdk.PinpadProperties.Refactor.Formatter;
using Pinpad.Sdk.PinpadProperties.Refactor.Parser;
using Pinpad.Sdk.PinpadProperties.Refactor.Property;
using System;

namespace Pinpad.Sdk.Commands 
{
	/// <summary>
	/// GCR request
	/// </summary>
	internal sealed class GcrRequest : PinpadProperties.Refactor.BaseCommand 
	{
        /// <summary>
		/// Name of the command
		/// </summary>
		public override string CommandName { get { return "GCR"; } }
        /// <summary>
        /// Length of the first region of the command
        /// </summary>
        public RegionProperty CMD_LEN1 { get; private set; }
        /// <summary>
        /// Aid Table Acquirer index or 0 to ignore
        /// </summary>
        public FixedLengthProperty<Nullable<int>> GCR_ACQIDXREQ { get; private set; }
        /// <summary>
        /// Aid Table application type or 99 to ignore
        /// </summary>
        public FixedLengthProperty<Nullable<int>> GCR_APPTYPREQ { get; private set; }
        /// <summary>
        /// Amount of the transaction or 0 to ignore
        /// </summary>
        public FixedLengthProperty<Nullable<long>> GCR_AMOUNT { get; private set; }
        /// <summary>
        /// DateTime of the transaction
        /// </summary>
        public TextProperty<Nullable<DateTime>> GCR_DATE_TIME { get; private set; }
        /// <summary>
        /// Excepted Table Version
        /// </summary>
        public FixedLengthProperty<string> GCR_TABVER { get; private set; }
        /// <summary>
        /// Aid Table references to use
        /// </summary>
        public DataCollectionProperty<GcrIdApp> GCR_IDAPP_Collection { get; private set; }
        /// <summary>
        /// Contactless status or null to ignore
        /// </summary>
        public TextProperty<Nullable<bool>> GCR_CTLSON { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public GcrRequest() 
		{
			CMD_LEN1 = new RegionProperty("CMD_LEN1", 3);
			GCR_ACQIDXREQ = new FixedLengthProperty<Nullable<int>>("GCR_ACQIDXREQ", 2, false, 
                StringFormatter.IntegerStringFormatter, StringParser.IntegerStringParser);
			GCR_APPTYPREQ = new FixedLengthProperty<Nullable<int>>("GCR_APPTYPREQ", 2, false, 
                StringFormatter.IntegerStringFormatter, StringParser.IntegerStringParser);
			GCR_AMOUNT = new FixedLengthProperty<Nullable<long>>("GCR_AMOUNT", 12, false, 
                StringFormatter.LongIntegerStringFormatter, StringParser.LongIntegerStringParser);
			GCR_DATE_TIME = new TextProperty<Nullable<DateTime>>("GCR_DATE_TIME", false, 
                StringFormatter.DateTimeStringFormatter, StringParser.DateTimeStringParser);
			GCR_TABVER = new FixedLengthProperty<string>("GCR_TABVER", 10, false, 
                StringFormatter.StringStringFormatter, StringParser.StringStringParser);
			GCR_IDAPP_Collection = new DataCollectionProperty<GcrIdApp>("GCR_IDAPP_Collection", 2, 0, 4, 
                StringFormatter.PropertyControllerStringFormatter, 
                StringParser.PropertyControllerStringParser<GcrIdApp>);
			GCR_CTLSON = new TextProperty<Nullable<bool>>("GCR_CTLSON", true, 
                StringFormatter.BooleanStringFormatter, StringParser.BooleanStringParser);

			this.StartRegion(CMD_LEN1);
			{
				this.AddProperty(GCR_ACQIDXREQ);
				this.AddProperty(GCR_APPTYPREQ);
				this.AddProperty(GCR_AMOUNT);
				this.AddProperty(GCR_DATE_TIME);
				this.AddProperty(GCR_TABVER);
				this.AddProperty(GCR_IDAPP_Collection);
				this.AddProperty(GCR_CTLSON);
			}
			this.EndLastRegion();
		}
	}
}
