using System;
using Pinpad.Core.Pinpad;
using Pinpad.Sdk.EmvTable;

namespace Pinpad.Sdk.Test
{
    public class MockedPinpadFacade : IPinpadFacade
    {
        //public MockedPinpadFacade(MockedPinpadConnection mockedConn) : base(mockedConn) { }

        public PinpadCommunication Communication { get; set; }
        public PinpadDisplay Display { get; set; }
        public PinpadEncryption Encryption { get; set; }
        public PinpadInfos Infos { get; set; }
        public PinpadKeyboard Keyboard { get; set; }
        public PinpadPrinter Printer { get; set; }
        public PinpadStorage Storage { get; set; }
        public PinpadTable Table { get; set; }
    }
}
