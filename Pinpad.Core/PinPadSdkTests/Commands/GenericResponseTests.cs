using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PinPadSDK.Commands;

namespace PinPadSdkTests.Commands {
    [TestClass]
    public class GenericResponseTests {
        [TestMethod]
        public void ValidateGenericResponseWarnings() {
            GenericResponse response = new GenericResponse();

            Assert.IsFalse(response.IsBlockingCommand, "GenericResponse is marked as a blocking command.");

            string commandName = "123";
            response.SetCommandName(commandName);
            Assert.AreEqual(commandName, response.CommandName);

            BasePropertyTestUtils.TestBaseResponse(response);
            BasePropertyTestUtils.TestCommandString(response);
        }
    }
}
