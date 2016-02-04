using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PinPadSDK.Commands;

namespace PinPadSdkTests.Commands {
    [TestClass]
    public class TleRequestTests {
        [TestMethod]
        public void ValidateTleRequestWarnings() {
            TleRequest request = new TleRequest();

            Assert.AreEqual("TLE",
                request.CommandString);
            BasePropertyTestUtils.TestCommandString(request);
        }
    }
}
