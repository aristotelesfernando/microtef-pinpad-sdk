using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PinPadSDK.Commands;

namespace PinPadSdkTests.Commands {
    [TestClass]
    public class GkyRequestTests {
        [TestMethod]
        public void ValidateGkyCommandString() {
            GkyRequest request = new GkyRequest();

            Assert.AreEqual("GKY",
                request.CommandString);
            BasePropertyTestUtils.TestCommandString(request);
        }
    }
}
