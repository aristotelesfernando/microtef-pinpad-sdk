using MicroPos.CrossPlatform;

namespace Pinpad.Sdk.Test
{
    public class PinpadConnectionMock : IPinpadConnection
    {
        // Members
        public int BytesToRead { get { return 1; } }

        public CommunicationType CommunicationType
        {
            get
            {
                return CommunicationType.SerialPort;
            }
        }

        public string ConnectionName { get { return "COM7"; } }
        public bool IsOpen { get { return true; } }
        public int _readTimeout = 1000;
        public int ReadTimeout
        {
            get { return _readTimeout; }
            set { _readTimeout = value; }
        }
        public int _writeTimeout = 1000;
        public int WriteTimeout
        {
            get { return _writeTimeout; }
            set { _writeTimeout = value; }
        }

        // Methods
        public void Close() { }
        public void DiscardInBuffer() { }
        public void DiscardOutBuffer() { }
        public void Dispose() { }
        public void Open() { }
        public byte ReadByte() { return 0x00; }
        public void Write(byte[] buffer) { }
        public void WriteByte(byte data) { }
    }
}
