using Pinpad.Sdk.Commands.Context;
using Pinpad.Sdk.PinpadProperties.Refactor.Command;
using Pinpad.Sdk.PinpadProperties.Refactor.Property;

namespace Pinpad.Sdk.Commands.Request
{
    /// <summary>
    /// LFE - Load File Ends.
    /// Ends loading a file, that was already initialized with <see cref="LfiRequest"/>
    /// and loaded with <see cref="LfrRequest"/>.
    /// </summary>
    internal sealed class LfeRequest : BaseCommand
    {
        /// <summary>
        /// Command name, LFE in this case.
        /// </summary>
        public override string CommandName { get { return "LFE"; } }
        /// <summary>
        /// Command length, excluding itself.
        /// </summary>
        public RegionProperty CMD_LEN1 { get; set; }

        /// <summary>
        /// Creates a LFE request.
        /// </summary>
        public LfeRequest()
            : base (new IngenicoContext())
        {
            this.CMD_LEN1 = new RegionProperty("CMD_LEN1", 3);
        }
    }
}
