using Pinpad.Sdk.PinpadProperties.Refactor.Command;

namespace Pinpad.Sdk.Commands.Request
{
    /// <summary>
    /// Request of a Get Application Version. It gets the version of the current application running in the 
    /// device.
    /// This is a command made by Stone Payments, and it's only available for Stone Wi-Fi Pinpads.
    /// </summary>
    public sealed class GavRequest : BaseCommand
    {
        /// <summary>
        /// GAV command name as string.
        /// </summary>
        public override string CommandName { get { return "GAV"; } }
    }
}
