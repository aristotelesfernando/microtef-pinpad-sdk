using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PinPadSDK.Commands.CkeEventsData;
using PinPadSDK.Enums;
using PinPadSDK.Controllers.Tracks;
using PinPadSdkTests.Tracks;

namespace PinPadSdkTests.Commands.CkeEventsData {
    [TestClass]
    public class CkeMagneticStripeResponseDataTests {
        [TestMethod]
        public void ValidateCkeMagneticStripeResponseDataWarnings() {
            CkeMagneticStripeResponseData data = new CkeMagneticStripeResponseData();
            ValidateCkeMagneticStripeResponseDataWarnings(data);
        }
        public static void ValidateCkeMagneticStripeResponseDataWarnings(CkeMagneticStripeResponseData data) {
            Assert.AreEqual(CkeEvent.MagneticStripeCardPassed, data.CKE_EVENT);

            Assert.AreEqual("100                                                                            00                                     000                                                                                                        ",
                data.CommandString);
            BasePropertyTestUtils.TestCommandString(data);

            Track1 track1 = new Track1();
            Track1Tests.ValidateTrack1Warnings(track1);
            data.CKE_TRK1.Value = track1;
            Assert.AreEqual("176C1234567890123456789^12345678901234567890123456^150112312345678901234567890100                                     000                                                                                                        ",
                data.CommandString);
            BasePropertyTestUtils.TestCommandString(data);

            Track2 track2 = new Track2();
            Track2Tests.ValidateTrack2Warnings(track2);
            data.CKE_TRK2.Value = track2;
            Assert.AreEqual("176C1234567890123456789^12345678901234567890123456^1501123123456789012345678901371234567890123456789=15011231234567890000                                                                                                        ",
                data.CommandString);
            BasePropertyTestUtils.TestCommandString(data);

            BasePropertyTestUtils.TestProperty(data, data.CKE_TRK3, 104, true);
            Assert.AreEqual("176C1234567890123456789^12345678901234567890123456^1501123123456789012345678901371234567890123456789=1501123123456789010412345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234",
                data.CommandString);
            BasePropertyTestUtils.TestCommandString(data);
        }
    }
}
