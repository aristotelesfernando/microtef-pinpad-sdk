using Pinpad.Sdk.Properties;

namespace Pinpad.Sdk.Commands.Request
{
    // TODO: Documentar.
    internal sealed class LfeRequest : BaseCommand
    {
        public override string CommandName { get { return "LFE"; } }
        public RegionProperty CMD_LEN1 { get; set; }

        public LfeRequest()
        {
            this.CMD_LEN1 = new RegionProperty("CMD_LEN1", 3);
        }
    }
}
