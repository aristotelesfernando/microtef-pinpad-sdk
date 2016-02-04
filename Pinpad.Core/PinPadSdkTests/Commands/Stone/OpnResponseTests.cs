using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PinPadSDK.Commands.Stone;

namespace PinPadSdkTests.Commands.Stone {
    [TestClass]
    public class OpnResponseTests {
        [TestMethod]
        public void ValidateOpnResponseWarnings() {
            OpnResponse response = new OpnResponse();

            Assert.IsFalse(response.IsBlockingCommand, "OpnResponse is marked as a blocking command.");

            BasePropertyTestUtils.TestBaseResponse(response);

            Assert.AreEqual("OPN000",
                response.CommandString);
            BasePropertyTestUtils.TestCommandString(response);

            BasePropertyTestUtils.TestProperty(response, response.OPN_STONEVER, 3, true);

            Assert.AreEqual("OPN000003123",
                response.CommandString);
            BasePropertyTestUtils.TestCommandString(response);

            BasePropertyTestUtils.TestBaseResponse(response);
        }

        [TestMethod]
        public void ValidateOpnResponseWithoutStoneApp() {
            OpnResponse response = new OpnResponse();

            try {
                response.CommandString = "OPN000";
            }
            catch {
                Assert.Fail("Failed to receive OPN without stone app");
            }
        }
    }
}
