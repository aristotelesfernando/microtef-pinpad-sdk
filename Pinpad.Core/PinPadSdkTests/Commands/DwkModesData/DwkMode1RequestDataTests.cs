using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PinPadSDK.Commands.DwkModesData;
using PinPadSDK.Enums;

namespace PinPadSdkTests.Commands.DwkModesData {
    [TestClass]
    public class DwkMode1RequestDataTests {
        [TestMethod]
        public void ValidateDwkMode1RequestDataWarnings() {
            DwkMode1RequestData data = new DwkMode1RequestData();
            ValidateDwkMode1RequestDataWarnings(data);
        }
        public static void ValidateDwkMode1RequestDataWarnings(DwkMode1RequestData data) {
            Assert.AreEqual(DwkMode.Mode1, data.DWK_MODE);

            BasePropertyTestUtils.TestProperty(data, data.DWK_METHOD);
            BasePropertyTestUtils.TestProperty(data, data.DWK_MKIDX, 2);
            BasePropertyTestUtils.TestProperty(data, data.DWK_WKPAN, 32);

            Assert.AreEqual("031212345678901234567890123456789012",
                data.CommandString);
            BasePropertyTestUtils.TestCommandString(data);
        }
    }
}
