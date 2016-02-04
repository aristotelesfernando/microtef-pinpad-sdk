using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PinPadSDK.Commands.Stone;

namespace PinPadSdkTests.Commands.Stone {
    [TestClass]
    public class GbsRequestTests {
        [TestMethod]
        public void ValidateGbsRequestWarnings() {
            GbsRequest request = new GbsRequest();

            Assert.AreEqual(1, request.MinimumStoneVersion);

            Assert.AreEqual("GBS",
                request.CommandString);
            BasePropertyTestUtils.TestCommandString(request);
        }
    }
}
