using System;
using Microtef.CrossPlatform.TypeCode;
using Microtef.CrossPlatform;

namespace Pinpad.Sdk.Test.Stubs
{
    public class PinpadConnectionStub : IPinpadConnection
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
		public int ReadTimeout
		{
			get { return 1000; }
			set { }
		}
		public int WriteTimeout
		{
			get { return 1000; }
			set { }
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
