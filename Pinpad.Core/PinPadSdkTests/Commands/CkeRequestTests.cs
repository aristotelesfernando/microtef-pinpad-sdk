using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PinPadSDK.Commands;
using PinPadSDK.Enums;

namespace PinPadSdkTests.Commands {
    [TestClass]
    public class CkeRequestTests {
        [TestMethod]
        public void ValidateCkeRequestWarnings() {
            CkeRequest request = new CkeRequest();

            BasePropertyTestUtils.TestProperty(request, request.CKE_KEY);
            BasePropertyTestUtils.TestProperty(request, request.CKE_MAG);
            BasePropertyTestUtils.TestProperty < StatusWatcherMode>(request, request.CKE_ICC, 1);

            Assert.AreEqual("CKE003110", request.CommandString);

            BasePropertyTestUtils.TestProperty(request, request.CKE_CTLS, true);
            Assert.AreEqual("CKE0041101", request.CommandString);

            BasePropertyTestUtils.TestCommandString(request);
        }
    }
}
