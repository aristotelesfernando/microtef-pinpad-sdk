using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PinPadSDK.Enums;
using PinPadSDK.Property;
using PinPadSDK.Commands.Stone.PrtActionData;

namespace PinPadSdkTests.Commands.Stone.PrtActionData {
    [TestClass]
    public class BasePrtRequestDataTests {
        [TestMethod]
        public void ValidateBasePrtRequestDataWarnings() {
            BasePrtRequestData data = new BasePrtRequestData();
            PrivateObject privateData = new PrivateObject(data);

            BasePropertyTestUtils.TestProperty<PrtAction>(data, (PinPadFixedLengthPropertyController<PrtAction>)privateData.GetProperty("_PRT_ACTION"), 1);

            Assert.AreEqual(PrtAction.Begin, data.PRT_ACTION);

            Assert.AreEqual("0",
                data.CommandString);
            BasePropertyTestUtils.TestCommandString(data);
        }
    }
}
