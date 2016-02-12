using PinPadSDK.Commands.DwkModesData;
using PinPadSDK.Property;
using PinPadSDK.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PinPadSDK.Commands {
    /// <summary>
    /// DWK request
    /// </summary>
    public class DwkRequest : BaseCommand {
        /// <summary>
        /// Constructor
        /// </summary>
        public DwkRequest() {
            this.CMD_LEN1 = new RegionProperty("CMD_LEN1", 3);
            this.DWK_MODEDATA = new SimpleProperty<BaseDwkRequestData>("DWK_MODEDATA", false, DefaultStringFormatter.PropertyControllerStringFormatter, DwkRequest.EventDataStringParser);

            this.StartRegion(this.CMD_LEN1);
            {
                this.AddProperty(this.DWK_MODEDATA);
            }
            this.EndLastRegion();
        }

        /// <summary>
        /// Name of the command
        /// </summary>
        public override string CommandName { get { return "DWK"; } }

        /// <summary>
        /// Length of the first region of the command
        /// </summary>
        public RegionProperty CMD_LEN1 { get; private set; }

        /// <summary>
        /// Encryption Mode
        /// </summary>
        public virtual DwkMode DWK_MODE {
            get {
                if (this.DWK_MODEDATA.HasValue) {
                    return this.DWK_MODEDATA.Value.DWK_MODE;
                }
                else {
                    return DwkMode.Undefined;
                }
            }
        }

        /// <summary>
        /// Encryption Mode Data
        /// </summary>
        public SimpleProperty<BaseDwkRequestData> DWK_MODEDATA { get; private set; }

        /// <summary>
        /// Mode1 value of the command
        /// </summary>
        public DwkMode1RequestData DWK_MODE1 {
            get {
                return DWK_MODEDATA.Value as DwkMode1RequestData;
            }
            set {
                DWK_MODEDATA.Value = value;
            }
        }

        /// <summary>
        /// Mode2 value of the command
        /// </summary>
        public DwkMode2RequestData DWK_MODE2 {
            get {
                return DWK_MODEDATA.Value as DwkMode2RequestData;
            }
            set {
                DWK_MODEDATA.Value = value;
            }
        }

        /// <summary>
        /// Parses a Dwk Event Data
        /// </summary>
        /// <param name="reader">string reader</param>
        /// <returns>PinPadDwkBaseRequestController</returns>
        private static BaseDwkRequestData EventDataStringParser(StringReader reader) {
            string commandString = reader.ReadString(reader.Remaining);

            BaseDwkRequestData value = new BaseDwkRequestData() ;
            value.CommandString = commandString;
            switch (value.DWK_MODE) {
                case DwkMode.Mode1:
                    value = new DwkMode1RequestData();
                    break;

                case DwkMode.Mode2:
                    value = new DwkMode2RequestData();
                    break;

                default:
                    throw new InvalidOperationException("Attempt to parse unknown event: " + value.DWK_MODE);
            }
            value.CommandString = commandString;
            return value;
        }
    }
}
