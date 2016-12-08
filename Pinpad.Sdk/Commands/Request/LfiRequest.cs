using Pinpad.Sdk.Properties;

namespace Pinpad.Sdk.Commands.Request
{
    // TODO: Documentar.
    internal sealed class LfiRequest : BaseCommand
    {
        public override string CommandName { get { return "LFI"; } }
        public RegionProperty CMD_LEN1 { get; private set; }
        public RegionProperty LFI_FILENAMELEN { get; private set; }
        public SimpleProperty<string> LFI_FILENAME { get; private set; }
        
        public LfiRequest()
        {
            this.CMD_LEN1 = new RegionProperty("CMD_LEN1", 3);
            this.LFI_FILENAMELEN = new RegionProperty("LFI_FILENAMELEN", 3, false, false);
            this.LFI_FILENAME = new SimpleProperty<string>("LFI_FILENAME", false,
                DefaultStringFormatter.StringStringFormatter,
                DefaultStringParser.StringStringParser);

            this.StartRegion(this.CMD_LEN1);
            {
                this.AddProperty(this.LFI_FILENAMELEN);
                this.AddProperty(this.LFI_FILENAME);
            }
            this.EndLastRegion();

            this.StartRegion(this.LFI_FILENAMELEN);
            {
                this.AddProperty(this.LFI_FILENAME);
            }
            this.EndLastRegion();
        }
    }
}
