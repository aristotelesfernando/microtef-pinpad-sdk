using Pinpad.Sdk.Pinpad;

namespace Pinpad.Sdk.Test
{
    public class MockedPinpadFacade : IPinpadFacade
    {
        public PinpadCommunication Communication { get; set; }
        public PinpadDisplay Display { get; set; }
        public PinpadEncryption Encryption { get; set; }
        public PinpadInfos Infos { get; set; }
        public PinpadKeyboard Keyboard { get; set; }
        public PinpadPrinter Printer { get; set; }
        public PinpadStorage Storage { get; set; }
        public PinpadTable Table { get; set; }
		public PinpadTransaction TransactionService { get; set; }
	}
}
