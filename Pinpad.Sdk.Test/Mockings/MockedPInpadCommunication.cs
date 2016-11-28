using MicroPos.CrossPlatform;
using Moq;

namespace Pinpad.Sdk.Test
{
	public class MockedPinpadCommunication : PinpadCommunication
	{
		public MockedPinpadCommunication ()
			: base (new PinpadConnection(Mock.Of<IPinpadConnection>()))
		{

		}
	}
}
