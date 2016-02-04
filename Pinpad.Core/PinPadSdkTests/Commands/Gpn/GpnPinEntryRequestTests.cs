using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PinPadSDK.Commands.Gpn;

namespace PinPadSdkTests.Commands.Gpn {
    [TestClass]
    public class GpnPinEntryRequestTests {
        [TestMethod]
        public void ValidateGpnPinEntryRequestWarnings() {
            GpnPinEntryRequest entry = new GpnPinEntryRequest();
            GpnPinEntryRequestTests.ValidateGpnPinEntryRequestWarnings(entry);
        }

        public static void ValidateGpnPinEntryRequestWarnings(GpnPinEntryRequest entry) {

            BasePropertyTestUtils.TestProperty(entry, entry.GPN_MIN, 2);
            BasePropertyTestUtils.TestProperty(entry, entry.GPN_MAX, 2);
            BasePropertyTestUtils.TestProperty(entry, entry.GPN_MSG);

            Assert.AreEqual("121212345678901234567890123456789012",
                entry.CommandString);
            BasePropertyTestUtils.TestCommandString(entry);
        }
    }
}
