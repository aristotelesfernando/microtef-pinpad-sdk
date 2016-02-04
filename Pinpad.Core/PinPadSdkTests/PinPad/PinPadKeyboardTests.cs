using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PinPadSDK.PinPad;
using PinPadSDK.Enums;
using PinPadSDK.Commands.Stone;
using PinPadSDK.Exceptions;
using PinPadSDK.Property;
using StonePortableUtils;

namespace PinPadSdkTests.PinPad {
    [TestClass]
    public class PinPadKeyboardTests {
        [TestInitialize]
        public void TestSetup() {
            CrossPlatformDesktop.CrossPlatformDesktopInitializer.Initialize_CrossPlatformDesktop();
            //CrossPlatformController.SendMailController = new SendMailControllerMock();
        }
        [TestMethod]
        public void ValidatePinPadKeyboardExtendedKeyNotSupported() {
            PinPadConnectionMock connection = new PinPadConnectionMock();
            connection.OnCommandReceived = (command, checksumFailed) => {
                connection.SendPositiveAcknowledge();
                if (command == "OPN") {
                    connection.WriteResponse("OPN000");
                }
            };
            PinPadFacade facade = new PinPadFacade(connection);
            Assert.IsFalse(facade.Keyboard.ExtendedKeySupported);
        }
        [TestMethod]
        public void ValidatePinPadKeyboardExtendedKeySupported() {
            PinPadConnectionMock connection = new PinPadConnectionMock();
            connection.OnCommandReceived = (command, checksumFailed) => {
                connection.SendPositiveAcknowledge();
                if (command == "OPN") {
                    connection.WriteResponse("OPN000003001");
                }
            };
            PinPadFacade facade = new PinPadFacade(connection);
            Assert.IsTrue(facade.Keyboard.ExtendedKeySupported);
        }
        [TestMethod]
        public void ValidatePinPadKeyboardGetKey() {
            PinPadConnectionMock connection = new PinPadConnectionMock();
            connection.OnCommandReceived = (command, checksumFailed) => {
                connection.SendPositiveAcknowledge();
                if (command == "OPN") {
                    connection.WriteResponse("OPN000");
                }
                else if (command == "GKY") {
                    connection.WriteResponse("GKY000");
                }
            };
            PinPadFacade facade = new PinPadFacade(connection);
            Assert.AreEqual(PinPadKey.Return, facade.Keyboard.GetKey());
        }
        [TestMethod]
        public void ValidatePinPadKeyboardGetKeyFailure() {
            PinPadConnectionMock connection = new PinPadConnectionMock();
            connection.OnCommandReceived = (command, checksumFailed) => {
                if (command == "OPN") {
                    connection.SendPositiveAcknowledge();
                    connection.WriteResponse("OPN000");
                }
            };
            PinPadFacade facade = new PinPadFacade(connection);
            Assert.AreEqual(PinPadKey.Undefined, facade.Keyboard.GetKey());
        }
        [TestMethod]
        public void ValidatePinPadKeyboardGetKeyExtendedLightsOn() {
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
                    if (unsecuredRequest == "GKE0010") {
                        connection.WriteResponse("GKE000");
                    }
                }
            };
            PinPadFacade facade = new PinPadFacade(connection);
            Assert.AreEqual(PinPadKey.Return, facade.Keyboard.GetKeyExtended(true));
        }
        [TestMethod]
        public void ValidatePinPadKeyboardGetKeyExtendedLightsOnFailure() {
            PinPadConnectionMock connection = new PinPadConnectionMock();
            connection.OnCommandReceived = (command, checksumFailed) => {
                if (command == "OPN") {
                    connection.SendPositiveAcknowledge();
                    connection.WriteResponse("OPN000003001");
                }
            };
            PinPadFacade facade = new PinPadFacade(connection);
            Assert.AreEqual(PinPadKey.Undefined, facade.Keyboard.GetKeyExtended(true));
        }
        [TestMethod]
        public void ValidatePinPadKeyboardGetKeyExtendedLightsOnErrorExtendedKeyNotSupported() {
            PinPadConnectionMock connection = new PinPadConnectionMock();
            connection.OnCommandReceived = (command, checksumFailed) => {
                connection.SendPositiveAcknowledge();
                if (command == "OPN") {
                    connection.WriteResponse("OPN000");
                }
            };
            PinPadFacade facade = new PinPadFacade(connection);
            try {
                facade.Keyboard.GetKeyExtended(true);
                Assert.Fail("Did not complain about stone version");
            }
            catch (StoneVersionMismatch) {
            }
        }
        [TestMethod]
        public void ValidatePinPadKeyboardGetKeyExtendedLightsOff() {
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
                    if (unsecuredRequest == "GKE0012") {
                        connection.WriteResponse("GKE000");
                    }
                }
            };
            PinPadFacade facade = new PinPadFacade(connection);
            Assert.AreEqual(PinPadKey.Return, facade.Keyboard.GetKeyExtended(false));
        }
        [TestMethod]
        public void ValidatePinPadKeyboardGetKeyExtendedLightsOffFailure() {
            PinPadConnectionMock connection = new PinPadConnectionMock();
            connection.OnCommandReceived = (command, checksumFailed) => {
                if (command == "OPN") {
                    connection.SendPositiveAcknowledge();
                    connection.WriteResponse("OPN000003001");
                }
            };
            PinPadFacade facade = new PinPadFacade(connection);
            Assert.AreEqual(PinPadKey.Undefined, facade.Keyboard.GetKeyExtended(false));
        }
        [TestMethod]
        public void ValidatePinPadKeyboardGetKeyExtendedLightsOffErrorExtendedKeyNotSupported() {
            PinPadConnectionMock connection = new PinPadConnectionMock();
            connection.OnCommandReceived = (command, checksumFailed) => {
                connection.SendPositiveAcknowledge();
                if (command == "OPN") {
                    connection.WriteResponse("OPN000");
                }
            };
            PinPadFacade facade = new PinPadFacade(connection);
            try {
                facade.Keyboard.GetKeyExtended(false);
                Assert.Fail("Did not complain about stone version");
            }
            catch (StoneVersionMismatch) {
            }
        }
        [TestMethod]
        public void ValidatePinPadKeyboardClearKeyBuffer() {
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
                    if (unsecuredRequest == "GKE0011") {
                        connection.WriteResponse("GKE000");
                    }
                }
            };
            PinPadFacade facade = new PinPadFacade(connection);
            Assert.IsTrue(facade.Keyboard.ClearKeyBuffer());
        }
        [TestMethod]
        public void ValidatePinPadKeyboardClearKeyBufferFailure() {
            PinPadConnectionMock connection = new PinPadConnectionMock();
            connection.OnCommandReceived = (command, checksumFailed) => {
                if (command == "OPN") {
                    connection.SendPositiveAcknowledge();
                    connection.WriteResponse("OPN000003001");
                }
            };
            PinPadFacade facade = new PinPadFacade(connection);
            Assert.IsFalse(facade.Keyboard.ClearKeyBuffer());
        }
        [TestMethod]
        public void ValidatePinPadKeyboardClearKeyBufferErrorExtendedKeyNotSupported() {
            PinPadConnectionMock connection = new PinPadConnectionMock();
            connection.OnCommandReceived = (command, checksumFailed) => {
                connection.SendPositiveAcknowledge();
                if (command == "OPN") {
                    connection.WriteResponse("OPN000");
                }
            };
            PinPadFacade facade = new PinPadFacade(connection);
            try {
                facade.Keyboard.ClearKeyBuffer();
                Assert.Fail("Did not complain about stone version");
            }
            catch (StoneVersionMismatch) {
            }
        }
        [TestMethod]
        public void ValidatePinPadKeyboardGetDukptPin() {
            PinPadConnectionMock connection = new PinPadConnectionMock();
            connection.OnCommandReceived = (command, checksumFailed) => {
                connection.SendPositiveAcknowledge();
                if (command == "OPN") {
                    connection.WriteResponse("OPN000");
                }
                else if (command == "GPN09331200000000000000000000000000000000161234567890123456   1040612345678901234567890123456789012") {
                    connection.WriteResponse("GPN000036123456789012345612345678901234567890");
                }
            };
            PinPadFacade facade = new PinPadFacade(connection);
            HexadecimalData pinBlock;
            HexadecimalData ksn;
            Assert.IsTrue(facade.Keyboard.GetDukptPin(CryptographyMode.TripleDataEncryptionStandard, 12, "1234567890123456", 4, 6, new SimpleMessage("12345678901234567890123456789012"), out pinBlock, out ksn));
            Assert.AreEqual("1234567890123456", pinBlock.DataString);
            Assert.AreEqual("12345678901234567890", ksn.DataString);
        }
        [TestMethod]
        public void ValidatePinPadKeyboardGetDukptPinFailure() {
            PinPadConnectionMock connection = new PinPadConnectionMock();
            connection.OnCommandReceived = (command, checksumFailed) => {
                if (command == "OPN") {
                    connection.SendPositiveAcknowledge();
                    connection.WriteResponse("OPN000");
                }
            };
            PinPadFacade facade = new PinPadFacade(connection);
            HexadecimalData pinBlock;
            HexadecimalData ksn;
            Assert.IsFalse(facade.Keyboard.GetDukptPin(CryptographyMode.TripleDataEncryptionStandard, 12, "1234567890123456", 4, 6, new SimpleMessage("12345678901234567890123456789012"), out pinBlock, out ksn));
            Assert.IsNull(pinBlock);
            Assert.IsNull(ksn);
        }
    }
}
