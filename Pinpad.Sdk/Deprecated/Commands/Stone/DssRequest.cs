using PinPadSDK.Property;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PinPadSDK.Commands.Stone {
    /// <summary>
    /// DSS request
    /// </summary>
    public class DssRequest : BaseStoneRequest {
        /// <summary>
        /// Constructor
        /// </summary>
        public DssRequest() {
            this.CMD_LEN1 = new RegionProperty("CMD_LEN1", 3);
            this.DSS_X = new PinPadFixedLengthPropertyController<int?>("DSS_X", 3, false, DefaultStringFormatter.IntegerStringFormatter, DefaultStringParser.IntegerStringParser);
            this.DSS_Y = new PinPadFixedLengthPropertyController<int?>("DSS_Y", 3, false, DefaultStringFormatter.IntegerStringFormatter, DefaultStringParser.IntegerStringParser);
            this.DSS_BGRED = new PinPadFixedLengthPropertyController<int?>("DSS_BGRED", 3, false, DefaultStringFormatter.IntegerStringFormatter, DefaultStringParser.IntegerStringParser);
            this.DSS_BGGREEN = new PinPadFixedLengthPropertyController<int?>("DSS_BGGREEN", 3, false, DefaultStringFormatter.IntegerStringFormatter, DefaultStringParser.IntegerStringParser);
            this.DSS_BGBLUE = new PinPadFixedLengthPropertyController<int?>("DSS_BGBLUE", 3, false, DefaultStringFormatter.IntegerStringFormatter, DefaultStringParser.IntegerStringParser);
            this.DSS_FGRED = new PinPadFixedLengthPropertyController<int?>("DSS_FGRED", 3, false, DefaultStringFormatter.IntegerStringFormatter, DefaultStringParser.IntegerStringParser);
            this.DSS_FGGREEN = new PinPadFixedLengthPropertyController<int?>("DSS_FGGREEN", 3, false, DefaultStringFormatter.IntegerStringFormatter, DefaultStringParser.IntegerStringParser);
            this.DSS_FGBLUE = new PinPadFixedLengthPropertyController<int?>("DSS_FGBLUE", 3, false, DefaultStringFormatter.IntegerStringFormatter, DefaultStringParser.IntegerStringParser);
            this.DSS_MSG = new VariableLengthProperty<string>("DSS_MSG", 3, 160, 1.0f, false, false, DefaultStringFormatter.StringStringFormatter, DefaultStringParser.StringStringParser);

            this.StartRegion(this.CMD_LEN1);
            {
                this.AddProperty(this.DSS_X);
                this.AddProperty(this.DSS_Y);
                this.AddProperty(this.DSS_BGRED);
                this.AddProperty(this.DSS_BGGREEN);
                this.AddProperty(this.DSS_BGBLUE);
                this.AddProperty(this.DSS_FGRED);
                this.AddProperty(this.DSS_FGGREEN);
                this.AddProperty(this.DSS_FGBLUE);
                this.AddProperty(this.DSS_MSG);
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
        public override string CommandName { get { return "DSS"; } }

        /// <summary>
        /// Length of the first region of the command
        /// </summary>
        public RegionProperty CMD_LEN1 { get; private set; }

        /// <summary>
        /// Horizontal position of the message
        /// </summary>
        public PinPadFixedLengthPropertyController<Nullable<int>> DSS_X { get; private set; }

        /// <summary>
        /// Vertical position of the message
        /// </summary>
        public PinPadFixedLengthPropertyController<Nullable<int>> DSS_Y { get; private set; }

        /// <summary>
        /// Background Red value of the message
        /// </summary>
        public PinPadFixedLengthPropertyController<Nullable<int>> DSS_BGRED { get; private set; }

        /// <summary>
        /// Background Green value of the message
        /// </summary>
        public PinPadFixedLengthPropertyController<Nullable<int>> DSS_BGGREEN { get; private set; }

        /// <summary>
        /// Background Blue value of the message
        /// </summary>
        public PinPadFixedLengthPropertyController<Nullable<int>> DSS_BGBLUE { get; private set; }

        /// <summary>
        /// Foreground Red value of the message
        /// </summary>
        public PinPadFixedLengthPropertyController<Nullable<int>> DSS_FGRED { get; private set; }

        /// <summary>
        /// Foreground Green value of the message
        /// </summary>
        public PinPadFixedLengthPropertyController<Nullable<int>> DSS_FGGREEN { get; private set; }

        /// <summary>
        /// Foreground Blue value of the message
        /// </summary>
        public PinPadFixedLengthPropertyController<Nullable<int>> DSS_FGBLUE { get; private set; }

        /// <summary>
        /// Message to display, maximum of 16 chars
        /// </summary>
        public VariableLengthProperty<string> DSS_MSG { get; private set; }
    }
}
