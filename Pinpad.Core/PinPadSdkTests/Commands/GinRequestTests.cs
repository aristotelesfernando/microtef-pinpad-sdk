using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PinPadSDK.Commands;

namespace PinPadSdkTests.Commands {
    [TestClass]
    public class GinRequestTests {
        [TestMethod]
        public void ValidateGinRequestWarnings() {
            GinRequest request = new GinRequest();

            BasePropertyTestUtils.TestProperty(request, request.GIN_ACQIDX, 2);

            Assert.AreEqual("GIN00212",
                request.CommandString);
            BasePropertyTestUtils.TestCommandString(request);
        }
    }
}
