using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LegacyPinpadAid = Pinpad.Core.Tables.EmvAidTable;
using PinpadContactlessMode = Pinpad.Sdk.Model.TypeCode.ContactlessMode;
using LegacyContactlessMode = Pinpad.Core.TypeCode.ContactlessMode;
using Pinpad.Sdk.EmvTable.Mapper;
using Pinpad.Sdk.Model;
using Pinpad.Core.Utilities;
using Pinpad.Core.TypeCode;

namespace Pinpad.Sdk.Test.EmvTable.Mapper
{
    [TestClass]
    public class AidMapperTest
    {
        LegacyPinpadAid LegacyAid;
        AidEntry Aid;

        [TestInitialize]
        public void Setup()
        {
            this.LegacyAid = LegacyAidInitializer();
            this.Aid = AidInitializer();   
        }

        public LegacyPinpadAid LegacyAidInitializer()
        {
            LegacyPinpadAid aid = new LegacyPinpadAid();

            // Application stuff
            aid.TAB_ACQ.Value = 8;
            aid.TAB_RECIDX.Value = 12;
            aid.T1_AID.Value = new HexadecimalData("A0000000041010");
            aid.T1_APPTYPE.Value = 2;
            aid.T1_DEFLABEL.Value = "CREDITO".PadRight(16, ' ');

            // Merchant stuff
            aid.T1_MERCHID.Value = 98752.ToString("D15");
            aid.T1_MCC.Value = 345.ToString("D4");

            // Terminal stuff
            aid.T1_TRMID.Value = 673454.ToString("D8");
            aid.T1_TRMTYP.Value = 22;
            aid.T1_TRMCNTRY.Value = 076;
            aid.T1_TRMCURR.Value = 986;
            aid.T1_TRMCRREXP.Value = 2;
            aid.T1_TRMCAPAB.Value = new HexadecimalData("E0F0E8");
            aid.T1_ADDTRMCP.Value = new HexadecimalData("F000F0A001");

            // Termianl action codes
            aid.T1_TACDEF.Value = new HexadecimalData("F850ACF800");
            aid.T1_TACDEN.Value = new HexadecimalData("0400000000");
            aid.T1_TACONL.Value = new HexadecimalData("FC50ACA000");

            // Contactless stuff
            aid.T1_CTLSZEROAM.Value = false;
            aid.T1_CTLSMODE.Value = ContactlessMode.MasterCardPayPassMChip;
            aid.T1_CTLSTRNLIM.Value = new HexadecimalData("00000000");
            aid.T1_CTLSFLRLIM.Value = new HexadecimalData("00000000");
            aid.T1_CTLSCVMLIM.Value = new HexadecimalData("00000000");
            aid.T1_CTLSAPPVER.Value = new HexadecimalData("0000");

            // Transaction settings
            aid.T1_FLRLIMIT.Value = new HexadecimalData("00");
            aid.T1_TDOLDEF.Value = new HexadecimalData("0F9F02065F2A029A039C0195059F3704");
            aid.T1_DDOLDEF.Value = new HexadecimalData("039F3704");

            // Application version
            aid.T1_APPVER1.Value = new HexadecimalData("0000");
            aid.T1_APPVER2.Value = new HexadecimalData("0000");
            aid.T1_APPVER3.Value = new HexadecimalData("0000");

            return aid;
        }

        public AidEntry AidInitializer()
        {
            AidEntry aid = new AidEntry();

            // Filling AID mandatory data!
            aid.AcquirerNumber = 8;
            aid.AidIndex = "12";
            aid.ApplicationId = "A0000000041010";
            aid.ApplicationType = "2";
            aid.ApplicationName = "CREDITO";

            aid.TerminalCapabilities = "000EDF";
            aid.AdditionalTerminalCapabilities = "8368490500";

            aid.TerminalActionCodeDefault = "12345678";
            aid.TerminalActionCodeDenial = "12345678";
            aid.TerminalActionCodeOnline = "12345678";

            aid.ContactlessMode = PinpadContactlessMode.MasterChip;
            aid.ContactlessTransactionLimit = "999999";
            aid.ContactlessFloorLimit = "000000";
            aid.ContactlessCvmLimit = "999999";
            aid.ContactlessApplicationVersion = "01";

            aid.FloorLimit = "000000";
            aid.DefaultTransactionDataObjectList = "12345678";
            aid.DefaultDynamicDataObjectList = "12345678";

            return aid;
        }

