using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PinPadSDK.Commands.Stone;
using System.Globalization;

namespace PinPadSdkTests.Commands.Stone {
    [TestClass]
    public class StmRequestTests {
        [TestMethod]
        public void ValidateStmRequestWarnings() {
            string timestring = "150102030405";
            DateTime dateTime = DateTime.ParseExact(timestring, "yyMMddHHmmss", CultureInfo.InvariantCulture);

            StmRequest request = new StmRequest();

            Assert.AreEqual(1, request.MinimumStoneVersion);

            BasePropertyTestUtils.TestProperty(request, request.STM_DATETIME, dateTime);

            Assert.AreEqual("STM012150102030405",
                request.CommandString);
            BasePropertyTestUtils.TestCommandString(request);
        }
    }
}
