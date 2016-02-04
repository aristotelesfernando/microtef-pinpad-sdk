using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PinPadSDK.Commands.Stone.PrtActionData;
using PinPadSDK.Enums;

namespace PinPadSdkTests.Commands.Stone.PrtActionData {
    [TestClass]
    public class PrtEndResponseDataTests {
        [TestMethod]
        public void ValidatePrtAppendStringResponseDataWarnings() {
            PrtEndResponseData data = new PrtEndResponseData();

            Assert.AreEqual(PrtAction.End, data.PRT_ACTION);

            Assert.AreEqual("1",
                data.CommandString);
            BasePropertyTestUtils.TestCommandString(data);
        }
    }
}
