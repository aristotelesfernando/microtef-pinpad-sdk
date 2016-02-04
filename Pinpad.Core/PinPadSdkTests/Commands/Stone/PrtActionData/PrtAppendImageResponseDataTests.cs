using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PinPadSDK.Enums;
using PinPadSDK.Commands.Stone.PrtActionData;

namespace PinPadSdkTests.Commands.Stone.PrtActionData {
    [TestClass]
    public class PrtAppendImageResponseDataTests {
        [TestMethod]
        public void ValidatePrtAppendStringResponseDataWarnings() {
            PrtAppendImageResponseData data = new PrtAppendImageResponseData();

            Assert.AreEqual(PrtAction.AppendImage, data.PRT_ACTION);

            Assert.AreEqual("3",
                data.CommandString);
            BasePropertyTestUtils.TestCommandString(data);
        }
    }
}
