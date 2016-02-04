using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PinPadSDK.Commands.CkeEventsData;
using PinPadSDK.Enums;

namespace PinPadSdkTests.Commands.CkeEventsData {
    [TestClass]
    public class CkeContactlessResponseDataTests {
        [TestMethod]
        public void ValidateCkeContactlessResponseDataWarnings() {
            CkeContactlessResponseData data = new CkeContactlessResponseData();
            ValidateCkeContactlessResponseDataWarnings(data);
        }
        public static void ValidateCkeContactlessResponseDataWarnings(CkeContactlessResponseData data) {
            Assert.AreEqual(CkeEvent.CtlsUpdate, data.CKE_EVENT);

            BasePropertyTestUtils.TestProperty(data, data.CKE_CTLSSTAT);
            Assert.AreEqual("31",
                data.CommandString);
            BasePropertyTestUtils.TestCommandString(data);
        }
    }
}
