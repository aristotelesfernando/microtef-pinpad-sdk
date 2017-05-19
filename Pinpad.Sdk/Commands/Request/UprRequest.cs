using Pinpad.Sdk.Model;
using Pinpad.Sdk.Properties;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Pinpad.Sdk.Commands.Request
{
    // TODO: Doc
    public sealed class UprRequest : BaseCommand
    {
        public const int PackageSectionSize = 900;
        private const int TableLengthSize = 3;

        public override string CommandName { get { return "UPR"; } }
        /// <summary>
        /// Length of the first region of the command
        /// </summary>
        public RegionProperty CMD_LEN1 { get; private set; }
        /// <summary>
        /// Table records
        /// </summary>
        public VariableLengthCollectionProperty<List<byte>> UPR_REC { get; private set; }

        public UprRequest()
        {
            this.CMD_LEN1 = new RegionProperty("CMD_LEN1", 3);
            this.UPR_REC = new VariableLengthCollectionProperty<List<byte>>("TLR_REC",
                2, 1, UprRequest.PackageSectionSize);

            this.StartRegion(this.CMD_LEN1);
            {
                this.AddProperty(this.UPR_REC);
            }
            this.EndLastRegion();
        }

        // TODO: Testar.
        private static string PackageFormatter(List<byte> arg)
        {
            if (arg == null) { return null; }

            return Encoding.UTF8.GetString(arg.ToArray(), 0, arg.Count);
        }
        // TODO: Testar.
        private static List<byte> PackageParser(StringReader arg)
        {
            if (arg == null) { return null; }

            List<byte> bytes = Encoding.UTF8.GetBytes(arg.ToString())
                .ToList();

            return bytes;
        }
        // TODO: Tirar esse parser aqui.
        /// <summary>
        /// Parses a PinPad Table
        /// </summary>
        /// <param name="reader">string reader</param>
        /// <returns>PinPadBaseTableController</returns>
        private static BaseTable TableStringParser(StringReader reader)
        {
            // Retrieve the table length
            int length = reader.PeekInt(UprRequest.TableLengthSize);

            // Retrieve full table command string
            string commandString = reader.ReadString(length);

            // Parse the table command string
            BaseTable tableData = new BaseTable();
            tableData.CommandString = commandString;

            // Read the Table we just received
            BaseTable value;

            switch (tableData.TAB_ID)
            {
                // It's an AID table, check for the application standard
                case EmvTableType.Aid:
                    BaseAidTable aidTableData = new BaseAidTable();
                    aidTableData.CommandString = commandString;

                    switch (aidTableData.T1_ICCSTD)
                    {
                        case ApplicationType.IccEmv:
                            value = new AidTable();
                            break;

                        //We don't have the pattern, just keep the data received:
                        default:
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
