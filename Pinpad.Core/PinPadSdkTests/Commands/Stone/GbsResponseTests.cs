using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PinPadSDK.Commands.Stone;

namespace PinPadSdkTests.Commands.Stone {
    [TestClass]
    public class GbsResponseTests {
        [TestMethod]
        public void ValidateGbsResponseWarnings() {
            GbsResponse response = new GbsResponse();

            Assert.IsFalse(response.IsBlockingCommand, "GbsResponse is marked as a blocking command.");

            BasePropertyTestUtils.TestBaseResponse(response);

            BasePropertyTestUtils.TestProperty(response, response.GBS_STATUS);
            BasePropertyTestUtils.TestProperty(response, response.GBS_LEVEL, 3);
            BasePropertyTestUtils.TestProperty(response, response.GBS_CHARGING);

            Assert.AreEqual("GBS00000511231",
                response.CommandString);
            BasePropertyTestUtils.TestCommandString(response);

            BasePropertyTestUtils.TestBaseResponse(response);
        }
    }
}
