using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PinPadSDK.Commands.CkeEventsData;
using PinPadSDK.Enums;
using PinPadSDK.Property;

namespace PinPadSdkTests.Commands.CkeEventsData {
    [TestClass]
    public class BaseCkeResponseDataTests {
        [TestMethod]
        public void ValidateBaseCkeResponseDataWarnings() {
            BaseCkeResponseData data = new BaseCkeResponseData();
            PrivateObject privateData = new PrivateObject(data);

            BasePropertyTestUtils.TestProperty<CkeEvent>(data, (PinPadFixedLengthPropertyController<CkeEvent>)privateData.GetProperty("_CKE_EVENT"), 1);

            Assert.AreEqual(CkeEvent.KeyPress, data.CKE_EVENT);

            Assert.AreEqual("0",
                data.CommandString);
            BasePropertyTestUtils.TestCommandString(data);
        }
    }
}
