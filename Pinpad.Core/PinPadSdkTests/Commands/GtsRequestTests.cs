using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PinPadSDK.Commands;

namespace PinPadSdkTests.Commands {
    [TestClass]
    public class GtsRequestTests {
        [TestMethod]
        public void ValidateGtsRequestWarnings() {
            GtsRequest request = new GtsRequest();

            BasePropertyTestUtils.TestProperty(request, request.GTS_ACQIDX, 2);

            Assert.AreEqual("GTS00212",
                request.CommandString);
            BasePropertyTestUtils.TestCommandString(request);
        }
    }
}
