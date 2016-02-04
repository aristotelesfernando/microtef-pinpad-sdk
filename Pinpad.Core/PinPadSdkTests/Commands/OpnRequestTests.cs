using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PinPadSDK.Commands;

namespace PinPadSdkTests.Commands {
    [TestClass]
    public class OpnRequestTests {
        [TestMethod]
        public void ValidateOpnCommandString() {
            OpnRequest request = new OpnRequest();

            Assert.AreEqual("OPN",
                request.CommandString);
            BasePropertyTestUtils.TestCommandString(request);
        }
    }
}
