using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PinPadSDK.PinPad;
using Moq;
using Pinpad.Sdk.Transaction;
using CrossPlatformBase;
using Pinpad.Sdk.Model.TypeCode;

namespace Pinpad.Sdk.Test.Transaction
{
    [TestClass]
    public class PinReaderTest
    {
        PinPadFacade pinpadFacade;

        [TestInitialize]
        public void Setup()
        {
            Mock<IPinPadConnection> mockedConn = new Mock<IPinPadConnection>();
            this.pinpadFacade = new PinPadFacade(mockedConn.Object);
        }

        [TestMethod]
        public void PinReader_should_not_return_null()
        {
            PinReader auth = new PinReader(this.pinpadFacade, CardType.Emv);
            Assert.IsNotNull(auth);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void PinReader_should_throw_exception_if_invalid_CardType()
        {
            PinReader auth = new PinReader(this.pinpadFacade, CardType.Undefined);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void PinReader_should_throw_exception_if_null_PinpadFacade()
        {
            this.pinpadFacade = null;
            PinReader auth = new PinReader(this.pinpadFacade, CardType.MagneticStripe);
        }
    }
}
