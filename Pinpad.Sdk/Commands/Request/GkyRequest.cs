namespace Pinpad.Sdk.Commands
{
    /// <summary>
    /// GKY request
    /// </summary>
    internal sealed class GkyRequest : PinpadProperties.Refactor.BaseCommand
    {
        /// <summary>
        /// Name of the command
        /// </summary>
        public override string CommandName { get { return "GKY"; } }

        /// <summary>
        /// Constructor
        /// </summary>
        public GkyRequest() {  }
    }
}
