using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PinPadSDK.Commands.Stone;
using PinPadSDK.Enums;
using PinPadSDK.Commands.Stone.PrtActionData;
using PinPadSdkTests.Commands.Stone.PrtActionData;
using System.Reflection;
using PinPadSDK.Property;

namespace PinPadSdkTests.Commands.Stone {
    [TestClass]
    public class PrtResponseTests {
        [TestMethod]
        public void ValidatePrtResponseWithBeginData() {
            PrtResponse response = new PrtResponse();

            Assert.IsTrue(response.IsBlockingCommand, "PrtResponse not is marked as a blocking command.");

            BasePropertyTestUtils.TestBaseResponse(response);

            Assert.AreEqual(PrtAction.Undefined, response.PRT_ACTION);

            Assert.IsNull(response.BeginData);
            Assert.IsNull(response.EndData);
            Assert.IsNull(response.AppendStringData);
            Assert.IsNull(response.AppendImageData);
            Assert.IsNull(response.EndData);

            PrtBeginResponseData data = new PrtBeginResponseData();
            PrtBeginResponseDataTests.ValidatePrtBeginResponseDataWarnings(data);
            response.BeginData = data;

            Assert.AreEqual(PrtAction.Begin, response.PRT_ACTION);

            Assert.AreEqual(data, response.BeginData);
            Assert.IsNull(response.EndData);
            Assert.IsNull(response.AppendStringData);
            Assert.IsNull(response.AppendImageData);
            Assert.IsNull(response.EndData);

            Assert.AreEqual("PRT0000040000",
                response.CommandString);
            BasePropertyTestUtils.TestCommandString(response);

            BasePropertyTestUtils.TestBaseResponse(response);
        }
        [TestMethod]
        public void ValidatePrtResponseWithEndData() {
            PrtResponse response = new PrtResponse();

            Assert.IsTrue(response.IsBlockingCommand, "PrtResponse not is marked as a blocking command.");

            BasePropertyTestUtils.TestBaseResponse(response);

            Assert.AreEqual(PrtAction.Undefined, response.PRT_ACTION);

            Assert.IsNull(response.BeginData);
            Assert.IsNull(response.EndData);
            Assert.IsNull(response.AppendStringData);
            Assert.IsNull(response.AppendImageData);
            Assert.IsNull(response.EndData);

            PrtEndResponseData data = new PrtEndResponseData();
            response.EndData = data;

            Assert.AreEqual(PrtAction.End, response.PRT_ACTION);

            Assert.IsNull(response.BeginData);
            Assert.AreEqual(data, response.EndData);
            Assert.IsNull(response.AppendStringData);
            Assert.IsNull(response.AppendImageData);
            Assert.IsNull(response.StepData);

            Assert.AreEqual("PRT0000011",
                response.CommandString);
            BasePropertyTestUtils.TestCommandString(response);

            BasePropertyTestUtils.TestBaseResponse(response);
        }
        [TestMethod]
        public void ValidatePrtResponseWithAppendStringData() {
            PrtResponse response = new PrtResponse();

            Assert.IsTrue(response.IsBlockingCommand, "PrtResponse not is marked as a blocking command.");

            BasePropertyTestUtils.TestBaseResponse(response);

            Assert.AreEqual(PrtAction.Undefined, response.PRT_ACTION);

            Assert.IsNull(response.BeginData);
            Assert.IsNull(response.EndData);
            Assert.IsNull(response.AppendStringData);
            Assert.IsNull(response.AppendImageData);
            Assert.IsNull(response.EndData);

            PrtAppendStringResponseData data = new PrtAppendStringResponseData();
            response.AppendStringData = data;

            Assert.AreEqual(PrtAction.AppendString, response.PRT_ACTION);

            Assert.IsNull(response.BeginData);
            Assert.IsNull(response.EndData);
            Assert.AreEqual(data, response.AppendStringData);
            Assert.IsNull(response.AppendImageData);
            Assert.IsNull(response.EndData);

            Assert.AreEqual("PRT0000012",
                response.CommandString);
            BasePropertyTestUtils.TestCommandString(response);

            BasePropertyTestUtils.TestBaseResponse(response);
        }
        [TestMethod]
        public void ValidatePrtResponseWithAppendImageData() {
            PrtResponse response = new PrtResponse();

            Assert.IsTrue(response.IsBlockingCommand, "PrtResponse not is marked as a blocking command.");

            BasePropertyTestUtils.TestBaseResponse(response);

            Assert.AreEqual(PrtAction.Undefined, response.PRT_ACTION);

            Assert.IsNull(response.BeginData);
            Assert.IsNull(response.EndData);
            Assert.IsNull(response.AppendStringData);
            Assert.IsNull(response.AppendImageData);
            Assert.IsNull(response.EndData);

            PrtAppendImageResponseData data = new PrtAppendImageResponseData();
            response.AppendImageData = data;

            Assert.AreEqual(PrtAction.AppendImage, response.PRT_ACTION);

            Assert.IsNull(response.BeginData);
            Assert.IsNull(response.EndData);
            Assert.IsNull(response.AppendStringData);
            Assert.AreEqual(data, response.AppendImageData);
            Assert.IsNull(response.EndData);

            Assert.AreEqual("PRT0000013",
                response.CommandString);
            BasePropertyTestUtils.TestCommandString(response);

            BasePropertyTestUtils.TestBaseResponse(response);
        }
        [TestMethod]
        public void ValidatePrtResponseWithStepData() {
            PrtResponse response = new PrtResponse();

            Assert.IsTrue(response.IsBlockingCommand, "PrtResponse not is marked as a blocking command.");

            BasePropertyTestUtils.TestBaseResponse(response);

            Assert.AreEqual(PrtAction.Undefined, response.PRT_ACTION);

            Assert.IsNull(response.BeginData);
            Assert.IsNull(response.EndData);
            Assert.IsNull(response.AppendStringData);
            Assert.IsNull(response.AppendImageData);
            Assert.IsNull(response.EndData);

            PrtStepResponseData data = new PrtStepResponseData();
            response.StepData = data;

            Assert.AreEqual(PrtAction.Step, response.PRT_ACTION);

            Assert.IsNull(response.BeginData);
            Assert.IsNull(response.EndData);
            Assert.IsNull(response.AppendStringData);
            Assert.IsNull(response.AppendImageData);
            Assert.AreEqual(data, response.StepData);

            Assert.AreEqual("PRT0000014",
                response.CommandString);
            BasePropertyTestUtils.TestCommandString(response);

            BasePropertyTestUtils.TestBaseResponse(response);
        }
        [TestMethod]
        public void ValidatePrtRequestActionDataStringParser() {
            MethodInfo actionDataStringParserMethod = typeof(PrtResponse).GetMethod("ActionDataStringParser", BindingFlags.Static | BindingFlags.Instance | BindingFlags.NonPublic);
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
