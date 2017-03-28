using MicroPos.CrossPlatform;

namespace Pinpad.Sdk.Model.Pinpad
{
    // TODO: Doc
    public interface IBasicPinpadConnection
    {
        IPinpadConnection Connection { get; }
        bool IsOpen { get; }
        void Open(string portName);
        void Close();
    }
}
