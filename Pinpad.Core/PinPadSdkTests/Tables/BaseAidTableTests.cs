using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PinPadSDK.Controllers.Tables;
using PinPadSDK.Enums;
using PinPadSDK.Property;

namespace PinPadSdkTests.Tables
{
    [TestClass]
    public class BaseAidTableTests
    {
        [TestMethod]
        public void ValidateBaseAidTableWarnings()
        {
            BaseAidTable table = new BaseAidTable();
            BaseAidTableTests.ValidateBaseAidTableWarnings(table);
        }

        public static void ValidateBaseAidTableWarnings(BaseAidTable table)
        {
            BaseTableTests.ValidateBaseTableWarnings(table);

            PrivateObject privateTable = new PrivateObject(table);

            BasePropertyTestUtils.TestProperty(table, table.T1_AID, 32);
            BasePropertyTestUtils.TestProperty(table, table.T1_APPTYPE, 2);
            BasePropertyTestUtils.TestProperty(table, table.T1_DEFLABEL, 16);

            Assert.AreEqual(EmvTableType.Aid, table.TAB_ID);

            BaseAidTable newTable = new BaseAidTable();
            PrivateObject privateNewTable = new PrivateObject(newTable);

            newTable.TAB_ACQ.Value = table.TAB_ACQ.Value;
            newTable.TAB_RECIDX.Value = table.TAB_RECIDX.Value;
            newTable.T1_AID.Value = table.T1_AID.Value;
            newTable.T1_APPTYPE.Value = table.T1_APPTYPE.Value;
            newTable.T1_DEFLABEL.Value = table.T1_DEFLABEL.Value;
            BasePropertyTestUtils.TestProperty<ApplicationType>(newTable, (PinPadFixedLengthPropertyController<ApplicationType>)privateNewTable.GetProperty("_T1_ICCSTD"), 2); //Value used to sync with command string

            BasePropertyTestUtils.TestCommandString(newTable);

            Assert.AreEqual("06211212161234567890123456789012345678901212123456789012345600",
                newTable.CommandString);
        }
    }
}
