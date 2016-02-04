using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PinPadSDK.Controllers.Tables;
using PinPadSDK.Enums;
using PinPadSDK.Property;

namespace PinPadSdkTests.Tables {
    [TestClass]
    public class UnknownAidTableTests {
        [TestMethod]
        public void ValidateUnknownAidTableWarnings() {
            UnknownAidTable table = new UnknownAidTable();
            UnknownAidTableTests.ValidateUnknownAidTableWarnings(table);
        }

        public static void ValidateUnknownAidTableWarnings(UnknownAidTable table) {
            PrivateObject privateBaseTable = new PrivateObject(table, new PrivateType(typeof(BaseAidTable)));

            BaseAidTableTests.ValidateBaseAidTableWarnings(table);

            BasePropertyTestUtils.TestProperty<ApplicationType>(table, (PinPadFixedLengthPropertyController<ApplicationType>)privateBaseTable.GetProperty("_T1_ICCSTD"), 2); //Value used to sync with command string
            BasePropertyTestUtils.TestSimpleProperty(table, table.T1_UNKDATA, 100, true);

            Assert.AreEqual("162112121612345678901234567890123456789012121234567890123456001234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890",
                table.CommandString);
            BasePropertyTestUtils.TestCommandString(table);
        }
    }
}
