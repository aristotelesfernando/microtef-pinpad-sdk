using PinPadSDK.Property;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PinPadSDK.Commands {
    /// <summary>
    /// GIN response without a specific response format
    /// </summary>
    public class GinDefaultResponse : BaseResponse, IGinAcquirerResponse {
        /// <summary>
        /// Constructor
        /// </summary>
        public GinDefaultResponse() {
            this.RSP_LEN1 = new RegionProperty("RSP_LEN1", 3);
            this.GIN_ACQNAME = new PinPadFixedLengthPropertyController<string>("GIN_ACQNAME", 20, false, DefaultStringFormatter.StringStringFormatter, DefaultStringParser.StringStringParser);
            this.GIN_APPVERS = new PinPadFixedLengthPropertyController<string>("GIN_APPVERS", 13, false, DefaultStringFormatter.StringStringFormatter, DefaultStringParser.StringStringParser);
            this.GIN_SPECVER = new PinPadFixedLengthPropertyController<string>("GIN_SPECVER", 4, false, DefaultStringFormatter.StringStringFormatter, DefaultStringParser.StringStringParser);
            this.GIN_RUF1 = new PinPadFixedLengthPropertyController<string>("GIN_RUF1", 3, false, DefaultStringFormatter.StringStringFormatter, DefaultStringParser.StringStringParser);
            this.GIN_RUF2 = new PinPadFixedLengthPropertyController<int?>("GIN_RUF2", 2, false, DefaultStringFormatter.IntegerStringFormatter, DefaultStringParser.IntegerStringParser);

            this.StartRegion(this.RSP_LEN1);
            {
                this.AddProperty(this.GIN_ACQNAME);
                this.AddProperty(this.GIN_APPVERS);
                this.AddProperty(this.GIN_SPECVER);
                this.AddProperty(this.GIN_RUF1);
                this.AddProperty(this.GIN_RUF2);
            }
            this.EndLastRegion();
        }

        /// <summary>
        /// Is this a blocking command?
        /// </summary>
        public override bool IsBlockingCommand {
            get { return false; }
        }

        /// <summary>
        /// Name of the command
        /// </summary>
        public override string CommandName { get { return "GIN"; } }

        /// <summary>
        /// Length of the first region of the command
        /// </summary>
        public RegionProperty RSP_LEN1 { get; private set; }

        /// <summary>
        /// Name of the acquirer at the requested index
        /// </summary>
        public PinPadFixedLengthPropertyController<string> GIN_ACQNAME { get; private set; }

        /// <summary>
        /// ABECS application version with format "VVV.VV YYMMDD"
        /// </summary>
        public PinPadFixedLengthPropertyController<string> GIN_APPVERS { get; private set; }

        /// <summary>
        /// Specification version with format "V.vv" or "VvvA"
        /// Where V is the major version
        /// v is the minor version
        /// A is the alphanumeric modifier
        /// Example: "108a", "1.07"
        /// </summary>
        public PinPadFixedLengthPropertyController<string> GIN_SPECVER { get; private set; }

        /// <summary>
        /// Reserved for Future Use, filled with spaces
        /// </summary>
        public PinPadFixedLengthPropertyController<string> GIN_RUF1 { get; private set; }

        /// <summary>
        /// Reserved for Future Use, always 0
        /// </summary>
        public PinPadFixedLengthPropertyController<Nullable<int>> GIN_RUF2 { get; private set; }
    }
}
