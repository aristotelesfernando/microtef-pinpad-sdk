using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PinPadSDK.Commands;
using PinPadSDK.Enums;

namespace PinPadSdkTests.Commands {
    [TestClass]
    public class GocResponseTests {
        [TestMethod]
        public void ValidateGocResponseWarnings() {
            GocResponse response = new GocResponse();

            Assert.IsTrue(response.IsBlockingCommand, "GocResponse is not marked as a blocking command.");

            BasePropertyTestUtils.TestBaseResponse(response);

            BasePropertyTestUtils.TestProperty<OfflineTransactionStatus>(response, response.GOC_DECISION, 1);
            BasePropertyTestUtils.TestProperty(response, response.GOC_SIGNAT);
            BasePropertyTestUtils.TestProperty(response, response.GOC_PINOFF);
            BasePropertyTestUtils.TestProperty(response, response.GOC_ERRPINOFF, 1);
            BasePropertyTestUtils.TestProperty(response, response.GOC_PBLOCKED);
            BasePropertyTestUtils.TestProperty(response, response.GOC_PINONL);
            BasePropertyTestUtils.TestProperty(response, response.GOC_PINBLK, 16);
            BasePropertyTestUtils.TestProperty(response, response.GOC_KSN, 20);
            BasePropertyTestUtils.TestProperty(response, response.GOC_EMVDAT, 512);

            Assert.AreEqual("GOC00056001111112345678901234561234567890123456789025612345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012000",
                response.CommandString);
            BasePropertyTestUtils.TestCommandString(response);

            BasePropertyTestUtils.TestBaseResponse(response);
        }
    }
}
