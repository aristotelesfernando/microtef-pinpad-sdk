using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PinPadSDK.Commands.Stone;

namespace PinPadSdkTests.Commands.Stone {
    [TestClass]
    public class DsiRequestTests {
        [TestMethod]
        public void ValidateDsiRequestWarnings() {
            DsiRequest request = new DsiRequest();

            Assert.AreEqual(1, request.MinimumStoneVersion);

            BasePropertyTestUtils.TestProperty(request, request.DSI_IMGNAME, 15);

            Assert.AreEqual("DSI018015123456789012345",
                request.CommandString);
            BasePropertyTestUtils.TestCommandString(request);
        }
    }
}
