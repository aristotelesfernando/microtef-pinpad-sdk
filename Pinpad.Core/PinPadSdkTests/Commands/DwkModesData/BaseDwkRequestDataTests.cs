using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PinPadSDK.Commands.DwkModesData;
using PinPadSDK.Enums;
using PinPadSDK.Property;

namespace PinPadSdkTests.Commands.DwkModesData {
    [TestClass]
    public class BaseDwkRequestDataTests {
        [TestMethod]
        public void ValidateBaseDwkRequestDataWarnings() {
            BaseDwkRequestData data = new BaseDwkRequestData();
            PrivateObject privateData = new PrivateObject(data);

            BasePropertyTestUtils.TestProperty<DwkMode>(data, (PinPadFixedLengthPropertyController<DwkMode>)privateData.GetProperty("_DWK_MODE"), 1);

            Assert.AreEqual(DwkMode.Mode1, data.DWK_MODE);

            Assert.AreEqual("0",
                data.CommandString);
            BasePropertyTestUtils.TestCommandString(data);
        }
    }
}
