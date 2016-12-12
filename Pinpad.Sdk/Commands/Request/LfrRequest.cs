using Pinpad.Sdk.Properties;
using System;

namespace Pinpad.Sdk.Commands.Request
{
    internal sealed class LfrRequest : BaseCommand
    {
        public override string CommandName { get { return "LFR"; } }
        public RegionProperty CMD_LEN1 { get; private set; }
        public VariableLengthProperty<string> LFR_Data { get; private set; }

        public LfrRequest()
        {
            this.CMD_LEN1 = new RegionProperty("CMD_LEN1", 3);
            this.LFR_Data = new VariableLengthProperty<string>(
                "LFR_Data", 3, 999, 0.5f, false, false, 
                DefaultStringFormatter.StringStringFormatter,
                DefaultStringParser.StringStringParser);

            this.StartRegion(this.CMD_LEN1);
            {
                this.AddProperty(this.LFR_Data);
            }
            this.EndLastRegion();
        }

        private static HexadecimalData ImageStringParser (StringReader reader)
        {
            // Retrieve the table length
            int length = reader.PeekInt(3);

            // Retrieve full command string
            string commandString = reader.ReadString(length);

            return new HexadecimalData(commandString);
        }
    }
}