        [TestMethod]
        public void AidMapper_mapping_FromLegacy_should_not_return_null()
        {
            AidEntry mappedAid = AidMapper.MapToAidEntry(this.LegacyAid);
            Assert.IsNotNull(mappedAid);
        }

        [TestMethod]
        public void AidMapper_mapping_FromLegacy_ApplicationData_should_match()
        {
            AidEntry mappedAid = AidMapper.MapToAidEntry(this.LegacyAid);
            
            Assert.AreEqual(mappedAid.AcquirerNumber, this.LegacyAid.TAB_ACQ.Value);
            Assert.AreEqual(mappedAid.AidIndex, this.LegacyAid.TAB_RECIDX.Value.ToString());
            Assert.AreEqual(mappedAid.ApplicationId, this.LegacyAid.T1_AID.Value.DataString);
            Assert.AreEqual(mappedAid.ApplicationType, this.LegacyAid.T1_APPTYPE.Value.ToString());
            Assert.AreEqual(mappedAid.ApplicationName, this.LegacyAid.T1_DEFLABEL.Value);
        }

        [TestMethod]
        public void AidMapper_mapping_FromLegacy_MerchantData_should_match()
        {
            AidEntry mappedAid = AidMapper.MapToAidEntry(this.LegacyAid);

            Assert.AreEqual(mappedAid.MerchantId.ToString("D15"), this.LegacyAid.T1_MERCHID.Value);
            Assert.AreEqual(mappedAid.MerchantCategoryCode.ToString("D4"), this.LegacyAid.T1_MCC.Value);
        }

        [TestMethod]
        public void AidMapper_mapping_FromLegacy_TerminalData_should_match()
        {
            AidEntry mappedAid = AidMapper.MapToAidEntry(this.LegacyAid);

            Assert.AreEqual(mappedAid.TerminalId.ToString("D8"), this.LegacyAid.T1_TRMID.Value);
            Assert.AreEqual(mappedAid.TerminalType, this.LegacyAid.T1_TRMTYP.Value.ToString());
            Assert.AreEqual(mappedAid.TerminalCountryCode, this.LegacyAid.T1_TRMCNTRY.Value.ToString());
            Assert.AreEqual(mappedAid.TerminalCurrencyCode, this.LegacyAid.T1_TRMCURR.Value.ToString());
            Assert.AreEqual(mappedAid.TerminalCurrencyExponent, this.LegacyAid.T1_TRMCRREXP.Value.ToString());
            Assert.AreEqual(mappedAid.TerminalCapabilities, this.LegacyAid.T1_TRMCAPAB.Value.DataString);
            Assert.AreEqual(mappedAid.AdditionalTerminalCapabilities, this.LegacyAid.T1_ADDTRMCP.Value.DataString);
        }

        [TestMethod]
        public void AidMapper_mapping_FromLegacy_ActionCodes_should_match()
        {
            AidEntry mappedAid = AidMapper.MapToAidEntry(this.LegacyAid);

            Assert.AreEqual(mappedAid.TerminalActionCodeDefault, this.LegacyAid.T1_TACDEF.Value.DataString);
            Assert.AreEqual(mappedAid.TerminalActionCodeDenial, this.LegacyAid.T1_TACDEN.Value.DataString);
            Assert.AreEqual(mappedAid.TerminalActionCodeOnline, this.LegacyAid.T1_TACONL.Value.DataString);
        }

