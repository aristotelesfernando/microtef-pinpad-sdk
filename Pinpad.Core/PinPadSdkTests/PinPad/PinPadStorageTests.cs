using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PinPadSDK.PinPad;
using CrossPlatformBase;
using System.Text;
using PinPadSDK.Commands.Stone;

namespace PinPadSdkTests.PinPad {
    [TestClass]
    public class PinPadStorageTests {
        [TestInitialize]
        public void TestSetup() {
            CrossPlatformDesktop.CrossPlatformDesktopInitializer.Initialize_CrossPlatformDesktop();
        }
        [TestMethod]
        public void ValidatePinPadStorageLoadFiles() {
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
                    if (unsecuredRequest == "LFC008005file1") {
                        connection.WriteResponse("LFC00001100000000000");
                    }
                    else if (unsecuredRequest == "LFI008005file1") {
                        connection.WriteResponse("LFI000");
                    }
                    else if (unsecuredRequest == "LFR02301031323334353637383930") {
                        connection.WriteResponse("LFR000");
                    }
                    else if (unsecuredRequest == "LFE") {
                        connection.WriteResponse("LFE000");
                    }
                }
            };
            PinPadFacade facade = new PinPadFacade(connection);

            SystemStorageMock systemStorage = new SystemStorageMock();
            CrossPlatformController.StorageController = systemStorage;
            systemStorage.AddFile("path1", Encoding.ASCII.GetBytes("1234567890"));

            facade.Storage.OnFileLoaded += (sender, e) => {
                Assert.AreEqual(facade.Storage, sender);
                Assert.AreEqual(facade, e.PinPad);
                Assert.AreEqual("file1", e.Name);
                Assert.AreEqual("path1", e.Path);
                Assert.IsTrue(e.SucessfullyLoaded);
                Assert.AreEqual(1, e.TotalLoaded);
                Assert.AreEqual(1, e.TotalToLoad);
            };

