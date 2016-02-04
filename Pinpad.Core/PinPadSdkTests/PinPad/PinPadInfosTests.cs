using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PinPadSDK.PinPad;
using PinPadSDK.Commands;
using PinPadSDK.Enums;

namespace PinPadSdkTests.PinPad {
    [TestClass]
    public class PinPadInfosTests {
        [TestInitialize]
        public void TestSetup() {
            CrossPlatformDesktop.CrossPlatformDesktopInitializer.Initialize_CrossPlatformDesktop();
            //CrossPlatformController.SendMailController = new SendMailControllerMock();
        }
        [TestMethod]
        public void ValidatePinPadInfosTerminalInfos() {
            PinPadConnectionMock connection = new PinPadConnectionMock();
            connection.OnCommandReceived = (command, checksumFailed) => {
                connection.SendPositiveAcknowledge();
                if (command == "OPN") {
                    connection.WriteResponse("OPN000");
                }
                else if (command == "GIN00200") {
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
            };
            PinPadFacade facade = new PinPadFacade(connection);
            Assert.AreEqual("12345678901234567890", facade.Infos.ManufacturerName);
            Assert.AreEqual("1234567890123456789", facade.Infos.ModelVersion);
            Assert.IsTrue(facade.Infos.ContactlessSupported);
            Assert.AreEqual("12345678901234567890", facade.Infos.OperationalSystemVersion);
            Assert.AreEqual("1234", facade.Infos.SpecificationVersion);
            Assert.AreEqual("1234567890123456", facade.Infos.ManufacturedVersion);
            Assert.AreEqual("12345678901234567890", facade.Infos.SerialNumber);
        }
        [TestMethod]
        public void ValidatePinPadInfosTerminalInfosTimeout() {
            PinPadConnectionMock connection = new PinPadConnectionMock();
            connection.OnCommandReceived = (command, checksumFailed) => {
                if (command == "OPN") {
                    connection.SendPositiveAcknowledge();
                    connection.WriteResponse("OPN000");
                }
            };
            PinPadFacade facade = new PinPadFacade(connection);
            Assert.IsNull(facade.Infos.ManufacturerName);
            Assert.IsNull(facade.Infos.ModelVersion);
            Assert.IsFalse(facade.Infos.ContactlessSupported);
            Assert.IsNull(facade.Infos.OperationalSystemVersion);
            Assert.IsNull(facade.Infos.SpecificationVersion);
            Assert.IsNull(facade.Infos.ManufacturedVersion);
            Assert.IsNull(facade.Infos.SerialNumber);
        }
        [TestMethod]
        public void ValidatePinPadInfosTerminalInfosTimeoutRetry() {
            PinPadConnectionMock connection = new PinPadConnectionMock();
            connection.OnCommandReceived = (command, checksumFailed) => {
                if (command == "OPN") {
                    connection.SendPositiveAcknowledge();
                    connection.WriteResponse("OPN000");
                }
            };
            PinPadFacade facade = new PinPadFacade(connection);
            Assert.IsNull(facade.Infos.ManufacturerName);
            Assert.IsNull(facade.Infos.ModelVersion);
            Assert.IsFalse(facade.Infos.ContactlessSupported);
            Assert.IsNull(facade.Infos.OperationalSystemVersion);
            Assert.IsNull(facade.Infos.SpecificationVersion);
            Assert.IsNull(facade.Infos.ManufacturedVersion);
            Assert.IsNull(facade.Infos.SerialNumber);

            connection.OnCommandReceived = (command, checksumFailed) => {
                connection.SendPositiveAcknowledge();
                if (command == "OPN") {
                    connection.WriteResponse("OPN000");
                }
                else if (command == "GIN00200") {
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
            };

            Assert.AreEqual("12345678901234567890", facade.Infos.ManufacturerName);
            Assert.AreEqual("1234567890123456789", facade.Infos.ModelVersion);
            Assert.IsTrue(facade.Infos.ContactlessSupported);
            Assert.AreEqual("12345678901234567890", facade.Infos.OperationalSystemVersion);
            Assert.AreEqual("1234", facade.Infos.SpecificationVersion);
            Assert.AreEqual("1234567890123456", facade.Infos.ManufacturedVersion);
            Assert.AreEqual("12345678901234567890", facade.Infos.SerialNumber);
        }
    }
}
