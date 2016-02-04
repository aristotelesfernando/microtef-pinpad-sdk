using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PinPadSDK.Controllers.Tables;
using PinPadSDK.Enums;

namespace PinPadSdkTests.Tables {
    [TestClass]
    public class RevCerTableTests {
        [TestMethod]
        public void ValidateRevCerTableWarnings() {
            RevCerTable table = new RevCerTable();
            RevCerTableTests.ValidateRevCerTableWarnings(table);
        }

        public static void ValidateRevCerTableWarnings(RevCerTable table) {

            BaseTableTests.ValidateBaseTableWarnings(table);

            BasePropertyTestUtils.TestProperty(table, table.T3_RID, 10);
            BasePropertyTestUtils.TestProperty(table, table.T3_CAPKIDX, 2);
            BasePropertyTestUtils.TestProperty(table, table.T3_CERTSN, 6);

            Assert.AreEqual(EmvTableType.RevokedCertificate, table.TAB_ID);

            Assert.AreEqual("02631212123456789012123456",
                table.CommandString);
            BasePropertyTestUtils.TestCommandString(table);
        }
    }
}
