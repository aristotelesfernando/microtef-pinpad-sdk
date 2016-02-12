using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using CrossPlatformBase;
using Pinpad.Sdk.Transaction;
using Pinpad.Sdk.Model;
using Pinpad.Sdk.EmvTable;

namespace Pinpad.Sdk.Test.Transaction
{
    [TestClass]
    public class MagneticStripePinReaderTest
    {
        PinpadFacade pinpadFacade;
        string pan;
        long amount;
        string passwordLabel;
        Pin pin;

        [TestInitialize]
        public void Setup()
        {
			//Mock<IPinPadConnection> mockedConn = new Mock<IPinPadConnection>();
			//this.pinpadFacade = new PinpadFacade(mockedConn.Object);

            this.pan = "1234567890123456";
            this.amount = 10000;
            this.passwordLabel = "Senha:";
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void MagneticStripePinReader_should_throw_exception_if_negative_amount()
        {
            this.amount = -10;
            MagneticStripePinReader.Read(null, this.pan, this.amount, out this.pin);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void MagneticStripePinReader_should_throw_exception_if_zero_amount()
        {
            this.amount = 0;
            MagneticStripePinReader.Read(null, this.pan, this.amount, out this.pin);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void MagneticStripePinReader_should_throw_exception_if_null_pan()
        {
            this.pan = null;
            MagneticStripePinReader.Read(null, this.pan, this.amount, out this.pin);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void MagneticStripePinReader_should_throw_exception_if_empty_pan()
        {
            this.pan = string.Empty;
            MagneticStripePinReader.Read(null, this.pan, this.amount, out this.pin);
        }
    }
}
