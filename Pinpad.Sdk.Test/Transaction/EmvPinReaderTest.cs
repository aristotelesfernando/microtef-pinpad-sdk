using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pinpad.Sdk.Transaction;
using PinPadSDK.PinPad;
using CrossPlatformBase;
using Moq;
using Pinpad.Sdk.Model;

namespace Pinpad.Sdk.Test.Transaction
{
    [TestClass]
    public class EmvPinReaderTest
    {
        PinPadFacade pinpadFacade;
        Pin pin;

        [TestInitialize]
        public void Setup()
        {
            Mock<IPinPadConnection> mockedConn = new Mock<IPinPadConnection>();
            this.pinpadFacade = new PinPadFacade(mockedConn.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void EmvPinReaderTest_Read_should_throw_exception_if_negative_amount()
        {
            EmvPinReader.Read(this.pinpadFacade, -1, out this.pin);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void EmvPinReaderTest_Read_should_throw_exception_if_zero_amount()
        {
            EmvPinReader.Read(this.pinpadFacade, 0, out this.pin);
        }
    }
}
