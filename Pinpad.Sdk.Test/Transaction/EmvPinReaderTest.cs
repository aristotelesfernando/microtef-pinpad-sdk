using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pinpad.Sdk.Model;
using Pinpad.Sdk.Transaction;

namespace Pinpad.Sdk.Test.Transaction
{
	[TestClass]
	public class EmvPinReaderTest
	{
        EmvPinReader reader;
        MockedPinpadFacade mockedFacade;
		MockedPinpadCommunication mockedComm;
		Pin pin;

		[TestInitialize]
		public void Setup()
		{
			this.mockedComm = new MockedPinpadCommunication();
            this.mockedFacade = new MockedPinpadFacade();
            this.reader = new EmvPinReader();
		}

        [TestMethod]
        public void EmvPinReader_should_not_throw_exception_on_creation()
        {
            EmvPinReader emvReader = new EmvPinReader();
        }

        [TestMethod]
        public void EmvPinReader_should_not_return_null_on_creation()
        {
            // Arrange
            EmvPinReader emvReader = new EmvPinReader();

            // Assert
            Assert.IsNotNull(emvReader);
        }

        [TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void EmvPinReaderTest_Read_should_throw_exception_if_negative_amount()
		{
            // Act & Assert
			reader.Read(this.mockedComm, -1, out this.pin);
		}

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void EmvPinReaderTest_Read_should_throw_exception_if_amount_is_zero()
        {
            // Act & Assert
            reader.Read(this.mockedComm, 0, out this.pin);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void EmvPinReaderTest_Read_should_throw_exception_if_null_PinpadFacade()
        {
            // Act & Assert
            reader.Read(null, 10001, out this.pin);
        }
    }
}
