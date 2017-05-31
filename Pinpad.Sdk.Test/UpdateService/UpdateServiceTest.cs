using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pinpad.Sdk.Model;
using Pinpad.Sdk.Model.Pinpad;
using Pinpad.Sdk.Pinpad;
using Pinpad.Sdk.Test.Mockings;
using System;
using System.IO;

namespace Pinpad.Sdk.Test.UpdateService
{
    [TestClass]
    public sealed class UpdateServiceTest
    {
        public const string MockedApplicationName = "StonePinpadWifi.1.2.3.zip";
        
        [TestInitialize]
        public void Setup ()
        {
            MicroPos.Platform.Desktop.DesktopInitializer.Initialize();
            ApplicationFileMocker.MockNewApplicationFile(UpdateServiceTest.MockedApplicationName);
        }
        [TestCleanup]
        public void Cleanup ()
        {
            ApplicationFileMocker.Unmock();
        }

        /// <summary>
        /// Scenario: Creation should throw exception if pinpad information is null
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void UpdateService_Creation_ShouldThrowException_IfPinpadInformationIsNull()
        {
            // Arrange
            IPinpadInfos infos = null;
            IPinpadCommunication comm = new PinpadCommunicationMock();

            // Act and assert
            PinpadUpdateService pinpadUpdateService = new PinpadUpdateService(infos, comm);
        }
        /// <summary>
        /// Scenario: Creation should throw exception if pinpad communication is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void UpdateService_Creation_ShouldThrowException_IfPinpadCommunicationIsNull()
        {
            // Arrange
            IPinpadInfos infos = new PinpadInfosMock();
            IPinpadCommunication comm = null;

            // Act and assert
            PinpadUpdateService pinpadUpdateService = new PinpadUpdateService(infos, comm);
        }
        /// <summary>
        /// Scenario: Load should return true if the zipped application file exists.
        /// </summary>
        [TestMethod]
        public void UpdateService_Load_ShouldReturnTrue_IfTheZippedApplicationFileExists()
        {
            // Arrange
            IPinpadInfos infos = new PinpadInfosMock();
            IPinpadCommunication comm = new PinpadCommunicationMock();
            PinpadUpdateService pinpadUpdateService = new PinpadUpdateService(infos, comm);
            string applicationPath = Path.Combine(Directory.GetCurrentDirectory(),
                UpdateServiceTest.MockedApplicationName);

            // Act
            bool result = pinpadUpdateService.Load(applicationPath);

            // Assert
            Assert.IsTrue(result);
        }
        /// <summary>
        /// Scenario: Load should return false if the zipped application file does not exist.
        /// </summary>
        [TestMethod]
        public void UpdateService_Load_ShouldReturnFalse_IfZippedApplicationFileDoesNotExist()
        {
            // Arrange
            IPinpadInfos infos = new PinpadInfosMock();
            IPinpadCommunication comm = new PinpadCommunicationMock();
            PinpadUpdateService pinpadUpdateService = new PinpadUpdateService(infos, comm);
            string nonExistingApplicationPath = Path.Combine(Directory.GetCurrentDirectory(), "NotExistingApp.1.2.3.zip");

            // Act
            bool result = pinpadUpdateService.Load(nonExistingApplicationPath);

            // Assert
            Assert.IsFalse(result);
        }
        /// <summary>
        /// Scenario: Update should return false if the pinpad is not from Stone.
        /// </summary>
        [TestMethod]
        public void UpdateService_Update_ShouldReturnFalse_IfThePinpadIsNotFromStone()
        {
            // Arrange
            IPinpadInfos infos = new PinpadInfosMock(false);
            IPinpadCommunication comm = new PinpadCommunicationMock();
            PinpadUpdateService pinpadUpdateService = new PinpadUpdateService(infos, comm);
            string applicationPath = Path.Combine(Directory.GetCurrentDirectory(),
                UpdateServiceTest.MockedApplicationName);

            // Act
            pinpadUpdateService.Load(applicationPath);
            bool result = pinpadUpdateService.Update();

            // Assert
            Assert.IsFalse(result);
        }
        /// <summary>
        /// Scenario: Update should trhrow exception if the load was not previously called.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void UpdateService_Update_ShouldThrowException_IfLoadMethodWasNotPreviouslyCalled()
        {
            // Arrange
            IPinpadInfos infos = new PinpadInfosMock();
            IPinpadCommunication comm = new PinpadCommunicationMock();
            PinpadUpdateService pinpadUpdateService = new PinpadUpdateService(infos, comm);

            // Act and assert
            pinpadUpdateService.Update();
        }
        /// <summary>
        /// Scenario: Update should trhrow exception if the load was unsucessful.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void UpdateService_Update_ShouldThrowException_IfTheLoadMethodWasCalledButUnsucessful()
        {
            // Arrange
            IPinpadInfos infos = new PinpadInfosMock();
            IPinpadCommunication comm = new PinpadCommunicationMock();
            PinpadUpdateService pinpadUpdateService = new PinpadUpdateService(infos, comm);
            string nonExistingApplicationPath = Path.Combine(Directory.GetCurrentDirectory(), "NotExistingApp.1.2.3.zip");

            // Act and assert
            pinpadUpdateService.Load(nonExistingApplicationPath);
            pinpadUpdateService.Update();
        }
    }
}
