using PinPadSDK.Property;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PinPadSDK.Commands.Stone {
    /// <summary>
    /// GBS response
    /// </summary>
    public class GbsResponse : BaseResponse {
        /// <summary>
        /// Constructor
        /// </summary>
        public GbsResponse() {
            this.CMD_LEN1 = new RegionProperty("CMD_LEN1", 3);
            this.GBS_STATUS = new SimpleProperty<bool?>("GBS_STATUS", false, DefaultStringFormatter.BooleanStringFormatter, DefaultStringParser.BooleanStringParser);
            this.GBS_LEVEL = new PinPadFixedLengthPropertyController<int?>("GBS_LEVEL", 3, false, DefaultStringFormatter.IntegerStringFormatter, DefaultStringParser.IntegerStringParser);
            this.GBS_CHARGING = new SimpleProperty<bool?>("GBS_CHARGING", false, DefaultStringFormatter.BooleanStringFormatter, DefaultStringParser.BooleanStringParser);

            this.StartRegion(this.CMD_LEN1);
            {
                this.AddProperty(this.GBS_STATUS);
                this.AddProperty(this.GBS_LEVEL);
                this.AddProperty(this.GBS_CHARGING);
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
        public override string CommandName { get { return "GBS"; } }

        /// <summary>
        /// Length of the first region of the command
        /// </summary>
        public RegionProperty CMD_LEN1 { get; private set; }

        /// <summary>
        /// Is the battery present?
        /// </summary>
        public SimpleProperty<Nullable<bool>> GBS_STATUS { get; private set; }

        /// <summary>
        /// Battery level or 255 on error
        /// </summary>
        public PinPadFixedLengthPropertyController<Nullable<int>> GBS_LEVEL { get; private set; }

        /// <summary>
        /// Is the battery charging?
        /// </summary>
        public SimpleProperty<Nullable<bool>> GBS_CHARGING { get; private set; }
    }
}
