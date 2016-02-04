using PinPadSDK.Property;
using PinPadSDK.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StonePortableUtils;

namespace PinPadSDK.Controllers.Tables {
    /// <summary>
    /// EMV Application IDentifier table record
    /// </summary>
    public class EmvAidTable : BaseAidTable {
        /// <summary>
        /// Constructor
        /// </summary>
        public EmvAidTable() {
            this.T1_APPVER1 = new PinPadFixedLengthPropertyController<HexadecimalData>("T1_APPVER1", 4, false, DefaultStringFormatter.HexadecimalStringFormatter, DefaultStringParser.HexadecimalStringParser);
            this.T1_APPVER2 = new PinPadFixedLengthPropertyController<HexadecimalData>("T1_APPVER2", 4, false, DefaultStringFormatter.HexadecimalStringFormatter, DefaultStringParser.HexadecimalStringParser);
            this.T1_APPVER3 = new PinPadFixedLengthPropertyController<HexadecimalData>("T1_APPVER3", 4, false, DefaultStringFormatter.HexadecimalStringFormatter, DefaultStringParser.HexadecimalStringParser);
            this.T1_TRMCNTRY = new PinPadFixedLengthPropertyController<int?>("T1_TRMCNTRY", 3, false, DefaultStringFormatter.IntegerStringFormatter, DefaultStringParser.IntegerStringParser);
            this.T1_TRMCURR = new PinPadFixedLengthPropertyController<int?>("T1_TRMCURR", 3, false, DefaultStringFormatter.IntegerStringFormatter, DefaultStringParser.IntegerStringParser);
            this.T1_TRMCRREXP = new PinPadFixedLengthPropertyController<int?>("T1_TRMCRREXP", 1, false, DefaultStringFormatter.IntegerStringFormatter, DefaultStringParser.IntegerStringParser);
            this.T1_MERCHID = new PinPadFixedLengthPropertyController<string>("T1_MERCHID", 15, false, DefaultStringFormatter.StringStringFormatter, DefaultStringParser.StringStringParser);
            this.T1_MCC = new PinPadFixedLengthPropertyController<string>("T1_MCC", 4, false, DefaultStringFormatter.StringStringFormatter, DefaultStringParser.StringStringParser);
            this.T1_TRMID = new PinPadFixedLengthPropertyController<string>("T1_TRMID", 8, false, DefaultStringFormatter.StringStringFormatter, DefaultStringParser.StringStringParser);
            this.T1_TRMCAPAB = new PinPadFixedLengthPropertyController<HexadecimalData>("T1_TRMCAPAB", 6, false, DefaultStringFormatter.HexadecimalStringFormatter, DefaultStringParser.HexadecimalStringParser);
            this.T1_ADDTRMCP = new PinPadFixedLengthPropertyController<HexadecimalData>("T1_ADDTRMCP", 10, false, DefaultStringFormatter.HexadecimalStringFormatter, DefaultStringParser.HexadecimalStringParser);
            this.T1_TRMTYP = new PinPadFixedLengthPropertyController<int?>("T1_TRMTYP", 2, false, DefaultStringFormatter.IntegerStringFormatter, DefaultStringParser.IntegerStringParser);
            this.T1_TACDEF = new PinPadFixedLengthPropertyController<HexadecimalData>("T1_TACDEF", 10, false, DefaultStringFormatter.HexadecimalStringFormatter, DefaultStringParser.HexadecimalStringParser);
            this.T1_TACDEN = new PinPadFixedLengthPropertyController<HexadecimalData>("T1_TACDEN", 10, false, DefaultStringFormatter.HexadecimalStringFormatter, DefaultStringParser.HexadecimalStringParser);
            this.T1_TACONL = new PinPadFixedLengthPropertyController<HexadecimalData>("T1_TACONL", 10, false, DefaultStringFormatter.HexadecimalStringFormatter, DefaultStringParser.HexadecimalStringParser);
            this.T1_FLRLIMIT = new PinPadFixedLengthPropertyController<HexadecimalData>("T1_FLRLIMIT", 8, false, DefaultStringFormatter.HexadecimalStringFormatter, DefaultStringParser.HexadecimalStringParser);
            this.T1_TCC = new PinPadFixedLengthPropertyController<string>("T1_TCC", 1, false, DefaultStringFormatter.StringStringFormatter, DefaultStringParser.StringStringParser);
            this.T1_CTLSZEROAM = new SimpleProperty<bool?>("T1_CTLSZEROAM", false, DefaultStringFormatter.BooleanStringFormatter, DefaultStringParser.BooleanStringParser);
            this.T1_CTLSMODE = new PinPadFixedLengthPropertyController<ContactlessMode>("T1_CTLSMODE", 1, false, DefaultStringFormatter.EnumStringFormatter<ContactlessMode>, DefaultStringParser.EnumStringParser<ContactlessMode>);
            this.T1_CTLSTRNLIM = new PinPadFixedLengthPropertyController<HexadecimalData>("T1_CTLSTRNLIM", 8, false, DefaultStringFormatter.HexadecimalStringFormatter, DefaultStringParser.HexadecimalStringParser);
            this.T1_CTLSFLRLIM = new PinPadFixedLengthPropertyController<HexadecimalData>("T1_CTLSFLRLIM", 8, false, DefaultStringFormatter.HexadecimalStringFormatter, DefaultStringParser.HexadecimalStringParser);
            this.T1_CTLSCVMLIM = new PinPadFixedLengthPropertyController<HexadecimalData>("T1_CTLSCVMLIM", 8, false, DefaultStringFormatter.HexadecimalStringFormatter, DefaultStringParser.HexadecimalStringParser);
            this.T1_CTLSAPPVER = new PinPadFixedLengthPropertyController<HexadecimalData>("T1_CTLSAPPVER", 4, false, DefaultStringFormatter.HexadecimalStringFormatter, DefaultStringParser.HexadecimalStringParser);
            this.T1_RUF1 = new PinPadFixedLengthPropertyController<int?>("T1_RUF1", 1, false, DefaultStringFormatter.IntegerStringFormatter, DefaultStringParser.IntegerStringParser, null, 0);
            this.T1_TDOLDEF = new PinPadFixedLengthPropertyController<HexadecimalData>("T1_TDOLDEF", 40, false, DefaultStringFormatter.HexadecimalRightPaddingStringFormatter, DefaultStringParser.HexadecimalRightPaddingStringParser);
            this.T1_DDOLDEF = new PinPadFixedLengthPropertyController<HexadecimalData>("T1_DDOLDEF", 40, false, DefaultStringFormatter.HexadecimalRightPaddingStringFormatter, DefaultStringParser.HexadecimalRightPaddingStringParser);
            this.T1_ARCOFFLN = new PinPadFixedLengthPropertyController<string>("T1_ARCOFFLN", 8, false, DefaultStringFormatter.StringStringFormatter, DefaultStringParser.StringStringParser, null, "Y1Z1Y3Z3");
            this.T1_CTLSTACDEF = new PinPadFixedLengthPropertyController<HexadecimalData>("T1_CTLSTACDEF", 10, true, DefaultStringFormatter.HexadecimalStringFormatter, DefaultStringParser.HexadecimalStringParser);
            this.T1_CTLSTACDEN = new PinPadFixedLengthPropertyController<HexadecimalData>("T1_CTLSTACDEN", 10, true, DefaultStringFormatter.HexadecimalStringFormatter, DefaultStringParser.HexadecimalStringParser);
            this.T1_CTLSTACONL = new PinPadFixedLengthPropertyController<HexadecimalData>("T1_CTLSTACONL", 10, true, DefaultStringFormatter.HexadecimalStringFormatter, DefaultStringParser.HexadecimalStringParser);

            {//PinPadBaseTableController starts the region and doesn't close
                AddProperty(this.T1_APPVER1);
                AddProperty(this.T1_APPVER2);
                AddProperty(this.T1_APPVER3);
                AddProperty(this.T1_TRMCNTRY);
                AddProperty(this.T1_TRMCURR);
                AddProperty(this.T1_TRMCRREXP);
                AddProperty(this.T1_MERCHID);
                AddProperty(this.T1_MCC);
                AddProperty(this.T1_TRMID);
                AddProperty(this.T1_TRMCAPAB);
                AddProperty(this.T1_ADDTRMCP);
                AddProperty(this.T1_TRMTYP);
                AddProperty(this.T1_TACDEF);
                AddProperty(this.T1_TACDEN);
                AddProperty(this.T1_TACONL);
                AddProperty(this.T1_FLRLIMIT);
                AddProperty(this.T1_TCC);
                AddProperty(this.T1_CTLSZEROAM);
                AddProperty(this.T1_CTLSMODE);
                AddProperty(this.T1_CTLSTRNLIM);
                AddProperty(this.T1_CTLSFLRLIM);
                AddProperty(this.T1_CTLSCVMLIM);
                AddProperty(this.T1_CTLSAPPVER);
                AddProperty(this.T1_RUF1);
                AddProperty(this.T1_TDOLDEF);
                AddProperty(this.T1_DDOLDEF);
                AddProperty(this.T1_ARCOFFLN);
                AddProperty(this.T1_CTLSTACDEF);
                AddProperty(this.T1_CTLSTACDEN);
                AddProperty(this.T1_CTLSTACONL);
            }//PinPadBaseTableController starts the region and doesn't close
            EndLastRegion();
        }

