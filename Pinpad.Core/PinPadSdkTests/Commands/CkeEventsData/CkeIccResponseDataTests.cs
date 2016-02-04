using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PinPadSDK.Commands.CkeEventsData;
using PinPadSDK.Enums;

namespace PinPadSdkTests.Commands.CkeEventsData {
    [TestClass]
    public class CkeIccResponseDataTests {
        [TestMethod]
        public void ValidateCkeIccResponseDataWarnings() {
            CkeIccResponseData data = new CkeIccResponseData();
            ValidateCkeIccResponseDataWarnings(data);
        }
        public static void ValidateCkeIccResponseDataWarnings(CkeIccResponseData data) {
            Assert.AreEqual(CkeEvent.IccStatusChanged, data.CKE_EVENT);

            BasePropertyTestUtils.TestProperty(data, data.CKE_ICCSTAT);
            Assert.AreEqual("21",
                data.CommandString);
            BasePropertyTestUtils.TestCommandString(data);
        }
    }
}
