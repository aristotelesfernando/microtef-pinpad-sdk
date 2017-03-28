using MicroPos.CrossPlatform;
using Moq;
using Pinpad.Sdk.Model.Pinpad;
using System;
using System.Diagnostics;

namespace Pinpad.Sdk.Test
{
	public class PinpadCommunicationMock : IPinpadCommunication
	{
        public IBasicPinpadConnection PinpadConnection
        {
            get
            {
                throw new NotImplementedException();
            }
        }
        public string PortName
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
    }
}
