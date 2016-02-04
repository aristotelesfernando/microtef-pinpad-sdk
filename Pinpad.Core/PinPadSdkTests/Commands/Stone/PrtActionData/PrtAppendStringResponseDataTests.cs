using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PinPadSDK.Commands.Stone.PrtActionData;
using PinPadSDK.Enums;

namespace PinPadSdkTests.Commands.Stone.PrtActionData {
    [TestClass]
    public class PrtAppendStringResponseDataTests {
        [TestMethod]
        public void ValidatePrtAppendStringResponseDataWarnings() {
            PrtAppendStringResponseData data = new PrtAppendStringResponseData();

            Assert.AreEqual(PrtAction.AppendString, data.PRT_ACTION);

            Assert.AreEqual("2", 
                data.CommandString);
            BasePropertyTestUtils.TestCommandString(data);
        }
    }
}
