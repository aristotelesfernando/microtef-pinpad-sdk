using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PinPadSDK.PinPad;
using PinPadSDK.Property;
using PinPadSDK.Commands.Stone;
using PinPadSDK.Exceptions;

namespace PinPadSdkTests.PinPad {
    [TestClass]
    public class PinPadDisplayTests {
        [TestInitialize]
        public void TestSetup() {
            CrossPlatformDesktop.CrossPlatformDesktopInitializer.Initialize_CrossPlatformDesktop();
            //CrossPlatformController.SendMailController = new SendMailControllerMock();
        }
        [TestMethod]
        public void ValidatePinPadDisplayImagesNotSupported() {
            PinPadConnectionMock connection = new PinPadConnectionMock();
            connection.OnCommandReceived = (command, checksumFailed) => {
                connection.SendPositiveAcknowledge();
                if (command == "OPN") {
                    connection.WriteResponse("OPN000");
                }
            };
            PinPadFacade facade = new PinPadFacade(connection);
            Assert.IsFalse(facade.Display.ImagesSupported);
        }
        [TestMethod]
        public void ValidatePinPadDisplayImagesSupported() {
            PinPadConnectionMock connection = new PinPadConnectionMock();
            connection.OnCommandReceived = (command, checksumFailed) => {
                connection.SendPositiveAcknowledge();
                if (command == "OPN") {
                    connection.WriteResponse("OPN000003001");
                }
            };
            PinPadFacade facade = new PinPadFacade(connection);
            Assert.IsTrue(facade.Display.ImagesSupported);
        }
        [TestMethod]
        public void ValidatePinPadDisplaySimpleMessage() {
            PinPadConnectionMock connection = new PinPadConnectionMock();
            connection.OnCommandReceived = (command, checksumFailed) => {
                connection.SendPositiveAcknowledge();
                if (command == "OPN") {
                    connection.WriteResponse("OPN000");
                }
                else if (command == "DSP03212345678901234567890123456789012") {
                    connection.WriteResponse("DSP000");
                }
            };
            PinPadFacade facade = new PinPadFacade(connection);
            Assert.IsTrue(facade.Display.DisplayMessage(new SimpleMessage("12345678901234567890123456789012")));
        }
        [TestMethod]
        public void ValidatePinPadDisplayMultilineMessage() {
            PinPadConnectionMock connection = new PinPadConnectionMock();
            connection.OnCommandReceived = (command, checksumFailed) => {
                connection.SendPositiveAcknowledge();
                if (command == "OPN") {
                    connection.WriteResponse("OPN000");
                }
                else if (command == "DEX0360331234567890123456\r1234567890123456") {
                    connection.WriteResponse("DEX000");
                }
            };
            PinPadFacade facade = new PinPadFacade(connection);
            Assert.IsTrue(facade.Display.DisplayMessage(new MultilineMessage("1234567890123456", "1234567890123456")));
        }
        [TestMethod]
        public void ValidatePinPadDisplayImage() {
            PinPadConnectionMock connection = new PinPadConnectionMock();
            connection.OnCommandReceived = (command, checksumFailed) => {
                connection.SendPositiveAcknowledge();
                if (command == "OPN") {
                    connection.WriteResponse("OPN000003001");
                }
                else if (command.StartsWith("SEC")) {
                    SecRequest secureRequest = new SecRequest();
                    secureRequest.CommandString = command;

                    SecResponse secureResponse = new SecResponse();
                    secureResponse.SEC_CMDBLK.Value = secureRequest.SEC_CMDBLK.Value;

                    string unsecuredRequest = PinPadEncryption.Instance.UnwrapResponse(secureResponse);
                    if (unsecuredRequest == "DSI018015123456789012345") {
                        connection.WriteResponse("DSI000");
                    }
                }
            };
            PinPadFacade facade = new PinPadFacade(connection);
            Assert.IsTrue(facade.Display.DisplayImage("123456789012345"));
        }
        [TestMethod]
        public void ValidatePinPadDisplayImageErrorNotSupported() {
            PinPadConnectionMock connection = new PinPadConnectionMock();
            connection.OnCommandReceived = (command, checksumFailed) => {
                connection.SendPositiveAcknowledge();
                if (command == "OPN") {
                    connection.WriteResponse("OPN000");
                }
            };
            PinPadFacade facade = new PinPadFacade(connection);
            try {
                facade.Display.DisplayImage("123456789012345");
                Assert.Fail("Did not complain about stone version");
            }
            catch (StoneVersionMismatch) {
            }
        }
    }
}
