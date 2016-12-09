using Pinpad.Sdk.Properties;
using System;

namespace Pinpad.Sdk.Commands.Request
{
    // TODO: Documentar.
    internal sealed class LfcRequest : BaseCommand
    {
        public override string CommandName { get { return "LFC"; } }
        public RegionProperty CMD_LEN1 { get; private set; }
        public VariableLengthProperty<string> LFC_FILENAME { get; private set; }

        public LfcRequest()
        {
            this.CMD_LEN1 = new RegionProperty("CMD_LEN1", 3);
            this.LFC_FILENAME = new VariableLengthProperty<string>("LFC_FILENAME", 3,
                64, 1, false, false,
                DefaultStringFormatter.StringStringFormatter,
                DefaultStringParser.StringStringParser);

            this.StartRegion(this.CMD_LEN1);
            {
                this.AddProperty(this.LFC_FILENAME);
            }
            this.EndLastRegion();
        }
    }
}
