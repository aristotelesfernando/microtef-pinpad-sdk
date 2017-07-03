using Pinpad.Sdk.Model;
using System;
using Microtef.CrossPlatform;
using System.Diagnostics;

namespace Pinpad.Sdk.Test.Stubs
{
    public class PinpadCommunicationStub : IPinpadCommunication
    {
		public IPinpadConnection Connection
		{
			get
			{
				throw new NotImplementedException();
			}
		}
		public string ConnectionName
		{
			get
			{
				return "COM99";
			}
		}

		public bool CancelRequest()
		{
			Debug.WriteLine("Cancelling request on pinpad.");
			return true;
		}
		public bool ClosePinpadConnection(string message)
		{
			Debug.WriteLine("Closing pinpad connection.");
			return true;
		}
		public bool OpenPinpadConnection()
		{
			Debug.WriteLine("Opening pinpad connection.");
			return true;
		}
		public bool Ping()
		{
			Debug.WriteLine("Pinging pinpad.");
			return true;
		}
		public T SendRequestAndReceiveResponse<T>(object request) where T : new()
		{
			Debug.WriteLine("Sending request...");
			Debug.WriteLine("... and receiving response.");
			return default(T);
		}
		public bool SendRequestAndVerifyResponseCode(object request)
		{
			Debug.WriteLine("Sending request...");
			Debug.WriteLine("... and verifying response.");
			return true;
		}
		public void SetTimeout(int writeTimeout = 2000, int readTimeout = 2000)
        {
            this.Connection.WriteTimeout = writeTimeout;
            this.Connection.ReadTimeout = readTimeout;
        }
    }
}
