using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PinPadSDK.Commands;

namespace PinPadSdkTests.Commands {
    [TestClass]
    public class DwkMode2ResponseTests {
        [TestMethod]
        public void ValidateDwkMode2ResponseWarnings() {
            DwkMode2Response response = new DwkMode2Response();

            Assert.IsFalse(response.IsBlockingCommand, "DwkMode2Response is marked as a blocking command.");

            BasePropertyTestUtils.TestBaseResponse(response);

            BasePropertyTestUtils.TestProperty(response, response.DWK_CRYPT, 256);

            Assert.AreEqual("DWK0002561234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456",
                response.CommandString);
            BasePropertyTestUtils.TestCommandString(response);

            BasePropertyTestUtils.TestBaseResponse(response);
        }
    }
}
