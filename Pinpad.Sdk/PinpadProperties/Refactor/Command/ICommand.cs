using Pinpad.Sdk.Commands;

namespace Pinpad.Sdk.PinpadProperties.Refactor.Command
{
    // TODO: Doc
    public interface ICommand
    {
        string CommandName { get; }
        IContext Context { get; }
    }
}
