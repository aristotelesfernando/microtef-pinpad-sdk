using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pinpad.Sdk.Mapper;
using PinPadSDK.Commands;

namespace Pinpad.Sdk.Test.Mapper
{
    [TestClass]
    public class MagneticStripeTrackMapperTest
    {
        string Track1;
        string Track2;
        DateTime ExpirationDate;

        [TestInitialize]
        public void Setup()
        {
            this.Track1 = "4761739001010036^TESTING CARD MAPPER^123456789987456321";
            this.Track2 = "4761739001010036=23032011184404889";
            this.ExpirationDate = new DateTime(year: 2023, month: 3, day: 1);
        }

        [TestMethod]
        public void MagneticStripeTrackMapper_CardholderName_should_match()
        {
            string cardholderName = this.Track1.Split(MagneticStripeTrackMapper.TRACK1_FIELD_SEPARATOR)[MagneticStripeTrackMapper.CARDHOLDERNAME_INDEX];
            
            Assert.AreEqual(cardholderName, MagneticStripeTrackMapper.MapCardholderName(this.Track1, MagneticStripeTrackMapper.TRACK1_FIELD_SEPARATOR));
        }

        [TestMethod]
        public void MagneticStripeTrackMapper_Pan_should_match()
        {
            string pan = this.Track2.Split(MagneticStripeTrackMapper.TRACK2_FIELD_SEPARATOR)[MagneticStripeTrackMapper.PAN_INDEX];
            
            Assert.AreEqual(pan, MagneticStripeTrackMapper.MapPan(this.Track2, MagneticStripeTrackMapper.TRACK2_FIELD_SEPARATOR));
        }

        [TestMethod]
        public void MagneticStripeTrackMapper_ExpirationDate_should_match()
        {
            Assert.AreEqual(this.ExpirationDate, MagneticStripeTrackMapper.MapExpirationDate(this.Track2, MagneticStripeTrackMapper.TRACK2_FIELD_SEPARATOR));
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void MagneticStripeTrackMapper_MapValidTrack_should_throw_exception_if_invalid_track()
        {
            GcrResponse response = new GcrResponse();
            MagneticStripeTrackMapper.MapValidTrack(response);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void MagneticStripeTrackMapper_GetFieldSeparator_should_throw_exception_if_invalid_track()
        {
            string track = "hue br";
            MagneticStripeTrackMapper.GetFieldSeparator(track);
        }

        [TestMethod]
        public void MagneticStripeTrackMapper_GetFieldSeparator_should_return_TRACK1_FIELD_SEPARATOR()
        {
            char separator = MagneticStripeTrackMapper.GetFieldSeparator(MagneticStripeTrackMapper.TRACK1_FIELD_SEPARATOR.ToString());
            Assert.AreEqual(separator, MagneticStripeTrackMapper.TRACK1_FIELD_SEPARATOR);
        }

        [TestMethod]
        public void MagneticStripeTrackMapper_GetFieldSeparator_should_return_TRACK2_FIELD_SEPARATOR()
        {
            char separator = MagneticStripeTrackMapper.GetFieldSeparator(MagneticStripeTrackMapper.TRACK2_FIELD_SEPARATOR.ToString());
            Assert.AreEqual(separator, MagneticStripeTrackMapper.TRACK2_FIELD_SEPARATOR);
        }

        [TestMethod]
        public void MagneticStripeTrackMapper_MapCardholderName_should_return_empty_string_if_not_track1()
        {
            string chName = MagneticStripeTrackMapper.MapCardholderName(this.Track2, MagneticStripeTrackMapper.TRACK2_FIELD_SEPARATOR);
            Assert.AreEqual(chName, string.Empty);
        }

        [TestMethod]
        public void MagneticStripeTrackMapper_MapCardholderName_should_not_return_empty_string_if_track1()
        {
            string chName = MagneticStripeTrackMapper.MapCardholderName(this.Track1, MagneticStripeTrackMapper.TRACK1_FIELD_SEPARATOR);
            Assert.AreNotEqual(chName, string.Empty);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void MagneticStripeTrackMapper_MapExpirationDate_should_throw_exception_if_invalid_separator()
        {
            DateTime expDate = MagneticStripeTrackMapper.MapExpirationDate(this.Track1, '.');
        }
    }
}
