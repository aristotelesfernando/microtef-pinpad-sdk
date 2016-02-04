using PinPadSDK.Property;
using PinPadSDK.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PinPadSDK.Commands.Gcr;
using System.Diagnostics;

namespace PinPadSDK.Commands {
    /// <summary>
    /// GCR request
    /// </summary>
    public class GcrRequest : BaseCommand {
        /// <summary>
        /// Constructor
        /// </summary>
        public GcrRequest() 
		{
			Debug.WriteLine("Creating GCR request.");

            CMD_LEN1 = new RegionProperty("CMD_LEN1", 3);
			Debug.WriteLine("CMD_LEN1 created.");

			GCR_ACQIDXREQ = new PinPadFixedLengthPropertyController<Nullable<int>>("GCR_ACQIDXREQ", 2, false, DefaultStringFormatter.IntegerStringFormatter, DefaultStringParser.IntegerStringParser);
			Debug.WriteLine("GCR_ACQIDXREQ created.");

			GCR_APPTYPREQ = new PinPadFixedLengthPropertyController<Nullable<int>>("GCR_APPTYPREQ", 2, false, DefaultStringFormatter.IntegerStringFormatter, DefaultStringParser.IntegerStringParser);
			Debug.WriteLine("GCR_APPTYPREQ created");
			
			GCR_AMOUNT = new PinPadFixedLengthPropertyController<Nullable<long>>("GCR_AMOUNT", 12, false, DefaultStringFormatter.LongIntegerStringFormatter, DefaultStringParser.LongIntegerStringParser);
			Debug.WriteLine("GCR_AMOUNT created");
			
			GCR_DATE_TIME = new SimpleProperty<Nullable<DateTime>>("GCR_DATE_TIME", false, DefaultStringFormatter.DateTimeStringFormatter, DefaultStringParser.DateTimeStringParser);
			Debug.WriteLine("GCR_DATE_TIME created");
			
			GCR_TABVER = new PinPadFixedLengthPropertyController<string>("GCR_TABVER", 10, false, DefaultStringFormatter.StringStringFormatter, DefaultStringParser.StringStringParser);
			Debug.WriteLine("GCR_TABVER created");
			
			GCR_IDAPP_Collection = new PinPadCollectionPropertyController<GcrIdApp>("GCR_IDAPP_Collection", 2, 0, 4, DefaultStringFormatter.PropertyControllerStringFormatter, DefaultStringParser.PropertyControllerStringParser<GcrIdApp>);
			Debug.WriteLine("GCR_IDAPP_Collection created");
			
			GCR_CTLSON = new SimpleProperty<Nullable<bool>>("GCR_CTLSON", true, DefaultStringFormatter.BooleanStringFormatter, DefaultStringParser.BooleanStringParser);
			Debug.WriteLine("GCR_CTLSON created");

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

			Debug.WriteLine("GCR request created");
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
        public PinPadFixedLengthPropertyController<Nullable<int>> GCR_ACQIDXREQ { get; private set; }

        /// <summary>
        /// Aid Table application type or 99 to ignore
        /// </summary>
        public PinPadFixedLengthPropertyController<Nullable<int>> GCR_APPTYPREQ { get; private set; }

        /// <summary>
        /// Amount of the transaction or 0 to ignore
        /// </summary>
        public PinPadFixedLengthPropertyController<Nullable<long>> GCR_AMOUNT { get; private set; }

        /// <summary>
        /// DateTime of the transaction
        /// </summary>
        public SimpleProperty<Nullable<DateTime>> GCR_DATE_TIME { get; private set; }

        /// <summary>
        /// Excepted Table Version
        /// </summary>
        public PinPadFixedLengthPropertyController<string> GCR_TABVER { get; private set; }

        /// <summary>
        /// Aid Table references to use
        /// </summary>
        public PinPadCollectionPropertyController<GcrIdApp> GCR_IDAPP_Collection { get; private set; }

        /// <summary>
        /// Contactless status or null to ignore
        /// </summary>
        public SimpleProperty<Nullable<bool>> GCR_CTLSON { get; private set; }
    }
}
