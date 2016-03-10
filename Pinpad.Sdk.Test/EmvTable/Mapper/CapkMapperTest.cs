using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using LegacyPinpadCapk = Pinpad.Core.Tables.CapkTable;
using Pinpad.Sdk.Model;
using Pinpad.Sdk.EmvTable.Mapper;
using Pinpad.Core.Utilities;
using Pinpad.Core;

namespace Pinpad.Sdk.Test.EmvTable.Mapper
{
    [TestClass]
    public class CapkMapperTest
    {
        public LegacyPinpadCapk LegacyCapk;

        public PinpadCapk Capk;

        #region setting stuff up
        [TestInitialize]
        public void Setup()
        {
            this.LegacyCapk = LegacyCappkInitializer();
            this.Capk = CapkInitializer();
        }

        public LegacyPinpadCapk LegacyCappkInitializer()
        {
            LegacyPinpadCapk capk = new LegacyPinpadCapk();

            capk.T2_CAPKIDX.Value = new HexadecimalData("99");
            capk.T2_CHECKSUM.Value = new HexadecimalData("4ABFFD6B1C51212D05552E431C5B17007D2F5E6D");
            capk.T2_CHKSTAT.Value = true;
            capk.T2_EXP.Value = new HexadecimalData("030000");
            capk.T2_MOD.Value = new HexadecimalData("AB79FCC9520896967E776E64444E5DCDD6E13611874F3985722520425295EEA4BD0C2781DE7F31CD3D041F565F747306EED62954B17EDABA3A6C5B85A1DE1BEB9A34141AF38FCF8279C9DEA0D5A6710D08DB4124F041945587E20359BAB47B7575AD94262D4B25F264AF33DEDCF28E09615E937DE32EDC03C54445FE7E38277700000000000000000000000000000000");
            capk.T2_RID.Value = new HexadecimalData("A000000003");
            capk.TAB_RECIDX.Value = 99;
            capk.TAB_ACQ.Value = (int)StoneIndexCode.Generic;

            return capk;
        }

        public PinpadCapk CapkInitializer()
        {
            PinpadCapk capk = new PinpadCapk();

            capk.AcquirerNumber = (int)StoneIndexCode.Generic;
            capk.CapkIndex = "99";
            capk.CapkIndexInTable = "99";
            capk.CheckSum = "4ABFFD6B1C51212D05552E431C5B17007D2F5E6D";
            capk.CheckSumStatus = true;
            capk.DataSetVersion = 1;
            capk.Exponent = "030000";
            capk.Modulus = "AB79FCC9520896967E776E64444E5DCDD6E13611874F3985722520425295EEA4BD0C2781DE7F31CD3D041F565F747306EED62954B17EDABA3A6C5B85A1DE1BEB9A34141AF38FCF8279C9DEA0D5A6710D08DB4124F041945587E20359BAB47B7575AD94262D4B25F264AF33DEDCF28E09615E937DE32EDC03C54445FE7E38277700000000000000000000000000000000";
            capk.RecordedIdentification = "A000000003";

            return capk;
        }
        #endregion

        [TestMethod]
        public void CapkMapper_MapFromLegacyCapk_should_not_return_null()
        {
            PinpadCapk mappedCapk = CapkMapper.MapToPinpadCapk(this.LegacyCapk);
            Assert.IsNotNull(mappedCapk);
        }

        [TestMethod]
        public void CapkMapper_MapFromLegacyCapk_AcquirerNumber_should_match()
        {
            PinpadCapk mappedCapk = CapkMapper.MapToPinpadCapk(this.LegacyCapk);
            Assert.AreEqual(mappedCapk.AcquirerNumber, this.LegacyCapk.TAB_ACQ.Value);
        }

        [TestMethod]
        public void CapkMapper_MapFromLegacyCapk_CapkIndex_should_match()
        {
            PinpadCapk mappedCapk = CapkMapper.MapToPinpadCapk(this.LegacyCapk);
            Assert.AreEqual(mappedCapk.CapkIndex, this.LegacyCapk.T2_CAPKIDX.Value.DataString);
        }

        [TestMethod]
        public void CapkMapper_MapFromLegacyCapk_CapkIndexInTable_should_match()
        {
            PinpadCapk mappedCapk = CapkMapper.MapToPinpadCapk(this.LegacyCapk);
            Assert.AreEqual(Convert.ToInt32(mappedCapk.CapkIndexInTable), this.LegacyCapk.TAB_RECIDX.Value);
        }

