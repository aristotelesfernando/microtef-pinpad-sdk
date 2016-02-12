using Pinpad.Sdk.Model;
using Pinpad.Sdk.Model.TypeCode;
using System;
using LegacyPinpadAid = Pinpad.Core.Tables.EmvAidTable;
using PinpadContactlessMode = Pinpad.Sdk.Model.TypeCode.ContactlessMode;
using LegacyContactlessMode = Pinpad.Core.TypeCode.ContactlessMode;
using Pinpad.Core.Utilities;

namespace Pinpad.Sdk.EmvTable.Mapper
{
    /// <summary>
    /// Takes a <see cref="Pinpad.Sdk.EmvTable.Entry.AidEntry">Pinpd.Sdk AidEntry</see> into a legacy representation of Aid and vice versa, based on users need.
    /// </summary>
    internal class AidMapper
    {
        /// <summary>
        /// Translates a <see cref="Pinpad.Sdk.EmvTable.Entry.AidEntry">Pinpd.Sdk AidEntry</see> into legacy representation of Aid.
        /// </summary>
        /// <param name="aid">Aid entry.</param>
        /// <returns>Legacy representation of Aid.</returns>
        internal static LegacyPinpadAid MapToLegacyAid(AidEntry aid)
        {
            AidMapper.Validate(aid);

            LegacyPinpadAid mappedAid = new LegacyPinpadAid();

            // Mapping application stuff
            mappedAid.TAB_ACQ.Value = aid.AcquirerNumber;
            mappedAid.TAB_RECIDX.Value = Convert.ToInt32(aid.AidIndex);
            mappedAid.T1_AID.Value = new HexadecimalData(aid.ApplicationId);
            mappedAid.T1_APPTYPE.Value = Convert.ToInt32(aid.ApplicationType);
            // ApplicationName should have 16 characters.
            mappedAid.T1_DEFLABEL.Value = aid.ApplicationName.PadRight(16, ' ');
          
            // Mapping merchant stuff
            mappedAid.T1_MERCHID.Value = aid.MerchantId.ToString("D15");
            mappedAid.T1_MCC.Value = aid.MerchantCategoryCode.ToString("D4");

            // Mapping terminal stuff
            mappedAid.T1_TRMID.Value = aid.TerminalId.ToString("D8");
            mappedAid.T1_TRMTYP.Value = Convert.ToInt32(aid.TerminalType);
            mappedAid.T1_TRMCNTRY.Value = Convert.ToInt32(aid.TerminalCountryCode);
            mappedAid.T1_TRMCURR.Value = Convert.ToInt32(aid.TerminalCurrencyCode);
            mappedAid.T1_TRMCRREXP.Value = Convert.ToInt32(aid.TerminalCurrencyExponent);
            mappedAid.T1_TRMCAPAB.Value = new HexadecimalData(aid.TerminalCapabilities);
            mappedAid.T1_ADDTRMCP.Value = new HexadecimalData(aid.AdditionalTerminalCapabilities);

            // Mapping terminal action codes
            mappedAid.T1_TACDEF.Value = new HexadecimalData(aid.TerminalActionCodeDefault);
            mappedAid.T1_TACDEN.Value = new HexadecimalData(aid.TerminalActionCodeDenial);
            mappedAid.T1_TACONL.Value = new HexadecimalData(aid.TerminalActionCodeOnline); 

            // Mapping contactless stuff
            mappedAid.T1_CTLSZEROAM.Value = aid.ContactlessZeroAmount;
            mappedAid.T1_CTLSMODE.Value = AidMapper.MapPinpadContactlessMode(aid.ContactlessMode);
            mappedAid.T1_CTLSTRNLIM.Value = new HexadecimalData(aid.ContactlessTransactionLimit);
            mappedAid.T1_CTLSFLRLIM.Value = new HexadecimalData(aid.ContactlessFloorLimit);
            mappedAid.T1_CTLSCVMLIM.Value = new HexadecimalData(aid.ContactlessCvmLimit);
            mappedAid.T1_CTLSAPPVER.Value = new HexadecimalData(aid.ContactlessApplicationVersion);
              
            // Mapping transaction settings
            mappedAid.T1_FLRLIMIT.Value = new HexadecimalData(aid.FloorLimit);
            mappedAid.T1_TCC.Value = aid.TCC;
            mappedAid.T1_TDOLDEF.Value = new HexadecimalData(aid.DefaultTransactionDataObjectList);
            mappedAid.T1_DDOLDEF.Value = new HexadecimalData(aid.DefaultDynamicDataObjectList);
            
            // Hardcoded stuff
            mappedAid.T1_APPVER1.Value = new HexadecimalData("0000");
            mappedAid.T1_APPVER2.Value = new HexadecimalData("0000");
            mappedAid.T1_APPVER3.Value = new HexadecimalData("0000");

            return mappedAid;
        }