        [TestMethod]
        public void AidMapper_mapping_FromLegacy_ContactlessData_should_match()
        {
            AidEntry mappedAid = AidMapper.MapToAidEntry(this.LegacyAid);

            Assert.AreEqual(mappedAid.ContactlessZeroAmount, this.LegacyAid.T1_CTLSZEROAM.Value);
            Assert.AreEqual(mappedAid.ContactlessMode, AidMapper.MapLegacyContactlessMode(this.LegacyAid.T1_CTLSMODE.Value));
            Assert.AreEqual(mappedAid.ContactlessTransactionLimit, this.LegacyAid.T1_CTLSTRNLIM.Value.DataString);
            Assert.AreEqual(mappedAid.ContactlessFloorLimit, this.LegacyAid.T1_CTLSFLRLIM.Value.DataString);
            Assert.AreEqual(mappedAid.ContactlessCvmLimit, this.LegacyAid.T1_CTLSCVMLIM.Value.DataString);
            Assert.AreEqual(mappedAid.ContactlessApplicationVersion, this.LegacyAid.T1_CTLSAPPVER.Value.DataString);
        }

        [TestMethod]
        public void AidMapper_mapping_FromLegacy_TransactionData_should_match()
        {
            AidEntry mappedAid = AidMapper.MapToAidEntry(this.LegacyAid);

            Assert.AreEqual(mappedAid.FloorLimit, this.LegacyAid.T1_FLRLIMIT.Value.DataString);
            Assert.AreEqual(mappedAid.TCC, AidEntry.DEFAULT_TCC);
            Assert.AreEqual(mappedAid.DefaultTransactionDataObjectList, this.LegacyAid.T1_TDOLDEF.Value.DataString);
            Assert.AreEqual(mappedAid.DefaultDynamicDataObjectList, this.LegacyAid.T1_DDOLDEF.Value.DataString);
        }

        [TestMethod]
        public void AidMapper_mapping_ToLegacy_should_not_return_null()
        {
            LegacyPinpadAid legacyAid = AidMapper.MapToLegacyAid(this.Aid);
            Assert.IsNotNull(legacyAid);
        }

        [TestMethod]
        public void AidMapper_mapping_ToLegacy_ApplicationData_should_match()
        {
            LegacyPinpadAid mappedAid = AidMapper.MapToLegacyAid(this.Aid);

            Assert.AreEqual(this.Aid.AcquirerNumber, mappedAid.TAB_ACQ.Value);
            Assert.AreEqual(this.Aid.AidIndex, mappedAid.TAB_RECIDX.Value.ToString());
            Assert.AreEqual(this.Aid.ApplicationId, mappedAid.T1_AID.Value.DataString);
            Assert.AreEqual(this.Aid.ApplicationType, mappedAid.T1_APPTYPE.Value.ToString());
            Assert.IsTrue(mappedAid.T1_DEFLABEL.Value.Contains(this.Aid.ApplicationName));
        }

        [TestMethod]
        public void AidMapper_mapping_ToLegacy_MerchantData_should_match()
        {
            LegacyPinpadAid mappedAid = AidMapper.MapToLegacyAid(this.Aid);

            Assert.AreEqual(this.Aid.MerchantId.ToString("D15"), mappedAid.T1_MERCHID.Value);
            Assert.AreEqual(this.Aid.MerchantCategoryCode.ToString("D4"), mappedAid.T1_MCC.Value);
        }

        [TestMethod]
        public void AidMapper_mapping_ToLegacy_TerminalData_should_match()
        {
            LegacyPinpadAid mappedAid = AidMapper.MapToLegacyAid(this.Aid);

            Assert.AreEqual(this.Aid.TerminalId.ToString("D8"), mappedAid.T1_TRMID.Value);
            Assert.AreEqual(Convert.ToInt32(this.Aid.TerminalType), mappedAid.T1_TRMTYP.Value);
            Assert.AreEqual(Convert.ToInt32(this.Aid.TerminalCountryCode), mappedAid.T1_TRMCNTRY.Value);
            Assert.AreEqual(Convert.ToInt32(this.Aid.TerminalCurrencyCode), mappedAid.T1_TRMCURR.Value);
            Assert.AreEqual(Convert.ToInt32(this.Aid.TerminalCurrencyExponent), mappedAid.T1_TRMCRREXP.Value);
            Assert.AreEqual(this.Aid.TerminalCapabilities, mappedAid.T1_TRMCAPAB.Value.DataString);
            Assert.AreEqual(this.Aid.AdditionalTerminalCapabilities, mappedAid.T1_ADDTRMCP.Value.DataString);
        }

