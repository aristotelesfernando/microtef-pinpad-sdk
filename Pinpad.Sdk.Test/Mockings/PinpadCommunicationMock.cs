using MicroPos.CrossPlatform;
using Moq;
using Pinpad.Sdk.Model.Pinpad;
using System;
using System.Diagnostics;

namespace Pinpad.Sdk.Test
{
	public class PinpadCommunicationMock : IPinpadCommunication
	{
        
        private IPinpadConnection _connection;
        public IPinpadConnection Connection
        {
            get
            {
                return _connection;
            }
        }
        public string ConnectionName
        {
            get
            {
                return "COM99";
            }
        }
        public PinpadCommunicationMock()
        {
            _connection = new PinpadConnectionMock();
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
