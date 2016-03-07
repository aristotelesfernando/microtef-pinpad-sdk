using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Pinpad.Sdk.Transaction;
using Pinpad.Sdk.Model.TypeCode;
using Pinpad.Sdk.EmvTable;
using Pinpad.Core.Pinpad;
using Pinpad.Core.Commands;
using MicroPos.CrossPlatform;

namespace Pinpad.Sdk.Test.Transaction
{
    [TestClass]
    public class PinReaderTest
    {
        PinReader pinReader;

        [TestInitialize]
        public void Setup()
        {
            this.pinReader = new PinReader(new MockedPinpadFacade());
        }

        [TestMethod]
        public void PinReader_should_not_return_null()
        {
            Assert.IsNotNull(pinReader);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void PinReader_reading_should_throw_exception_if_undefined_CardType()
        {
            pinReader.Read(CardType.Undefined, 1000, "12345678901234567");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void PinReader_reading_should_throw_exception_if_invalid_CardType()
        {
            pinReader.Read((CardType)666, 1000, "12345678901234567");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void PinReader_reading_should_throw_exception_if_negative_amount()
        {
            pinReader.Read((CardType)666, -1000, "12345678901234567");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void PinReader_reading_should_throw_exception_if_amount_is_zero()
        {
            pinReader.Read((CardType)666, 0, "12345678901234567");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void PinReader_reading_should_throw_exception_if_null_pan()
        {
            pinReader.Read(CardType.MagneticStripe, 1000, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void PinReader_reading_should_throw_exception_if_pan_is_empty()
        {
            pinReader.Read(CardType.MagneticStripe, 1000, string.Empty);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void PinReader_should_throw_exception_if_null_PinpadFacade()
        {
			PinReader pinReader = new PinReader(null);
        }
    }
}
