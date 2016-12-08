namespace Pinpad.Sdk.Commands
{
    /// <summary>
    /// TLE request
    /// </summary>
    internal sealed class TleRequest : BaseCommand
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