        /// <summary>
        /// Application standard
        /// </summary>
        public override ApplicationType T1_ICCSTD {
            get {
                return ApplicationType.IccEmv;
            }
        }

        /// <summary>
        /// First option for terminal application version number
        /// If the application version at terminal does not match any of the terminal application versions the bit 8 of byte 2 from Terminal Verification Result will be set
        /// EMV TAG 9F09h
        /// </summary>
        public PinPadFixedLengthPropertyController<HexadecimalData> T1_APPVER1 { get; private set; }

        /// <summary>
        /// Second option for terminal application version number
        /// If the application version at terminal does not match any of the terminal application versions the bit 8 of byte 2 from Terminal Verification Result will be set
        /// EMV TAG 9F09h
        /// </summary>
        public PinPadFixedLengthPropertyController<HexadecimalData> T1_APPVER2 { get; private set; }

        /// <summary>
        /// Third option for terminal application version number
        /// If the application version at terminal does not match any of the terminal application versions the bit 8 of byte 2 from Terminal Verification Result will be set
        /// EMV TAG 9F09h
        /// </summary>
        public PinPadFixedLengthPropertyController<HexadecimalData> T1_APPVER3 { get; private set; }

        /// <summary>
        /// Terminal Country Code
        /// EMV TAG 9F1Ah
        /// </summary>
        public PinPadFixedLengthPropertyController<Nullable<int>> T1_TRMCNTRY { get; private set; }

