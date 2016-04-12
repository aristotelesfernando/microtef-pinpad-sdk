using Pinpad.Core.Pinpad;

namespace Pinpad.Sdk.Test
{
	public class MockedPinpadCommunication : PinpadCommunication
	{
		public MockedPinpadCommunication ()
			: base (new MockedPinpadConnection())
		{

		}
	}
}
