using PinPadSDK.Property;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PinPadSDK.Commands.Stone {
    /// <summary>
    /// LFC response
    /// </summary>
    public class LfcResponse : BaseResponse {
        /// <summary>
        /// Constructor
        /// </summary>
        public LfcResponse() {
            this.RSP_LEN1 = new RegionProperty("RSP_LEN1", 3);
            this.LFC_EXISTS = new SimpleProperty<bool?>("LFC_EXISTS", false, DefaultStringFormatter.BooleanStringFormatter, DefaultStringParser.BooleanStringParser);
            this.LFC_FILESIZE = new PinPadFixedLengthPropertyController<long?>("LFC_FILESIZE", 10, false, DefaultStringFormatter.LongIntegerStringFormatter, DefaultStringParser.LongIntegerStringParser);

            this.StartRegion(this.RSP_LEN1);
            {
                this.AddProperty(this.LFC_EXISTS);
                this.AddProperty(this.LFC_FILESIZE);
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
        public override string CommandName { get { return "LFC"; } }

        /// <summary>
        /// Length of the first region of the command
        /// </summary>
        public RegionProperty RSP_LEN1 { get; private set; }

        /// <summary>
        /// Does the file exist?
        /// </summary>
        public SimpleProperty<Nullable<bool>> LFC_EXISTS { get; private set; }

        /// <summary>
        /// Size of the file
        /// </summary>
        public PinPadFixedLengthPropertyController<Nullable<long>> LFC_FILESIZE { get; private set; }
    }
}
