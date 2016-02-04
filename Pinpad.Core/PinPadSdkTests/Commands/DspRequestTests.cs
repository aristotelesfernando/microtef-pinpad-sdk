using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PinPadSDK.Commands;

namespace PinPadSdkTests.Commands {
    [TestClass]
    public class DspRequestTests {
        [TestMethod]
        public void ValidateDspRequestWarnings() {
            DspRequest request = new DspRequest();

            BasePropertyTestUtils.TestProperty(request, request.DSP_MSG);

            Assert.AreEqual("DSP03212345678901234567890123456789012",
                request.CommandString);
            BasePropertyTestUtils.TestCommandString(request);
        }
    }
}