        [TestMethod]
        public void AidMapper_mapping_ToLegacy_ActionCodes_should_match()
        {
            LegacyPinpadAid mappedAid = AidMapper.MapToLegacyAid(this.Aid);

            Assert.AreEqual(this.Aid.TerminalActionCodeDefault, mappedAid.T1_TACDEF.Value.DataString);
            Assert.AreEqual(this.Aid.TerminalActionCodeDenial, mappedAid.T1_TACDEN.Value.DataString);
            Assert.AreEqual(this.Aid.TerminalActionCodeOnline, mappedAid.T1_TACONL.Value.DataString);
        }

        [TestMethod]
        public void AidMapper_mapping_ToLegacy_ContactlessData_should_match()
        {
            LegacyPinpadAid mappedAid = AidMapper.MapToLegacyAid(this.Aid);

            Assert.AreEqual(this.Aid.ContactlessZeroAmount, mappedAid.T1_CTLSZEROAM.Value);
            Assert.AreEqual(this.Aid.ContactlessMode, AidMapper.MapLegacyContactlessMode(mappedAid.T1_CTLSMODE.Value));
            Assert.AreEqual(this.Aid.ContactlessTransactionLimit, mappedAid.T1_CTLSTRNLIM.Value.DataString);
            Assert.AreEqual(this.Aid.ContactlessFloorLimit, mappedAid.T1_CTLSFLRLIM.Value.DataString);
            Assert.AreEqual(this.Aid.ContactlessCvmLimit, mappedAid.T1_CTLSCVMLIM.Value.DataString);
            Assert.AreEqual(this.Aid.ContactlessApplicationVersion, mappedAid.T1_CTLSAPPVER.Value.DataString);
        }

