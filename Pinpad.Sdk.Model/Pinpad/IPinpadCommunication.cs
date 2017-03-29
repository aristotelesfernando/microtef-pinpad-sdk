using MicroPos.CrossPlatform;

namespace Pinpad.Sdk.Model.Pinpad
{
    // TODO: Doc
    public interface IPinpadCommunication
    {
        IPinpadConnection Connection { get; }
        string PortName { get; }
        bool OpenPinpadConnection();
        bool ClosePinpadConnection(string message);
        bool Ping();
        bool CancelRequest();
        T SendRequestAndReceiveResponse<T>(object request)
            where T : new();
        bool SendRequestAndVerifyResponseCode(object request);
    }
}
