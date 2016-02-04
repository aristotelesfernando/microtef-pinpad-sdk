using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PinPadSDK.Commands;

namespace PinPadSdkTests.Commands {
    [TestClass]
    public class RmcRequestTests {
        [TestMethod]
        public void ValidateRmcRequestWarnings() {
            RmcRequest request = new RmcRequest();

            BasePropertyTestUtils.TestProperty(request, request.RMC_MSG);

            Assert.AreEqual("RMC03212345678901234567890123456789012",
                request.CommandString);
            BasePropertyTestUtils.TestCommandString(request);
        }
    }
}
