using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PinPadSDK.Commands.Stone.PrtActionData;
using PinPadSDK.Enums;
using PinPadSDK.Property;

namespace PinPadSdkTests.Commands.Stone.PrtActionData {
    [TestClass]
    public class BasePrtResponseDataTests {
        [TestMethod]
        public void ValidateBasePrtResponseDataWarnings() {
            BasePrtResponseData data = new BasePrtResponseData();
            PrivateObject privateData = new PrivateObject(data);

            BasePropertyTestUtils.TestProperty<PrtAction>(data, (PinPadFixedLengthPropertyController<PrtAction>)privateData.GetProperty("_PRT_ACTION"), 1);

            Assert.AreEqual(PrtAction.Begin, data.PRT_ACTION);

            Assert.AreEqual("0",
                data.CommandString);
            BasePropertyTestUtils.TestCommandString(data);
        }
    }
}
