using System;
using Pinpad.Sdk.Model;
using Pinpad.Sdk.Commands;
using Pinpad.Sdk.Transaction;
using NUnit.Framework;

namespace Pinpad.Sdk.Test.Mapper
{
    [TestFixture]
    public class EmvTrackMapperTest
    {
        internal GcrResponse GcrResponse;

        [SetUp]
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

        [Test]
        public void EmvTrackMapper_MapCardFromEmvTrack_ShouldNotReturnNull()
        {
            // Act
            CardEntry mappedCard = EmvTrackMapper.MapCardFromEmvTrack(this.GcrResponse);

            // Assert
            Assert.IsNotNull(mappedCard);
        }

        [Test]
        public void EmvTrackMapper_MapCardFromEmvTrack_ShouldReturnEmvCardType_Always ()
        {
            // Act
            CardEntry mappedCard = EmvTrackMapper.MapCardFromEmvTrack(this.GcrResponse);

            // Assert
            Assert.IsTrue(mappedCard.Type == CardType.Emv);
        }
    }
}
