using PinPadSDK.Property;
using PinPadSDK.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PinPadSDK.Commands {
    /// <summary>
    /// CKE request
    /// </summary>
    public class CkeRequest : BaseCommand {
        /// <summary>
        /// Constructor
        /// </summary>
        public CkeRequest() {
            this.CMD_LEN1 = new RegionProperty("CMD_LEN1", 3);
            this.CKE_KEY = new SimpleProperty<bool?>("CKE_KEY", false, DefaultStringFormatter.BooleanStringFormatter, DefaultStringParser.BooleanStringParser);
            this.CKE_MAG = new SimpleProperty<bool?>("CKE_MAG", false, DefaultStringFormatter.BooleanStringFormatter, DefaultStringParser.BooleanStringParser);
            this.CKE_ICC = new PinPadFixedLengthPropertyController<StatusWatcherMode>("CKE_ICC", 1, false, DefaultStringFormatter.EnumStringFormatter<StatusWatcherMode>, DefaultStringParser.EnumStringParser<StatusWatcherMode>);
            this.CKE_CTLS = new SimpleProperty<bool?>("CKE_CTLS", true, DefaultStringFormatter.BooleanStringFormatter, DefaultStringParser.BooleanStringParser);

            this.StartRegion(this.CMD_LEN1);
            {
                this.AddProperty(this.CKE_KEY);
                this.AddProperty(this.CKE_MAG);
                this.AddProperty(this.CKE_ICC);
                this.AddProperty(this.CKE_CTLS);
            }
            this.EndLastRegion();
        }

        /// <summary>
        /// Name of the command
        /// </summary>
        public override string CommandName { get { return "CKE"; } }

        /// <summary>
        /// Length of the first region of the command
        /// </summary>
        public RegionProperty CMD_LEN1 { get; private set; }

        /// <summary>
        /// Verify key press
        /// </summary>
        public SimpleProperty<Nullable<bool>> CKE_KEY { get; private set; }

        /// <summary>
        /// Verify Magnetic Stripe cards
        /// </summary>
        public SimpleProperty<Nullable<bool>> CKE_MAG { get; private set; }

        /// <summary>
        /// Verify ICC status
        /// </summary>
        public PinPadFixedLengthPropertyController<StatusWatcherMode> CKE_ICC { get; private set; }

        /// <summary>
        /// Verify CTLS cards
        /// </summary>
        public SimpleProperty<Nullable<bool>> CKE_CTLS { get; private set; }
    }
}
