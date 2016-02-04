using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PinPadSDK.PinPad;
using Moq;
using CrossPlatformBase;
using Pinpad.Sdk.Transaction;
using Pinpad.Sdk.Model;

namespace Pinpad.Sdk.Test.Transaction
{
    [TestClass]
    public class MagneticStripePinReaderTest
    {
        PinPadFacade pinpadFacade;
        string pan;
        long amount;
        string passwordLabel;
        Pin pin;

        [TestInitialize]
        public void Setup()
        {
            Mock<IPinPadConnection> mockedConn = new Mock<IPinPadConnection>();
            this.pinpadFacade = new PinPadFacade(mockedConn.Object);

            this.pan = "1234567890123456";
            this.amount = 10000;
            this.passwordLabel = "Senha:";
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void MagneticStripePinReader_should_throw_exception_if_negative_amount()
        {
            this.amount = -10;
            MagneticStripePinReader.Read(this.pinpadFacade, this.pan, this.amount, out this.pin);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void MagneticStripePinReader_should_throw_exception_if_zero_amount()
        {
            this.amount = 0;
            MagneticStripePinReader.Read(this.pinpadFacade, this.pan, this.amount, out this.pin);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void MagneticStripePinReader_should_throw_exception_if_null_pan()
        {
            this.pan = null;
            MagneticStripePinReader.Read(this.pinpadFacade, this.pan, this.amount, out this.pin);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void MagneticStripePinReader_should_throw_exception_if_empty_pan()
        {
            this.pan = string.Empty;
            MagneticStripePinReader.Read(this.pinpadFacade, this.pan, this.amount, out this.pin);
        }
    }
}
