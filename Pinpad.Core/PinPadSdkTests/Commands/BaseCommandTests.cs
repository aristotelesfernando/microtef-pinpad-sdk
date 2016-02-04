using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PinPadSDK.Commands;
using PinPadSDK.Exceptions;

namespace PinPadSdkTests.Commands {
    [TestClass]
    public class BaseCommandTests {
        public class BaseCommandMock : BaseCommand {
            public override string CommandName {
                get { return "123"; }
            }

            public void SetCommandName(string value) {
                this.CMD_ID.Value = value;
            }
        }

        [TestMethod]
        public void ValidateBaseCommandWrongNameParseError() {
            BaseCommandMock command = new BaseCommandMock();

            try {
                command.CommandString = "321000";
                Assert.Fail("Did not complain about wrong command name");
            }
            catch (PropertyParseException ex) {
                Assert.IsInstanceOfType(ex.InnerException, typeof(CommandNameMismatchException), "Did not complain about wrong command name");
            }
        }

        [TestMethod]
        public void ValidateBaseCommandNameChangedError() {
            BaseCommandMock command = new BaseCommandMock();
            PrivateObject privateCommand = new PrivateObject(command);

            Assert.IsTrue(BasePropertyTestUtils.IsActionThrowingExceptions(() => { command.SetCommandName(null); }), "Did not complain about setting null command name");

            try {
                command.SetCommandName("321");
                Assert.Fail("Did not complain about setting wrong command name");
            }
            catch (InvalidValueException ex) {
                Assert.IsInstanceOfType(ex.InnerException, typeof(CommandNameMismatchException), "Did not complain about setting wrong command name");
            }
        }
    }
}
