using System;
using Pinpad.Sdk.Model;
using Pinpad.Sdk.Commands;
using Pinpad.Sdk.Transaction;
using NUnit.Framework;
using Pinpad.Sdk.Test.Mockers;

namespace Pinpad.Sdk.Test.Mapper
{
    [TestFixture]
    public class CardMapperTest
    {
        string Track1;
        string Track2;
        string Track3;

        internal GcrResponse GcrResponse;

        [SetUp]
        public void Setup()
        {
            this.Track1 = "4761739001010036^TESTING CARD MAPPER^123456789987456321";
            this.Track2 = "4761739001010036=15122011184404889";
            this.Track3 = string.Empty;
            this.GcrResponse = GcrResponseInitializer();
        }
        internal GcrResponse GcrResponseInitializer()
        {
            GcrResponse response = new GcrResponse();

            response.GCR_RECIDX.Value = 99;
            response.GCR_CHNAME.Value = "TESTING CARD MAPPER";
            response.GCR_CARDEXP.Value = DateTime.Now;
            response.GCR_PAN.Value = "4761739001010036";

            return response;
        }

        [Test]
        public void CardMapper_MapCardFromTracks_ShouldNotReturnNull ()
        {
            // Arrange
            this.GcrResponse.GCR_CARDTYPE.Value = ApplicationType.ContactlessEmv;

            // Act
            CardEntry mappedCard = CardMapper.MapCardFromTracks(
                this.GcrResponse, 
                CardBrandMocker.Mock());

            // Assert
            Assert.IsNotNull(mappedCard);
        }
        [Test]
        public void CardMapper_GetCardType_ShouldReturnMagneticStripe_IfApplicationTypeIsMagneticStripe ()
        {
            // Arrange
            this.GcrResponse.GCR_CARDTYPE.Value = ApplicationType.MagneticStripe;

            // Act
            CardType cardType = CardMapper.GetCardType(
                this.GcrResponse.GCR_CARDTYPE.Value);

            // Assert
            Assert.AreEqual(CardType.MagneticStripe, cardType);
        }
        [Test]
        public void CardMapper_GetCardType_ShouldReturnMagneticStripe_IfApplicationTypeIsContactlessMagneticStripe ()
        {
            // Arrange
            this.GcrResponse.GCR_CARDTYPE.Value = ApplicationType.ContactlessMagneticStripe;

            // Act
            CardType cardType = CardMapper.GetCardType(
                this.GcrResponse.GCR_CARDTYPE.Value);

            // Assert
            Assert.AreEqual(CardType.MagneticStripe, cardType);
        }
        [Test]
        public void CardMapper_GetCardType_ShouldReturnEmv_IfApplicationTypeIsIccEmv ()
        {
            // Arrange
            this.GcrResponse.GCR_CARDTYPE.Value = ApplicationType.IccEmv;

            // Act
            CardType cardType = CardMapper.GetCardType(
                this.GcrResponse.GCR_CARDTYPE.Value);

            // Assert
            Assert.AreEqual(CardType.Emv, cardType);
        }
        [Test]
        public void CardMapper_GetCardType_ShouldReturnEmv_IfApplicationTypeIsContactlessEmv ()
        {
            // Arrange
            this.GcrResponse.GCR_CARDTYPE.Value = ApplicationType.ContactlessEmv;

            // Act
            CardType cardType = CardMapper.GetCardType(this.GcrResponse.GCR_CARDTYPE.Value);

            // Assert
            Assert.AreEqual(CardType.Emv, cardType);
        }
        [Test]
        public void CardMapper_MapCardFromTracks_ShouldThrowException_IfApplicationTypeIsVisaCashOverTIBv1 ()
        {
            // Assert
            Assert.Throws<NotImplementedException>(() =>
            {
                // Arrange
                this.GcrResponse.GCR_CARDTYPE.Value = ApplicationType.VisaCashOverTIBCv1;

                // Act
                CardEntry mappedCard = CardMapper.MapCardFromTracks(
                    this.GcrResponse, 
                    CardBrandMocker.Mock());
            });
        }
        [Test]
        public void CardMapper_MapCardFromTracks_ShouldThrowException_IfApplicationTypeIsVisaCashOverTIBCv3 ()
        {
            // Assert
            Assert.Throws<NotImplementedException>(() =>
            {
                // Arrange
                this.GcrResponse.GCR_CARDTYPE.Value = ApplicationType.VisaCashOverTIBCv3;

                // Act
                CardEntry mappedCard = CardMapper.MapCardFromTracks(
                    this.GcrResponse,
                    CardBrandMocker.Mock());
            });
        }
        [Test]
        public void CardMapper_MapCardFromTracks_ShouldThrowException_IfApplicationTypeIsEasyEntryOverTIBCv1 ()
        {
            // Assert
            Assert.Throws<NotImplementedException>(() =>
            {
                // Arrange
                this.GcrResponse.GCR_CARDTYPE.Value = ApplicationType.EasyEntryOverTIBCv1;

                // Act
                CardEntry mappedCard = CardMapper.MapCardFromTracks(
                    this.GcrResponse, 
                    CardBrandMocker.Mock());
            });
        }
    }
}
