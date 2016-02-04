using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PinPadSDK.Controllers.Tracks;
using System.Globalization;

namespace PinPadSdkTests.Tracks {
    [TestClass]
    public class Track2Tests {
        [TestMethod]
        public void ValidateTrack2Warnings() {
            Track2 track = new Track2();
            Track2Tests.ValidateTrack2Warnings(track);
        }
        public static void ValidateTrack2Warnings(Track2 track) {
            string timestring = "1501";
            DateTime dateTime = DateTime.ParseExact(timestring, "yyMM", CultureInfo.InvariantCulture);

            BasePropertyTestUtils.TestSimpleProperty(track, track.PAN, 19);
            BasePropertyTestUtils.TestProperty(track, track.ServiceCode);

            Assert.AreEqual("1234567890123456789==123",
                track.CommandString);
            BasePropertyTestUtils.TestCommandString(track);

            BasePropertyTestUtils.TestProperty(track, track.ExpirationDate, dateTime, true);
            Assert.AreEqual("1234567890123456789=" + timestring + "123",
                track.CommandString);
            BasePropertyTestUtils.TestCommandString(track);

            BasePropertyTestUtils.TestSimpleProperty(track, track.DiscretionaryData, 10, true);
            Assert.AreEqual("1234567890123456789=" + timestring + "1231234567890",
                track.CommandString);
            BasePropertyTestUtils.TestCommandString(track);
        }
    }
}
