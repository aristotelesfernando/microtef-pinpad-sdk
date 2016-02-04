using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PinPadSDK.Commands.Stone.PrtActionData;
using PinPadSDK.Enums;

namespace PinPadSdkTests.Commands.Stone.PrtActionData {
    [TestClass]
    public class PrtAppendImageRequestDataTests {
        [TestMethod]
        public void ValidatePrtAppendStringRequestDataWarnings() {
            PrtAppendImageRequestData data = new PrtAppendImageRequestData();
            PrtAppendImageRequestDataTests.ValidatePrtAppendStringRequestDataWarnings(data);
        }

        public static void ValidatePrtAppendStringRequestDataWarnings(PrtAppendImageRequestData data) {
            Assert.AreEqual(PrtAction.AppendImage, data.PRT_ACTION);

            BasePropertyTestUtils.TestProperty(data, data.PRT_PADDING, 4);
            BasePropertyTestUtils.TestProperty(data, data.PRT_FILENAME, 15);

            Assert.AreEqual("31234015123456789012345",
                data.CommandString);
            BasePropertyTestUtils.TestCommandString(data);
        }
    }
}
