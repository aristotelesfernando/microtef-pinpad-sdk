using System;
using Pinpad.Sdk.Model;
using NUnit.Framework;

namespace Pinpad.Sdk.Test.Transaction
{
    [TestFixture]
    public class PinReaderTest
    {
        PinReader pinReader;

        [SetUp]
        public void Setup()
        {
            this.pinReader = new PinReader(new Stubs.PinpadCommunicationStub());
        }

        [Test]
        public void PinReader_Construction_ShouldNotThrowException ()
        {
            // Assert
            Assert.IsNotNull(pinReader);
        }
        [Test]
        public void PinReader_Read_ShouldThrowException_IfCardTypeIsUndefined ()
        {
            // Assert
            Assert.Throws<ArgumentException>(() =>
            {
                // Arrange
                CardType undefinedCardType = CardType.Undefined;

                // Assert
                pinReader.Read(undefinedCardType, 
                               amount: 1000, 
                               pan: "12345678901234567");
            });
        }
        [Test]
        public void PinReader_Read_ShouldThrowException_IfCardTypeIsInvalid ()
        {
            // Assert
            Assert.Throws<ArgumentException>(() =>
            {
                // Arrange
                CardType invalidCardType = (CardType)666;

                // Assert
                pinReader.Read(invalidCardType,
                               amount: 1000,
                               pan: "12345678901234567");
            });
        }
        [Test]
        public void PinReader_Read_ShouldThrowException_IfAmountIsNegative ()
        {
            // Assert
            Assert.Throws<ArgumentException>(() =>
            {
                // Arrange
                decimal negativeAmount = -666;

                // Act
                pinReader.Read(CardType.Emv,
                               negativeAmount,
                               pan: "12345678901234567");
            });
        }
        [Test]
        public void PinReader_Read_ShouldThrowException_IfAmountIsZero ()
        {
            // Assert
            Assert.Throws<ArgumentException>(() =>
            {
                // Arrange
                decimal zeroAmount = 0;

                // Act
                pinReader.Read(CardType.Emv, 
                               zeroAmount, 
                               pan: "12345678901234567");
            });
        }
        [Test]
        public void PinReader_Read_ShouldThrowException_IfPanIsNull ()
        {
            // Assert
            Assert.Throws<ArgumentException>(() =>
            {
                // Arrange
                string nullPan = null;

                // Act
                pinReader.Read(CardType.MagneticStripe, 
                               amount: 1000, 
                               pan: nullPan);
            });
        }
        [Test]
        public void PinReader_Read_ShouldThrowException_IfPanIsEmpty ()
        {
            // Assert
            Assert.Throws<ArgumentException>(() =>
            {
                // Arrange
                string emptyPan = string.Empty;

                // Act
                pinReader.Read(CardType.MagneticStripe, 
                               amount: 1000, 
                               pan: emptyPan);
            });
        }
        [Test]
        public void PinReader_Read_ShouldThrowException_IfPinpadCommunicationIsNull ()
        {
            // Assert
            Assert.Throws<InvalidOperationException>(() =>
            {
                // Arrange 
                IPinpadCommunication nullPinpadCommunication = null;

                // Act
                PinReader pinReader = new PinReader(nullPinpadCommunication);
            });
        }
    }
}
