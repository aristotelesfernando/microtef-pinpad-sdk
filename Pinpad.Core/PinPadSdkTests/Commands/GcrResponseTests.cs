using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PinPadSDK.Commands;
using PinPadSDK.Enums;
using PinPadSDK.Property;
using System.Globalization;
using PinPadSDK.Controllers.Tracks;
using PinPadSdkTests.Tracks;

namespace PinPadSdkTests.Commands {
    /// <summary>
    /// Summary description for GcrResponseTests
    /// </summary>
    [TestClass]
    public class GcrResponseTests {
        [TestMethod]
        public void ValidateGcrResponseWarnings() {
            string timestring = "010203";
            DateTime dateTime = DateTime.ParseExact(timestring, "yyMMdd", CultureInfo.InvariantCulture);

            GcrResponse response = new GcrResponse();

            Assert.IsTrue(response.IsBlockingCommand, "GcrResponse is not marked as a blocking command.");

            BasePropertyTestUtils.TestBaseResponse(response);

            BasePropertyTestUtils.TestProperty<ApplicationType>(response, response.GCR_CARDTYPE, 2);
            BasePropertyTestUtils.TestProperty<FallbackStatus>(response, response.GCR_STATCHIP, 1);
            BasePropertyTestUtils.TestProperty(response, response.GCR_APPTYPE, 2);
            BasePropertyTestUtils.TestProperty(response, response.GCR_ACQIDX, 2);
            BasePropertyTestUtils.TestProperty(response, response.GCR_RECIDX, 2);
            BasePropertyTestUtils.TestProperty(response, response.GCR_PAN, 19);
            BasePropertyTestUtils.TestProperty(response, response.GCR_PANSEQNO, 2);
            BasePropertyTestUtils.TestProperty(response, response.GCR_APPLABEL, 16);
            BasePropertyTestUtils.TestProperty(response, response.GCR_SRVCODE);
            BasePropertyTestUtils.TestProperty(response, response.GCR_CHNAME, 26);
            BasePropertyTestUtils.TestProperty(response, response.GCR_CARDEXP, dateTime);
            BasePropertyTestUtils.TestProperty(response, response.GCR_ISSCNTRY, 3);
            Assert.AreEqual("GCR00034200012121200                                                                            00                                     000                                                                                                        19123456789012345678912123456789012345612312345678901234567890123456" + timestring + "                             123000",
                response.CommandString);
            BasePropertyTestUtils.TestCommandString(response);

            Track1 track1 = new Track1();
            Track1Tests.ValidateTrack1Warnings(track1);
            response.GCR_TRK1.Value = track1;

            Assert.AreEqual("GCR00034200012121276C1234567890123456789^12345678901234567890123456^150112312345678901234567890100                                     000                                                                                                        19123456789012345678912123456789012345612312345678901234567890123456" + timestring + "                             123000",
                response.CommandString);
            BasePropertyTestUtils.TestCommandString(response);

            Track2 track2 = new Track2();
            Track2Tests.ValidateTrack2Warnings(track2);
            response.GCR_TRK2.Value = track2;
            Assert.AreEqual("GCR00034200012121276C1234567890123456789^12345678901234567890123456^1501123123456789012345678901371234567890123456789=15011231234567890000                                                                                                        19123456789012345678912123456789012345612312345678901234567890123456" + timestring + "                             123000",
                response.CommandString);
            BasePropertyTestUtils.TestCommandString(response);

            BasePropertyTestUtils.TestProperty(response, response.GCR_TRK3, 104, true);
            Assert.AreEqual("GCR00034200012121276C1234567890123456789^12345678901234567890123456^1501123123456789012345678901371234567890123456789=150112312345678901041234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123419123456789012345678912123456789012345612312345678901234567890123456" + timestring + "                             123000",
                response.CommandString);
            BasePropertyTestUtils.TestCommandString(response);

            BasePropertyTestUtils.TestProperty(response, response.GCR_RUF1, 29);
            Assert.AreEqual("GCR00034200012121276C1234567890123456789^12345678901234567890123456^1501123123456789012345678901371234567890123456789=150112312345678901041234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123419123456789012345678912123456789012345612312345678901234567890123456" + timestring + "12345678901234567890123456789123000",
                response.CommandString);
            BasePropertyTestUtils.TestCommandString(response);

            BasePropertyTestUtils.TestProperty(response, response.GCR_ACQRD, 66, true);
            Assert.AreEqual("GCR00040800012121276C1234567890123456789^12345678901234567890123456^1501123123456789012345678901371234567890123456789=150112312345678901041234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123419123456789012345678912123456789012345612312345678901234567890123456" + timestring + "12345678901234567890123456789123066123456789012345678901234567890123456789012345678901234567890123456",
                response.CommandString);
            BasePropertyTestUtils.TestCommandString(response);

            BasePropertyTestUtils.TestBaseResponse(response);
        }
    }
}
