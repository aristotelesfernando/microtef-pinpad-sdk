using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PinPadSDK.Commands;

namespace PinPadSdkTests.Commands {
    [TestClass]
    public class TliRequestTests {
        [TestMethod]
        public void ValidateTliRequestWarnings() {
            TliRequest request = new TliRequest();

            BasePropertyTestUtils.TestProperty(request, request.TLI_ACQIDX, 2);
            BasePropertyTestUtils.TestFixedProperty(request, request.TLI_TABVER, 10);

            Assert.AreEqual("TLI012121234567890",
                request.CommandString);
            BasePropertyTestUtils.TestCommandString(request);
        }
    }
}
