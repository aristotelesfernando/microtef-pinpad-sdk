using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PinPadSDK.PinPad;
using PinPadSDK.Controllers.Tables;
using PinPadSdkTests.Tables;
using StonePortableUtils;
using System.Collections.Generic;

namespace PinPadSdkTests.PinPad {
    [TestClass]
    public class PinPadTableTests {
        [TestInitialize]
        public void TestSetup() {
            CrossPlatformDesktop.CrossPlatformDesktopInitializer.Initialize_CrossPlatformDesktop();
            //CrossPlatformController.SendMailController = new SendMailControllerMock();
        }
        [TestMethod]
        public void ValidatePinPadTableTableVersion() {
            PinPadConnectionMock connection = new PinPadConnectionMock();
            connection.OnCommandReceived = (command, checksumFailed) => {
                connection.SendPositiveAcknowledge();
                if (command == "OPN") {
                    connection.WriteResponse("OPN000");
                }
                else if (command == "GTS00200") {
                    connection.WriteResponse("GTS0000101234567890");
                }
            };
            PinPadFacade facade = new PinPadFacade(connection);
            Assert.AreEqual("1234567890", facade.Table.TableVersion);
        }
        [TestMethod]
        public void ValidatePinPadTableTableVersionFailToSend() {
            PinPadConnectionMock connection = new PinPadConnectionMock();
            connection.OnCommandReceived = (command, checksumFailed) => {
                if (command == "OPN") {
                    connection.SendPositiveAcknowledge();
                    connection.WriteResponse("OPN000");
                }
            };
            PinPadFacade facade = new PinPadFacade(connection);
            Assert.IsNull(facade.Table.TableVersion);
        }
        [TestMethod]
        public void ValidatePinPadTableTableVersionResponseError() {
            PinPadConnectionMock connection = new PinPadConnectionMock();
            connection.OnCommandReceived = (command, checksumFailed) => {
                connection.SendPositiveAcknowledge();
                if (command == "OPN") {
                    connection.WriteResponse("OPN000");
                }
                else if (command == "GTS00200") {
                    connection.WriteResponse("GTS040");
                }
            };
            PinPadFacade facade = new PinPadFacade(connection);
            Assert.IsNull(facade.Table.TableVersion);
        }
        [TestMethod]
        public void ValidatePinPadTableTablesLoaded() {
            PinPadConnectionMock connection = new PinPadConnectionMock();
            connection.OnCommandReceived = (command, checksumFailed) => {
                connection.SendPositiveAcknowledge();
                if (command == "OPN") {
                    connection.WriteResponse("OPN000");
                }
                else if (command == "GTS00200") {
                    connection.WriteResponse("GTS0000101234567890");
                }
            };
            PinPadFacade facade = new PinPadFacade(connection);
            facade.Table.ExpectedTableVersion = "1234567890";
            Assert.IsTrue(facade.Table.TablesLoaded);
        }
        [TestMethod]
        public void ValidatePinPadTableTablesLoadedWrongVersion() {
            PinPadConnectionMock connection = new PinPadConnectionMock();
            connection.OnCommandReceived = (command, checksumFailed) => {
                connection.SendPositiveAcknowledge();
                if (command == "OPN") {
                    connection.WriteResponse("OPN000");
                }
                else if (command == "GTS00200") {
                    connection.WriteResponse("GTS0000101234567890");
                }
            };
            PinPadFacade facade = new PinPadFacade(connection);
            facade.Table.ExpectedTableVersion = "0987654321";
            Assert.IsFalse(facade.Table.TablesLoaded);
        }
        [TestMethod]
        public void ValidatePinPadTableTablesLoadedModified() {
            PinPadConnectionMock connection = new PinPadConnectionMock();
            connection.OnCommandReceived = (command, checksumFailed) => {
                connection.SendPositiveAcknowledge();
                if (command == "OPN") {
                    connection.WriteResponse("OPN000");
                }
                else if (command == "GTS00200") {
                    connection.WriteResponse("GTS0000101234567890");
                }
            };
            PinPadFacade facade = new PinPadFacade(connection);
            facade.Table.ExpectedTableVersion = "1234567890";
            Assert.IsTrue(facade.Table.TablesLoaded);

            EmvAidTable table = new EmvAidTable();
            EmvAidTableTests.ValidateEmvAidTableWarnings(table);
            facade.Table.AddTable(table);

            Assert.IsFalse(facade.Table.TablesLoaded);
        }
        [TestMethod]
        public void ValidatePinPadTableTablesLoadedFailToSend() {
            PinPadConnectionMock connection = new PinPadConnectionMock();
            connection.OnCommandReceived = (command, checksumFailed) => {
                if (command == "OPN") {
                    connection.SendPositiveAcknowledge();
                    connection.WriteResponse("OPN000");
                }
            };
            PinPadFacade facade = new PinPadFacade(connection);
            facade.Table.ExpectedTableVersion = "1234567890";
            Assert.IsFalse(facade.Table.TablesLoaded);
        }
        [TestMethod]
        public void ValidatePinPadTableTablesLoadedResponseError() {
            PinPadConnectionMock connection = new PinPadConnectionMock();
            connection.OnCommandReceived = (command, checksumFailed) => {
                connection.SendPositiveAcknowledge();
                if (command == "OPN") {
                    connection.WriteResponse("OPN000");
                }
                else if (command == "GTS00200") {
                    connection.WriteResponse("GTS040");
                }
            };
            PinPadFacade facade = new PinPadFacade(connection);
            facade.Table.ExpectedTableVersion = "1234567890";
            Assert.IsFalse(facade.Table.TablesLoaded);
        }
        [TestMethod]
        public void ValidatePinPadTableExpectedTableVersion() {
            PinPadConnectionMock connection = new PinPadConnectionMock();
            PinPadFacade facade = new PinPadFacade(connection);
            facade.Table.ExpectedTableVersion = "1234567890";
            Assert.AreEqual("1234567890", facade.Table.ExpectedTableVersion);
        }
        [TestMethod]
        public void ValidatePinPadTableExpectedTableVersionNull() {
            PinPadConnectionMock connection = new PinPadConnectionMock();
            PinPadFacade facade = new PinPadFacade(connection);
            try {
                facade.Table.ExpectedTableVersion = null;
            }
            catch (InvalidOperationException) { }
        }
        [TestMethod]
        public void ValidatePinPadTableExpectedTableVersionEmptyString() {
            PinPadConnectionMock connection = new PinPadConnectionMock();
            PinPadFacade facade = new PinPadFacade(connection);
            try {
                facade.Table.ExpectedTableVersion = "";
            }
            catch (InvalidOperationException) { }
        }
        [TestMethod]
        public void ValidatePinPadTableExpectedTableVersionTooSmallString() {
            PinPadConnectionMock connection = new PinPadConnectionMock();
            PinPadFacade facade = new PinPadFacade(connection);
            try {
                facade.Table.ExpectedTableVersion = "123456789";
            }
            catch (InvalidOperationException) { }
        }
        [TestMethod]
        public void ValidatePinPadTableExpectedTableVersionTooBigString() {
            PinPadConnectionMock connection = new PinPadConnectionMock();
            PinPadFacade facade = new PinPadFacade(connection);
            try {
                facade.Table.ExpectedTableVersion = "12345678901";
            }
            catch (InvalidOperationException) { }
        }
        [TestMethod]
        public void ValidatePinPadTableTables() {
            PinPadConnectionMock connection = new PinPadConnectionMock();
            PinPadFacade facade = new PinPadFacade(connection);

            EmvAidTable emvAidTable = new EmvAidTable();
            EmvAidTableTests.ValidateEmvAidTableWarnings(emvAidTable);
            facade.Table.AddTable(emvAidTable);

            UnknownAidTable unknownAidTable = new UnknownAidTable();
            UnknownAidTableTests.ValidateUnknownAidTableWarnings(unknownAidTable);
            unknownAidTable.TAB_RECIDX.Value++;
            facade.Table.AddTable(unknownAidTable);

            CapkTable capkTable = new CapkTable();
            CapkTableTests.ValidateCapkTableWarnings(capkTable);
            facade.Table.AddTable(capkTable);

            RevCerTable revCerTable = new RevCerTable();
            RevCerTableTests.ValidateRevCerTableWarnings(revCerTable);
            facade.Table.AddTable(revCerTable);

            Assert.AreEqual(4, facade.Table.TableCollection.Length);
            Assert.AreEqual(1, facade.Table.EmvAidTableCollection.Length);
            Assert.AreEqual(1, facade.Table.CapkTableCollection.Length);

            facade.Table.ClearTables();

            Assert.AreEqual(0, facade.Table.TableCollection.Length);
            Assert.AreEqual(0, facade.Table.EmvAidTableCollection.Length);
            Assert.AreEqual(0, facade.Table.CapkTableCollection.Length);
        }
        [TestMethod]
        public void ValidatePinPadTableTableEntryCollision() {
            PinPadConnectionMock connection = new PinPadConnectionMock();
            PinPadFacade facade = new PinPadFacade(connection);

            EmvAidTable emvAidTable1 = new EmvAidTable();
            EmvAidTableTests.ValidateEmvAidTableWarnings(emvAidTable1);

            EmvAidTable emvAidTable2 = new EmvAidTable();
            EmvAidTableTests.ValidateEmvAidTableWarnings(emvAidTable2);
            emvAidTable2.T1_AID.Value = new HexadecimalData("1234567890ABCDEF");

            EmvAidTable emvAidTable3 = new EmvAidTable();
            EmvAidTableTests.ValidateEmvAidTableWarnings(emvAidTable3);
            emvAidTable3.TAB_ACQ.Value++;

            EmvAidTable emvAidTable4 = new EmvAidTable();
            EmvAidTableTests.ValidateEmvAidTableWarnings(emvAidTable4);
            emvAidTable4.TAB_RECIDX.Value++;

            Assert.IsTrue(facade.Table.AddTable(emvAidTable1));
            Assert.AreEqual(1, facade.Table.EmvAidTableCollection.Length);

            Assert.IsTrue(facade.Table.AddTable(emvAidTable2));
            Assert.AreEqual(1, facade.Table.EmvAidTableCollection.Length);

            Assert.IsTrue(facade.Table.AddTable(emvAidTable3));
            Assert.AreEqual(2, facade.Table.EmvAidTableCollection.Length);

            Assert.IsTrue(facade.Table.AddTable(emvAidTable4));
            Assert.AreEqual(3, facade.Table.EmvAidTableCollection.Length);

            List<EmvAidTable> loadedEmvAidTableCollection = new List<EmvAidTable>(facade.Table.EmvAidTableCollection);
            Assert.IsFalse(loadedEmvAidTableCollection.Contains(emvAidTable1));
            Assert.IsTrue(loadedEmvAidTableCollection.Contains(emvAidTable2));
            Assert.IsTrue(loadedEmvAidTableCollection.Contains(emvAidTable3));
            Assert.IsTrue(loadedEmvAidTableCollection.Contains(emvAidTable4));
        }
        [TestMethod]
        public void ValidatePinPadTableInvalidTableEntry() {
            PinPadConnectionMock connection = new PinPadConnectionMock();
            PinPadFacade facade = new PinPadFacade(connection);

            EmvAidTable emvAidTable = new EmvAidTable();
            Assert.IsFalse(facade.Table.AddTable(emvAidTable));
            Assert.AreEqual(0, facade.Table.EmvAidTableCollection.Length);

            EmvAidTableTests.ValidateEmvAidTableWarnings(emvAidTable);
            Assert.IsTrue(facade.Table.AddTable(emvAidTable));
            Assert.AreEqual(1, facade.Table.EmvAidTableCollection.Length);
        }
        [TestMethod]
        public void ValidatePinPadTableTableLoad() {
            PinPadConnectionMock connection = new PinPadConnectionMock();
            string tableVersion = "0987654321";
            connection.OnCommandReceived = (command, checksumFailed) => {
                connection.SendPositiveAcknowledge();
                if (command == "OPN") {
                    connection.WriteResponse("OPN000");
                }
                else if (command == "GTS00200") {
                    connection.WriteResponse("GTS000010" + tableVersion);
                }
                else if (command == "TLI012001234567890") {
                    connection.WriteResponse("TLI020");
                }
                else if (command.StartsWith("TLR") == true) {
                    connection.WriteResponse("TLR000");
                }
                else if (command == "TLE") {
                    connection.WriteResponse("TLE000");
                    tableVersion = "1234567890";
                }
            };
            PinPadFacade facade = new PinPadFacade(connection);

            facade.Table.ExpectedTableVersion = "1234567890";

            EmvAidTable emvAidTable = new EmvAidTable();
            EmvAidTableTests.ValidateEmvAidTableWarnings(emvAidTable);
            Assert.IsTrue(facade.Table.AddTable(emvAidTable));

            UnknownAidTable unknownAidTable = new UnknownAidTable();
            UnknownAidTableTests.ValidateUnknownAidTableWarnings(unknownAidTable);
            unknownAidTable.TAB_RECIDX.Value++;
            Assert.IsTrue(facade.Table.AddTable(unknownAidTable));

            CapkTable capkTable = new CapkTable();
            CapkTableTests.ValidateCapkTableWarnings(capkTable);
            Assert.IsTrue(facade.Table.AddTable(capkTable));

            RevCerTable revCerTable = new RevCerTable();
            RevCerTableTests.ValidateRevCerTableWarnings(revCerTable);
            Assert.IsTrue(facade.Table.AddTable(revCerTable));

            Assert.AreEqual(4, facade.Table.TableCollection.Length);
            Assert.AreEqual(1, facade.Table.EmvAidTableCollection.Length);
            Assert.AreEqual(1, facade.Table.CapkTableCollection.Length);

            Assert.AreEqual("0987654321", facade.Table.TableVersion);

            Assert.IsFalse(facade.Table.TablesLoaded);
            Assert.IsTrue(facade.Table.LoadTables());
            Assert.IsTrue(facade.Table.TablesLoaded);

            Assert.AreEqual("1234567890", facade.Table.TableVersion);
        }
        [TestMethod]
        public void ValidatePinPadTableTableReload() {
            PinPadConnectionMock connection = new PinPadConnectionMock();
            string tableVersion = "0987654321";
            connection.OnCommandReceived = (command, checksumFailed) => {
                connection.SendPositiveAcknowledge();
                if (command == "OPN") {
                    connection.WriteResponse("OPN000");
                }
                else if (command == "GTS00200") {
                    connection.WriteResponse("GTS000010" + tableVersion);
                }
                else if (command == "TLI012001234567890") {
                    connection.WriteResponse("TLI000");
                }
                else if (command.StartsWith("TLR") == true) {
                    connection.WriteResponse("TLR000");
                }
                else if (command == "TLE") {
                    connection.WriteResponse("TLE000");
                    tableVersion = "1234567890";
                }
            };
            PinPadFacade facade = new PinPadFacade(connection);

            facade.Table.ExpectedTableVersion = "1234567890";

            EmvAidTable emvAidTable = new EmvAidTable();
            EmvAidTableTests.ValidateEmvAidTableWarnings(emvAidTable);
            Assert.IsTrue(facade.Table.AddTable(emvAidTable));

            UnknownAidTable unknownAidTable = new UnknownAidTable();
            UnknownAidTableTests.ValidateUnknownAidTableWarnings(unknownAidTable);
            unknownAidTable.TAB_RECIDX.Value++;
            Assert.IsTrue(facade.Table.AddTable(unknownAidTable));

            CapkTable capkTable = new CapkTable();
            CapkTableTests.ValidateCapkTableWarnings(capkTable);
            Assert.IsTrue(facade.Table.AddTable(capkTable));

            RevCerTable revCerTable = new RevCerTable();
            RevCerTableTests.ValidateRevCerTableWarnings(revCerTable);
            Assert.IsTrue(facade.Table.AddTable(revCerTable));

            Assert.AreEqual(4, facade.Table.TableCollection.Length);
            Assert.AreEqual(1, facade.Table.EmvAidTableCollection.Length);
            Assert.AreEqual(1, facade.Table.CapkTableCollection.Length);

            Assert.AreEqual("0987654321", facade.Table.TableVersion);

            Assert.IsFalse(facade.Table.TablesLoaded);
            Assert.IsTrue(facade.Table.LoadTables());
            Assert.IsTrue(facade.Table.TablesLoaded);

            Assert.AreEqual("1234567890", facade.Table.TableVersion);
        }
        [TestMethod]
        public void ValidatePinPadTableTableLoadTliFailToSend() {
            PinPadConnectionMock connection = new PinPadConnectionMock();
            string tableVersion = "0987654321";
            connection.OnCommandReceived = (command, checksumFailed) => {
                if (command == "OPN") {
                    connection.SendPositiveAcknowledge();
                    connection.WriteResponse("OPN000");
                }
                else if (command == "GTS00200") {
                    connection.SendPositiveAcknowledge();
                    connection.WriteResponse("GTS000010" + tableVersion);
                }
            };
            PinPadFacade facade = new PinPadFacade(connection);

            facade.Table.ExpectedTableVersion = "1234567890";

            EmvAidTable emvAidTable = new EmvAidTable();
            EmvAidTableTests.ValidateEmvAidTableWarnings(emvAidTable);
            Assert.IsTrue(facade.Table.AddTable(emvAidTable));

            UnknownAidTable unknownAidTable = new UnknownAidTable();
            UnknownAidTableTests.ValidateUnknownAidTableWarnings(unknownAidTable);
            unknownAidTable.TAB_RECIDX.Value++;
            Assert.IsTrue(facade.Table.AddTable(unknownAidTable));

            CapkTable capkTable = new CapkTable();
            CapkTableTests.ValidateCapkTableWarnings(capkTable);
            Assert.IsTrue(facade.Table.AddTable(capkTable));

            RevCerTable revCerTable = new RevCerTable();
            RevCerTableTests.ValidateRevCerTableWarnings(revCerTable);
            Assert.IsTrue(facade.Table.AddTable(revCerTable));

            Assert.AreEqual(4, facade.Table.TableCollection.Length);
            Assert.AreEqual(1, facade.Table.EmvAidTableCollection.Length);
            Assert.AreEqual(1, facade.Table.CapkTableCollection.Length);

            Assert.AreEqual("0987654321", facade.Table.TableVersion);

            Assert.IsFalse(facade.Table.TablesLoaded);
            Assert.IsFalse(facade.Table.LoadTables());
            Assert.IsFalse(facade.Table.TablesLoaded);

            Assert.AreEqual("0987654321", facade.Table.TableVersion);
        }
        [TestMethod]
        public void ValidatePinPadTableTableLoadTliFailed() {
            PinPadConnectionMock connection = new PinPadConnectionMock();
            string tableVersion = "0987654321";
            connection.OnCommandReceived = (command, checksumFailed) => {
                connection.SendPositiveAcknowledge();
                if (command == "OPN") {
                    connection.WriteResponse("OPN000");
                }
                else if (command == "GTS00200") {
                    connection.WriteResponse("GTS000010" + tableVersion);
                }
                else if (command == "TLI012001234567890") {
                    connection.WriteResponse("TLI040");
                }
            };
            PinPadFacade facade = new PinPadFacade(connection);

            facade.Table.ExpectedTableVersion = "1234567890";

            EmvAidTable emvAidTable = new EmvAidTable();
            EmvAidTableTests.ValidateEmvAidTableWarnings(emvAidTable);
            Assert.IsTrue(facade.Table.AddTable(emvAidTable));

            UnknownAidTable unknownAidTable = new UnknownAidTable();
            UnknownAidTableTests.ValidateUnknownAidTableWarnings(unknownAidTable);
            unknownAidTable.TAB_RECIDX.Value++;
            Assert.IsTrue(facade.Table.AddTable(unknownAidTable));

            CapkTable capkTable = new CapkTable();
            CapkTableTests.ValidateCapkTableWarnings(capkTable);
            Assert.IsTrue(facade.Table.AddTable(capkTable));

            RevCerTable revCerTable = new RevCerTable();
            RevCerTableTests.ValidateRevCerTableWarnings(revCerTable);
            Assert.IsTrue(facade.Table.AddTable(revCerTable));

            Assert.AreEqual(4, facade.Table.TableCollection.Length);
            Assert.AreEqual(1, facade.Table.EmvAidTableCollection.Length);
            Assert.AreEqual(1, facade.Table.CapkTableCollection.Length);

            Assert.AreEqual("0987654321", facade.Table.TableVersion);

            Assert.IsFalse(facade.Table.TablesLoaded);
            Assert.IsFalse(facade.Table.LoadTables());
            Assert.IsFalse(facade.Table.TablesLoaded);

            Assert.AreEqual("0987654321", facade.Table.TableVersion);
        }
        [TestMethod]
        public void ValidatePinPadTableTableLoadTlrFailed() {
            PinPadConnectionMock connection = new PinPadConnectionMock();
            string tableVersion = "0987654321";
            connection.OnCommandReceived = (command, checksumFailed) => {
                connection.SendPositiveAcknowledge();
                if (command == "OPN") {
                    connection.WriteResponse("OPN000");
                }
                else if (command == "GTS00200") {
                    connection.WriteResponse("GTS000010" + tableVersion);
                }
                else if (command == "TLI012001234567890") {
                    connection.WriteResponse("TLI020");
                }
                else if (command.StartsWith("TLR") == true) {
                    connection.WriteResponse("TLR040");
                }
            };
            PinPadFacade facade = new PinPadFacade(connection);

            facade.Table.ExpectedTableVersion = "1234567890";

            EmvAidTable emvAidTable = new EmvAidTable();
            EmvAidTableTests.ValidateEmvAidTableWarnings(emvAidTable);
            Assert.IsTrue(facade.Table.AddTable(emvAidTable));

            UnknownAidTable unknownAidTable = new UnknownAidTable();
            UnknownAidTableTests.ValidateUnknownAidTableWarnings(unknownAidTable);
            unknownAidTable.TAB_RECIDX.Value++;
            Assert.IsTrue(facade.Table.AddTable(unknownAidTable));

            CapkTable capkTable = new CapkTable();
            CapkTableTests.ValidateCapkTableWarnings(capkTable);
            Assert.IsTrue(facade.Table.AddTable(capkTable));

            RevCerTable revCerTable = new RevCerTable();
            RevCerTableTests.ValidateRevCerTableWarnings(revCerTable);
            Assert.IsTrue(facade.Table.AddTable(revCerTable));

            Assert.AreEqual(4, facade.Table.TableCollection.Length);
            Assert.AreEqual(1, facade.Table.EmvAidTableCollection.Length);
            Assert.AreEqual(1, facade.Table.CapkTableCollection.Length);

            Assert.AreEqual("0987654321", facade.Table.TableVersion);

            Assert.IsFalse(facade.Table.TablesLoaded);
            Assert.IsFalse(facade.Table.LoadTables());
            Assert.IsFalse(facade.Table.TablesLoaded);

            Assert.AreEqual("0987654321", facade.Table.TableVersion);
        }
        [TestMethod]
        public void ValidatePinPadTableTableLoadTlrFailToSend() {
            PinPadConnectionMock connection = new PinPadConnectionMock();
            string tableVersion = "0987654321";
            connection.OnCommandReceived = (command, checksumFailed) => {
                if (command == "OPN") {
                    connection.SendPositiveAcknowledge();
                    connection.WriteResponse("OPN000");
                }
                else if (command == "GTS00200") {
                    connection.SendPositiveAcknowledge();
                    connection.WriteResponse("GTS000010" + tableVersion);
                }
                else if (command == "TLI012001234567890") {
                    connection.SendPositiveAcknowledge();
                    connection.WriteResponse("TLI020");
                }
            };
            PinPadFacade facade = new PinPadFacade(connection);

            facade.Table.ExpectedTableVersion = "1234567890";

            EmvAidTable emvAidTable = new EmvAidTable();
            EmvAidTableTests.ValidateEmvAidTableWarnings(emvAidTable);
            Assert.IsTrue(facade.Table.AddTable(emvAidTable));

            UnknownAidTable unknownAidTable = new UnknownAidTable();
            UnknownAidTableTests.ValidateUnknownAidTableWarnings(unknownAidTable);
            unknownAidTable.TAB_RECIDX.Value++;
            Assert.IsTrue(facade.Table.AddTable(unknownAidTable));

            CapkTable capkTable = new CapkTable();
            CapkTableTests.ValidateCapkTableWarnings(capkTable);
            Assert.IsTrue(facade.Table.AddTable(capkTable));

            RevCerTable revCerTable = new RevCerTable();
            RevCerTableTests.ValidateRevCerTableWarnings(revCerTable);
            Assert.IsTrue(facade.Table.AddTable(revCerTable));

            Assert.AreEqual(4, facade.Table.TableCollection.Length);
            Assert.AreEqual(1, facade.Table.EmvAidTableCollection.Length);
            Assert.AreEqual(1, facade.Table.CapkTableCollection.Length);

            Assert.AreEqual("0987654321", facade.Table.TableVersion);

            Assert.IsFalse(facade.Table.TablesLoaded);
            Assert.IsFalse(facade.Table.LoadTables());
            Assert.IsFalse(facade.Table.TablesLoaded);

            Assert.AreEqual("0987654321", facade.Table.TableVersion);
        }
        [TestMethod]
        public void ValidatePinPadTableTableLoadTleFailed() {
            PinPadConnectionMock connection = new PinPadConnectionMock();
            string tableVersion = "0987654321";
            connection.OnCommandReceived = (command, checksumFailed) => {
                connection.SendPositiveAcknowledge();
                if (command == "OPN") {
                    connection.WriteResponse("OPN000");
                }
                else if (command == "GTS00200") {
                    connection.WriteResponse("GTS000010" + tableVersion);
                }
                else if (command == "TLI012001234567890") {
                    connection.WriteResponse("TLI020");
                }
                else if (command.StartsWith("TLR") == true) {
                    connection.WriteResponse("TLR000");
                }
                else if (command == "TLE") {
                    connection.WriteResponse("TLE040");
                }
            };
            PinPadFacade facade = new PinPadFacade(connection);

            facade.Table.ExpectedTableVersion = "1234567890";

            EmvAidTable emvAidTable = new EmvAidTable();
            EmvAidTableTests.ValidateEmvAidTableWarnings(emvAidTable);
            Assert.IsTrue(facade.Table.AddTable(emvAidTable));

            UnknownAidTable unknownAidTable = new UnknownAidTable();
            UnknownAidTableTests.ValidateUnknownAidTableWarnings(unknownAidTable);
            unknownAidTable.TAB_RECIDX.Value++;
            Assert.IsTrue(facade.Table.AddTable(unknownAidTable));

            CapkTable capkTable = new CapkTable();
            CapkTableTests.ValidateCapkTableWarnings(capkTable);
            Assert.IsTrue(facade.Table.AddTable(capkTable));

            RevCerTable revCerTable = new RevCerTable();
            RevCerTableTests.ValidateRevCerTableWarnings(revCerTable);
            Assert.IsTrue(facade.Table.AddTable(revCerTable));

            Assert.AreEqual(4, facade.Table.TableCollection.Length);
            Assert.AreEqual(1, facade.Table.EmvAidTableCollection.Length);
            Assert.AreEqual(1, facade.Table.CapkTableCollection.Length);

            Assert.AreEqual("0987654321", facade.Table.TableVersion);

            Assert.IsFalse(facade.Table.TablesLoaded);
            Assert.IsFalse(facade.Table.LoadTables());
            Assert.IsFalse(facade.Table.TablesLoaded);

            Assert.AreEqual("0987654321", facade.Table.TableVersion);
        }
        [TestMethod]
        public void ValidatePinPadTableTableLoadTleFailToSend() {
            PinPadConnectionMock connection = new PinPadConnectionMock();
            string tableVersion = "0987654321";
            connection.OnCommandReceived = (command, checksumFailed) => {
                if (command == "OPN") {
                    connection.SendPositiveAcknowledge();
                    connection.WriteResponse("OPN000");
                }
                else if (command == "GTS00200") {
                    connection.SendPositiveAcknowledge();
                    connection.WriteResponse("GTS000010" + tableVersion);
                }
                else if (command == "TLI012001234567890") {
                    connection.SendPositiveAcknowledge();
                    connection.WriteResponse("TLI020");
                }
                else if (command.StartsWith("TLR") == true) {
                    connection.SendPositiveAcknowledge();
                    connection.WriteResponse("TLR000");
                }
            };
            PinPadFacade facade = new PinPadFacade(connection);

            facade.Table.ExpectedTableVersion = "1234567890";

            EmvAidTable emvAidTable = new EmvAidTable();
            EmvAidTableTests.ValidateEmvAidTableWarnings(emvAidTable);
            Assert.IsTrue(facade.Table.AddTable(emvAidTable));

            UnknownAidTable unknownAidTable = new UnknownAidTable();
            UnknownAidTableTests.ValidateUnknownAidTableWarnings(unknownAidTable);
            unknownAidTable.TAB_RECIDX.Value++;
            Assert.IsTrue(facade.Table.AddTable(unknownAidTable));

            CapkTable capkTable = new CapkTable();
            CapkTableTests.ValidateCapkTableWarnings(capkTable);
            Assert.IsTrue(facade.Table.AddTable(capkTable));

            RevCerTable revCerTable = new RevCerTable();
            RevCerTableTests.ValidateRevCerTableWarnings(revCerTable);
            Assert.IsTrue(facade.Table.AddTable(revCerTable));

            Assert.AreEqual(4, facade.Table.TableCollection.Length);
            Assert.AreEqual(1, facade.Table.EmvAidTableCollection.Length);
            Assert.AreEqual(1, facade.Table.CapkTableCollection.Length);

            Assert.AreEqual("0987654321", facade.Table.TableVersion);

            Assert.IsFalse(facade.Table.TablesLoaded);
            Assert.IsFalse(facade.Table.LoadTables());
            Assert.IsFalse(facade.Table.TablesLoaded);

            Assert.AreEqual("0987654321", facade.Table.TableVersion);
        }
        [TestMethod]
        public void ValidatePinPadTableTableUnsetTableVersion() {
            PinPadConnectionMock connection = new PinPadConnectionMock();
            string tableVersion = "0987654321";
            connection.OnCommandReceived = (command, checksumFailed) => {
                connection.SendPositiveAcknowledge();
                if (command == "OPN") {
                    connection.WriteResponse("OPN000");
                }
                else if (command == "GTS00200") {
                    connection.WriteResponse("GTS000010" + tableVersion);
                }
                else if (command == "TLI012001234567890") {
                    connection.WriteResponse("TLI020");
                }
                else if (command.StartsWith("TLR") == true) {
                    connection.WriteResponse("TLR000");
                }
                else if (command == "TLE") {
                    connection.WriteResponse("TLE000");
                    tableVersion = "1234567890";
                }
            };
            PinPadFacade facade = new PinPadFacade(connection);

            EmvAidTable emvAidTable = new EmvAidTable();
            EmvAidTableTests.ValidateEmvAidTableWarnings(emvAidTable);
            Assert.IsTrue(facade.Table.AddTable(emvAidTable));

            UnknownAidTable unknownAidTable = new UnknownAidTable();
            UnknownAidTableTests.ValidateUnknownAidTableWarnings(unknownAidTable);
            unknownAidTable.TAB_RECIDX.Value++;
            Assert.IsTrue(facade.Table.AddTable(unknownAidTable));

            CapkTable capkTable = new CapkTable();
            CapkTableTests.ValidateCapkTableWarnings(capkTable);
            Assert.IsTrue(facade.Table.AddTable(capkTable));

            RevCerTable revCerTable = new RevCerTable();
            RevCerTableTests.ValidateRevCerTableWarnings(revCerTable);
            Assert.IsTrue(facade.Table.AddTable(revCerTable));

            Assert.AreEqual(4, facade.Table.TableCollection.Length);
            Assert.AreEqual(1, facade.Table.EmvAidTableCollection.Length);
            Assert.AreEqual(1, facade.Table.CapkTableCollection.Length);

            Assert.AreEqual("0987654321", facade.Table.TableVersion);

            Assert.IsFalse(facade.Table.TablesLoaded);
            try {
                facade.Table.LoadTables();
                Assert.Fail("Did not complain about null ExpectedTableVersion");
            }
            catch (InvalidOperationException) { }
            Assert.IsFalse(facade.Table.TablesLoaded);

            Assert.AreEqual("0987654321", facade.Table.TableVersion);
        }
    }
}