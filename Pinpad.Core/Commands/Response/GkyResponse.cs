namespace Pinpad.Core.Commands
{
    /// <summary>
    /// GKY response
    /// </summary>
    public class GkyResponse : BaseKeyResponse
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public GkyResponse() {  }

        /// <summary>
        /// Is this a blocking command?
        /// </summary>
        public override bool IsBlockingCommand { get { return true; } }

        /// <summary>
        /// Name of the command
        /// </summary>
        public override string CommandName { get { return "GKY"; } }
    }
}

