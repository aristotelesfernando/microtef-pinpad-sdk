using System;
using Pinpad.Sdk.Model;
using NUnit.Framework;
using Moq;

namespace Pinpad.Sdk.Test.Transaction
{
    [TestFixture]
    public class MagneticStripePinReaderTest
    {
        MagneticStripePinReader emvPinReader;
        IPinpadFacade dummyPinpadFacade;
        Pin dummyPin;

        [SetUp]
        public void Setup()
        {
            this.dummyPinpadFacade = Mock.Of<IPinpadFacade>();
            this.emvPinReader = new MagneticStripePinReader();
        }

        [Test]
        public void MagneticStripePinReader_Construction_ShouldNotThrowException ()
        {
            // Act
            MagneticStripePinReader msReader = new MagneticStripePinReader();

            // Assert
			Assert.IsNotNull(msReader);
        }
        [Test]
        public void MagneticStripePinReader_Read_ShouldThrowException_IfPinpadCommunicationIsNull ()
        {
            // Assert
            Assert.Throws<InvalidOperationException>(() =>
            {
                // Arrange
                IPinpadCommunication nullPinpadCommunication = null;

                // Act
                this.emvPinReader.Read(nullPinpadCommunication, 
                                       pan: "1234567890123456", 
                                       amount: 1000, 
                                       pin: out this.dummyPin);
            });
        }
        [Test]
        public void MagneticStripePinReader_Read_ShouldThrowException_IfAmountIsNegative ()
        {
            // Assert
            Assert.Throws<InvalidOperationException>(() =>
            {
                // Arrange
                decimal negativeAmount = -1000;

                // Act
                this.emvPinReader.Read(this.dummyPinpadFacade.Communication, 
                                       pan: "1234567890123456", 
                                       amount: negativeAmount,
                                       pin: out this.dummyPin);
            });
        }
        [Test]
        public void MagneticStripePinReader_Read_ShouldThrowException_IfAmountIsZero ()
        {
            // Assert
            Assert.Throws<InvalidOperationException>(() =>
            {
                // Arrange
                decimal zeroAmount = 0;

                // Act
                this.emvPinReader.Read(this.dummyPinpadFacade.Communication, 
                                       pan: "1234567890123456", 
                                       amount: zeroAmount, 
                                       pin: out this.dummyPin);
            });
        }
        [Test]
        public void MagneticStripePinReader_Read_ShouldThrowException_IfPanIsNull ()
        {
            // Assert
            Assert.Throws<InvalidOperationException>(() =>
            {
                // Arrange
                string nullPan = null;

                // Act
                this.emvPinReader.Read(this.dummyPinpadFacade.Communication, 
                                       pan: nullPan, 
                                       amount: 1000, 
                                       pin: out this.dummyPin);
            });
        }
        [Test]
        public void MagneticStripePinReader_Read_ShouldThrowException_IfEmptyPan ()
        {
            // Assert
            Assert.Throws<InvalidOperationException>(() =>
            {
                // Arrange
                string emptyPan = string.Empty;

                // Act
                this.emvPinReader.Read(this.dummyPinpadFacade.Communication, 
                                       pan: emptyPan, 
                                       amount: 1000, 
                                       pin: out this.dummyPin);
            });
        }
    }
}
