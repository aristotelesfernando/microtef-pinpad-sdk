using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PinPadSDK.Commands;
using PinPadSDK.Commands.Gpn;
using PinPadSdkTests.Commands.Gpn;

namespace PinPadSdkTests.Commands {
    [TestClass]
    public class GpnRequestTests {
        [TestMethod]
        public void ValidateGpnRequestWarnings() {
            GpnRequest request = new GpnRequest();

            BasePropertyTestUtils.TestProperty(request, request.GPN_METHOD);
            BasePropertyTestUtils.TestProperty(request, request.GPN_KEYIDX, 2);
            BasePropertyTestUtils.TestProperty(request, request.GPN_WKENC, 32);
            BasePropertyTestUtils.TestProperty(request, request.GPN_PAN, 19);

            GpnPinEntryRequest entry = new GpnPinEntryRequest();
            GpnPinEntryRequestTests.ValidateGpnPinEntryRequestWarnings(entry);
            request.GPN_ENTRIES.Value.Add(entry);

            Assert.AreEqual("GPN093312123456789012345678901234567890121912345678901234567891121212345678901234567890123456789012",
                request.CommandString);
            BasePropertyTestUtils.TestCommandString(request);
        }
    }
}
