using System;
using Pinpad.Sdk.Model;

namespace Pinpad.Sdk.Test
{
    public class MockedPinpadFacade : IPinpadFacade
    {
        public PinpadCommunication Communication { get; set; }
		public PinpadConnection Connection { get; set; }
		public IPinpadDisplay Display { get; set; }
        public IPinpadInfos Infos { get; set; }
        public IPinpadKeyboard Keyboard { get; set; }

        public IPinpadPrinter Printer { get; internal set; }

        public PinpadTable Table { get; set; }
		public PinpadTransaction TransactionService { get; set; }
	}
}
