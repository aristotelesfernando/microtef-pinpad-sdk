using Pinpad.Sdk.Properties;

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
        public VariableLengthCollectionProperty<string> UPR_REC { get; private set; }

        /// <summary>
        /// Creates the request with all it's properties.
        /// </summary>
        public UprRequest()
        {
            this.CMD_LEN1 = new RegionProperty("CMD_LEN1", 3);
            this.UPR_REC = new VariableLengthCollectionProperty<string>("TLR_REC", 2, 1, 
                UprRequest.PackageSectionSize, DefaultStringFormatter.StringStringFormatter, 
                DefaultStringParser.StringStringParser);

            this.StartRegion(this.CMD_LEN1);
            {
                this.AddProperty(this.UPR_REC);
            }
            this.EndLastRegion();
        }
    }
}
