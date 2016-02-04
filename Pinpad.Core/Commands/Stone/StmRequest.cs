using PinPadSDK.Property;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PinPadSDK.Commands.Stone {
    /// <summary>
    /// STM request
    /// </summary>
    public class StmRequest : BaseStoneRequest {
        /// <summary>
        /// Constructor
        /// </summary>
        public StmRequest() {
            this.CMD_LEN1 = new RegionProperty("CMD_LEN1", 3);
            this.STM_DATETIME = new SimpleProperty<DateTime?>("STM_DATETIME", false, DefaultStringFormatter.DateTimeStringFormatter, DefaultStringParser.DateTimeStringParser, null, DateTime.Now);

            this.StartRegion(this.CMD_LEN1);
            {
                this.AddProperty(this.STM_DATETIME);
            }
            this.EndLastRegion();
        }

        /// <summary>
        /// Minimum stone version required for the request
        /// </summary>
        public override int MinimumStoneVersion { get { return 1; } }

        /// <summary>
        /// Name of the command
        /// </summary>
        public override string CommandName { get { return "STM"; } }

        /// <summary>
        /// Length of the first region of the command
        /// </summary>
        public RegionProperty CMD_LEN1 { get; private set; }

        /// <summary>
        /// DateTime to set the PinPad clock
        /// </summary>
        public SimpleProperty<Nullable<DateTime>> STM_DATETIME { get; private set; }
    }
}
