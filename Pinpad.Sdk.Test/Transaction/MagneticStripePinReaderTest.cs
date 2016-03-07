using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Pinpad.Sdk.Transaction;
using Pinpad.Sdk.Model;
using Pinpad.Sdk.EmvTable;

namespace Pinpad.Sdk.Test.Transaction
{
    [TestClass]
    public class MagneticStripePinReaderTest
    {
        MagneticStripePinReader reader;
        MockedPinpadFacade pinpadFacade;
        Pin pin;

        [TestInitialize]
        public void Setup()
        {
            this.pinpadFacade = new MockedPinpadFacade();
            this.reader = new MagneticStripePinReader();
        }

        [TestMethod]
        public void MagneticStripePinReader_should_not_throw_exception_on_creation()
        {
            MagneticStripePinReader msReader = new MagneticStripePinReader();
        }

        [TestMethod]
        public void MagneticStripePinReader_should_not_return_null_on_creation()
        {
            MagneticStripePinReader msReader = new MagneticStripePinReader();
            Assert.IsNotNull(msReader);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void MagneticStripePinReader_should_throw_exception_if_null_PinpadFacade()
        {
            this.reader.Read(null, "1234567890123456", 1000, out this.pin);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void MagneticStripePinReader_should_throw_exception_if_negative_amount()
        {
            this.reader.Read(this.pinpadFacade, "1234567890123456", -1000, out this.pin);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void MagneticStripePinReader_should_throw_exception_if_zero_amount()
        {
            this.reader.Read(this.pinpadFacade, "1234567890123456", 0, out this.pin);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void MagneticStripePinReader_should_throw_exception_if_null_pan()
        {
            this.reader.Read(this.pinpadFacade, null, 1000, out this.pin);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void MagneticStripePinReader_should_throw_exception_if_empty_pan()
        {
            this.reader.Read(this.pinpadFacade, string.Empty, 1000, out this.pin);
        }
    }
}
