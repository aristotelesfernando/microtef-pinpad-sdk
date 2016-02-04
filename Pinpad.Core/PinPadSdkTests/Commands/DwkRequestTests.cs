using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PinPadSDK.Commands;
using PinPadSDK.Enums;
using PinPadSDK.Commands.DwkModesData;
using PinPadSdkTests.Commands.DwkModesData;
using System.Reflection;
using PinPadSDK.Property;

namespace PinPadSdkTests.Commands {
    [TestClass]
    public class DwkRequestTests {
        [TestMethod]
        public void ValidateDwkRequestWarningsWithDwkMode1RequestData() {
            DwkRequest request = new DwkRequest();

            Assert.AreEqual(DwkMode.Undefined, request.DWK_MODE);

            Assert.IsNull(request.DWK_MODE1);
            Assert.IsNull(request.DWK_MODE2);

            DwkMode1RequestData data = new DwkMode1RequestData();
            DwkMode1RequestDataTests.ValidateDwkMode1RequestDataWarnings(data);
            request.DWK_MODE1 = data;

            Assert.AreEqual(DwkMode.Mode1, request.DWK_MODE);

            Assert.AreEqual(data, request.DWK_MODE1);
            Assert.IsNull(request.DWK_MODE2);

            Assert.AreEqual("DWK036031212345678901234567890123456789012",
                request.CommandString);
            BasePropertyTestUtils.TestCommandString(request);
        }
        [TestMethod]
        public void ValidateDwkRequestWarningsWithDwkMode2RequestData() {
            DwkRequest request = new DwkRequest();

            Assert.AreEqual(DwkMode.Undefined, request.DWK_MODE);

            Assert.IsNull(request.DWK_MODE1);
            Assert.IsNull(request.DWK_MODE2);

            DwkMode2RequestData data = new DwkMode2RequestData();
            DwkMode2RequestDataTests.ValidateDwkMode2RequestDataWarnings(data);
            request.DWK_MODE2 = data;

            Assert.AreEqual(DwkMode.Mode2, request.DWK_MODE);

            Assert.IsNull(request.DWK_MODE1);
            Assert.AreEqual(data, request.DWK_MODE2);

            Assert.AreEqual("DWK26311234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456123456",
                request.CommandString);
            BasePropertyTestUtils.TestCommandString(request);
        }
        [TestMethod]
        public void ValidateDwkRequestEventDataStringParser() {
            MethodInfo eventDataStringParserMethod = typeof(DwkRequest).GetMethod("EventDataStringParser", BindingFlags.Static | BindingFlags.Instance | BindingFlags.NonPublic);
            try {
                eventDataStringParserMethod.Invoke(null, new object[] { new StringReader("9") });
                Assert.Fail();
            }
            catch (TargetInvocationException ex) {
                Assert.IsInstanceOfType(ex.InnerException, typeof(InvalidOperationException));
            }
        }
    }
}
