using Pinpad.Sdk.Model;
using Pinpad.Sdk.Pinpad;
using System;
using System.IO;
using NUnit.Framework;

namespace Pinpad.Sdk.Test.UpdateService
{
    [TestFixture]
    public sealed class UpdateServiceTest
    {
        public const string MockedApplicationName = "StonePinpadWifi.1.2.3.zip";
        
        [SetUp]
        public void Setup ()
        {
            Microtef.Platform.Desktop.DesktopInitializer.Initialize();
            Mockers.ApplicationFileMocker.MockNewApplicationFile(
                UpdateServiceTest.MockedApplicationName);
        }
        [TearDown]
        public void Cleanup ()
        {
            Mockers.ApplicationFileMocker.Unmock();
        }

        /// <summary>
        /// Scenario: Creation should throw exception if pinpad information is null
        /// </summary>
        [Test]
        public void UpdateService_Creation_ShouldThrowException_IfPinpadInformationIsNull()
        {
            // Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                // Arrange
                IPinpadInfos infos = null;
                IPinpadCommunication comm = new Stubs.PinpadCommunicationStub();

                // Act
                PinpadUpdateService pinpadUpdateService = new PinpadUpdateService(infos, comm);
            });
        }
        /// <summary>
        /// Scenario: Creation should throw exception if pinpad communication is null.
        /// </summary>
        [Test]
        public void UpdateService_Creation_ShouldThrowException_IfPinpadCommunicationIsNull()
        {
            // Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                // Arrange
                IPinpadInfos infos = new Stubs.PinpadInfosStub();
                IPinpadCommunication comm = null;

                // Act and assert
                PinpadUpdateService pinpadUpdateService = new PinpadUpdateService(
                    infos, 
                    comm);
            });
        }
        /// <summary>
        /// Scenario: Load should return true if the zipped application file exists.
        /// </summary>
        [Test]
        public void UpdateService_Load_ShouldReturnTrue_IfTheZippedApplicationFileExists()
        {
            // Arrange
            IPinpadInfos infos = new Stubs.PinpadInfosStub();
            IPinpadCommunication comm = new Stubs.PinpadCommunicationStub();
            PinpadUpdateService pinpadUpdateService = new PinpadUpdateService(
                infos, 
                comm);
            string applicationPath = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                UpdateServiceTest.MockedApplicationName);

            // Act
            bool result = pinpadUpdateService.Load(applicationPath);

            // Assert
            Assert.IsTrue(result);
        }
        /// <summary>
        /// Scenario: Load should return false if the zipped application file does not exist.
        /// </summary>
        [Test]
        public void UpdateService_Load_ShouldReturnFalse_IfZippedApplicationFileDoesNotExist()
        {
            // Arrange
            IPinpadInfos infos = new Stubs.PinpadInfosStub();
            IPinpadCommunication comm = new Stubs.PinpadCommunicationStub();
            PinpadUpdateService pinpadUpdateService = new PinpadUpdateService(
                infos, 
                comm);
            string nonExistingApplicationPath = Path.Combine(
                Directory.GetCurrentDirectory(), 
                "NotExistingApp.1.2.3.zip");

            // Act
            bool result = pinpadUpdateService.Load(nonExistingApplicationPath);

            // Assert
            Assert.IsFalse(result);
        }
        /// <summary>
        /// Scenario: Update should return false if the pinpad is not from Stone.
        /// </summary>
        [Test]
        public void UpdateService_Update_ShouldReturnFalse_IfThePinpadIsNotFromStone()
        {
            // Arrange
            IPinpadInfos infos = new Stubs.PinpadInfosStub(false);
            IPinpadCommunication comm = new Stubs.PinpadCommunicationStub();
            PinpadUpdateService pinpadUpdateService = new PinpadUpdateService(
                infos, 
                comm);
            string applicationPath = Path.Combine(
                Directory.GetCurrentDirectory(),
                UpdateServiceTest.MockedApplicationName);
			pinpadUpdateService.Load(applicationPath);

            // Act
            bool result = pinpadUpdateService.Update();

            // Assert
            Assert.IsFalse(result);
        }
        /// <summary>
        /// Scenario: Update should trhrow exception if the load was not previously called.
        /// </summary>
        [Test]
        public void UpdateService_Update_ShouldThrowException_IfLoadMethodWasNotPreviouslyCalled()
        {
            // Assert
            Assert.Throws<InvalidOperationException>(() =>
            {
                // Arrange
                IPinpadInfos infos = new Stubs.PinpadInfosStub();
                IPinpadCommunication comm = new Stubs.PinpadCommunicationStub();
                PinpadUpdateService pinpadUpdateService = new PinpadUpdateService(
                    infos, 
                    comm);

                // Act
                pinpadUpdateService.Update();
            });
        }
        /// <summary>
        /// Scenario: Update should trhrow exception if the load was unsucessful.
        /// </summary>
        [Test]
        public void UpdateService_Update_ShouldThrowException_IfTheLoadMethodWasCalledButUnsucessful()
        {
            // Assert
            Assert.Throws<InvalidOperationException>(() =>
            {
                // Arrange
                IPinpadInfos infos = new Stubs.PinpadInfosStub();
                IPinpadCommunication comm = new Stubs.PinpadCommunicationStub();
                PinpadUpdateService pinpadUpdateService = new PinpadUpdateService(infos, comm);
                string nonExistingApplicationPath = Path.Combine(Directory.GetCurrentDirectory(), "NotExistingApp.1.2.3.zip");

                // Act and assert
                pinpadUpdateService.Load(nonExistingApplicationPath);
                pinpadUpdateService.Update();
            });
        }
    }
}
