using Pinpad.Sdk.PinpadProperties.Refactor.Command;

namespace Pinpad.Sdk.Commands
{
    /// <summary>
    /// TLE request
    /// </summary>
    internal sealed class TleRequest : BaseCommand
    {
        /// <summary>
        /// Name of the command
        /// </summary>
        public override string CommandName { get { return "TLE"; } }

        /// <summary>
        /// Constructor
        /// </summary>
        public TleRequest() { }
    }
}