        [TestMethod]
        public void AidMapper_mapping_ToLegacy_TransactionData_should_match()
        {
            LegacyPinpadAid mappedAid = AidMapper.MapToLegacyAid(this.Aid);

            Assert.AreEqual(this.Aid.FloorLimit, mappedAid.T1_FLRLIMIT.Value.DataString);
            Assert.AreEqual(this.Aid.TCC, AidEntry.DEFAULT_TCC);
            Assert.AreEqual(this.Aid.DefaultTransactionDataObjectList, mappedAid.T1_TDOLDEF.Value.DataString);
            Assert.AreEqual(this.Aid.DefaultDynamicDataObjectList, mappedAid.T1_DDOLDEF.Value.DataString);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AidMapper_ToLegacy_should_throw_exception_if_zero_AcquirerNumber()
        {
            this.Aid.AcquirerNumber = 0;
            LegacyPinpadAid legacyAid = AidMapper.MapToLegacyAid(this.Aid);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AidMapper_ToLegacy_should_throw_exception_if_negative_AcquirerNumber()
        {
            this.Aid.AcquirerNumber = -1;
            LegacyPinpadAid legacyAid = AidMapper.MapToLegacyAid(this.Aid);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AidMapper_ToLegacy_should_throw_exception_if_empty_AidIndex()
        {
            this.Aid.AidIndex = string.Empty;
            LegacyPinpadAid legacyAid = AidMapper.MapToLegacyAid(this.Aid);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AidMapper_ToLegacy_should_throw_exception_if_null_AidIndex()
        {
            this.Aid.AidIndex = null;
            LegacyPinpadAid legacyAid = AidMapper.MapToLegacyAid(this.Aid);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AidMapper_ToLegacy_should_throw_exception_if_empty_ApplicationId()
        {
            this.Aid.ApplicationId = string.Empty;
            LegacyPinpadAid legacyAid = AidMapper.MapToLegacyAid(this.Aid);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AidMapper_ToLegacy_should_throw_exception_if_null_ApplicationId()
        {
            this.Aid.ApplicationId = null;
            LegacyPinpadAid legacyAid = AidMapper.MapToLegacyAid(this.Aid);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AidMapper_ToLegacy_should_throw_exception_if_empty_ApplicationName()
        {
            this.Aid.ApplicationName = string.Empty;
            LegacyPinpadAid legacyAid = AidMapper.MapToLegacyAid(this.Aid);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AidMapper_ToLegacy_should_throw_exception_if_null_ApplicationName()
        {
            this.Aid.ApplicationName = null;
            LegacyPinpadAid legacyAid = AidMapper.MapToLegacyAid(this.Aid);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AidMapper_ToLegacy_should_throw_exception_if_empty_TerminalCapabilities()
        {
            this.Aid.TerminalCapabilities = string.Empty;
            LegacyPinpadAid legacyAid = AidMapper.MapToLegacyAid(this.Aid);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AidMapper_ToLegacy_should_throw_exception_if_null_TerminalCapabilities()
        {
            this.Aid.TerminalCapabilities = null;
            LegacyPinpadAid legacyAid = AidMapper.MapToLegacyAid(this.Aid);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AidMapper_ToLegacy_should_throw_exception_if_invalid_TerminalCapabilities_length()
        {
            this.Aid.TerminalCapabilities = "1234567";
            LegacyPinpadAid legacyAid = AidMapper.MapToLegacyAid(this.Aid);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AidMapper_ToLegacy_should_throw_exception_if_empty_AdditionalTerminalCapabilities()
        {
            this.Aid.AdditionalTerminalCapabilities = string.Empty;
            LegacyPinpadAid legacyAid = AidMapper.MapToLegacyAid(this.Aid);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AidMapper_ToLegacy_should_throw_exception_if_null_AdditionalTerminalCapabilities()
        {
            this.Aid.AdditionalTerminalCapabilities = null;
            LegacyPinpadAid legacyAid = AidMapper.MapToLegacyAid(this.Aid);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AidMapper_ToLegacy_should_throw_exception_if_invalid_AdditionalTerminalCapabilities_length()
        {
            this.Aid.AdditionalTerminalCapabilities = "1234567";
            LegacyPinpadAid legacyAid = AidMapper.MapToLegacyAid(this.Aid);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AidMapper_ToLegacy_should_throw_exception_if_empty_TerminalActionCodeDefault()
        {
            this.Aid.TerminalActionCodeDefault = string.Empty;
            LegacyPinpadAid legacyAid = AidMapper.MapToLegacyAid(this.Aid);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AidMapper_ToLegacy_should_throw_exception_if_null_TerminalActionCodeDefault()
        {
            this.Aid.TerminalActionCodeDefault = null;
            LegacyPinpadAid legacyAid = AidMapper.MapToLegacyAid(this.Aid);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AidMapper_ToLegacy_should_throw_exception_if_empty_TerminalActionCodeDenial()
        {
            this.Aid.TerminalActionCodeDenial = string.Empty;
            LegacyPinpadAid legacyAid = AidMapper.MapToLegacyAid(this.Aid);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AidMapper_ToLegacy_should_throw_exception_if_null_TerminalActionCodeDenial()
        {
            this.Aid.TerminalActionCodeDenial = null;
            LegacyPinpadAid legacyAid = AidMapper.MapToLegacyAid(this.Aid);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AidMapper_ToLegacy_should_throw_exception_if_empty_TerminalActionCodeOnline()
        {
            this.Aid.TerminalActionCodeOnline = string.Empty;
            LegacyPinpadAid legacyAid = AidMapper.MapToLegacyAid(this.Aid);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AidMapper_ToLegacy_should_throw_exception_if_null_TerminalActionCodeOnline()
        {
            this.Aid.TerminalActionCodeOnline = null;
            LegacyPinpadAid legacyAid = AidMapper.MapToLegacyAid(this.Aid);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AidMapper_ToLegacy_should_throw_exception_if_Undefined_ContactlessMode()
        {
            this.Aid.ContactlessMode = PinpadContactlessMode.Undefined;
            LegacyPinpadAid legacyAid = AidMapper.MapToLegacyAid(this.Aid);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AidMapper_ToLegacy_should_throw_exception_if_invalid_ContactlessMode()
        {
            this.Aid.ContactlessMode = (PinpadContactlessMode)33;
            LegacyPinpadAid legacyAid = AidMapper.MapToLegacyAid(this.Aid);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AidMapper_ToLegacy_should_throw_exception_if_empty_ContactlessTransactionLimit()
        {
            this.Aid.ContactlessTransactionLimit = string.Empty;
            LegacyPinpadAid legacyAid = AidMapper.MapToLegacyAid(this.Aid);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AidMapper_ToLegacy_should_throw_exception_if_null_ContactlessTransactionLimit()
        {
            this.Aid.ContactlessTransactionLimit = null;
            LegacyPinpadAid legacyAid = AidMapper.MapToLegacyAid(this.Aid);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AidMapper_ToLegacy_should_throw_exception_if_empty_ContactlessFloorLimit()
        {
            this.Aid.ContactlessFloorLimit = string.Empty;
            LegacyPinpadAid legacyAid = AidMapper.MapToLegacyAid(this.Aid);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AidMapper_ToLegacy_should_throw_exception_if_null_ContactlessFloorLimit()
        {
            this.Aid.ContactlessFloorLimit = null;
            LegacyPinpadAid legacyAid = AidMapper.MapToLegacyAid(this.Aid);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AidMapper_ToLegacy_should_throw_exception_if_empty_ContactlessCvmLimit()
        {
            this.Aid.ContactlessCvmLimit = string.Empty;
            LegacyPinpadAid legacyAid = AidMapper.MapToLegacyAid(this.Aid);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AidMapper_ToLegacy_should_throw_exception_if_null_ContactlessCvmLimit()
        {
            this.Aid.ContactlessCvmLimit = null;
            LegacyPinpadAid legacyAid = AidMapper.MapToLegacyAid(this.Aid);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AidMapper_ToLegacy_should_throw_exception_if_empty_ContactlessApplicationVersion()
        {
            this.Aid.ContactlessApplicationVersion = string.Empty;
            LegacyPinpadAid legacyAid = AidMapper.MapToLegacyAid(this.Aid);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AidMapper_ToLegacy_should_throw_exception_if_null_ContactlessApplicationVersion()
        {
            this.Aid.ContactlessApplicationVersion = null;
            LegacyPinpadAid legacyAid = AidMapper.MapToLegacyAid(this.Aid);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AidMapper_ToLegacy_should_throw_exception_if_empty_FloorLimit()
        {
            this.Aid.FloorLimit = string.Empty;
            LegacyPinpadAid legacyAid = AidMapper.MapToLegacyAid(this.Aid);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AidMapper_ToLegacy_should_throw_exception_if_null_FloorLimit()
        {
            this.Aid.FloorLimit = null;
            LegacyPinpadAid legacyAid = AidMapper.MapToLegacyAid(this.Aid);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AidMapper_ToLegacy_should_throw_exception_if_empty_DefaultTransactionDataObjectList()
        {
            this.Aid.DefaultTransactionDataObjectList = string.Empty;
            LegacyPinpadAid legacyAid = AidMapper.MapToLegacyAid(this.Aid);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AidMapper_ToLegacy_should_throw_exception_if_null_DefaultTransactionDataObjectList()
        {
            this.Aid.DefaultTransactionDataObjectList = null;
            LegacyPinpadAid legacyAid = AidMapper.MapToLegacyAid(this.Aid);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AidMapper_ToLegacy_should_throw_exception_if_empty_DefaultDynamicDataObjectList()
        {
            this.Aid.DefaultDynamicDataObjectList = string.Empty;
            LegacyPinpadAid legacyAid = AidMapper.MapToLegacyAid(this.Aid);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AidMapper_ToLegacy_should_throw_exception_if_null_DefaultDynamicDataObjectList()
        {
            this.Aid.DefaultDynamicDataObjectList = null;
            LegacyPinpadAid legacyAid = AidMapper.MapToLegacyAid(this.Aid);
        }
    }
}
