namespace Pinpad.Sdk.Commands
{
    /// <summary>
    /// TLE request
    /// </summary>
    internal sealed class TleRequest : PinpadProperties.Refactor.BaseCommand
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
