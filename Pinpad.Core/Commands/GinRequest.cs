using PinPadSDK.Property;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PinPadSDK.Commands {
    /// <summary>
    /// GIN request
    /// </summary>
    public class GinRequest : BaseCommand {
        /// <summary>
        /// Constructor
        /// </summary>
        public GinRequest() {
            this.CMD_LEN1 = new RegionProperty("CMD_LEN1", 3);
            this.GIN_ACQIDX = new PinPadFixedLengthPropertyController<int?>("GIN_ACQIDX", 2, false, DefaultStringFormatter.IntegerStringFormatter, DefaultStringParser.IntegerStringParser);

            this.StartRegion(this.CMD_LEN1);
            {
                this.AddProperty(this.GIN_ACQIDX);
            }
            this.EndLastRegion();
        }

        /// <summary>
        /// Name of the command
        /// </summary>
        public override string CommandName { get { return "GIN"; } }

        /// <summary>
        /// Length of the first region of the command
        /// </summary>
        public RegionProperty CMD_LEN1 { get; private set; }

        /// <summary>
        /// Acquirer index to get information from or 0 to get PinPad information
        /// </summary>
        public PinPadFixedLengthPropertyController<Nullable<int>> GIN_ACQIDX { get; private set; }
    }
}
