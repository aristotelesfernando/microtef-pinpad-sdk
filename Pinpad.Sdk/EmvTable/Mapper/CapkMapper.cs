using System;
//using LegacyPinpadCapk = Pinpad.Core.Tables.CapkTable;
using Pinpad.Sdk.Model;
using Pinpad.Core.Utilities;
using Pinpad.Core.Tables;
using System.Collections.Generic;

namespace Pinpad.Sdk.EmvTable.Mapper
{
	/// <summary>
	/// Takes a <see cref="Pinpad.Sdk.EmvTable.Entry.CapkEntry">Pinpd.Sdk CapkEntry</see> into a legacy representation of Capk and vice versa, based on users need.
	/// </summary>
	internal class CapkMapper
	{
		internal static ICollection<PinpadCapk> MapToPinpadCapkCollection (CapkTable [] capkCollection)
		{
			ICollection<PinpadCapk> capks = new List<PinpadCapk>();

			foreach (CapkTable c in capkCollection)
			{
				PinpadCapk newCapk = MapToPinpadCapk(c);
				capks.Add(newCapk);
			}

			return capks;
        }

        internal static CapkTable MapGenericToLegacyCapk (BaseTableEntry capk)
        {
            if (capk is PinpadCapk)
            {
                return MapToLegacyCapk(capk as PinpadCapk);
            }

            return null;
        }

        /// <summary>
        /// Translates a <see cref="Pinpad.Sdk.EmvTable.Entry.CapkEntry">CapkEntry</see> into a <see cref="Pinpad.Core.Controllers.Tables.CapkTable">legacy instance of CapkEntry</see>.
        /// </summary>
        /// <param name="capk"><see cref="Pinpad.Sdk.EmvTable.Entry.CapkEntry">CapkEntry</see> instance.</param>
        /// <returns>A <see cref="Pinpad.Core.Controllers.Tables.CapkTable">legacy instance of CapkEntry</see>.</returns>
        internal static CapkTable MapToLegacyCapk (PinpadCapk capk)
		{
			CapkTable mappedCapk = new CapkTable();

			mappedCapk.TAB_ACQ.Value = capk.AcquirerNumber;
			mappedCapk.TAB_RECIDX.Value = Convert.ToInt32(capk.CapkIndexInTable);
			mappedCapk.T2_RID.Value = new HexadecimalData(capk.RecordedIdentification);
			mappedCapk.T2_CAPKIDX.Value = new HexadecimalData(capk.CapkIndex);
			
			if (capk.ExponentLength.HasValue && capk.ExponentLength > 0)
			{
				mappedCapk.T2_EXP.Value = new HexadecimalData(capk.Exponent.Substring(0, capk.ExponentLength.Value * 2));
			}
			else 
			{
				mappedCapk.T2_EXP.Value = new HexadecimalData(capk.Exponent); 
			}

			if (capk.ModulusLength.HasValue && capk.ModulusLength > 0)
			{
				mappedCapk.T2_MOD.Value = new HexadecimalData(capk.Modulus.Substring(0, capk.ModulusLength.Value * 2));
			}
			else
			{
				mappedCapk.T2_MOD.Value = new HexadecimalData(capk.Modulus);
			}

			mappedCapk.T2_CHKSTAT.Value = capk.CheckSumStatus;
			if (capk.CheckSumStatus == true)
			{
				mappedCapk.T2_CHECKSUM.Value = new HexadecimalData(capk.CheckSum);
			}
			else
			{
				mappedCapk.T2_CHECKSUM.Value = new HexadecimalData("0000000000000000000000000000000000000000");
			}

			return mappedCapk;
		}

		/// <summary>
		/// Translates a <see cref="Pinpad.Core.Controllers.Tables.CapkTable">legacy instance of CapkEntry</see> into a <see cref="Pinpad.Sdk.EmvTable.Entry.CapkEntry">CapkEntry</see>.
		/// </summary>
		/// <param name="capk">A <see cref="Pinpad.Core.Controllers.Tables.CapkTable">legacy instance of CapkEntry</see>.</param>
		/// <returns>An actual <see cref="Pinpad.Sdk.EmvTable.Entry.CapkEntry">CapkEntry</see> instance.</returns>
		internal static PinpadCapk MapToPinpadCapk(CapkTable capk)
		{
			PinpadCapk mappedCapk = new PinpadCapk();

			mappedCapk.AcquirerNumber = capk.TAB_ACQ.Value.Value;
			mappedCapk.CapkIndexInTable = capk.TAB_RECIDX.Value.Value.ToString();
			mappedCapk.RecordedIdentification = HexadecimalController.FromBytes(capk.T2_RID.Value.Data);
			mappedCapk.CapkIndex = HexadecimalController.FromBytes(capk.T2_CAPKIDX.Value.Data);
			mappedCapk.Exponent = HexadecimalController.FromBytes(capk.T2_EXP.Value.Data);
			mappedCapk.Modulus = HexadecimalController.FromBytes(capk.T2_MOD.Value.Data);
			
			mappedCapk.CheckSum = HexadecimalController.FromBytes(capk.T2_CHECKSUM.Value.Data);
			if (string.IsNullOrEmpty(mappedCapk.CheckSum) == false)
			{
				mappedCapk.CheckSumStatus = true;
			}

			return mappedCapk;
		}
	}
}
