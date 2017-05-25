using Pinpad.Sdk.Commands;

namespace Pinpad.Sdk.PinpadProperties.Refactor
{
    // TODO: Doc
    public interface ICommand
    {
        string CommandName { get; }
        IContext Context { get; }
    }
}
