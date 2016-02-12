using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pinpad.Sdk.EmvTable;
using Pinpad.Sdk.Connection;
using Moq;
using Pinpad.Sdk.Model;
using MicroPos.CrossPlatform;

namespace Pinpad.Sdk.Test.EmvTable
{
    [TestClass]
    public class PinpadTableTest
    {
        Mock<BasePinpadConnection> MockedConnection;
        CapkEntry BasicCapk;

        #region setting stuff up
        [TestInitialize]
        public void Setup()
        {
            this.MockedConnection = MockedConnectionInitializer();
            this.BasicCapk = BasicCapkInitializer();
        }

        public Mock<BasePinpadConnection> MockedConnectionInitializer()
        {
            Mock<BasePinpadConnection> conn = new Mock<BasePinpadConnection>();
            
            conn.Object.PlatformPinpadConnection = new Mock<IPinpadConnection>().Object;
            
            return conn;
        }

        public CapkEntry BasicCapkInitializer()
        {
            CapkEntry capk = new CapkEntry();

            capk.RecordedIdentification = "A000000003";
            capk.CapkIndex = "99";
            capk.Exponent = "030000";
            capk.Modulus = "AB79FCC9520896967E776E64444E5DCDD6E13611874F3985722520425295EEA4BD0C2781DE7F31CD3D041F565F747306EED62954B17EDABA3A6C5B85A1DE1BEB9A34141AF38FCF8279C9DEA0D5A6710D08DB4124F041945587E20359BAB47B7575AD94262D4B25F264AF33DEDCF28E09615E937DE32EDC03C54445FE7E38277700000000000000000000000000000000";

            return capk;
        }
        #endregion

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void PinpadTablpeoe_should_throw_exception_if_null_PinpadConnection()
        {
            PinpadTable table = PinpadTable.GetInstance(null);
        }

        [TestMethod]
        public void PinpadTable_should_not_return_null()
        {
            PinpadTable table = PinpadTable.GetInstance(this.MockedConnection.Object);
            Assert.IsNotNull(table);
        }

        [TestMethod]
        [ExpectedException(typeof(NotImplementedException))]
        public void PinpadTable_AddEntry_should_throw_exception_if_unknown_entry()
        {
            PinpadTable table = PinpadTable.GetInstance(this.MockedConnection.Object);
            Mock<BaseTableEntry> mockedEntry = new Mock<BaseTableEntry>();
            table.AddEntry(mockedEntry.Object);
        }

		[TestMethod]
		public void PinpadTable_loading_tables_should_succeed()
		{
			PinpadTable table = PinpadTable.GetInstance(this.MockedConnection.Object);

			Assert.AreEqual(table.CapkTable.Count, 0);

			for (int i = 0; i < 10; i++) { table.AddEntry(this.BasicCapk); }

			Assert.AreEqual(table.CapkTable.Count, 10);

			for (int i = 0; i < 5; i++) { table.AddEntry(this.BasicCapk); }

			Assert.AreEqual(table.CapkTable.Count, 15);
		}
    }
}
