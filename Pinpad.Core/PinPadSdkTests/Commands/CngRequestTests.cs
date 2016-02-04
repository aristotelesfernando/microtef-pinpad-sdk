using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PinPadSDK.Commands;

namespace PinPadSdkTests.Commands {
    [TestClass]
    public class CngRequestTests {
        [TestMethod]
        public void ValidateCngRequestWarnings() {
            CngRequest request = new CngRequest();

            BasePropertyTestUtils.TestProperty(request, request.CNG_EMVDAT, 198);

            Assert.AreEqual("CNG20099123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678",
                request.CommandString);
            BasePropertyTestUtils.TestCommandString(request);
        }
    }
}
