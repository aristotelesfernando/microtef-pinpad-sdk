using System;
using Pinpad.Sdk.Model;
using NUnit.Framework;
using Moq;

namespace Pinpad.Sdk.Test.Transaction
{
    [TestFixture]
	public class EmvPinReaderTest
	{
        EmvPinReader emvPinReader;
        IPinpadFacade dummyPinpadFacade;
        IPinpadCommunication mockedComm;
        Pin dummyPin;

        [SetUp]
		public void Setup()
		{
            this.mockedComm = new Stubs.PinpadCommunicationStub();
            this.dummyPinpadFacade = Mock.Of<IPinpadFacade>();
            this.emvPinReader = new EmvPinReader();
		}

        [Test]
        public void EmvPinReader_Construction_ShouldNotThrowException ()
        {
            // Act
            EmvPinReader emvReader = new EmvPinReader();
			
            // Assert
			Assert.IsNotNull(emvReader);
        }
        [Test]
        public void EmvPinReaderTest_Read_ShouldThrowException_IfNegativeAmount ()
		{
            // Assert
            Assert.Throws<ArgumentException>(() =>
            {
                // Arrange
                decimal negativeAmount = -1;

                // Act
                emvPinReader.Read(this.mockedComm, negativeAmount, 
                                  out this.dummyPin);
            });
		}
        [Test]
        public void EmvPinReaderTest_Read_ShouldThrowException_IfAmountIsZero ()
        {
            // Assert
            Assert.Throws<ArgumentException>(() =>
            {
                // Act
                emvPinReader.Read(this.mockedComm, 0, out this.dummyPin);
            });
        }
        [Test]
        public void EmvPinReaderTest_Read_should_throw_exception_if_null_PinpadFacade()
        {
            // Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                // Arrange
                IPinpadCommunication nullPinpadCommunication = null;
                decimal amount = 1001;

                // Act
                this.emvPinReader.Read(nullPinpadCommunication, amount, 
                                       out this.dummyPin);
            });
        }
    }
}
