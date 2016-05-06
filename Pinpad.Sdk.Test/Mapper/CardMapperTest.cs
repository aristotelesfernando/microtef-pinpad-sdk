using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pinpad.Sdk.Model;
using Pinpad.Sdk.Commands;
using Pinpad.Sdk.Transaction;

namespace Pinpad.Sdk.Test.Mapper
{
    [TestClass]
    public class CardMapperTest
    {
        string Track1;
        string Track2;
        string Track3;

        internal GcrResponse GcrResponse;

        [TestInitialize]
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

        [TestMethod]
        public void CardMapper_should_not_return_null()
        {
            this.GcrResponse.GCR_CARDTYPE.Value = ApplicationType.ContactlessEmv;
            CardEntry mappedCard = CardMapper.MapCardFromTracks(this.GcrResponse);
            Assert.IsNotNull(mappedCard);
        }

        [TestMethod]
        public void CardMapper_CardType_should_be_MagneticStripe_if_AplicationType_is_MagneticStripe()
        {
            this.GcrResponse.GCR_CARDTYPE.Value = ApplicationType.MagneticStripe;
            CardType cardType = CardMapper.MapCardType(this.GcrResponse.GCR_CARDTYPE.Value);
            Assert.IsTrue(cardType == CardType.MagneticStripe);
        }

        [TestMethod]
        public void CardMapper_CardType_should_be_MagneticStripe_if_AplicationType_is_ContactlessMagneticStripe()
        {
            this.GcrResponse.GCR_CARDTYPE.Value = ApplicationType.ContactlessMagneticStripe;
            CardType cardType = CardMapper.MapCardType(this.GcrResponse.GCR_CARDTYPE.Value);
            Assert.IsTrue(cardType == CardType.MagneticStripe);
        }

        [TestMethod]
        public void CardMapper_CardType_should_be_Emv_if_AplicationType_is_IccEmv()
        {
            this.GcrResponse.GCR_CARDTYPE.Value = ApplicationType.IccEmv;
            CardType cardType = CardMapper.MapCardType(this.GcrResponse.GCR_CARDTYPE.Value);
            Assert.IsTrue(cardType == CardType.Emv);
        }

        [TestMethod]
        public void CardMapper_CardType_should_be_Emv_if_AplicationType_is_ContactlessEmv()
        {
            this.GcrResponse.GCR_CARDTYPE.Value = ApplicationType.ContactlessEmv;
            CardType cardType = CardMapper.MapCardType(this.GcrResponse.GCR_CARDTYPE.Value);
            Assert.IsTrue(cardType == CardType.Emv);
        }

        [TestMethod]
        [ExpectedException(typeof(NotImplementedException))]
        public void CardMapper_should_throw_exception_if_ApplicationType_is_VisaCashOverTIBCv1()
        {
            this.GcrResponse.GCR_CARDTYPE.Value = ApplicationType.VisaCashOverTIBCv1;
            CardEntry mappedCard = CardMapper.MapCardFromTracks(this.GcrResponse);
        }

        [TestMethod]
        [ExpectedException(typeof(NotImplementedException))]
        public void CardMapper_should_throw_exception_if_ApplicationType_is_VisaCashOverTIBCv3()
        {
            this.GcrResponse.GCR_CARDTYPE.Value = ApplicationType.VisaCashOverTIBCv3;
            CardEntry mappedCard = CardMapper.MapCardFromTracks(this.GcrResponse);
        }

        [TestMethod]
        [ExpectedException(typeof(NotImplementedException))]
        public void CardMapper_should_throw_exception_if_ApplicationType_is_EasyEntryOverTIBCv1()
        {
            this.GcrResponse.GCR_CARDTYPE.Value = ApplicationType.EasyEntryOverTIBCv1;
            CardEntry mappedCard = CardMapper.MapCardFromTracks(this.GcrResponse);
        }
    }
}
