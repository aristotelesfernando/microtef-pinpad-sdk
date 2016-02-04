using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PinPadSDK.Commands.Stone;

namespace PinPadSdkTests.Commands.Stone {
    [TestClass]
    public class DssRequestTests {
        [TestMethod]
        public void ValidateDssRequestWarnings() {
            DssRequest request = new DssRequest();

            Assert.AreEqual(1, request.MinimumStoneVersion);

            BasePropertyTestUtils.TestProperty(request, request.DSS_X, 3);
            BasePropertyTestUtils.TestProperty(request, request.DSS_Y, 3);
            BasePropertyTestUtils.TestProperty(request, request.DSS_BGRED, 3);
            BasePropertyTestUtils.TestProperty(request, request.DSS_BGGREEN, 3);
            BasePropertyTestUtils.TestProperty(request, request.DSS_BGBLUE, 3);
            BasePropertyTestUtils.TestProperty(request, request.DSS_FGRED, 3);
            BasePropertyTestUtils.TestProperty(request, request.DSS_FGGREEN, 3);
            BasePropertyTestUtils.TestProperty(request, request.DSS_FGBLUE, 3);
            BasePropertyTestUtils.TestProperty(request, request.DSS_MSG, 160);

            Assert.AreEqual("DSS1871231231231231231231231231601234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890",
                request.CommandString);
            BasePropertyTestUtils.TestCommandString(request);
        }
    }
}
