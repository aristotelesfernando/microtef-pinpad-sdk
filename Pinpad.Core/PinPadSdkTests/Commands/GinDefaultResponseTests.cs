using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PinPadSDK.Commands;

namespace PinPadSdkTests.Commands {
    [TestClass]
    public class GinDefaultResponseTests {
        [TestMethod]
        public void ValidateGinDefaultResponseWarnings() {
            GinDefaultResponse response = new GinDefaultResponse();

            Assert.IsFalse(response.IsBlockingCommand, "GcrResponse is marked as a blocking command.");

            BasePropertyTestUtils.TestBaseResponse(response);

            BasePropertyTestUtils.TestProperty(response, response.GIN_ACQNAME, 20);
            BasePropertyTestUtils.TestProperty(response, response.GIN_APPVERS, 13);
            BasePropertyTestUtils.TestProperty(response, response.GIN_SPECVER, 4);
            BasePropertyTestUtils.TestProperty(response, response.GIN_RUF1, 3);
            BasePropertyTestUtils.TestProperty(response, response.GIN_RUF2, 2);
            Assert.AreEqual("GIN000042123456789012345678901234567890123123412312",
                response.CommandString);
            BasePropertyTestUtils.TestCommandString(response);

            BasePropertyTestUtils.TestBaseResponse(response);
        }
    }
}
