using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PinPadSDK.Commands.Stone;
using PinPadSDK.Enums;

namespace PinPadSdkTests.Commands.Stone {
    [TestClass]
    public class GkeRequestTests {
        [TestMethod]
        public void ValidateGkeRequestWarnings() {
            GkeRequest request = new GkeRequest();

            Assert.AreEqual(1, request.MinimumStoneVersion);

            BasePropertyTestUtils.TestProperty<GkeAction>(request, request.GKE_ACTION, 1);

            Assert.AreEqual("GKE0010",
                request.CommandString);
            BasePropertyTestUtils.TestCommandString(request);
        }
    }
}
