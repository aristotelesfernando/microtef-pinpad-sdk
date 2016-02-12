using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pinpad.Sdk.Transaction;
using CrossPlatformBase;
using Moq;
using Pinpad.Sdk.Model;
using Pinpad.Sdk.EmvTable;
using Pinpad.Core.Commands;
using Pinpad.Core.Pinpad;

namespace Pinpad.Sdk.Test.Transaction
{
    [TestClass]
    public class EmvPinReaderTest
    {
		Mock<PinpadFacade> mockedFacade;
        Pin pin;

        [TestInitialize]
        public void Setup()
        {
            Mock<IPinPadConnection> mockedConn = new Mock<IPinPadConnection>();
			mockedFacade = new Mock<PinpadFacade>();

			// Examples of GIN command:
			// Setup request
			GinRequest request = new GinRequest();
			request.GIN_ACQIDX.Value = 0;

			// Setup response
			GinResponse response = new GinResponse();
			response.CommandString = "GIN000100GERTEC              MOBI PIN 10         0040_0022_0026_0110@1.08001.03 150113       8000021509010882";
        }

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void EmvPinReaderTest_Read_should_throw_exception_if_negative_amount()
		{
			EmvPinReader.Read(null, -1, out this.pin);
		}
    }
}
