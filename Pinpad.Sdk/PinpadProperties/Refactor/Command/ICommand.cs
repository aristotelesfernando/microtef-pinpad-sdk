using Pinpad.Sdk.Commands;

namespace Pinpad.Sdk.PinpadProperties.Refactor.Command
{
    /// <summary>
    /// Define what every command sent to the pinpad shall have.
    /// </summary>
    public interface ICommand
    {
        /// <summary>
        /// Name of the command to be sent to the pinpad.
        /// </summary>
        string CommandName { get; }
        /// <summary>
        /// Context in which the command is created.
        /// Contains a set of rules to read and understand the command.
        /// </summary>
        IContext Context { get; }
    }
}
