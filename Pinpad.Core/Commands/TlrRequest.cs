using PinPadSDK.Property;
using PinPadSDK.Controllers.Tables;
using PinPadSDK.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PinPadSDK.Commands {
    /// <summary>
    /// Controller for TLR command
    /// Table Load Record is used to load one or more table records
    /// </summary>
    public class TlrRequest : BaseCommand {
        /// <summary>
        /// Constructor
        /// </summary>
        public TlrRequest() {
            this.CMD_LEN1 = new RegionProperty("CMD_LEN1", 3);
            this.TLR_REC = new VariableLengthCollectionProperty<BaseTable>("TLR_REC", 2, 1, 999, DefaultStringFormatter.PropertyControllerStringFormatter, TlrRequest.TableStringParser);

            this.StartRegion(this.CMD_LEN1);
            {
                this.AddProperty(this.TLR_REC);
            }
            this.EndLastRegion();
        }

        /// <summary>
        /// Name of the command
        /// </summary>
        public override string CommandName { get { return "TLR"; } }

        /// <summary>
        /// Length of the first region of the command
        /// </summary>
        public RegionProperty CMD_LEN1 { get; private set; }

        /// <summary>
        /// Table records
        /// </summary>
        public VariableLengthCollectionProperty<BaseTable> TLR_REC { get; private set; }

        private const int TAB_LEN_Length = 3;

        /// <summary>
        /// Parses a PinPad Table
        /// </summary>
        /// <param name="reader">string reader</param>
        /// <returns>PinPadBaseTableController</returns>
        private static BaseTable TableStringParser(StringReader reader) {
            int length = reader.PeekInt(TAB_LEN_Length); //Retrieve the table length
            string commandString = reader.ReadString(length); //Retrieve full table command string

            BaseTable tableData = new BaseTable();
            tableData.CommandString = commandString; //Parse the table command string

            //Now read the Table we just received
            BaseTable value;
            switch (tableData.TAB_ID) {
                case EmvTableType.Aid:
                    //It's an AID table, check for the application standard
                    BaseAidTable aidTableData = new BaseAidTable();
                    aidTableData.CommandString = commandString;
                    switch (aidTableData.T1_ICCSTD) {
                        case ApplicationType.IccEmv:
                            value = new EmvAidTable();
                            break;

                        default:
                            //We don't have the pattern, just keep the data received
                            value = new UnknownAidTable();
                            break;
                    }
                    break;

                case EmvTableType.Capk:
                    value = new CapkTable();
                    break;

                case EmvTableType.RevokedCertificate:
                    value = new RevCerTable();
                    break;

                default:
                    throw new InvalidOperationException("Attempt to parse unknown table: " + tableData.TAB_ID);
            }
            value.CommandString = commandString;
            return value;
        }
    }
}
