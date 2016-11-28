using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pinpad.Sdk.Commands;
using Pinpad.Sdk.Properties;
using Pinpad.Sdk.Transaction.Mapper.MagneticStripe;
using System;

namespace Pinpad.Sdk.Test.Mapper.MagneticStripe
{
    [TestClass]
    public class DefaultMagneticStripeTrackReaderTest
    {
        private DefaultMagneticStripeTrackReader defaultReader;

        [TestInitialize]
        public void Setup()
        {
            this.defaultReader = new DefaultMagneticStripeTrackReader();
        }

        [TestMethod]
        public void DefaultMagneticStripeTrackReader_MapPan_ShouldMatchPan_IfCorrectParameters()
        {
            // Arrange
            string track2 = "4761739001010036=23032011184404889";
            string expectedPan = "4761739001010036";
            char expectedFieldSeparator = '=';

            // Act
            string mappedPan = this.defaultReader.MapPan(track2, expectedFieldSeparator);

            // Assert
            Assert.AreEqual(expectedPan, mappedPan);
        }
        [TestMethod]
        public void DefaultMagneticStripeTrackReader_MapCadholderName_ShouldMatchCardholderName_IfCorrectParameters ()
        {
            // Arrange
            string track1 = "4761739001010036^TESTING CARD MAPPER^123456789987456321";
            string expectedCardholderName = "TESTING CARD MAPPER";
            char expectedFieldSeparator = '^';

            // Act
            string mappedCardholderName = this.defaultReader.MapCardholderName(track1, expectedFieldSeparator);

            // Assert
            Assert.AreEqual(expectedCardholderName, mappedCardholderName);
        }
        [TestMethod]
        public void DefaultMagneticStripeTrackReader_MapExpirationDate_ShouldMatchExpirationDate_IfCorrectParameters ()
        {
            // Arrange
            string track2 = "4761739001010036=23032011184404889";
            DateTime expectedExpirationDate = new DateTime(2023, 3, 1);
            char expectedFieldSeparator = '=';

            // Act
            DateTime mappedExpirationDate = this.defaultReader.MapExpirationDate(track2, expectedFieldSeparator);

            // Assert
            Assert.AreEqual(expectedExpirationDate, mappedExpirationDate);
        }
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void DefaultMagneticStripeTrackReader_MapValidTrack_ShouldThrowException_IfInvalidTrack ()
        {
            // Arrange
            GcrResponse response = new GcrResponse();

            // Act && Assert
            this.defaultReader.MapValidTrack(response);
        }
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void DefaultMagneticStripeTrackReader_GetFieldSeparator_ShouldThrowException_IfInvalidTrack()
        {
            // Arrange
            string track = "hue br";

            // Act && Assert
            this.defaultReader.GetFieldSeparator(track);
        }
        [TestMethod]
        public void DefaultMagneticStripeTrackReader_GetFieldSeparator_ShouldReturnTrack1FieldSeparator_IfTrack1IsSent ()
        {
            // Arrange
            string track1 = "4761739001010036^TESTING CARD MAPPER^123456789987456321";
            char expectedFieldSeparator = '^';

            // Act
            char mappedSeparator = this.defaultReader.GetFieldSeparator(track1);

            // Assert
            Assert.AreEqual(expectedFieldSeparator, mappedSeparator);
        }
        [TestMethod]
        public void DefaultMagneticStripeTrackReader_GetFieldSeparator_ShouldReturnTrack2FieldSeparator_IfTrack2IsSent ()
        {
            // Arrange
            string track2 = "4761739001010036=23032011184404889";
            char expectedFieldSeparator = '=';

            // Act
            char mappedSeparator = this.defaultReader.GetFieldSeparator(track2);

            // Assert
            Assert.AreEqual(expectedFieldSeparator, mappedSeparator);
        }
        [TestMethod]
        public void DefaultMagneticStripeTrackReader_MapCarholderName_ShouldReturnEmptyCardholderName_IfTrack1IsEmpty ()
        {
            // Arrange
            string track2 = "4761739001010036=23032011184404889";
            string expectedCardholderName = string.Empty;
            char expectedFieldSeparator = '=';

            // Act
            string mappedCardholderName = this.defaultReader.MapCardholderName(track2, expectedFieldSeparator);

            // Assert
            Assert.AreEqual(expectedCardholderName, mappedCardholderName);
        }
        [TestMethod]
        public void DefaultMagneticStripeTrackReader_MapCarholderName_ShouldNotReturnNullOrEmptyString_IfTrack1IsCorrect ()
        {
            // Arrange
            string track1 = "4761739001010036^TESTING CARD MAPPER^123456789987456321";
            char expectedFieldSeparator = '^';

            // Act
            bool cardholderExist = string.IsNullOrEmpty(this.defaultReader.MapCardholderName(track1, expectedFieldSeparator)) == false;

            // Assert
            Assert.IsTrue(cardholderExist);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void DefaultMagneticStripeTrackReader_MapCarholderName_ShouldThrowException_IfInvalidFieldSeparator ()
        {
            // Arrange
            string track1 = "4761739001010036^TESTING CARD MAPPER^123456789987456321";
            char invalidFieldSeparator = '.';

            // Act && Assert
            DateTime expDate = this.defaultReader.MapExpirationDate(track1, invalidFieldSeparator);
        }
        [TestMethod]
        public void DefaultMagneticStripeTrackReader_MapServiceCode_ShouldReturnExpectedCodeFromServiceCode_IfParametersAreValid ()
        {
            // Arrange
            string track1 = "B4205932010016391^ /^2309121000000000000000225000000";
            char expectedFieldSeparator = '^';
            string expectedCode = "121";

            // Act
            ServiceCode mappedServiceCode = this.defaultReader.MapServiceCode(track1, expectedFieldSeparator);

            // Assert
            Assert.AreEqual(expectedCode, mappedServiceCode.Code);
        }
        [TestMethod]
        public void DefaultMagneticStripeTrackReader_MapServiceCode_ShouldReturnIsEmvAsFalse_IfParametersAreValid ()
        {
            // Arrange
            string track1 = "B4205932010016391^ /^2309121000000000000000225000000";
            char expectedFieldSeparator = '^';

            // Act
            ServiceCode mappedServiceCode = this.defaultReader.MapServiceCode(track1, expectedFieldSeparator);

            // Assert
            Assert.IsFalse(mappedServiceCode.IsEmv);
        }
        [TestMethod]
        public void DefaultMagneticStripeTrackReader_MapServiceCode_ShouldReturnIsPinMandatoryAsFalse_IfParametersAreValid()
        {
            // Arrange
            string track1 = "B4205932010016391^ /^2309121000000000000000225000000";
            char expectedFieldSeparator = '^';

            // Act
            ServiceCode mappedServiceCode = this.defaultReader.MapServiceCode(track1, expectedFieldSeparator);

            // Assert
            Assert.IsFalse(mappedServiceCode.IsPinMandatory);
        }
        [TestMethod]
        public void DefaultMagneticStripeTrackReader_MapServiceCode_ShouldReturnIsPinRequiredAsFalse_IfParametersAreValid()
        {
            // Arrange
            string track1 = "B4205932010016391^ /^2309121000000000000000225000000";
            char expectedFieldSeparator = '^';

            // Act
            ServiceCode mappedServiceCode = this.defaultReader.MapServiceCode(track1, expectedFieldSeparator);

            // Assert
            Assert.IsFalse(mappedServiceCode.IsPinRequired);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void DefaultMagneticStripeTrackReader_MapServiceCode_ShouldThrowException_IfInvalidFieldSeparator ()
        {
            // Arrange
            string track1 = "B4205932010016391^ /^2309121000000000000000225000000";
            char invalidFieldSeparator = '~';

            // Act && Assert
            ServiceCode serviceCode = this.defaultReader.MapServiceCode(track1, invalidFieldSeparator);
        }
    }
}
