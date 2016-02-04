using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PinPadSDK.Commands.Stone.PrtActionData;
using PinPadSDK.Enums;

namespace PinPadSdkTests.Commands.Stone.PrtActionData {
    [TestClass]
    public class PrtAppendStringRequestDataTests {
        [TestMethod]
        public void ValidatePrtAppendStringRequestDataWarnings() {
            PrtAppendStringRequestData data = new PrtAppendStringRequestData();
            PrtAppendStringRequestDataTests.ValidatePrtAppendStringRequestDataWarnings(data);
        }
            
        public static void ValidatePrtAppendStringRequestDataWarnings(PrtAppendStringRequestData data) {
            Assert.AreEqual(PrtAction.AppendString, data.PRT_ACTION);

            BasePropertyTestUtils.TestProperty<PrtStringSize>(data, data.PRT_SIZE, 1);
            BasePropertyTestUtils.TestProperty<PrtAlignment>(data, data.PRT_ALIGNMENT, 1);
            BasePropertyTestUtils.TestProperty(data, data.PRT_MSG, 512);

            Assert.AreEqual("20051212345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012",
                data.CommandString);
            BasePropertyTestUtils.TestCommandString(data);
        }
    }
}
