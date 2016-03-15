namespace Pinpad.Core.Commands
{
    /// <summary>
    /// Base Controller for Stone command Requests
    /// </summary>
    public abstract class BaseStoneRequest : BaseCommand
    {
        /// <summary>
        /// Minimum stone version required for the request
        /// </summary>
        public abstract int MinimumStoneVersion { get; }
    }
}
