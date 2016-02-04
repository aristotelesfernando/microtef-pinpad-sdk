using PinPadSDK.Property;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PinPadSDK.Commands {
    /// <summary>
    /// GTS request
    /// </summary>
    public class GtsRequest : BaseCommand {
        /// <summary>
        /// Constructor
        /// </summary>
        public GtsRequest() {
            this.CMD_LEN1 = new RegionProperty("CMD_LEN1", 3);
            this.GTS_ACQIDX = new PinPadFixedLengthPropertyController<int?>("GTS_ACQIDX", 2, false, DefaultStringFormatter.IntegerStringFormatter, DefaultStringParser.IntegerStringParser);

            this.StartRegion(this.CMD_LEN1);
            {
                this.AddProperty(this.GTS_ACQIDX);
            }
            this.EndLastRegion();
        }

        /// <summary>
        /// Name of the command
        /// </summary>
        public override string CommandName { get { return "GTS"; } }

        /// <summary>
        /// Length of the first region of the command
        /// </summary>
        public RegionProperty CMD_LEN1 { get; private set; }

        /// <summary>
        /// EMV Table Acquirer Index to request version or 0 when using a single table for all acquirers
        /// </summary>
        public PinPadFixedLengthPropertyController<Nullable<int>> GTS_ACQIDX { get; private set; }
    }
}
