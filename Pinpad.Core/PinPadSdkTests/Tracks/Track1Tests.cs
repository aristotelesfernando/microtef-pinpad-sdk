using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PinPadSDK.Controllers.Tracks;
using System.Globalization;

namespace PinPadSdkTests.Tracks {
    [TestClass]
    public class Track1Tests {
        [TestMethod]
        public void ValidateTrack1Warnings() {
            Track1 track = new Track1();
            Track1Tests.ValidateTrack1Warnings(track);
        }

        public static void ValidateTrack1Warnings(Track1 track) {
            string timestring = "1501";
            DateTime dateTime = DateTime.ParseExact(timestring, "yyMM", CultureInfo.InvariantCulture);

            BasePropertyTestUtils.TestProperty(track, track.FormatCode);
            BasePropertyTestUtils.TestSimpleProperty(track, track.PAN, 19);
            BasePropertyTestUtils.TestSimpleProperty(track, track.Name, 26);
            BasePropertyTestUtils.TestProperty(track, track.ServiceCode);

            Assert.AreEqual("C1234567890123456789^12345678901234567890123456^^123",
                track.CommandString);
            BasePropertyTestUtils.TestCommandString(track);

            BasePropertyTestUtils.TestProperty(track, track.ExpirationDate, dateTime, true);
            Assert.AreEqual("C1234567890123456789^12345678901234567890123456^" + timestring + "123",
                track.CommandString);
            BasePropertyTestUtils.TestCommandString(track);

            BasePropertyTestUtils.TestSimpleProperty(track, track.DiscretionaryData, 21, true);
            Assert.AreEqual("C1234567890123456789^12345678901234567890123456^" + timestring + "123123456789012345678901",
                track.CommandString);
            BasePropertyTestUtils.TestCommandString(track);
        }
    }
}
