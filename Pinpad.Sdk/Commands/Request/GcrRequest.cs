using Pinpad.Sdk.Properties;
using System;
namespace Pinpad.Sdk.Commands 
{
	/// <summary>
	/// GCR request
	/// </summary>
	internal sealed class GcrRequest : BaseCommand 
	{
		/// <summary>
		/// Constructor
		/// </summary>
		public GcrRequest() 
		{
			CMD_LEN1 = new RegionProperty("CMD_LEN1", 3);
			GCR_ACQIDXREQ = new PinpadFixedLengthProperty<Nullable<int>>("GCR_ACQIDXREQ", 2, false, DefaultStringFormatter.IntegerStringFormatter, DefaultStringParser.IntegerStringParser);
			GCR_APPTYPREQ = new PinpadFixedLengthProperty<Nullable<int>>("GCR_APPTYPREQ", 2, false, DefaultStringFormatter.IntegerStringFormatter, DefaultStringParser.IntegerStringParser);
			GCR_AMOUNT = new PinpadFixedLengthProperty<Nullable<long>>("GCR_AMOUNT", 12, false, DefaultStringFormatter.LongIntegerStringFormatter, DefaultStringParser.LongIntegerStringParser);
			GCR_DATE_TIME = new SimpleProperty<Nullable<DateTime>>("GCR_DATE_TIME", false, DefaultStringFormatter.DateTimeStringFormatter, DefaultStringParser.DateTimeStringParser);
			GCR_TABVER = new PinpadFixedLengthProperty<string>("GCR_TABVER", 10, false, DefaultStringFormatter.StringStringFormatter, DefaultStringParser.StringStringParser);
			GCR_IDAPP_Collection = new PinpadCollectionProperty<GcrIdApp>("GCR_IDAPP_Collection", 2, 0, 4, DefaultStringFormatter.PropertyControllerStringFormatter, DefaultStringParser.PropertyControllerStringParser<GcrIdApp>);
			GCR_CTLSON = new SimpleProperty<Nullable<bool>>("GCR_CTLSON", true, DefaultStringFormatter.BooleanStringFormatter, DefaultStringParser.BooleanStringParser);

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
		public PinpadFixedLengthProperty<Nullable<int>> GCR_ACQIDXREQ { get; private set; }

		/// <summary>
		/// Aid Table application type or 99 to ignore
		/// </summary>
		public PinpadFixedLengthProperty<Nullable<int>> GCR_APPTYPREQ { get; private set; }

		/// <summary>
		/// Amount of the transaction or 0 to ignore
		/// </summary>
		public PinpadFixedLengthProperty<Nullable<long>> GCR_AMOUNT { get; private set; }

		/// <summary>
		/// DateTime of the transaction
		/// </summary>
		public SimpleProperty<Nullable<DateTime>> GCR_DATE_TIME { get; private set; }

		/// <summary>
		/// Excepted Table Version
		/// </summary>
		public PinpadFixedLengthProperty<string> GCR_TABVER { get; private set; }

		/// <summary>
		/// Aid Table references to use
		/// </summary>
		public PinpadCollectionProperty<GcrIdApp> GCR_IDAPP_Collection { get; private set; }

		/// <summary>
		/// Contactless status or null to ignore
		/// </summary>
		public SimpleProperty<Nullable<bool>> GCR_CTLSON { get; private set; }
	}
}
