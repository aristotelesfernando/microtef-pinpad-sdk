namespace Pinpad.Core.Commands.Stone
{
    /// <summary>
    /// GKE response
    /// </summary>
    public class GkeResponse : BaseKeyResponse {
        /// <summary>
        /// Constructor
        /// </summary>
        public GkeResponse() {
        }

        /// <summary>
        /// Is this a blocking command?
        /// </summary>
        public override bool IsBlockingCommand {
            get { return true; }
        }

        /// <summary>
        /// Name of the command
        /// </summary>
        public override string CommandName { get { return "GKE"; } }
    }
}
