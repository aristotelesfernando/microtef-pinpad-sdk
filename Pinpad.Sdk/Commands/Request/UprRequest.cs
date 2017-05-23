using Pinpad.Sdk.Properties;

namespace Pinpad.Sdk.Commands.Request
{
    // TODO: Doc
    public sealed class UprRequest : BaseCommand
    {
        public const int PackageSectionSize = 900;
        private const int TableLengthSize = 3;

        public override string CommandName { get { return "UPR"; } }
        ///// <summary>
        ///// Length of the first region of the command
        ///// </summary>
        //public RegionProperty CMD_LEN1 { get; private set; }
        /// <summary>
        /// Table records
        /// </summary>
        public VariableLengthProperty<string> UPR_REC { get; private set; }

        /// <summary>
        /// Creates the request with all it's properties.
        /// </summary>
        public UprRequest()
        {
            this.UPR_REC = new VariableLengthProperty<string>("TLR_REC", 3, UprRequest.PackageSectionSize, 1, 
                false, false,
                DefaultStringFormatter.StringStringFormatter, 
                DefaultStringParser.StringStringParser);

            this.AddProperty(this.UPR_REC);
        }   
    }
}
