namespace Pinpad.Sdk.Commands
{
    /// <summary>
    /// GKY request
    /// </summary>
    internal class GkyRequest : BaseCommand
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public GkyRequest() {  }

        /// <summary>
        /// Name of the command
        /// </summary>
        public override string CommandName { get { return "GKY"; } }
    }
}
