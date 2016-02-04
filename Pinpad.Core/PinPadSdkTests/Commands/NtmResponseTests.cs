using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PinPadSDK.Commands;

namespace PinPadSdkTests.Commands {
    [TestClass]
    public class NtmResponseTests {
        [TestMethod]
        public void ValidateNtmResponseWarnings() {
            NtmResponse response = new NtmResponse();

            Assert.IsTrue(response.IsBlockingCommand, "NtmResponse is not marked as a blocking command.");

            BasePropertyTestUtils.TestBaseResponse(response);

            BasePropertyTestUtils.TestProperty(response, response.NTM_MSG);

            Assert.AreEqual("NTM00003212345678901234567890123456789012",
                response.CommandString);
            BasePropertyTestUtils.TestCommandString(response);

            BasePropertyTestUtils.TestBaseResponse(response);
        }
    }
}
