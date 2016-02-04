using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PinPadSDK.Commands.CkeEventsData;
using PinPadSDK.Enums;

namespace PinPadSdkTests.Commands.CkeEventsData {
    [TestClass]
    public class CkeKeyPressResponseDataTests {
        [TestMethod]
        public void ValidateCkeKeyPressResponseDataWarnings() {
            CkeKeyPressResponseData data = new CkeKeyPressResponseData();
            ValidateCkeKeyPressResponseDataWarnings(data);
        }
        public static void ValidateCkeKeyPressResponseDataWarnings(CkeKeyPressResponseData data) {
            Assert.AreEqual(CkeEvent.KeyPress, data.CKE_EVENT);

            BasePropertyTestUtils.TestProperty < PinPadKey>(data, data.CKE_KEYCODE, 2);
            Assert.AreEqual("000",
                data.CommandString);
            BasePropertyTestUtils.TestCommandString(data);
        }
    }
}
