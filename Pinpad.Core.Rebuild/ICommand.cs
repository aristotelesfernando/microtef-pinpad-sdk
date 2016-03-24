namespace Pinpad.Core.Rebuild
{
    public interface ICommand
    {
		IContext CommandContext { get; }
    }
}
