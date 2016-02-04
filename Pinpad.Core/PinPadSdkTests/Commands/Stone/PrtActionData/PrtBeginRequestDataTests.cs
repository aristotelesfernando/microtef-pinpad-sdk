using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PinPadSDK.Commands.Stone.PrtActionData;
using PinPadSDK.Enums;

namespace PinPadSdkTests.Commands.Stone.PrtActionData {
    [TestClass]
    public class PrtBeginRequestDataTests {
        [TestMethod]
        public void ValidatePrtBeginRequestDataWarnings() {
            PrtBeginRequestData data = new PrtBeginRequestData();

            Assert.AreEqual(PrtAction.Begin, data.PRT_ACTION);

            Assert.AreEqual("0",
                data.CommandString);
            BasePropertyTestUtils.TestCommandString(data);
        }
    }
}