        /// <summary>
        /// Translates a legacy instance of Aid to a <see cref="Pinpad.Sdk.EmvTable.Entry.AidEntry">Pinpd.Sdk AidEntry</see>.
        /// </summary>
        /// <param name="aid">Legacy representation of Aid.</param>
        /// <returns>Actual Aid.</returns>
        internal static AidEntry MapToAidEntry(LegacyPinpadAid aid)
        {
            AidEntry mappedAid = new AidEntry();

            // Mapping application stuff
            mappedAid.AcquirerNumber = aid.TAB_ACQ.Value.Value;
            mappedAid.AidIndex = aid.TAB_RECIDX.Value.Value.ToString();
            mappedAid.ApplicationId = HexadecimalController.FromBytes(aid.T1_AID.Value.Data);
            mappedAid.ApplicationType = aid.T1_APPTYPE.Value.Value.ToString();
            mappedAid.ApplicationName = aid.T1_DEFLABEL.Value;

            // Mapping merchant stuff
            mappedAid.MerchantId = Convert.ToInt32(aid.T1_MERCHID.Value);
            mappedAid.MerchantCategoryCode = Convert.ToInt32(aid.T1_MCC.Value);

            // Mapping terminal stuff
            mappedAid.TerminalId = Convert.ToInt32(aid.T1_TRMID.Value);
            mappedAid.TerminalType = aid.T1_TRMTYP.Value.Value.ToString();
            mappedAid.TerminalCountryCode = aid.T1_TRMCNTRY.Value.Value.ToString();
            mappedAid.TerminalCurrencyCode = aid.T1_TRMCURR.Value.Value.ToString();
            mappedAid.TerminalCurrencyExponent = aid.T1_TRMCRREXP.Value.Value.ToString();
            mappedAid.TerminalCapabilities = HexadecimalController.FromBytes(aid.T1_TRMCAPAB.Value.Data);
            mappedAid.AdditionalTerminalCapabilities = HexadecimalController.FromBytes(aid.T1_ADDTRMCP.Value.Data);

            // Mapping terminal action codes
            mappedAid.TerminalActionCodeDefault = HexadecimalController.FromBytes(aid.T1_TACDEF.Value.Data);
            mappedAid.TerminalActionCodeOnline = HexadecimalController.FromBytes(aid.T1_TACONL.Value.Data);
            mappedAid.TerminalActionCodeDenial = HexadecimalController.FromBytes(aid.T1_TACDEN.Value.Data);

            // Mapping contactless stuff
            mappedAid.ContactlessZeroAmount = aid.T1_CTLSZEROAM.Value.Value;
            mappedAid.ContactlessMode = MapLegacyContactlessMode(aid.T1_CTLSMODE.Value);
            mappedAid.ContactlessTransactionLimit = HexadecimalController.FromBytes(aid.T1_CTLSTRNLIM.Value.Data);
            mappedAid.ContactlessFloorLimit = HexadecimalController.FromBytes(aid.T1_CTLSFLRLIM.Value.Data);
            mappedAid.ContactlessCvmLimit = HexadecimalController.FromBytes(aid.T1_CTLSCVMLIM.Value.Data);
            mappedAid.ContactlessApplicationVersion = HexadecimalController.FromBytes(aid.T1_CTLSAPPVER.Value.Data);

            // Mapping transaction settings
            mappedAid.FloorLimit = HexadecimalController.FromBytes(aid.T1_FLRLIMIT.Value.Data);
            mappedAid.DefaultTransactionDataObjectList = HexadecimalController.FromBytes(aid.T1_TDOLDEF.Value.Data);
            mappedAid.DefaultDynamicDataObjectList = HexadecimalController.FromBytes(aid.T1_DDOLDEF.Value.Data);

            return mappedAid;
        }

        /// <summary>
        /// Translates PinpadContactlessMode into LegacyContactlessMode (Pinpad.Core ContactlessMode).
        /// </summary>
        /// <param name="mode">String representing the enum <see cref="Pinpad.Core.Enums.ContactlessMode">ContactlessMode</see>.</param>
        /// <returns>Enumerator value corresponding to the string received as parameter.</returns>
        internal static LegacyContactlessMode MapPinpadContactlessMode(PinpadContactlessMode mode)
        {
            switch (mode)
            {
                case PinpadContactlessMode.Unsupported: return LegacyContactlessMode.Unsupported;
                case PinpadContactlessMode.VisaMagneticStripe: return LegacyContactlessMode.VisaMSD;
                case PinpadContactlessMode.VisaChip: return LegacyContactlessMode.VisaQVSDC;
                case PinpadContactlessMode.MasterMagneticStripe: return LegacyContactlessMode.MasterCardPayPassMagneticStripe;
                case PinpadContactlessMode.MasterChip: return LegacyContactlessMode.MasterCardPayPassMChip;
                case PinpadContactlessMode.AmexMagneticStripe: return LegacyContactlessMode.AmexExpresspayMagneticStripe;
                case PinpadContactlessMode.AmexChip: return LegacyContactlessMode.AmexExpresspayEMV;
                default: return LegacyContactlessMode.Undefined;
            }
        }

