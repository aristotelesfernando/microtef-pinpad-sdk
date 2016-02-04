using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PinPadSDK.Commands.Stone;
using PinPadSDK.Commands.Stone.PrtActionData;
using PinPadSDK.Enums;
using PinPadSdkTests.Commands.Stone.PrtActionData;
using System.Reflection;
using PinPadSDK.Property;

namespace PinPadSdkTests.Commands.Stone {
    [TestClass]
    public class PrtRequestTests {
        [TestMethod]
        public void ValidatePrtRequestWarningsWithBeginData() {
            PrtRequest request = new PrtRequest();

            Assert.AreEqual(1, request.MinimumStoneVersion);

            Assert.AreEqual(PrtAction.Undefined, request.PRT_ACTION);

            Assert.IsNull(request.BeginData);
            Assert.IsNull(request.EndData);
            Assert.IsNull(request.AppendStringData);
            Assert.IsNull(request.AppendImageData);
            Assert.IsNull(request.StepData);

            PrtBeginRequestData data = new PrtBeginRequestData();
            request.BeginData = data;

            Assert.AreEqual(PrtAction.Begin, request.PRT_ACTION);

            Assert.AreEqual(data, request.BeginData);
            Assert.IsNull(request.EndData);
            Assert.IsNull(request.AppendStringData);
            Assert.IsNull(request.AppendImageData);
            Assert.IsNull(request.StepData);

            Assert.AreEqual("PRT0010",
                request.CommandString);
            BasePropertyTestUtils.TestCommandString(request);
        }
        [TestMethod]
        public void ValidatePrtRequestWarningsWithEndData() {
            PrtRequest request = new PrtRequest();

            Assert.AreEqual(1, request.MinimumStoneVersion);

            Assert.AreEqual(PrtAction.Undefined, request.PRT_ACTION);

            Assert.IsNull(request.BeginData);
            Assert.IsNull(request.EndData);
            Assert.IsNull(request.AppendStringData);
            Assert.IsNull(request.AppendImageData);
            Assert.IsNull(request.StepData);

            PrtEndRequestData data = new PrtEndRequestData();
            request.EndData = data;

            Assert.AreEqual(PrtAction.End, request.PRT_ACTION);

            Assert.IsNull(request.BeginData);
            Assert.AreEqual(data, request.EndData);
            Assert.IsNull(request.AppendStringData);
            Assert.IsNull(request.AppendImageData);
            Assert.IsNull(request.StepData);

            Assert.AreEqual("PRT0011",
                request.CommandString);
            BasePropertyTestUtils.TestCommandString(request);
        }
        [TestMethod]
        public void ValidatePrtRequestWarningsWithAppendStringData() {
            PrtRequest request = new PrtRequest();

            Assert.AreEqual(1, request.MinimumStoneVersion);

            Assert.AreEqual(PrtAction.Undefined, request.PRT_ACTION);

            Assert.IsNull(request.BeginData);
            Assert.IsNull(request.EndData);
            Assert.IsNull(request.AppendStringData);
            Assert.IsNull(request.AppendImageData);
            Assert.IsNull(request.StepData);

            PrtAppendStringRequestData data = new PrtAppendStringRequestData();
            PrtAppendStringRequestDataTests.ValidatePrtAppendStringRequestDataWarnings(data);
            request.AppendStringData = data;

            Assert.AreEqual(PrtAction.AppendString, request.PRT_ACTION);

            Assert.IsNull(request.BeginData);
            Assert.IsNull(request.EndData);
            Assert.AreEqual(data, request.AppendStringData);
            Assert.IsNull(request.AppendImageData);
            Assert.IsNull(request.StepData);

            Assert.AreEqual("PRT51820051212345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012",
                request.CommandString);
            BasePropertyTestUtils.TestCommandString(request);
        }
        [TestMethod]
        public void ValidatePrtRequestWarningsWithAppendImageData() {
            PrtRequest request = new PrtRequest();

            Assert.AreEqual(1, request.MinimumStoneVersion);

            Assert.AreEqual(PrtAction.Undefined, request.PRT_ACTION);

            Assert.IsNull(request.BeginData);
            Assert.IsNull(request.EndData);
            Assert.IsNull(request.AppendStringData);
            Assert.IsNull(request.AppendImageData);
            Assert.IsNull(request.StepData);

            PrtAppendImageRequestData data = new PrtAppendImageRequestData();
            PrtAppendImageRequestDataTests.ValidatePrtAppendStringRequestDataWarnings(data);
            request.AppendImageData = data;

            Assert.AreEqual(PrtAction.AppendImage, request.PRT_ACTION);

            Assert.IsNull(request.BeginData);
            Assert.IsNull(request.EndData);
            Assert.IsNull(request.AppendStringData);
            Assert.AreEqual(data, request.AppendImageData);
            Assert.IsNull(request.StepData);

            Assert.AreEqual("PRT02331234015123456789012345",
                request.CommandString);
            BasePropertyTestUtils.TestCommandString(request);
        }
        [TestMethod]
        public void ValidatePrtRequestWarningsWithStepData() {
            PrtRequest request = new PrtRequest();

            Assert.AreEqual(1, request.MinimumStoneVersion);
            Assert.AreEqual(PrtAction.Undefined, request.PRT_ACTION);

            Assert.IsNull(request.BeginData);
            Assert.IsNull(request.EndData);
            Assert.IsNull(request.AppendStringData);
            Assert.IsNull(request.AppendImageData);
            Assert.IsNull(request.StepData);


            PrtStepRequestData data = new PrtStepRequestData();
            PrtStepRequestDataTests.ValidatePrtAppendStringRequestDataWarnings(data);
            request.StepData = data;

            Assert.AreEqual(PrtAction.Step, request.PRT_ACTION);

            Assert.IsNull(request.BeginData);
            Assert.IsNull(request.EndData);
            Assert.IsNull(request.AppendStringData);
            Assert.IsNull(request.AppendImageData);
            Assert.AreEqual(data, request.StepData);

            Assert.AreEqual("PRT00541234",
                request.CommandString);
            BasePropertyTestUtils.TestCommandString(request);
        }
        [TestMethod]
        public void ValidatePrtRequestActionDataStringParser() {
            MethodInfo actionDataStringParserMethod = typeof(PrtRequest).GetMethod("ActionDataStringParser", BindingFlags.Static | BindingFlags.Instance | BindingFlags.NonPublic);
            try {
                actionDataStringParserMethod.Invoke(null, new object[] { new StringReader("9") });
                Assert.Fail();
            }
            catch (TargetInvocationException ex) {
                Assert.IsInstanceOfType(ex.InnerException, typeof(InvalidOperationException));
            }
        }
    }
}
