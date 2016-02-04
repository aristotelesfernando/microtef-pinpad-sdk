using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PinPadSDK.Commands;

namespace PinPadSdkTests.Commands {
    [TestClass]
    public class DexRequestTests {
        [TestMethod]
        public void ValidateDexRequestWarnings() {
            DexRequest request = new DexRequest();

            BasePropertyTestUtils.TestProperty(request, request.DEX_MSG);

            Assert.AreEqual("DEX0360331234567890123456\r1234567890123456",
                request.CommandString);
            BasePropertyTestUtils.TestCommandString(request);
        }
    }
}
