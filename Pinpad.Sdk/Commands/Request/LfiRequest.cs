using Pinpad.Sdk.Properties;

namespace Pinpad.Sdk.Commands.Request
{
    // TODO: Documentar.
    internal sealed class LfiRequest : BaseCommand
    {
        public override string CommandName { get { return "LFI"; } }
        public RegionProperty CMD_LEN1 { get; private set; }
        public VariableLengthProperty<string> LFI_FILENAME { get; private set; }
        
        public LfiRequest()
        {
            this.CMD_LEN1 = new RegionProperty("CMD_LEN1", 3);
            this.LFI_FILENAME = new VariableLengthProperty<string>("LFI_FILENAME", 3,
                64, 1f, false, false,
                DefaultStringFormatter.StringStringFormatter,
                DefaultStringParser.StringStringParser);

            this.StartRegion(this.CMD_LEN1);
            {
                this.AddProperty(this.LFI_FILENAME);
            }
            this.EndLastRegion();
        }
    }
}