            Assert.IsTrue(facade.Storage.AreAllFilesUpToDate);
            facade.Storage.AddFile("file1", "path1");
            Assert.IsFalse(facade.Storage.AreAllFilesUpToDate);
            Assert.IsTrue(facade.Storage.LoadFiles());
            Assert.IsTrue(facade.Storage.AreAllFilesUpToDate);
        }
        [TestMethod]
        public void ValidatePinPadStorageLoadFilesLfcError() {
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
                    if (unsecuredRequest == "LFC008005file1") {
                        connection.WriteResponse("LFC040");
                    }
                    else if (unsecuredRequest == "LFI008005file1") {
                        connection.WriteResponse("LFI000");
                    }
                    else if (unsecuredRequest == "LFR02301031323334353637383930") {
                        connection.WriteResponse("LFR000");
                    }
                    else if (unsecuredRequest == "LFE") {
                        connection.WriteResponse("LFE000");
                    }
                }
            };
            PinPadFacade facade = new PinPadFacade(connection);

            SystemStorageMock systemStorage = new SystemStorageMock();
            CrossPlatformController.StorageController = systemStorage;
            systemStorage.AddFile("path1", Encoding.ASCII.GetBytes("1234567890"));

            facade.Storage.OnFileLoaded += (sender, e) => {
                Assert.AreEqual(facade.Storage, sender);
                Assert.AreEqual(facade, e.PinPad);
                Assert.AreEqual("file1", e.Name);
                Assert.AreEqual("path1", e.Path);
                Assert.IsTrue(e.SucessfullyLoaded);
                Assert.AreEqual(1, e.TotalLoaded);
                Assert.AreEqual(1, e.TotalToLoad);
            };

            Assert.IsTrue(facade.Storage.AreAllFilesUpToDate);
            facade.Storage.AddFile("file1", "path1");
            Assert.IsFalse(facade.Storage.AreAllFilesUpToDate);
            Assert.IsTrue(facade.Storage.LoadFiles());
            Assert.IsTrue(facade.Storage.AreAllFilesUpToDate);
        }
        [TestMethod]
        public void ValidatePinPadStorageLoadFilesLfiError() {
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
                    if (unsecuredRequest == "LFC008005file1") {
                        connection.WriteResponse("LFC00001100000000000");
                    }
                    else if (unsecuredRequest == "LFI008005file1") {
                        connection.WriteResponse("LFI040");
                    }
                }
            };
            PinPadFacade facade = new PinPadFacade(connection);

            SystemStorageMock systemStorage = new SystemStorageMock();
            CrossPlatformController.StorageController = systemStorage;
            systemStorage.AddFile("path1", Encoding.ASCII.GetBytes("1234567890"));

            facade.Storage.OnFileLoaded += (sender, e) => {
                Assert.AreEqual(facade.Storage, sender);
                Assert.AreEqual(facade, e.PinPad);
                Assert.AreEqual("file1", e.Name);
                Assert.AreEqual("path1", e.Path);
                Assert.IsFalse(e.SucessfullyLoaded);
                Assert.AreEqual(1, e.TotalLoaded);
                Assert.AreEqual(1, e.TotalToLoad);
            };

            Assert.IsTrue(facade.Storage.AreAllFilesUpToDate);
            facade.Storage.AddFile("file1", "path1");
            Assert.IsFalse(facade.Storage.AreAllFilesUpToDate);
            Assert.IsFalse(facade.Storage.LoadFiles());
            Assert.IsFalse(facade.Storage.AreAllFilesUpToDate);
        }
        [TestMethod]
        public void ValidatePinPadStorageLoadFilesLfrError() {
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
                    if (unsecuredRequest == "LFC008005file1") {
                        connection.WriteResponse("LFC00001100000000000");
                    }
                    else if (unsecuredRequest == "LFI008005file1") {
                        connection.WriteResponse("LFI000");
                    }
                    else if (unsecuredRequest == "LFR02301031323334353637383930") {
                        connection.WriteResponse("LFR040");
                    }
                }
            };
            PinPadFacade facade = new PinPadFacade(connection);

            SystemStorageMock systemStorage = new SystemStorageMock();
            CrossPlatformController.StorageController = systemStorage;
            systemStorage.AddFile("path1", Encoding.ASCII.GetBytes("1234567890"));

            facade.Storage.OnFileLoaded += (sender, e) => {
                Assert.AreEqual(facade.Storage, sender);
                Assert.AreEqual(facade, e.PinPad);
                Assert.AreEqual("file1", e.Name);
                Assert.AreEqual("path1", e.Path);
                Assert.IsFalse(e.SucessfullyLoaded);
                Assert.AreEqual(1, e.TotalLoaded);
                Assert.AreEqual(1, e.TotalToLoad);
            };

            Assert.IsTrue(facade.Storage.AreAllFilesUpToDate);
            facade.Storage.AddFile("file1", "path1");
            Assert.IsFalse(facade.Storage.AreAllFilesUpToDate);
            Assert.IsFalse(facade.Storage.LoadFiles());
            Assert.IsFalse(facade.Storage.AreAllFilesUpToDate);
        }
        [TestMethod]
        public void ValidatePinPadStorageLoadFilesLfeError() {
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
                    if (unsecuredRequest == "LFC008005file1") {
                        connection.WriteResponse("LFC00001100000000000");
                    }
                    else if (unsecuredRequest == "LFI008005file1") {
                        connection.WriteResponse("LFI000");
                    }
                    else if (unsecuredRequest == "LFR02301031323334353637383930") {
                        connection.WriteResponse("LFR000");
                    }
                    else if (unsecuredRequest == "LFE") {
                        connection.WriteResponse("LFE040");
                    }
                }
            };
            PinPadFacade facade = new PinPadFacade(connection);

            SystemStorageMock systemStorage = new SystemStorageMock();
            CrossPlatformController.StorageController = systemStorage;
            systemStorage.AddFile("path1", Encoding.ASCII.GetBytes("1234567890"));

            facade.Storage.OnFileLoaded += (sender, e) => {
                Assert.AreEqual(facade.Storage, sender);
                Assert.AreEqual(facade, e.PinPad);
                Assert.AreEqual("file1", e.Name);
                Assert.AreEqual("path1", e.Path);
                Assert.IsFalse(e.SucessfullyLoaded);
                Assert.AreEqual(1, e.TotalLoaded);
                Assert.AreEqual(1, e.TotalToLoad);
            };

            Assert.IsTrue(facade.Storage.AreAllFilesUpToDate);
            facade.Storage.AddFile("file1", "path1");
            Assert.IsFalse(facade.Storage.AreAllFilesUpToDate);
            Assert.IsFalse(facade.Storage.LoadFiles());
            Assert.IsFalse(facade.Storage.AreAllFilesUpToDate);
        }
        [TestMethod]
        public void ValidatePinPadStorageForcedLoadFiles() {
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
                    if (unsecuredRequest == "LFI008005file1") {
                        connection.WriteResponse("LFI000");
                    }
                    else if (unsecuredRequest == "LFR02301031323334353637383930") {
                        connection.WriteResponse("LFR000");
                    }
                    else if (unsecuredRequest == "LFE") {
                        connection.WriteResponse("LFE000");
                    }
                }
            };
            PinPadFacade facade = new PinPadFacade(connection);

            SystemStorageMock systemStorage = new SystemStorageMock();
            CrossPlatformController.StorageController = systemStorage;
            systemStorage.AddFile("path1", Encoding.ASCII.GetBytes("1234567890"));

            facade.Storage.OnFileLoaded += (sender, e) => {
                Assert.AreEqual(facade.Storage, sender);
                Assert.AreEqual(facade, e.PinPad);
                Assert.AreEqual("file1", e.Name);
                Assert.AreEqual("path1", e.Path);
                Assert.IsTrue(e.SucessfullyLoaded);
                Assert.AreEqual(1, e.TotalLoaded);
                Assert.AreEqual(1, e.TotalToLoad);
            };

            Assert.IsTrue(facade.Storage.AreAllFilesUpToDate);
            facade.Storage.AddFile("file1", "path1");
            Assert.IsFalse(facade.Storage.AreAllFilesUpToDate);
            Assert.IsTrue(facade.Storage.LoadFiles(true));
            Assert.IsTrue(facade.Storage.AreAllFilesUpToDate);
        }
        [TestMethod]
        public void ValidatePinPadStorageForcedLoadFilesLfiError() {
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
                    if (unsecuredRequest == "LFI008005file1") {
                        connection.WriteResponse("LFI040");
                    }
                }
            };
            PinPadFacade facade = new PinPadFacade(connection);

            SystemStorageMock systemStorage = new SystemStorageMock();
            CrossPlatformController.StorageController = systemStorage;
            systemStorage.AddFile("path1", Encoding.ASCII.GetBytes("1234567890"));

            facade.Storage.OnFileLoaded += (sender, e) => {
                Assert.AreEqual(facade.Storage, sender);
                Assert.AreEqual(facade, e.PinPad);
                Assert.AreEqual("file1", e.Name);
                Assert.AreEqual("path1", e.Path);
                Assert.IsFalse(e.SucessfullyLoaded);
                Assert.AreEqual(1, e.TotalLoaded);
                Assert.AreEqual(1, e.TotalToLoad);
            };

            Assert.IsTrue(facade.Storage.AreAllFilesUpToDate);
            facade.Storage.AddFile("file1", "path1");
            Assert.IsFalse(facade.Storage.AreAllFilesUpToDate);
            Assert.IsFalse(facade.Storage.LoadFiles(true));
            Assert.IsFalse(facade.Storage.AreAllFilesUpToDate);
        }
        [TestMethod]
        public void ValidatePinPadStorageForcedLoadFilesLfrError() {
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
                    if (unsecuredRequest == "LFI008005file1") {
                        connection.WriteResponse("LFI000");
                    }
                    else if (unsecuredRequest == "LFR02301031323334353637383930") {
                        connection.WriteResponse("LFR040");
                    }
                }
            };
            PinPadFacade facade = new PinPadFacade(connection);

            SystemStorageMock systemStorage = new SystemStorageMock();
            CrossPlatformController.StorageController = systemStorage;
            systemStorage.AddFile("path1", Encoding.ASCII.GetBytes("1234567890"));

            facade.Storage.OnFileLoaded += (sender, e) => {
                Assert.AreEqual(facade.Storage, sender);
                Assert.AreEqual(facade, e.PinPad);
                Assert.AreEqual("file1", e.Name);
                Assert.AreEqual("path1", e.Path);
                Assert.IsFalse(e.SucessfullyLoaded);
                Assert.AreEqual(1, e.TotalLoaded);
                Assert.AreEqual(1, e.TotalToLoad);
            };

            Assert.IsTrue(facade.Storage.AreAllFilesUpToDate);
            facade.Storage.AddFile("file1", "path1");
            Assert.IsFalse(facade.Storage.AreAllFilesUpToDate);
            Assert.IsFalse(facade.Storage.LoadFiles(true));
            Assert.IsFalse(facade.Storage.AreAllFilesUpToDate);
        }
        [TestMethod]
        public void ValidatePinPadStorageForcedLoadFilesLfeError() {
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
                    if (unsecuredRequest == "LFC008005file1") {
                        connection.WriteResponse("LFC00001100000000000");
                    }
                    else if (unsecuredRequest == "LFI008005file1") {
                        connection.WriteResponse("LFI000");
                    }
                    else if (unsecuredRequest == "LFR02301031323334353637383930") {
                        connection.WriteResponse("LFR000");
                    }
                    else if (unsecuredRequest == "LFE") {
                        connection.WriteResponse("LFE040");
                    }
                }
            };
            PinPadFacade facade = new PinPadFacade(connection);

            SystemStorageMock systemStorage = new SystemStorageMock();
            CrossPlatformController.StorageController = systemStorage;
            systemStorage.AddFile("path1", Encoding.ASCII.GetBytes("1234567890"));

            facade.Storage.OnFileLoaded += (sender, e) => {
                Assert.AreEqual(facade.Storage, sender);
                Assert.AreEqual(facade, e.PinPad);
                Assert.AreEqual("file1", e.Name);
                Assert.AreEqual("path1", e.Path);
                Assert.IsFalse(e.SucessfullyLoaded);
                Assert.AreEqual(1, e.TotalLoaded);
                Assert.AreEqual(1, e.TotalToLoad);
            };

            Assert.IsTrue(facade.Storage.AreAllFilesUpToDate);
            facade.Storage.AddFile("file1", "path1");
            Assert.IsFalse(facade.Storage.AreAllFilesUpToDate);
            Assert.IsFalse(facade.Storage.LoadFiles(true));
            Assert.IsFalse(facade.Storage.AreAllFilesUpToDate);
        }
        [TestMethod]
        public void ValidatePinPadStorageAreAllFilesUpToDate() {
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
                    if (unsecuredRequest == "LFC008005file1") {
                        connection.WriteResponse("LFC00001110000000010");
                    }
                }
            };
            PinPadFacade facade = new PinPadFacade(connection);

            SystemStorageMock systemStorage = new SystemStorageMock();
            CrossPlatformController.StorageController = systemStorage;
            systemStorage.AddFile("path1", Encoding.ASCII.GetBytes("1234567890"));

            Assert.IsTrue(facade.Storage.AreAllFilesUpToDate);
            facade.Storage.AddFile("file1", "path1");
            Assert.IsFalse(facade.Storage.AreAllFilesUpToDate);
            Assert.IsTrue(facade.Storage.LoadFiles());
            Assert.IsTrue(facade.Storage.AreAllFilesUpToDate);
        }
        [TestMethod]
        public void ValidatePinPadStorageFileNameCollection() {
            PinPadConnectionMock connection = new PinPadConnectionMock();
            PinPadFacade facade = new PinPadFacade(connection);

            SystemStorageMock systemStorage = new SystemStorageMock();
            CrossPlatformController.StorageController = systemStorage;
            systemStorage.AddFile("path1", Encoding.ASCII.GetBytes("1234567890"));
            systemStorage.AddFile("path2", Encoding.ASCII.GetBytes("1234567890"));

            Assert.AreEqual(0, facade.Storage.FileNameCollection.Length);

            facade.Storage.AddFile("file1", "path1");
            Assert.AreEqual(1, facade.Storage.FileNameCollection.Length);
            Assert.AreEqual("file1", facade.Storage.FileNameCollection[0]);
            Assert.AreEqual("path1", facade.Storage.GetFilePath("file1"));
            Assert.IsNull(facade.Storage.GetFilePath("file2"));

            facade.Storage.AddFile("file2", "path2");
            Assert.AreEqual(2, facade.Storage.FileNameCollection.Length);
            Assert.AreEqual("file1", facade.Storage.FileNameCollection[0]);
            Assert.AreEqual("file2", facade.Storage.FileNameCollection[1]);
            Assert.AreEqual("path1", facade.Storage.GetFilePath("file1"));
            Assert.AreEqual("path2", facade.Storage.GetFilePath("file2"));

            facade.Storage.RemoveFile("file1");
            Assert.AreEqual(1, facade.Storage.FileNameCollection.Length);
            Assert.AreEqual("file2", facade.Storage.FileNameCollection[0]);
            Assert.AreEqual("path2", facade.Storage.GetFilePath("file2"));
            Assert.IsNull(facade.Storage.GetFilePath("file1"));

            facade.Storage.RemoveFile("file2");
            Assert.AreEqual(0, facade.Storage.FileNameCollection.Length);
            Assert.IsNull(facade.Storage.GetFilePath("file1"));
            Assert.IsNull(facade.Storage.GetFilePath("file2"));
        }
        [TestMethod]
        public void ValidatePinPadStorageFileExists() {
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
                    if (unsecuredRequest == "LFC008005file1") {
                        connection.WriteResponse("LFC00001110000000010");
                    }
                    else if (unsecuredRequest == "LFC008005file2") {
                        connection.WriteResponse("LFC00001110000000009");
                    }
                    else if (unsecuredRequest == "LFC008005file3") {
                        connection.WriteResponse("LFC00001110000000011");
                    }
                    else if (unsecuredRequest == "LFC008005file4") {
                        connection.WriteResponse("LFC00001100000000000");
                    }
                }
            };
            PinPadFacade facade = new PinPadFacade(connection);

            SystemStorageMock systemStorage = new SystemStorageMock();
            CrossPlatformController.StorageController = systemStorage;
            systemStorage.AddFile("path1", Encoding.ASCII.GetBytes("1234567890"));
            facade.Storage.AddFile("file1", "path1");
            facade.Storage.AddFile("file2", "path1");
            facade.Storage.AddFile("file3", "path1");
            facade.Storage.AddFile("file4", "path1");

            Assert.IsTrue(facade.Storage.FileExists("file1").Value);
            Assert.IsTrue(facade.Storage.FileExistsAndHasSameSize("file1", "path1").Value);

            Assert.IsTrue(facade.Storage.FileExists("file2").Value);
            Assert.IsFalse(facade.Storage.FileExistsAndHasSameSize("file2", "path1").Value);

            Assert.IsTrue(facade.Storage.FileExists("file3").Value);
            Assert.IsFalse(facade.Storage.FileExistsAndHasSameSize("file3", "path1").Value);

            Assert.IsFalse(facade.Storage.FileExists("file4").Value);
            Assert.IsFalse(facade.Storage.FileExistsAndHasSameSize("file4", "path1").Value);
        }
        [TestMethod]
        public void ValidatePinPadStorageFileExistsSendTimeout() {
            PinPadConnectionMock connection = new PinPadConnectionMock();
            connection.OnCommandReceived = (command, checksumFailed) => {
                if (command == "OPN") {
                    connection.SendPositiveAcknowledge();
                    connection.WriteResponse("OPN000003001");
                }
            };
            PinPadFacade facade = new PinPadFacade(connection);

            SystemStorageMock systemStorage = new SystemStorageMock();
            CrossPlatformController.StorageController = systemStorage;
            systemStorage.AddFile("path1", Encoding.ASCII.GetBytes("1234567890"));
            facade.Storage.AddFile("file1", "path1");

            Assert.IsNull(facade.Storage.FileExists("file1"));
            Assert.IsNull(facade.Storage.FileExistsAndHasSameSize("file1", "path1"));
        }
        [TestMethod]
        public void ValidatePinPadStorageFileExistsInvalidResponse() {
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
                    if (unsecuredRequest == "LFC008005file1") {
                        connection.WriteResponse("LFC040");
                    }
                }
            };
            PinPadFacade facade = new PinPadFacade(connection);

            SystemStorageMock systemStorage = new SystemStorageMock();
            CrossPlatformController.StorageController = systemStorage;
            systemStorage.AddFile("path1", Encoding.ASCII.GetBytes("1234567890"));
            facade.Storage.AddFile("file1", "path1");

            Assert.IsNull(facade.Storage.FileExists("file1"));
            Assert.IsNull(facade.Storage.FileExistsAndHasSameSize("file1", "path1"));
        }
        [TestMethod]
        public void ValidatePinPadStorageAddFile() {
            PinPadConnectionMock connection = new PinPadConnectionMock();
            PinPadFacade facade = new PinPadFacade(connection);

            SystemStorageMock systemStorage = new SystemStorageMock();
            CrossPlatformController.StorageController = systemStorage;
            systemStorage.AddFile("path1", Encoding.ASCII.GetBytes("1234567890"));
            systemStorage.AddFile("path2", Encoding.ASCII.GetBytes("1234567890"));

            Assert.IsTrue(BasePropertyTestUtils.IsActionThrowingException<ArgumentException>(() => {
                facade.Storage.AddFile(null, "path1");
            }));

            Assert.IsTrue(BasePropertyTestUtils.IsActionThrowingException<ArgumentException>(() => {
                facade.Storage.AddFile(string.Empty.PadLeft(15, ' '), "path1");
            }));

            Assert.IsTrue(BasePropertyTestUtils.IsActionThrowingException<ArgumentException>(() => {
                facade.Storage.AddFile("1234567890123456", "path1");
            }));

            Assert.IsTrue(BasePropertyTestUtils.IsActionThrowingException<ArgumentException>(() => {
                facade.Storage.AddFile("123456789012345", null);
            }));

            Assert.IsTrue(BasePropertyTestUtils.IsActionThrowingException<ArgumentException>(() => {
                facade.Storage.AddFile("123456789012345", string.Empty.PadLeft(15, ' '));
            }));

            Assert.IsTrue(BasePropertyTestUtils.IsActionThrowingException<ArgumentException>(() => {
                facade.Storage.AddFile("123456789012345", "unknownPath");
            }));

            facade.Storage.AddFile("file1", "path1");
            Assert.AreEqual(1, facade.Storage.FileNameCollection.Length);
            Assert.AreEqual("file1", facade.Storage.FileNameCollection[0]);
            Assert.AreEqual("path1", facade.Storage.GetFilePath("file1"));

            facade.Storage.AddFile("file1", "path2");
            Assert.AreEqual(1, facade.Storage.FileNameCollection.Length);
            Assert.AreEqual("file1", facade.Storage.FileNameCollection[0]);
            Assert.AreEqual("path2", facade.Storage.GetFilePath("file1"));
        }
    }
}
