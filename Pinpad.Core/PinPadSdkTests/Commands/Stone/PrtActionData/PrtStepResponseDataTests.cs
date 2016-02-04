using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PinPadSDK.Commands.Stone.PrtActionData;
using PinPadSDK.Enums;

namespace PinPadSdkTests.Commands.Stone.PrtActionData {
    [TestClass]
    public class PrtStepResponseDataTests {
        [TestMethod]
        public void ValidatePrtAppendStringResponseDataWarnings() {
            PrtStepResponseData data = new PrtStepResponseData();

            Assert.AreEqual(PrtAction.Step, data.PRT_ACTION);

            Assert.AreEqual("4",
                data.CommandString);
            BasePropertyTestUtils.TestCommandString(data);
        }
    }
}
