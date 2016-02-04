using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PinPadSDK.Commands.Stone;

namespace PinPadSdkTests.Commands.Stone {
    [TestClass]
    public class SecRequestTests {
        [TestMethod]
        public void ValidateSecRequestWarnings() {
            SecRequest request = new SecRequest();

            Assert.AreEqual(1, request.MinimumStoneVersion);

            BasePropertyTestUtils.TestProperty(request, request.SEC_ACQIDX, 2);
            BasePropertyTestUtils.TestSimpleProperty(request, request.SEC_CMDBLK, 32);

            Assert.AreEqual("SEC0371200412345678901234567890123456789012",
                request.CommandString);
            BasePropertyTestUtils.TestCommandString(request);
        }
    }
}
