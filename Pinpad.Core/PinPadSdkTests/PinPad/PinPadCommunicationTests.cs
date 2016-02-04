using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PinPadSDK.PinPad;
using PinPadSDK.Commands;
using System.Threading.Tasks;
using System.Threading;
using PinPadSDK.Exceptions;
using PinPadSDK.Enums;
using PinPadSDK.Commands.Stone;
using PinPadSDK.Property;
using CrossPlatformBase;

namespace PinPadSdkTests.PinPad {
    [TestClass]
    public class PinPadCommunicationTests {
        [TestInitialize]
        public void TestSetup() {
            CrossPlatformDesktop.CrossPlatformDesktopInitializer.Initialize_CrossPlatformDesktop();
            CrossPlatformController.SendMailController = new SendMailControllerMock();
        }
        [TestMethod]
        public void ValidatePinPadCommunicationIsConnectionAlive() {
            PinPadConnectionMock connection = new PinPadConnectionMock();
            connection.OnCommandReceived = (command, checksumFailed) => {
                connection.SendPositiveAcknowledge();
                if (command == "OPN") {
                    connection.WriteResponse("OPN000");
                }
            };
            PinPadFacade facade = new PinPadFacade(connection);
            Assert.IsTrue(facade.Communication.IsConnectionAlive());
        }
        [TestMethod]
        public void ValidatePinPadCommunicationStoneIsConnectionAlive() {
            PinPadConnectionMock connection = new PinPadConnectionMock();
            connection.OnCommandReceived = (command, checksumFailed) => {
                connection.SendPositiveAcknowledge();
                if (command == "OPN") {
                    connection.WriteResponse("OPN000003001");
                }
            };
            PinPadFacade facade = new PinPadFacade(connection);
            Assert.IsTrue(facade.Communication.IsConnectionAlive());

            connection.OnCommandReceived = null;

            Assert.AreEqual(1, facade.Communication.StoneVersion);
            Assert.IsTrue(facade.Communication.IsConnectionAlive());
        }
        [TestMethod]
        public void ValidatePinPadCommunicationStoneIsConnectionAliveTimeout() {
            PinPadConnectionMock connection = new PinPadConnectionMock();
            connection.ReplyToStoneKeepAlive = false;
            connection.OnCommandReceived = (command, checksumFailed) => {
                connection.SendPositiveAcknowledge();
                if (command == "OPN") {
                    connection.WriteResponse("OPN000003001");
                }
            };
            PinPadFacade facade = new PinPadFacade(connection);
            Assert.IsTrue(facade.Communication.IsConnectionAlive());

            connection.OnCommandReceived = null;

            Assert.AreEqual(1, facade.Communication.StoneVersion);
            Assert.IsFalse(facade.Communication.IsConnectionAlive());
        }
        [TestMethod]
        public void ValidatePinPadCommunicationIsConnectionAliveTimeout() {
            PinPadConnectionMock connection = new PinPadConnectionMock();
            connection.OnCommandReceived = (command, checksumFailed) => {
                connection.SendPositiveAcknowledge();
            };
            PinPadFacade facade = new PinPadFacade(connection);
            Assert.IsFalse(facade.Communication.IsConnectionAlive());
        }
        [TestMethod]
        public void ValidatePinPadCommunicationIsConnectionAliveFailToSend() {
            PinPadConnectionMock connection = new PinPadConnectionMock();
            connection.OnCommandReceived = (command, checksumFailed) => {
                connection.SendNegativeAcknowledge();
            };
            PinPadFacade facade = new PinPadFacade(connection);
            Assert.IsFalse(facade.Communication.IsConnectionAlive());
        }
        [TestMethod]
        public void ValidatePinPadCommunicationCommandResend() {
            PinPadConnectionMock connection = new PinPadConnectionMock();
            int failedCount = 0;
            connection.OnCommandReceived = (command, checksumFailed) => {
                if (command == "OPN") {
                    if (failedCount >= 2) {
                        connection.SendPositiveAcknowledge();
                        connection.WriteResponse("OPN000");
                    }
                    else {
                        connection.SendNegativeAcknowledge();
                        failedCount++;
                    }
                }
            };
            PinPadFacade facade = new PinPadFacade(connection);
            Assert.IsTrue(facade.Communication.IsConnectionAlive());
        }
        [TestMethod]
        public void ValidatePinPadCommunicationCommandFailToSend() {
            PinPadConnectionMock connection = new PinPadConnectionMock();
            int failedCount = 0;
            connection.OnCommandReceived = (command, checksumFailed) => {
                if (command == "OPN") {
                    connection.SendPositiveAcknowledge();
                    connection.WriteResponse("OPN000");
                }
                else if (command == "GKY") {
                    connection.SendNegativeAcknowledge();
                    failedCount++;
                }
            };
            PinPadFacade facade = new PinPadFacade(connection);
            Assert.IsFalse(facade.Communication.SendRequest(new GkyRequest()));
            Assert.AreEqual(3, failedCount);
        }
        [TestMethod]
        public void ValidatePinPadCommunicationCommandResendInvalidChecksum() {
            PinPadConnectionMock connection = new PinPadConnectionMock();
            int failedCount = 0;
            connection.OnCommandReceived = (command, checksumFailed) => {
                connection.SendPositiveAcknowledge();
                if (command == "OPN") {
                    if (failedCount >= 2) {
                        connection.WriteResponse("OPN000");
                    }
                    else {
                        connection.WriteResponse("OPN000", new byte[] { 0x01, 0x02 });
                        failedCount++;
                    }
                }
            };
            connection.OnAcknowledgeFailed += (sender, e) => {
                connection.OnCommandReceived("OPN", false);
            };
            PinPadFacade facade = new PinPadFacade(connection);
            Assert.IsTrue(facade.Communication.IsConnectionAlive());
        }
        [TestMethod]
        public void ValidatePinPadCommunicationCommandFailInvalidChecksum() {
            PinPadConnectionMock connection = new PinPadConnectionMock();
            int failedCount = 0;
            connection.OnCommandReceived = (command, checksumFailed) => {
                connection.SendPositiveAcknowledge();
                if (command == "OPN") {
                    connection.WriteResponse("OPN", new byte[] { 0x01, 0x02 });
                }
                else if (command == "GKY") {
                    connection.WriteResponse("GKY000", new byte[] { 0x01, 0x02 });
                    failedCount++;
                }
            };
            connection.OnAcknowledgeFailed += (sender, e) => {
                connection.OnCommandReceived("GKY", false);
            };
            PinPadFacade facade = new PinPadFacade(connection);
            try {
                GkyResponse response = facade.Communication.SendRequestAndReceiveResponse<GkyResponse>(new GkyRequest());
                Assert.Fail("Accepted a response with invalid Checksum");
            }
            catch (InvalidChecksumException) {
            }
        }
        [TestMethod]
        public void ValidatePinPadCommunicationCommandCancelled() {
            PinPadConnectionMock connection = new PinPadConnectionMock();
            PinPadFacade facade = new PinPadFacade(connection);
            connection.OnCommandReceived = (command, checksumFailed) => {
                connection.SendPositiveAcknowledge();
                if (command == "OPN") {
                    connection.WriteResponse("OPN000");
                }
                else if (command == "GKY") {
                    new Thread(() => {
                        Thread.Sleep(100);
                        Assert.IsTrue(facade.Communication.CancelRequest());
                    }).Start();
                }
            };
            GenericResponse response = facade.Communication.SendRequestAndReceiveResponse<GenericResponse>(new GkyRequest());
            Assert.IsNull(response);
            Assert.IsTrue(facade.Communication.CancelRequest());
        }
        [TestMethod]
        public void ValidatePinPadCommunicationCancelCommandTimeout() {
            PinPadConnectionMock connection = new PinPadConnectionMock();
            PinPadFacade facade = new PinPadFacade(connection);
            connection.OnCommandReceived = (command, checksumFailed) => {
                connection.SendPositiveAcknowledge();
                if (command == "OPN") {
                    connection.WriteResponse("OPN000");
                }
                else if (command == "GKY") {
                    new Thread(() => {
                        connection.InterceptCancelRequest = true;
                        Assert.IsFalse(facade.Communication.CancelRequest());
                        connection.InterceptCancelRequest = false;
                    }).Start();
                }
            };
            GenericResponse response = facade.Communication.SendRequestAndReceiveResponse<GenericResponse>(new GkyRequest());
            Assert.IsNull(response);

            connection.InterceptCancelRequest = true;
            Assert.IsFalse(facade.Communication.CancelRequest());
            connection.InterceptCancelRequest = false;

            Assert.IsTrue(facade.Communication.CancelRequest());
        }
        [TestMethod]
        public void ValidatePinPadCommunicationSecureCommand() {
            PinPadConnectionMock connection = new PinPadConnectionMock();
            connection.OnCommandReceived = (command, checksumFailed) => {
                if (command == "OPN") {
                    connection.SendPositiveAcknowledge();
                    connection.WriteResponse("OPN000003001");
                }
                else if (command == "SEC02116002171A4FAB4D8E61A3") {
                    connection.SendPositiveAcknowledge();
                    //Get the encrypted block from secure request
                    SecRequest secureRequest = PinPadEncryption.Instance.WrapRequest("GKY000");
                    SecResponse secureResponse = new SecResponse();
                    secureResponse.RSP_STAT.Value = ResponseStatus.ST_OK;
                    secureResponse.SEC_CMDBLK.Value = secureRequest.SEC_CMDBLK.Value;
                    connection.WriteResponse(secureResponse.CommandString);
                }
            };
            PinPadFacade facade = new PinPadFacade(connection);
            GkyResponse response = facade.Communication.SendRequestAndReceiveResponse<GkyResponse>(new GkyRequest());
            Assert.AreEqual(PinPadKey.Return, response.PressedKey);
        }
        [TestMethod]
        public void ValidatePinPadCommunicationStoneCommand() {
            PinPadConnectionMock connection = new PinPadConnectionMock();

            GkeRequest request = new GkeRequest();
            request.GKE_ACTION.Value = GkeAction.GKE_ReadKey;

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
                    if (unsecuredRequest == request.CommandString) {
                        connection.WriteResponse("GKE000");
                    }
                }
            };
            PinPadFacade facade = new PinPadFacade(connection);
            GkeResponse response = facade.Communication.SendRequestAndReceiveResponse<GkeResponse>(request);
            Assert.IsNotNull(response);
        }
        [TestMethod]
        public void ValidatePinPadCommunicationCommandNotifications() {
            PinPadConnectionMock connection = new PinPadConnectionMock();
            connection.OnCommandReceived = (command, checksumFailed) => {
                connection.SendPositiveAcknowledge();
                if (command == "OPN") {
                    connection.WriteResponse("OPN000");
                }
                else if (command == "GKY") {
                    connection.WriteResponse("NTM03212345678901234567890123456789012");
                    connection.WriteResponse("NTM03212345678901234567890123456789012");
                    connection.WriteResponse("GKY000");
                }
            };
            PinPadFacade facade = new PinPadFacade(connection);
            int notificationsCount = 0;
            facade.Communication.OnNotification += (sender, e) => {
                notificationsCount++;
            };
            GkyResponse response = facade.Communication.SendRequestAndReceiveResponse<GkyResponse>(new GkyRequest());
            Assert.IsNotNull(response);
            Assert.AreEqual(2, notificationsCount);
        }
        [TestMethod]
        public void ValidatePinPadCommunicationCommandSendAndReceiveVerifyingResponseCode() {
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
            Assert.IsTrue(facade.Communication.SendRequestAndVerifyResponseCode(new GkyRequest()));
        }
        [TestMethod]
        public void ValidatePinPadCommunicationCommandSendAndReceiveVerifyingResponseCodeFailed() {
            PinPadConnectionMock connection = new PinPadConnectionMock();
            connection.OnCommandReceived = (command, checksumFailed) => {
                connection.SendPositiveAcknowledge();
                if (command == "OPN") {
                    connection.WriteResponse("OPN000");
                }
                else if (command == "GKY") {
                    connection.WriteResponse("GKY013");
                }
            };
            PinPadFacade facade = new PinPadFacade(connection);
            Assert.IsFalse(facade.Communication.SendRequestAndVerifyResponseCode(new GkyRequest()));
        }
        [TestMethod]
        public void ValidatePinPadCommunicationCommandSendAndReceiveVerifyingResponseCodeWithErrResponse() {
            PinPadConnectionMock connection = new PinPadConnectionMock();
            connection.OnCommandReceived = (command, checksumFailed) => {
                connection.SendPositiveAcknowledge();
                if (command == "OPN") {
                    connection.WriteResponse("OPN000");
                }
                else if (command == "GKY") {
                    connection.WriteResponse("ERR000");
                }
            };
            PinPadFacade facade = new PinPadFacade(connection);
            Assert.IsFalse(facade.Communication.SendRequestAndVerifyResponseCode(new GkyRequest()));
        }
        [TestMethod]
        public void ValidatePinPadCommunicationCommandSendAndReceiveVerifyingResponseCodeWithTimeout() {
            PinPadConnectionMock connection = new PinPadConnectionMock();
            connection.OnCommandReceived = (command, checksumFailed) => {
                connection.SendPositiveAcknowledge();
                if (command == "OPN") {
                    connection.WriteResponse("OPN000");
                }
            };
            PinPadFacade facade = new PinPadFacade(connection);
            Assert.IsFalse(facade.Communication.SendRequestAndVerifyResponseCode(new GkyRequest()));
        }
        [TestMethod]
        public void ValidatePinPadCommunicationCommandSendAndReceiveVerifyingResponseCodeWithSendTimeout() {
            PinPadConnectionMock connection = new PinPadConnectionMock();
            connection.OnCommandReceived = (command, checksumFailed) => {
                if (command == "OPN") {
                    connection.SendPositiveAcknowledge();
                    connection.WriteResponse("OPN000");
                }
            };
            PinPadFacade facade = new PinPadFacade(connection);
            Assert.IsFalse(facade.Communication.SendRequestAndVerifyResponseCode(new GkyRequest()));
        }
        [TestMethod]
        public void ValidatePinPadCommunicationCommandSendAndReceiveResponseCodeWithSendTimeout() {
            PinPadConnectionMock connection = new PinPadConnectionMock();
            connection.OnCommandReceived = (command, checksumFailed) => {
                if (command == "OPN") {
                    connection.SendPositiveAcknowledge();
                    connection.WriteResponse("OPN000");
                }
            };
            PinPadFacade facade = new PinPadFacade(connection);
            Assert.IsNull(facade.Communication.SendRequestAndReceiveResponse<GkyResponse>(new GkyRequest()));
        }
        [TestMethod]
        public void ValidatePinPadCommunicationStoneCommandWithoutStoneApp() {
            PinPadConnectionMock connection = new PinPadConnectionMock();
            connection.OnCommandReceived = (command, checksumFailed) => {
                connection.SendPositiveAcknowledge();
                if (command == "OPN") {
                    connection.WriteResponse("OPN000");
                }
            };
            PinPadFacade facade = new PinPadFacade(connection);
            GkeRequest request = new GkeRequest();
            request.GKE_ACTION.Value = GkeAction.GKE_ReadKey;
            try {
                GkeResponse response = facade.Communication.SendRequestAndReceiveResponse<GkeResponse>(request);
                Assert.Fail("Did not complain about Stone Version");
            }
            catch (StoneVersionMismatch) {

            }
        }
        public class TestStoneCommand : BaseStoneRequest {
            public override int MinimumStoneVersion {
                get { return 2; }
            }
            public override string CommandName {
                get { return "STN"; }
            }
        }
        [TestMethod]
        public void ValidatePinPadCommunicationStoneCommandUnderStoneAppVersion() {
            PinPadConnectionMock connection = new PinPadConnectionMock();
            connection.OnCommandReceived = (command, checksumFailed) => {
                connection.SendPositiveAcknowledge();
                if (command == "OPN") {
                    connection.WriteResponse("OPN000003001");
                }
            };
            PinPadFacade facade = new PinPadFacade(connection);
            try {
                GkeResponse response = facade.Communication.SendRequestAndReceiveResponse<GkeResponse>(new TestStoneCommand());
                Assert.Fail("Did not complain about Stone Version");
            }
            catch (StoneVersionMismatch) {

            }
        }
    }
}