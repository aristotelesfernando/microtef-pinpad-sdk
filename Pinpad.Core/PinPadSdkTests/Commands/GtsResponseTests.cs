using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PinPadSDK.Commands;

namespace PinPadSdkTests.Commands {
    [TestClass]
    public class GtsResponseTests {
        [TestMethod]
        public void ValidateGtsResponseWarnings() {
            GtsResponse response = new GtsResponse();

            Assert.IsFalse(response.IsBlockingCommand, "GtsResponse is marked as a blocking command.");

            BasePropertyTestUtils.TestBaseResponse(response);
            BasePropertyTestUtils.TestFixedProperty(response, response.GTS_TABVER, 10);

            Assert.AreEqual("GTS0000101234567890",
                response.CommandString);
            BasePropertyTestUtils.TestCommandString(response);

            BasePropertyTestUtils.TestBaseResponse(response);
        }
    }
}
