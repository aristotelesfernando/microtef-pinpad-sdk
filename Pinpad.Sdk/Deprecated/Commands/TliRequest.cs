using PinPadSDK.Property;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PinPadSDK.Commands {
    /// <summary>
    /// TLI request
    /// </summary>
    public class TliRequest : BaseCommand {
        /// <summary>
        /// Constructor
        /// </summary>
        public TliRequest() {
            this.CMD_LEN1 = new RegionProperty("CMD_LEN1", 3);
            this.TLI_ACQIDX = new PinPadFixedLengthPropertyController<int?>("TLI_ACQIDX", 2, false, DefaultStringFormatter.IntegerStringFormatter, DefaultStringParser.IntegerStringParser);
            this.TLI_TABVER = new PinPadFixedLengthPropertyController<string>("TLI_TABVER", 10, false, DefaultStringFormatter.StringStringFormatter, DefaultStringParser.StringStringParser);

            this.StartRegion(this.CMD_LEN1);
            {
                this.AddProperty(this.TLI_ACQIDX);
                this.AddProperty(this.TLI_TABVER);
            }
            this.EndLastRegion();
        }

        /// <summary>
        /// Name of the command
        /// </summary>
        public override string CommandName { get { return "TLI"; } }

        /// <summary>
        /// Length of the first region of the command
        /// </summary>
        public RegionProperty CMD_LEN1 { get; private set; }

        /// <summary>
        /// EMV Table acquirer index to update
        /// To change all acquirers use the generic index (0)
        /// </summary>
        public PinPadFixedLengthPropertyController<Nullable<int>> TLI_ACQIDX { get; private set; }

        /// <summary>
        /// EMV Table version to load
        /// </summary>
        public PinPadFixedLengthPropertyController<string> TLI_TABVER { get; private set; }
    }
}
