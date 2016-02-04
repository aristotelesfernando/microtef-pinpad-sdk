using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PinPadSDK.Controllers.Tables;
using PinPadSDK.Enums;

namespace PinPadSdkTests.Tables
{
    [TestClass]
    public class EmvAidTableTests
    {
        [TestMethod]
        public void ValidateEmvAidTableWarnings() {
            EmvAidTable table = new EmvAidTable();
            EmvAidTableTests.ValidateEmvAidTableWarnings(table);
        }

        public static void ValidateEmvAidTableWarnings(EmvAidTable table)
        {

            BaseAidTableTests.ValidateBaseAidTableWarnings(table);

            BasePropertyTestUtils.TestProperty(table, table.T1_APPVER1, 4);
            BasePropertyTestUtils.TestProperty(table, table.T1_APPVER2, 4);
            BasePropertyTestUtils.TestProperty(table, table.T1_APPVER3, 4);
            BasePropertyTestUtils.TestProperty(table, table.T1_TRMCNTRY, 3);
            BasePropertyTestUtils.TestProperty(table, table.T1_TRMCURR, 3);
            BasePropertyTestUtils.TestProperty(table, table.T1_TRMCRREXP, 1);
            BasePropertyTestUtils.TestProperty(table, table.T1_MERCHID, 15);
            BasePropertyTestUtils.TestProperty(table, table.T1_MCC, 4);
            BasePropertyTestUtils.TestProperty(table, table.T1_TRMID, 8);
            BasePropertyTestUtils.TestProperty(table, table.T1_TRMCAPAB, 6);
            BasePropertyTestUtils.TestProperty(table, table.T1_ADDTRMCP, 10);
            BasePropertyTestUtils.TestProperty(table, table.T1_TRMTYP, 2);
            BasePropertyTestUtils.TestProperty(table, table.T1_TACDEF, 10);
            BasePropertyTestUtils.TestProperty(table, table.T1_TACDEN, 10);
            BasePropertyTestUtils.TestProperty(table, table.T1_TACONL, 10);
            BasePropertyTestUtils.TestProperty(table, table.T1_FLRLIMIT, 8);
            BasePropertyTestUtils.TestProperty(table, table.T1_TCC, 1);
            BasePropertyTestUtils.TestProperty(table, table.T1_CTLSZEROAM);
            BasePropertyTestUtils.TestProperty<ContactlessMode>(table, table.T1_CTLSMODE, 1);
            BasePropertyTestUtils.TestProperty(table, table.T1_CTLSTRNLIM, 8);
            BasePropertyTestUtils.TestProperty(table, table.T1_CTLSFLRLIM, 8);
            BasePropertyTestUtils.TestProperty(table, table.T1_CTLSCVMLIM, 8);
            BasePropertyTestUtils.TestProperty(table, table.T1_CTLSAPPVER, 4);
            BasePropertyTestUtils.TestProperty(table, table.T1_RUF1, 1);
            BasePropertyTestUtils.TestProperty(table, table.T1_TDOLDEF, 40);
            BasePropertyTestUtils.TestProperty(table, table.T1_DDOLDEF, 40);
            BasePropertyTestUtils.TestProperty(table, table.T1_ARCOFFLN, 8);

            Assert.AreEqual(ApplicationType.IccEmv, table.T1_ICCSTD);

            Assert.AreEqual("28411212161234567890123456789012345678901212123456789012345603123412341234123123112345678901234512341234567812345612345678901212345678901234567890123456789012345678110123456781234567812345678123411234567890123456789012345678901234567890123456789012345678901234567890123456789012345678",
                table.CommandString);
            BasePropertyTestUtils.TestCommandString(table);

            BasePropertyTestUtils.TestProperty(table, table.T1_CTLSTACDEF, 10, true);
            BasePropertyTestUtils.TestProperty(table, table.T1_CTLSTACDEN, 10, true);
            BasePropertyTestUtils.TestProperty(table, table.T1_CTLSTACONL, 10, true);

            Assert.AreEqual("31411212161234567890123456789012345678901212123456789012345603123412341234123123112345678901234512341234567812345612345678901212345678901234567890123456789012345678110123456781234567812345678123411234567890123456789012345678901234567890123456789012345678901234567890123456789012345678123456789012345678901234567890",
                table.CommandString);
            BasePropertyTestUtils.TestCommandString(table);
        }
    }
}