        /// <summary>
        /// Translates LegacyContactlessMode (Pinpad.Code ContactlessMode) into PinpadContactlessMode.
        /// </summary>
        /// <param name="mode">Enumerator value.</param>
        /// <returns>String corresponding to the enum received as parameter.</returns>
        internal static PinpadContactlessMode MapLegacyContactlessMode(LegacyContactlessMode mode)
        {
            switch (mode)
            {
                case LegacyContactlessMode.Unsupported: return PinpadContactlessMode.Unsupported;
                case LegacyContactlessMode.VisaMSD: return PinpadContactlessMode.VisaMagneticStripe;
                case LegacyContactlessMode.VisaQVSDC: return PinpadContactlessMode.VisaChip;
                case LegacyContactlessMode.MasterCardPayPassMagneticStripe: return PinpadContactlessMode.MasterMagneticStripe;
                case LegacyContactlessMode.MasterCardPayPassMChip: return PinpadContactlessMode.MasterChip;
                case LegacyContactlessMode.AmexExpresspayMagneticStripe: return PinpadContactlessMode.AmexMagneticStripe;
                case LegacyContactlessMode.AmexExpresspayEMV: return PinpadContactlessMode.AmexChip;
                default: return PinpadContactlessMode.Undefined;
            }
        }

        /// <summary>
        /// Validates mandatory AID information.
        /// </summary>
        /// <param name="aid">AID information.</param>
        private static void Validate(AidEntry aid)
        {
            if (aid.AcquirerNumber <= 0)
            {
                throw new ArgumentException("Invalid AcquirerNumber.");
            }
            if (string.IsNullOrEmpty(aid.AidIndex) ==  true)
            {
                throw new ArgumentException("Invalid (null or empty) AidIndex.");
            }
            if (string.IsNullOrEmpty(aid.ApplicationId) == true)
            {
                throw new ArgumentException("Invalid (null or empty) ApplicationId (AID).");
            }
            if (string.IsNullOrEmpty(aid.ApplicationName) == true)
            {
                throw new ArgumentException("Invalid (null or empty) ApplicationName.");
            }
            if (string.IsNullOrEmpty(aid.TerminalCapabilities) == true)
            {
                throw new ArgumentException("Invalid (null or empty) TerminalCapabilities.");
            }
            if (aid.TerminalCapabilities.Length != AidEntry.TERMINAL_CAPABILITIES_LENGTH)
            {
                throw new ArgumentException("TerminalCapabilities must have 6 characters.");
            }
            if (string.IsNullOrEmpty(aid.AdditionalTerminalCapabilities) == true)
            {
                throw new ArgumentException("Invalid (null or empty) AdditionalTerminalCapabilities");
            }
            if (aid.AdditionalTerminalCapabilities.Length != AidEntry.ADDITIONAL_TERMINAL_CAPABILITIES_LENGTH)
            {
                throw new ArgumentException("AdditionalTerminalCapabilities must have 8 characters.");
            }
            if (string.IsNullOrEmpty(aid.TerminalActionCodeDefault) == true)
            {
                throw new ArgumentException("Invalid (null or empty) TerminalActionCodeDefault.");
            }
            if (string.IsNullOrEmpty(aid.TerminalActionCodeDenial) == true)
            {
                throw new ArgumentException("Invalid (null or empty) TerminalActionCodeDenial.");
            }
            if (string.IsNullOrEmpty(aid.TerminalActionCodeOnline) == true)
            {
                throw new ArgumentException("Invalid (null or empty) TerminalActionCodeOnline.");
            }
            if (aid.ContactlessMode == PinpadContactlessMode.Undefined)
            {
                throw new ArgumentException(string.Format("Unnable to process <{0}> ContactlessMode.", aid.ContactlessMode));
            }
            if (Enum.IsDefined(typeof(PinpadContactlessMode), aid.ContactlessMode) == false)
            {
                throw new ArgumentException("ContactlessMode has invalid value.");
            }
            if (string.IsNullOrEmpty(aid.ContactlessTransactionLimit) == true)
            {
                throw new ArgumentException("Invalid (null or empty) ContactlessTransactionLimit.");
            }
            if (string.IsNullOrEmpty(aid.ContactlessFloorLimit) == true)
            {
                throw new ArgumentException("Invalid (null or empty) ContactlessFloorLimit.");
            }
            if (string.IsNullOrEmpty(aid.ContactlessCvmLimit) == true)
            {
                throw new ArgumentException("Invalid (null or empty) ContactlessCvmLimit.");
            }
            if (string.IsNullOrEmpty(aid.ContactlessApplicationVersion) == true)
            {
                throw new ArgumentException("Invalid (null or empty) ContactlessApplicationVersion.");
            }
            if (string.IsNullOrEmpty(aid.FloorLimit) == true)
            {
                throw new ArgumentException("Invalid (null or empty) FloorLimit.");
            }
            if (string.IsNullOrEmpty(aid.DefaultTransactionDataObjectList) == true)
            {
                throw new ArgumentException("Invalid (null or empty) DefaultTransactionDataObjectList (tDOL).");
            }
            if (string.IsNullOrEmpty(aid.DefaultDynamicDataObjectList) == true)
            {
                throw new ArgumentException("Invalid (null or empty) DefaultDynamicDataObjectList (dDOL).");
            }
        }
    }
}