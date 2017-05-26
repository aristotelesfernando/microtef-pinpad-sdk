using Pinpad.Sdk.Commands.Context;
using Pinpad.Sdk.PinpadProperties.Refactor.Command;
using Pinpad.Sdk.PinpadProperties.Refactor.Formatter;
using Pinpad.Sdk.PinpadProperties.Refactor.Parser;
using Pinpad.Sdk.PinpadProperties.Refactor.Property;

namespace Pinpad.Sdk.Commands.Request
{
    /// <summary>
    /// LFI - Load File Initialization.
    /// Initialize loading a file into pinpad memory.
    /// </summary>
    internal sealed class LfiRequest : BaseCommand
    {
        /// <summary>
        /// Command name, LFI in this case.
        /// </summary>
        public override string CommandName { get { return "LFI"; } }
        /// <summary>
        /// Command length, excluding itself.
        /// </summary>
        public RegionProperty CMD_LEN1 { get; private set; }
        /// <summary>
        /// File to be put into pinpad memory.
        /// </summary>
        public VariableLengthProperty<string> LFI_FILENAME { get; private set; }

        /// <summary>
        /// Creates a LFI request and it's properties.
        /// </summary>
        public LfiRequest()
            : base (new IngenicoContext())
        {
            this.CMD_LEN1 = new RegionProperty("CMD_LEN1", 3);
            this.LFI_FILENAME = new VariableLengthProperty<string>("LFI_FILENAME", 3,
                64, 1f, false, false, StringFormatter.StringStringFormatter, StringParser.StringStringParser);

            this.StartRegion(this.CMD_LEN1);
            {
                this.AddProperty(this.LFI_FILENAME);
            }
            this.EndLastRegion();
        }
    }
}
