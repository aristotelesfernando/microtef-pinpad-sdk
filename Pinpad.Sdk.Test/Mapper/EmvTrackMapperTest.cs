using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pinpad.Sdk.Model;
using Pinpad.Sdk.Commands;
using Pinpad.Sdk.Transaction;

namespace Pinpad.Sdk.Test.Mapper
{
    [TestClass]
    public class EmvTrackMapperTest
    {
        internal GcrResponse GcrResponse;

        [TestInitialize]
        public void Setup()
        {
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
        public void EmvTrackMapper_MapCardFromEmvTrack_should_not_return_null()
        {
            CardEntry mappedCard = EmvTrackMapper.MapCardFromEmvTrack(this.GcrResponse);
            Assert.IsNotNull(mappedCard);
        }

        [TestMethod]
        public void EmvTrackMapper_MapCardFromEmvTrack_CardType_should_be_Emv()
        {
            CardEntry mappedCard = EmvTrackMapper.MapCardFromEmvTrack(this.GcrResponse);
            Assert.IsTrue(mappedCard.Type == CardType.Emv);
        }
    }
}
