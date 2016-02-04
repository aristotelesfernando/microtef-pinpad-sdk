using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PinPadSDK.Commands.Stone.PrtActionData;
using PinPadSDK.Enums;

namespace PinPadSdkTests.Commands.Stone.PrtActionData {
    [TestClass]
    public class PrtEndRequestDataTests {
        [TestMethod]
        public void ValidatePrtEndRequestDataWarnings() {
            PrtEndRequestData data = new PrtEndRequestData();

            Assert.AreEqual(PrtAction.End, data.PRT_ACTION);

            Assert.AreEqual("1",
                data.CommandString);
            BasePropertyTestUtils.TestCommandString(data);
        }
    }
}
