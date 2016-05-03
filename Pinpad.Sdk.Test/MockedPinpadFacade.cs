using System;

namespace Pinpad.Sdk.Test
{
    public class MockedPinpadFacade : IPinpadFacade
    {
        public PinpadCommunication Communication { get; set; }

		public PinpadConnection Connection { get; set; }
		public PinpadDisplay Display { get; set; }
        public PinpadInfos Infos { get; set; }
        public PinpadKeyboard Keyboard { get; set; }
        public PinpadTable Table { get; set; }
		public PinpadTransaction TransactionService { get; set; }
	}
}
