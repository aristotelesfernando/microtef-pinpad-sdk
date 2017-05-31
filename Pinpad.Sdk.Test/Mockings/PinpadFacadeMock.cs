using System;
using Pinpad.Sdk.Model;
using Pinpad.Sdk.Model.Pinpad;

namespace Pinpad.Sdk.Test
{
    public class MockedPinpadFacade : IPinpadFacade
    {
        public IPinpadCommunication Communication { get; set; }
		public PinpadConnectionProvider Connection { get; set; }
		public IPinpadDisplay Display { get; set; }
        public IPinpadInfos Infos { get; set; }
        public IPinpadKeyboard Keyboard { get; set; }
        public IPinpadPrinter Printer { get; internal set; }
        public IPinpadUpdateService UpdateService { get; internal set; }

        public PinpadTable Table { get; set; }
		public PinpadTransaction TransactionService { get; set; }

    }
}
