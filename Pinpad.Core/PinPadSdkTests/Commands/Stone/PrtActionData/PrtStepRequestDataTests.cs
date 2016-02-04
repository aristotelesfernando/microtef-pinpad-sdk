using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PinPadSDK.Commands.Stone.PrtActionData;
using PinPadSDK.Enums;

namespace PinPadSdkTests.Commands.Stone.PrtActionData {
    [TestClass]
    public class PrtStepRequestDataTests {
        [TestMethod]
        public void ValidatePrtAppendStringRequestDataWarnings() {
            PrtStepRequestData data = new PrtStepRequestData();
            PrtStepRequestDataTests.ValidatePrtAppendStringRequestDataWarnings(data);
        }

        public static void ValidatePrtAppendStringRequestDataWarnings(PrtStepRequestData data) {
            Assert.AreEqual(PrtAction.Step, data.PRT_ACTION);

            BasePropertyTestUtils.TestProperty(data, data.PRT_STEPS, 4);

            Assert.AreEqual("41234",
                data.CommandString);
            BasePropertyTestUtils.TestCommandString(data);
        }
    }
}
