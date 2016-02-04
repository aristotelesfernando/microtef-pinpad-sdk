using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PinPadSDK.Commands;

namespace PinPadSdkTests.Commands {
    [TestClass]
    public class Gin00ResponseTests {
        [TestMethod]
        public void ValidateGin00ResponseWarnings() {
            Gin00Response response = new Gin00Response();

            Assert.IsFalse(response.IsBlockingCommand, "Gin00Response is marked as a blocking command.");

            BasePropertyTestUtils.TestBaseResponse(response);

            BasePropertyTestUtils.TestProperty(response, response.GIN_MNAME, 20);
            BasePropertyTestUtils.TestProperty(response, response.GIN_MODEL, 19);
            BasePropertyTestUtils.TestProperty(response, response.GIN_CTLSUP, 1);
            BasePropertyTestUtils.TestProperty(response, response.GIN_SOVER, 20);
            BasePropertyTestUtils.TestProperty(response, response.GIN_SPECVER, 4);
            BasePropertyTestUtils.TestProperty(response, response.GIN_MANVER, 16);
            BasePropertyTestUtils.TestProperty(response, response.GIN_SERNUM, 20);
            Assert.AreEqual("GIN0001001234567890123456789012345678901234567891123456789012345678901234123456789012345612345678901234567890",
                response.CommandString);
            BasePropertyTestUtils.TestCommandString(response);

            BasePropertyTestUtils.TestBaseResponse(response);
        }
    }
}
