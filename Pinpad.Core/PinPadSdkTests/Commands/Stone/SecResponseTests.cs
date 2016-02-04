using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PinPadSDK.Commands.Stone;

namespace PinPadSdkTests.Commands.Stone {
    [TestClass]
    public class SecResponseTests {
        [TestMethod]
        public void ValidateSecResponseWarnings() {
            SecResponse response = new SecResponse();

            Assert.IsTrue(response.IsBlockingCommand, "FncResponse is not marked as a blocking command.");

            BasePropertyTestUtils.TestBaseResponse(response);

            BasePropertyTestUtils.TestSimpleProperty(response, response.SEC_CMDBLK, 32);

            Assert.AreEqual("SEC000003503212345678901234567890123456789012",
                response.CommandString);
            BasePropertyTestUtils.TestCommandString(response);

            BasePropertyTestUtils.TestBaseResponse(response);
        }
    }
}
