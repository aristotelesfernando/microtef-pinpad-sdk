using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PinPadSDK.Commands.Stone.PrtActionData;
using PinPadSDK.Enums;

namespace PinPadSdkTests.Commands.Stone.PrtActionData {
    [TestClass]
    public class PrtBeginResponseDataTests {
        [TestMethod]
        public void ValidatePrtBeginResponseDataWarnings() {
            PrtBeginResponseData data = new PrtBeginResponseData();
            PrtBeginResponseDataTests.ValidatePrtBeginResponseDataWarnings(data);
        }

        public static void ValidatePrtBeginResponseDataWarnings(PrtBeginResponseData data) {
            Assert.AreEqual(PrtAction.Begin, data.PRT_ACTION);

            BasePropertyTestUtils.TestProperty<PinPadPrinterStatus>(data, data.PRT_STATUS, 3);

            Assert.AreEqual("0000",
                data.CommandString);
            BasePropertyTestUtils.TestCommandString(data);
        }
    }
}
