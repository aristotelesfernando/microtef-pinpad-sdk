using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PinPadSDK.Commands.DwkModesData;
using PinPadSDK.Enums;

namespace PinPadSdkTests.Commands.DwkModesData {
    [TestClass]
    public class DwkMode2RequestDataTests {
        [TestMethod]
        public void ValidateDwkMode2RequestDataWarnings() {
            DwkMode2RequestData data = new DwkMode2RequestData();
            ValidateDwkMode2RequestDataWarnings(data);
        }
        public static void ValidateDwkMode2RequestDataWarnings(DwkMode2RequestData data) {
            Assert.AreEqual(DwkMode.Mode2, data.DWK_MODE);

            BasePropertyTestUtils.TestProperty(data, data.DWK_RSAMOD, 256);
            BasePropertyTestUtils.TestProperty(data, data.DWK_RSAEXP, 6);

            Assert.AreEqual("11234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456123456",
                data.CommandString);
            BasePropertyTestUtils.TestCommandString(data);
        }
    }
}
