namespace Pinpad.Core.Commands
{
    /// <summary>
    /// TLE request
    /// </summary>
    public class TleRequest : BaseCommand
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public TleRequest() { }

        /// <summary>
        /// Name of the command
        /// </summary>
        public override string CommandName { get { return "TLE"; } }
    }
}
