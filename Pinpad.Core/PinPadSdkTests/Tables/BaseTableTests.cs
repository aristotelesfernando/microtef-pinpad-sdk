using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PinPadSDK.Controllers.Tables;
using PinPadSDK.Enums;
using PinPadSDK.Property;

namespace PinPadSdkTests.Tables {
    [TestClass]
    public class BaseTableTests {
        [TestMethod]
        public void ValidateBaseTableWarnings() {
            BaseTable table = new BaseTable();
            BaseTableTests.ValidateBaseTableWarnings(table);
        }

        public static void ValidateBaseTableWarnings(BaseTable table) {
            PrivateObject privateTable = new PrivateObject(table);

            if (table.TAB_ID == EmvTableType.Undefined) {
                BasePropertyTestUtils.TestProperty<EmvTableType>(table, (PinPadFixedLengthPropertyController<EmvTableType>)privateTable.GetProperty("_TAB_ID"), 2);
            }
            BasePropertyTestUtils.TestProperty(table, table.TAB_ACQ, 2);
            BasePropertyTestUtils.TestProperty(table, table.TAB_RECIDX, 2);

            Assert.AreNotEqual(EmvTableType.Undefined, table.TAB_ID);

            BaseTable newTable = new BaseTable();
            PrivateObject privateNewTable = new PrivateObject(newTable);
            ((PinPadFixedLengthPropertyController<EmvTableType>)privateNewTable.GetProperty("_TAB_ID")).Value = EmvTableType.Aid; //Value used to sync with command string
            newTable.TAB_ACQ.Value = table.TAB_ACQ.Value;
            newTable.TAB_RECIDX.Value = table.TAB_RECIDX.Value;

            BasePropertyTestUtils.TestCommandString(newTable);

            Assert.AreEqual("00811212",
                newTable.CommandString);
        }
    }
}
