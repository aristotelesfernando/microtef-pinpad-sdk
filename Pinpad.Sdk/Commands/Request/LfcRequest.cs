using Pinpad.Sdk.Commands.Context;
using Pinpad.Sdk.Properties;

namespace Pinpad.Sdk.Commands.Request
{
    /// <summary>
    /// Request to verify whether a file exists on pinpad memory or not.
    /// Currenty supported by Ingenico pinpads only.
    /// </summary>
    internal sealed class LfcRequest : BaseCommand
    {
        /// <summary>
        /// Command name, LFC in this case.
        /// </summary>
        public override string CommandName { get { return "LFC"; } }
        /// <summary>
        /// Command length, excluding itself.
        /// </summary>
        public RegionProperty CMD_LEN1 { get; private set; }
        /// <summary>
        /// Name of the file to be verified.
        /// </summary>
        public VariableLengthProperty<string> LFC_FILENAME { get; private set; }

        /// <summary>
        /// Creates a LFC request and it's properties.
        /// </summary>
        public LfcRequest()
            : base(new IngenicoContext())
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
