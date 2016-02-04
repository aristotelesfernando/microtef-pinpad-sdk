using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PinPadSDK.Commands;

namespace PinPadSdkTests.Commands {
    [TestClass]
    public class CloRequestTests {
        [TestMethod]
        public void ValidateCloRequestWarnings() {
            CloRequest request = new CloRequest();

            BasePropertyTestUtils.TestProperty(request, request.CLO_MSG);

            Assert.AreEqual("CLO03212345678901234567890123456789012",
                request.CommandString);
            BasePropertyTestUtils.TestCommandString(request);
        }
    }
}
