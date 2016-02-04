using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PinPadSDK.Commands;
using PinPadSDK.Enums;
using PinPadSDK.Commands.CkeEventsData;
using PinPadSdkTests.Commands.CkeEventsData;
using System.Reflection;
using PinPadSDK.Property;

namespace PinPadSdkTests.Commands {
    [TestClass]
    public class CkeResponseTests {
        [TestMethod]
        public void ValidateCkeResponseWarningsWithCkeKeyPressResponseData() {
            CkeResponse response = new CkeResponse();

            Assert.IsTrue(response.IsBlockingCommand, "CkeResponse is not marked as a blocking command.");

            BasePropertyTestUtils.TestBaseResponse(response);

            Assert.AreEqual(CkeEvent.Undefined, response.CKE_EVENT);

            Assert.IsNull(response.KeyPressData);
            Assert.IsNull(response.MagneticStripeData);
            Assert.IsNull(response.IccData);
            Assert.IsNull(response.CtlsData);

            CkeKeyPressResponseData data = new CkeKeyPressResponseData();
            CkeKeyPressResponseDataTests.ValidateCkeKeyPressResponseDataWarnings(data);
            response.KeyPressData = data;

            Assert.AreEqual(CkeEvent.KeyPress, response.CKE_EVENT);

            Assert.AreEqual(data, response.KeyPressData);
            Assert.IsNull(response.MagneticStripeData);
            Assert.IsNull(response.IccData);
            Assert.IsNull(response.CtlsData);

            Assert.AreEqual("CKE000003000",
                response.CommandString);
            BasePropertyTestUtils.TestCommandString(response);

            BasePropertyTestUtils.TestBaseResponse(response);
        }
        [TestMethod]
        public void ValidateCkeResponseWarningsWithCkeMagneticStripeResponseData() {
            CkeResponse response = new CkeResponse();

            Assert.IsTrue(response.IsBlockingCommand, "CkeResponse is not marked as a blocking command.");

            BasePropertyTestUtils.TestBaseResponse(response);

            Assert.AreEqual(CkeEvent.Undefined, response.CKE_EVENT);

            Assert.IsNull(response.KeyPressData);
            Assert.IsNull(response.MagneticStripeData);
            Assert.IsNull(response.IccData);
            Assert.IsNull(response.CtlsData);

            CkeMagneticStripeResponseData data = new CkeMagneticStripeResponseData();
            CkeMagneticStripeResponseDataTests.ValidateCkeMagneticStripeResponseDataWarnings(data);
            response.MagneticStripeData = data;

            Assert.AreEqual(CkeEvent.MagneticStripeCardPassed, response.CKE_EVENT);

            Assert.IsNull(response.KeyPressData);
            Assert.AreEqual(data, response.MagneticStripeData);
            Assert.IsNull(response.IccData);
            Assert.IsNull(response.CtlsData);

            Assert.AreEqual("CKE000225176C1234567890123456789^12345678901234567890123456^1501123123456789012345678901371234567890123456789=1501123123456789010412345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234",
                response.CommandString);
            BasePropertyTestUtils.TestCommandString(response);

            BasePropertyTestUtils.TestBaseResponse(response);
        }
        [TestMethod]
        public void ValidateCkeResponseWarningsWithCkeIccResponseData() {
            CkeResponse response = new CkeResponse();

            Assert.IsTrue(response.IsBlockingCommand, "CkeResponse is not marked as a blocking command.");

            BasePropertyTestUtils.TestBaseResponse(response);

            Assert.AreEqual(CkeEvent.Undefined, response.CKE_EVENT);

            Assert.IsNull(response.KeyPressData);
            Assert.IsNull(response.MagneticStripeData);
            Assert.IsNull(response.IccData);
            Assert.IsNull(response.CtlsData);

            CkeIccResponseData data = new CkeIccResponseData();
            CkeIccResponseDataTests.ValidateCkeIccResponseDataWarnings(data);
            response.IccData = data;

            Assert.AreEqual(CkeEvent.IccStatusChanged, response.CKE_EVENT);

            Assert.IsNull(response.KeyPressData);
            Assert.IsNull(response.MagneticStripeData);
            Assert.AreEqual(data, response.IccData);
            Assert.IsNull(response.CtlsData);

            Assert.AreEqual("CKE00000221",
                response.CommandString);
            BasePropertyTestUtils.TestCommandString(response);

            BasePropertyTestUtils.TestBaseResponse(response);
        }
        [TestMethod]
        public void ValidateCkeResponseWarningsWithCkeContactlessResponseData() {
            CkeResponse response = new CkeResponse();

            Assert.IsTrue(response.IsBlockingCommand, "CkeResponse is not marked as a blocking command.");

            BasePropertyTestUtils.TestBaseResponse(response);

            Assert.AreEqual(CkeEvent.Undefined, response.CKE_EVENT);

            Assert.IsNull(response.KeyPressData);
            Assert.IsNull(response.MagneticStripeData);
            Assert.IsNull(response.IccData);
            Assert.IsNull(response.CtlsData);

            CkeContactlessResponseData data = new CkeContactlessResponseData();
            CkeContactlessResponseDataTests.ValidateCkeContactlessResponseDataWarnings(data);
            response.CtlsData = data;

            Assert.AreEqual(CkeEvent.CtlsUpdate, response.CKE_EVENT);

            Assert.IsNull(response.KeyPressData);
            Assert.IsNull(response.MagneticStripeData);
            Assert.IsNull(response.IccData);
            Assert.AreEqual(data, response.CtlsData);

            Assert.AreEqual("CKE00000231",
                response.CommandString);
            BasePropertyTestUtils.TestCommandString(response);

            BasePropertyTestUtils.TestBaseResponse(response);
        }
        [TestMethod]
        public void ValidateCkeResponseEventDataStringParser() {
            MethodInfo eventDataStringParserMethod = typeof(CkeResponse).GetMethod("EventDataStringParser", BindingFlags.Static | BindingFlags.Instance | BindingFlags.NonPublic);
            try {
                eventDataStringParserMethod.Invoke(null, new object[]{new StringReader("9")});
                Assert.Fail();
            }
            catch (TargetInvocationException ex) {
                Assert.IsInstanceOfType(ex.InnerException, typeof(InvalidOperationException));
            }
        }
    }
}
