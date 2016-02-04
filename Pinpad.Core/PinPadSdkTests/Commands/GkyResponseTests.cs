using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PinPadSDK.Commands;
using PinPadSDK.Enums;

namespace PinPadSdkTests.Commands {
    [TestClass]
    public class GkyResponseTests {
        [TestMethod]
        public void ValidateGkyResponseWarnings() {
            GkyResponse response = new GkyResponse();

            Assert.IsTrue(response.IsBlockingCommand, "GpnResponse is not marked as a blocking command.");

            Assert.AreEqual(PinPadKey.Undefined, response.PressedKey);

            BasePropertyTestUtils.TestBaseResponse(response);

            response.RSP_STAT.Value = ResponseStatus.ST_OK;
            Assert.AreEqual("GKY000",
                response.CommandString);
            Assert.AreEqual(ResponseStatus.ST_OK, response.RSP_STAT.Value);
            Assert.AreEqual(PinPadKey.Return, response.PressedKey);
            response.PressedKey = PinPadKey.Return;
            Assert.AreEqual(ResponseStatus.ST_OK, response.RSP_STAT.Value);
            Assert.AreEqual(PinPadKey.Return, response.PressedKey);
            BasePropertyTestUtils.TestCommandString(response);

            response.RSP_STAT.Value = PinPadSDK.Enums.ResponseStatus.ST_F1;
            Assert.AreEqual("GKY004",
                response.CommandString);
            Assert.AreEqual(ResponseStatus.ST_F1, response.RSP_STAT.Value);
            Assert.AreEqual(PinPadKey.Function1, response.PressedKey);
            response.PressedKey = PinPadKey.Function1;
            Assert.AreEqual(ResponseStatus.ST_F1, response.RSP_STAT.Value);
            Assert.AreEqual(PinPadKey.Function1, response.PressedKey);
            BasePropertyTestUtils.TestCommandString(response);

            response.RSP_STAT.Value = PinPadSDK.Enums.ResponseStatus.ST_F2;
            Assert.AreEqual("GKY005",
                response.CommandString);
            Assert.AreEqual(ResponseStatus.ST_F2, response.RSP_STAT.Value);
            Assert.AreEqual(PinPadKey.Function2, response.PressedKey);
            response.PressedKey = PinPadKey.Function2;
            Assert.AreEqual(ResponseStatus.ST_F2, response.RSP_STAT.Value);
            Assert.AreEqual(PinPadKey.Function2, response.PressedKey);
            BasePropertyTestUtils.TestCommandString(response);

            response.RSP_STAT.Value = PinPadSDK.Enums.ResponseStatus.ST_F3;
            Assert.AreEqual("GKY006",
                response.CommandString);
            Assert.AreEqual(ResponseStatus.ST_F3, response.RSP_STAT.Value);
            Assert.AreEqual(PinPadKey.Function3, response.PressedKey);
            response.PressedKey = PinPadKey.Function3;
            Assert.AreEqual(ResponseStatus.ST_F3, response.RSP_STAT.Value);
            Assert.AreEqual(PinPadKey.Function3, response.PressedKey);
            BasePropertyTestUtils.TestCommandString(response);

            response.RSP_STAT.Value = PinPadSDK.Enums.ResponseStatus.ST_F4;
            Assert.AreEqual("GKY007",
                response.CommandString);
            Assert.AreEqual(ResponseStatus.ST_F4, response.RSP_STAT.Value);
            Assert.AreEqual(PinPadKey.Function4, response.PressedKey);
            response.PressedKey = PinPadKey.Function4;
            Assert.AreEqual(ResponseStatus.ST_F4, response.RSP_STAT.Value);
            Assert.AreEqual(PinPadKey.Function4, response.PressedKey);
            BasePropertyTestUtils.TestCommandString(response);

            response.RSP_STAT.Value = PinPadSDK.Enums.ResponseStatus.ST_BACKSP;
            Assert.AreEqual("GKY008",
                response.CommandString);
            Assert.AreEqual(ResponseStatus.ST_BACKSP, response.RSP_STAT.Value);
            Assert.AreEqual(PinPadKey.Backspace, response.PressedKey);
            response.PressedKey = PinPadKey.Backspace;
            Assert.AreEqual(ResponseStatus.ST_BACKSP, response.RSP_STAT.Value);
            Assert.AreEqual(PinPadKey.Backspace, response.PressedKey);
            BasePropertyTestUtils.TestCommandString(response);

            response.RSP_STAT.Value = PinPadSDK.Enums.ResponseStatus.ST_CANCEL;
            Assert.AreEqual("GKY013",
                response.CommandString);
            Assert.AreEqual(ResponseStatus.ST_CANCEL, response.RSP_STAT.Value);
            Assert.AreEqual(PinPadKey.Cancel, response.PressedKey);
            response.PressedKey = PinPadKey.Cancel;
            Assert.AreEqual(ResponseStatus.ST_CANCEL, response.RSP_STAT.Value);
            Assert.AreEqual(PinPadKey.Cancel, response.PressedKey);
            BasePropertyTestUtils.TestCommandString(response);

            response.RSP_STAT.Value = ResponseStatus.ST_INTERR;
            Assert.AreEqual("GKY040",
                response.CommandString);
            Assert.AreEqual(ResponseStatus.ST_INTERR, response.RSP_STAT.Value);
            Assert.AreEqual(PinPadKey.Undefined, response.PressedKey);
            response.PressedKey = (PinPadKey)ResponseStatus.ST_INTERR;
            Assert.AreEqual(ResponseStatus.ST_INTERR, response.RSP_STAT.Value);
            Assert.AreEqual(PinPadKey.Undefined, response.PressedKey);
            BasePropertyTestUtils.TestCommandString(response);

            BasePropertyTestUtils.TestBaseResponse(response);
        }
    }
}
