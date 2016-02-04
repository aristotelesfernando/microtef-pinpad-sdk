using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PinPadSDK.PinPad;
using PinPadSDK.Commands;
using PinPadSDK.Enums;
using PinPadSDK.Commands.Stone;
using PinPadSDK.Commands.Stone.PrtActionData;
using PinPadSDK.Exceptions;

namespace PinPadSdkTests.PinPad {
    [TestClass]
    public class PinPadPrinterTests {
        [TestInitialize]
        public void TestSetup() {
            CrossPlatformDesktop.CrossPlatformDesktopInitializer.Initialize_CrossPlatformDesktop();
            //CrossPlatformController.SendMailController = new SendMailControllerMock();
        }
        [TestMethod]
        public void ValidatePinPadPrinterPrinterNotSupportedByStoneApplication() {
            PinPadConnectionMock connection = new PinPadConnectionMock();
            connection.OnCommandReceived = (command, checksumFailed) => {
                connection.SendPositiveAcknowledge();
                if (command == "OPN") {
                    connection.WriteResponse("OPN000");
                }
            };
            PinPadFacade facade = new PinPadFacade(connection);
            Assert.IsFalse(facade.Printer.PrinterSupported);
        }
        [TestMethod]
        public void ValidatePinPadPrinterPrinterSupportedTimeout() {
            PinPadConnectionMock connection = new PinPadConnectionMock();
            connection.OnCommandReceived = (command, checksumFailed) => {
            };
            PinPadFacade facade = new PinPadFacade(connection);
            Assert.IsFalse(facade.Printer.PrinterSupported);
        }
        [TestMethod]
        public void ValidatePinPadPrinterPrinterSupportedTimeoutRetry() {
            PinPadConnectionMock connection = new PinPadConnectionMock();
            connection.OnCommandReceived = (command, checksumFailed) => {
            };
            PinPadFacade facade = new PinPadFacade(connection);
            Assert.IsFalse(facade.Printer.PrinterSupported);

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
                    if (unsecuredRequest == "GIN00200") {
                        Gin00Response response = new Gin00Response();
                        response.RSP_STAT.Value = ResponseStatus.ST_OK;
                        response.GIN_MNAME.Value = "12345678901234567890";
                        response.GIN_MODEL.Value = "D210".PadRight(19);
                        response.GIN_CTLSUP.Value = "C";
                        response.GIN_SOVER.Value = "12345678901234567890";
                        response.GIN_SPECVER.Value = "1234";
                        response.GIN_MANVER.Value = "1234567890123456";
                        response.GIN_SERNUM.Value = "12345678901234567890";

                        connection.WriteResponse(response.CommandString);
                    }
                }
            };
            Assert.IsTrue(facade.Printer.PrinterSupported);
        }
        [TestMethod]
        public void ValidatePinPadPrinterPrinterNotSupportedByPinPadModel() {
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
                    if (unsecuredRequest == "GIN00200") {
                        Gin00Response response = new Gin00Response();
                        response.RSP_STAT.Value = ResponseStatus.ST_OK;
                        response.GIN_MNAME.Value = "12345678901234567890";
                        response.GIN_MODEL.Value = "1234567890123456789";
                        response.GIN_CTLSUP.Value = "C";
                        response.GIN_SOVER.Value = "12345678901234567890";
                        response.GIN_SPECVER.Value = "1234";
                        response.GIN_MANVER.Value = "1234567890123456";
                        response.GIN_SERNUM.Value = "12345678901234567890";

                        connection.WriteResponse(response.CommandString);
                    }
                }
            };
            PinPadFacade facade = new PinPadFacade(connection);
            Assert.IsFalse(facade.Printer.PrinterSupported);
        }
        [TestMethod]
        public void ValidatePinPadPrinterPrinterSupported() {
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
                    if (unsecuredRequest == "GIN00200") {
                        Gin00Response response = new Gin00Response();
                        response.RSP_STAT.Value = ResponseStatus.ST_OK;
                        response.GIN_MNAME.Value = "12345678901234567890";
                        response.GIN_MODEL.Value = "D210".PadRight(19);
                        response.GIN_CTLSUP.Value = "C";
                        response.GIN_SOVER.Value = "12345678901234567890";
                        response.GIN_SPECVER.Value = "1234";
                        response.GIN_MANVER.Value = "1234567890123456";
                        response.GIN_SERNUM.Value = "12345678901234567890";

                        connection.WriteResponse(response.CommandString);
                    }
                }
            };
            PinPadFacade facade = new PinPadFacade(connection);
            Assert.IsTrue(facade.Printer.PrinterSupported);
        }
        [TestMethod]
        public void ValidatePinPadPrinterBegin() {
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
                    if (unsecuredRequest == "PRT0010") {
                        PrtResponse response = new PrtResponse();
                        response.RSP_STAT.Value = ResponseStatus.ST_OK;

                        PrtBeginResponseData data = new PrtBeginResponseData();
                        data.PRT_STATUS.Value = PinPadPrinterStatus.Ready;

                        response.BeginData = data;

                        connection.WriteResponse(response.CommandString);
                    }
                }
            };
            PinPadFacade facade = new PinPadFacade(connection);
            Assert.IsTrue(facade.Printer.Begin());
        }
        [TestMethod]
        public void ValidatePinPadPrinterBeginFailToSend() {
            PinPadConnectionMock connection = new PinPadConnectionMock();
            connection.OnCommandReceived = (command, checksumFailed) => {
                if (command == "OPN") {
                    connection.SendPositiveAcknowledge();
                    connection.WriteResponse("OPN000003001");
                }
            };
            PinPadFacade facade = new PinPadFacade(connection);
            Assert.IsFalse(facade.Printer.Begin());
        }
        [TestMethod]
        public void ValidatePinPadPrinterBeginPrinterBusy() {
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
                    if (unsecuredRequest == "PRT0010") {
                        PrtResponse response = new PrtResponse();
                        response.RSP_STAT.Value = ResponseStatus.ST_OK;

                        PrtBeginResponseData data = new PrtBeginResponseData();
                        data.PRT_STATUS.Value = PinPadPrinterStatus.Busy;

                        response.BeginData = data;

                        connection.WriteResponse(response.CommandString);
                    }
                }
            };
            PinPadFacade facade = new PinPadFacade(connection);
            try {
                facade.Printer.Begin();
                Assert.Fail("Did not fire printer busy exception");
            }
            catch (PrinterBusyException) { }
        }
        [TestMethod]
        public void ValidatePinPadPrinterBeginPrinterOutOfPaper() {
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
                    if (unsecuredRequest == "PRT0010") {
                        PrtResponse response = new PrtResponse();
                        response.RSP_STAT.Value = ResponseStatus.ST_OK;

                        PrtBeginResponseData data = new PrtBeginResponseData();
                        data.PRT_STATUS.Value = PinPadPrinterStatus.OutOfPaper;

                        response.BeginData = data;

                        connection.WriteResponse(response.CommandString);
                    }
                }
            };
            PinPadFacade facade = new PinPadFacade(connection);
            try {
                facade.Printer.Begin();
                Assert.Fail("Did not fire printer out of paper exception");
            }
            catch (PrinterOutOfPaperException) { }
        }
        [TestMethod]
        public void ValidatePinPadPrinterBeginResponseError() {
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
                    if (unsecuredRequest == "PRT0010") {
                        PrtResponse response = new PrtResponse();
                        response.RSP_STAT.Value = ResponseStatus.ST_INTERR;

                        connection.WriteResponse(response.CommandString);
                    }
                }
            };
            PinPadFacade facade = new PinPadFacade(connection);
            Assert.IsFalse(facade.Printer.Begin());
        }
        [TestMethod]
        public void ValidatePinPadPrinterEnd() {
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
                    if (unsecuredRequest == "PRT0011") {
                        PrtResponse response = new PrtResponse();
                        response.RSP_STAT.Value = ResponseStatus.ST_OK;

                        response.EndData = new PrtEndResponseData();

                        connection.WriteResponse(response.CommandString);
                    }
                }
            };
            PinPadFacade facade = new PinPadFacade(connection);
            Assert.IsTrue(facade.Printer.End());
        }
        [TestMethod]
        public void ValidatePinPadPrinterEndFailToSend() {
            PinPadConnectionMock connection = new PinPadConnectionMock();
            connection.OnCommandReceived = (command, checksumFailed) => {
                if (command == "OPN") {
                    connection.SendPositiveAcknowledge();
                    connection.WriteResponse("OPN000003001");
                }
            };
            PinPadFacade facade = new PinPadFacade(connection);
            Assert.IsFalse(facade.Printer.End());
        }
        [TestMethod]
        public void ValidatePinPadPrinterEndResponseError() {
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
                    if (unsecuredRequest == "PRT0011") {
                        PrtResponse response = new PrtResponse();
                        response.RSP_STAT.Value = ResponseStatus.ST_INTERR;

                        connection.WriteResponse(response.CommandString);
                    }
                }
            };
            PinPadFacade facade = new PinPadFacade(connection);
            Assert.IsFalse(facade.Printer.End());
        }
        [TestMethod]
        public void ValidatePinPadPrinterAppendString() {
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
                    if (unsecuredRequest == "PRT0162010101234567890") {
                        PrtResponse response = new PrtResponse();
                        response.RSP_STAT.Value = ResponseStatus.ST_OK;

                        response.AppendStringData = new PrtAppendStringResponseData();

                        connection.WriteResponse(response.CommandString);
                    }
                }
            };
            PinPadFacade facade = new PinPadFacade(connection);
            Assert.IsTrue(facade.Printer.AppendString("1234567890", PrtStringSize.Small, PrtAlignment.Center));
        }
        [TestMethod]
        public void ValidatePinPadPrinterAppendStringFailToSend() {
            PinPadConnectionMock connection = new PinPadConnectionMock();
            connection.OnCommandReceived = (command, checksumFailed) => {
                if (command == "OPN") {
                    connection.SendPositiveAcknowledge();
                    connection.WriteResponse("OPN000003001");
                }
            };
            PinPadFacade facade = new PinPadFacade(connection);
            Assert.IsFalse(facade.Printer.AppendString("1234567890", PrtStringSize.Small, PrtAlignment.Center));
        }
        [TestMethod]
        public void ValidatePinPadPrinterAppendStringResponseError() {
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
                    if (unsecuredRequest == "PRT0162010101234567890") {
                        PrtResponse response = new PrtResponse();
                        response.RSP_STAT.Value = ResponseStatus.ST_INTERR;

                        connection.WriteResponse(response.CommandString);
                    }
                }
            };
            PinPadFacade facade = new PinPadFacade(connection);
            Assert.IsFalse(facade.Printer.AppendString("1234567890", PrtStringSize.Small, PrtAlignment.Center));
        }
        [TestMethod]
        public void ValidatePinPadPrinterAppendImage() {
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
                    if (unsecuredRequest == "PRT02330000015123456789013245") {
                        PrtResponse response = new PrtResponse();
                        response.RSP_STAT.Value = ResponseStatus.ST_OK;

                        response.AppendImageData = new PrtAppendImageResponseData();

                        connection.WriteResponse(response.CommandString);
                    }
                }
            };
            PinPadFacade facade = new PinPadFacade(connection);
            Assert.IsTrue(facade.Printer.AppendImage("123456789013245", 0));
        }
        [TestMethod]
        public void ValidatePinPadPrinterAppendImageFailToSend() {
            PinPadConnectionMock connection = new PinPadConnectionMock();
            connection.OnCommandReceived = (command, checksumFailed) => {
                if (command == "OPN") {
                    connection.SendPositiveAcknowledge();
                    connection.WriteResponse("OPN000003001");
                }
            };
            PinPadFacade facade = new PinPadFacade(connection);
            Assert.IsFalse(facade.Printer.AppendImage("123456789013245", 0));
        }
        [TestMethod]
        public void ValidatePinPadPrinterAppendImageResponseError() {
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
                    if (unsecuredRequest == "PRT02330000015123456789013245") {
                        PrtResponse response = new PrtResponse();
                        response.RSP_STAT.Value = ResponseStatus.ST_INTERR;

                        connection.WriteResponse(response.CommandString);
                    }
                }
            };
            PinPadFacade facade = new PinPadFacade(connection);
            Assert.IsFalse(facade.Printer.AppendImage("123456789013245", 0));
        }
        [TestMethod]
        public void ValidatePinPadPrinterStep() {
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
                    if (unsecuredRequest == "PRT0054-123") {
                        PrtResponse response = new PrtResponse();
                        response.RSP_STAT.Value = ResponseStatus.ST_OK;

                        response.StepData = new PrtStepResponseData();

                        connection.WriteResponse(response.CommandString);
                    }
                }
            };
            PinPadFacade facade = new PinPadFacade(connection);
            Assert.IsTrue(facade.Printer.Step(-123));
        }
        [TestMethod]
        public void ValidatePinPadPrinterStepFailToSend() {
            PinPadConnectionMock connection = new PinPadConnectionMock();
            connection.OnCommandReceived = (command, checksumFailed) => {
                if (command == "OPN") {
                    connection.SendPositiveAcknowledge();
                    connection.WriteResponse("OPN000003001");
                }
            };
            PinPadFacade facade = new PinPadFacade(connection);
            Assert.IsFalse(facade.Printer.Step(-123));
        }
        [TestMethod]
        public void ValidatePinPadPrinterStepResponseError() {
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
                    if (unsecuredRequest == "PRT0054-123") {
                        PrtResponse response = new PrtResponse();
                        response.RSP_STAT.Value = ResponseStatus.ST_INTERR;

                        connection.WriteResponse(response.CommandString);
                    }
                }
            };
            PinPadFacade facade = new PinPadFacade(connection);
            Assert.IsFalse(facade.Printer.Step(-123));
        }
    }
}
