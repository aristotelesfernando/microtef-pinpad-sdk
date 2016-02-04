using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PinPadSDK.Commands.Stone;
using PinPadSDK.Enums;

namespace PinPadSdkTests.Commands.Stone {
    [TestClass]
    public class PsmRequestTests {
        [TestMethod]
        public void ValidatePsmRequestWarnings() {
            PsmRequest request = new PsmRequest();

            Assert.AreEqual(1, request.MinimumStoneVersion);

            BasePropertyTestUtils.TestProperty<BacklightStatus>(request, request.PSM_LIGHTSTATUS, 1);
            BasePropertyTestUtils.TestProperty(request, request.PSM_SLEEPTIME, 3);

            Assert.AreEqual("PSM0040123",
                request.CommandString);
            BasePropertyTestUtils.TestCommandString(request);
        }
    }
}
