using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PinPadSDK.Commands;

namespace PinPadSdkTests.Commands {
    [TestClass]
    public class GpnResponseTests {
        [TestMethod]
        public void ValidateGpnResponseWarnings() {
            GpnResponse response = new GpnResponse();

            Assert.IsTrue(response.IsBlockingCommand, "GpnResponse is not marked as a blocking command.");

            BasePropertyTestUtils.TestBaseResponse(response);
            BasePropertyTestUtils.TestProperty(response, response.GPN_PINBLK, 16);
            BasePropertyTestUtils.TestProperty(response, response.GPN_KSN, 20);

            Assert.AreEqual("GPN000036123456789012345612345678901234567890",
                response.CommandString);
            BasePropertyTestUtils.TestCommandString(response);

            BasePropertyTestUtils.TestBaseResponse(response);
        }
    }
}