        [TestMethod]
        public void CapkMapper_MapFromLegacyCapk_RecordedIdentification_should_match()
        {
            PinpadCapk mappedCapk = CapkMapper.MapToPinpadCapk(this.LegacyCapk);
            Assert.AreEqual(mappedCapk.RecordedIdentification, this.LegacyCapk.T2_RID.Value.DataString);
        }

        [TestMethod]
        public void CapkMapper_MapFromLegacyCapk_Exponent_should_match()
        {
            PinpadCapk mappedCapk = CapkMapper.MapToPinpadCapk(this.LegacyCapk);
            Assert.AreEqual(mappedCapk.Exponent, this.LegacyCapk.T2_EXP.Value.DataString);
        }

        [TestMethod]
        public void CapkMapper_MapFromLegacyCapk_Modulus_should_match()
        {
            PinpadCapk mappedCapk = CapkMapper.MapToPinpadCapk(this.LegacyCapk);
            Assert.AreEqual(mappedCapk.Modulus, this.LegacyCapk.T2_MOD.Value.DataString);
        }

        [TestMethod]
        public void CapkMapper_MapFromLegacyCapk_CheckSum_should_match()
        {
            PinpadCapk mappedCapk = CapkMapper.MapToPinpadCapk(this.LegacyCapk);
            Assert.AreEqual(mappedCapk.CheckSum, this.LegacyCapk.T2_CHECKSUM.Value.DataString);
            Assert.IsTrue(mappedCapk.CheckSumStatus);
        }

        [TestMethod]
        public void CapkMapper_MapToLegacyCapk_should_not_return_null()
        {
            LegacyPinpadCapk mappedCapk = CapkMapper.MapToLegacyCapk(this.Capk);
            Assert.IsNotNull(mappedCapk);
        }

        [TestMethod]
        public void CapkMapper_MapToLegacyCapk_AcquirerNumber_should_match()
        {
            LegacyPinpadCapk mappedCapk = CapkMapper.MapToLegacyCapk(this.Capk);
            Assert.AreEqual(mappedCapk.TAB_ACQ.Value, this.Capk.AcquirerNumber);
        }

        [TestMethod]
        public void CapkMapper_MapToLegacyCapk_CapkIndex_should_match()
        {
            LegacyPinpadCapk mappedCapk = CapkMapper.MapToLegacyCapk(this.Capk);
            Assert.AreEqual(mappedCapk.T2_CAPKIDX.Value.DataString, this.Capk.CapkIndex);
        }

        [TestMethod]
        public void CapkMapper_MapToLegacyCapk_CapkIndexInTable_should_match()
        {
            LegacyPinpadCapk mappedCapk = CapkMapper.MapToLegacyCapk(this.Capk);
            Assert.AreEqual(mappedCapk.TAB_RECIDX.Value, Convert.ToInt32(this.Capk.CapkIndexInTable));
        }

        [TestMethod]
        public void CapkMapper_MapToLegacyCapk_CheckSum_should_match()
        {
            LegacyPinpadCapk mappedCapk = CapkMapper.MapToLegacyCapk(this.Capk);
            Assert.AreEqual(mappedCapk.T2_CHECKSUM.Value.DataString, this.Capk.CheckSum);
        }

        [TestMethod]
        public void CapkMapper_MapToLegacyCapk_CheckSumStatus_should_match()
        {
            LegacyPinpadCapk mappedCapk = CapkMapper.MapToLegacyCapk(this.Capk);
            Assert.AreEqual(mappedCapk.T2_CHKSTAT.Value, this.Capk.CheckSumStatus);
        }

        [TestMethod]
        public void CapkMapper_MapToLegacyCapk_Exponent_should_match()
        {
            LegacyPinpadCapk mappedCapk = CapkMapper.MapToLegacyCapk(this.Capk);
            Assert.AreEqual(mappedCapk.T2_EXP.Value.DataString, this.Capk.Exponent);
        }

        [TestMethod]
        public void CapkMapper_MapToLegacyCapk_Modulus_should_match()
        {
            LegacyPinpadCapk mappedCapk = CapkMapper.MapToLegacyCapk(this.Capk);
            Assert.AreEqual(mappedCapk.T2_MOD.Value.DataString, this.Capk.Modulus);
        }

        [TestMethod]
        public void CapkMapper_MapToLegacyCapk_RecordedIdentification_should_match()
        {
            LegacyPinpadCapk mappedCapk = CapkMapper.MapToLegacyCapk(this.Capk);
            Assert.AreEqual(mappedCapk.T2_RID.Value.DataString, this.Capk.RecordedIdentification);
        }
    }
}
