using Pinpad.Sdk.PinpadProperties.Refactor.Command;

namespace Pinpad.Sdk.Commands.Request
{
    /// <summary>
    /// Update End request.
    /// Signals when all <see cref="UprRequest"/> have been sent and the update flow can be finished.
    /// </summary>
    public sealed class UpeRequest : BaseCommand
    {
        /// <summary>
        /// Name of this command.
        /// </summary>
        public override string CommandName { get { return "UPE"; } }
    }
}