        /// <summary>
        /// Terminal Currency Code
        /// EMV TAG 5F2Ah
        /// </summary>
        public PinPadFixedLengthPropertyController<Nullable<int>> T1_TRMCURR { get; private set; }

        /// <summary>
        /// Terminal Currency Exponent
        /// EMV TAG 5F36h
        /// </summary>
        public PinPadFixedLengthPropertyController<Nullable<int>> T1_TRMCRREXP { get; private set; }

        /// <summary>
        /// Merchant Identifier
        /// EMV TAG 9F16h
        /// 15 chars long
        /// </summary>
        public PinPadFixedLengthPropertyController<string> T1_MERCHID { get; private set; }

        /// <summary>
        /// Merchant Category Code
        /// EMV TAG 9F15h
        /// 4 chars long
        /// </summary>
        public PinPadFixedLengthPropertyController<string> T1_MCC { get; private set; }

        /// <summary>
        /// Terminal Identification
        /// EMV TAG 9F1Ch
        /// 8 chars long
        /// </summary>
        public PinPadFixedLengthPropertyController<string> T1_TRMID { get; private set; }

        /// <summary>
        /// Terminal Capabilities
        /// EMV TAG 9F33h
        /// </summary>
        public PinPadFixedLengthPropertyController<HexadecimalData> T1_TRMCAPAB { get; private set; }

        /// <summary>
        /// Additional Terminal Capabilities
        /// EMV TAG 9F40h
        /// </summary>
        public PinPadFixedLengthPropertyController<HexadecimalData> T1_ADDTRMCP { get; private set; }

        /// <summary>
        /// Terminal Type
        /// EMV TAG 9F35h
        /// </summary>
        public PinPadFixedLengthPropertyController<Nullable<int>> T1_TRMTYP { get; private set; }

        /// <summary>
        /// Terminal Action Code for Default Case
        /// The last TAC checked, if matched the transaction will be denied even if it could have been approved online.
        /// </summary>
        public PinPadFixedLengthPropertyController<HexadecimalData> T1_TACDEF { get; private set; }

        /// <summary>
        /// Terminal Action Code for offline Denial
        /// The first TAC checked, if matched the transaction will be denied before going online.
        /// </summary>
        public PinPadFixedLengthPropertyController<HexadecimalData> T1_TACDEN { get; private set; }

        /// <summary>
        /// Terminal Action Code for online processing
        /// The second TAC checked, if matched the transaction may go online to authorize.
        /// </summary>
        public PinPadFixedLengthPropertyController<HexadecimalData> T1_TACONL { get; private set; }

        /// <summary>
        /// Terminal Floor Limit
        /// Used to determine if the transaction should go online
        /// EMV TAG 9F1Bh
        /// </summary>
        public PinPadFixedLengthPropertyController<HexadecimalData> T1_FLRLIMIT { get; private set; }

        /// <summary>
        /// Transaction Category Code
        /// EMV TAG 9F53h
        /// </summary>
        public PinPadFixedLengthPropertyController<string> T1_TCC { get; private set; }

        /// <summary>
        /// Supports CTLS if the transaction value is zero?
        /// </summary>
        public SimpleProperty<Nullable<bool>> T1_CTLSZEROAM { get; private set; }

        /// <summary>
        /// Contactless mode for this AID
        /// </summary>
        public PinPadFixedLengthPropertyController<ContactlessMode> T1_CTLSMODE { get; private set; }

        /// <summary>
        /// Terminal Contactless Transaction Limit
        /// </summary>
        public PinPadFixedLengthPropertyController<HexadecimalData> T1_CTLSTRNLIM { get; private set; }

        /// <summary>
        /// Terminal Contactless Floor Limit
        /// </summary>
        public PinPadFixedLengthPropertyController<HexadecimalData> T1_CTLSFLRLIM { get; private set; }

        /// <summary>
        /// Terminal Contactless Cardholder Verification Methods Required Limit
        /// </summary>
        public PinPadFixedLengthPropertyController<HexadecimalData> T1_CTLSCVMLIM { get; private set; }

        /// <summary>
        /// PayPass Magnetic Stripe Application Version Number
        /// EMV TAG 9F6Dh
        /// </summary>
        public PinPadFixedLengthPropertyController<HexadecimalData> T1_CTLSAPPVER { get; private set; }

        /// <summary>
        /// Reserved for Future Use
        /// </summary>
        public PinPadFixedLengthPropertyController<Nullable<int>> T1_RUF1 { get; private set; }

        /// <summary>
        /// Default Transaction Certificate Data Object List
        /// </summary>
        public PinPadFixedLengthPropertyController<HexadecimalData> T1_TDOLDEF { get; private set; }

        /// <summary>
        /// Default Dynamic Data Authentication Data Object List
        /// </summary>
        public PinPadFixedLengthPropertyController<HexadecimalData> T1_DDOLDEF { get; private set; }

        /// <summary>
        /// Authorization Response Codes for offline transactions
        /// Obsolete and ignored since EMV 4, default is "Y1Z1Y3Z3"
        /// </summary>
        public PinPadFixedLengthPropertyController<string> T1_ARCOFFLN { get; private set; }

        /// <summary>
        /// Contactless Terminal Action Code for Default Case
        /// The last TAC checked, if matched the transaction will be denied even if it could have been approved online.
        /// </summary>
        public PinPadFixedLengthPropertyController<HexadecimalData> T1_CTLSTACDEF { get; private set; }

        /// <summary>
        /// Contactless Terminal Action Code for offline Denial
        /// The first TAC checked, if matched the transaction will be denied before going online.
        /// </summary>
        public PinPadFixedLengthPropertyController<HexadecimalData> T1_CTLSTACDEN { get; private set; }

        /// <summary>
        /// Contactless Terminal Action Code for online processing
        /// The second TAC checked, if matched the transaction may go online to authorize.
        /// </summary>
        public PinPadFixedLengthPropertyController<HexadecimalData> T1_CTLSTACONL { get; private set; }
    }
}
