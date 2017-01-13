using Pinpad.Sdk.Commands;
using Pinpad.Sdk.Model;
using Pinpad.Sdk.Properties;
using System;

namespace Pinpad.Sdk
{
    /// <summary>
    /// Certification Authority Public Key table
    /// CAPKs are used by EMV cards for offline autentication and pin cryptography
    /// </summary>
    public class CapkTable : BaseTable {
        /// <summary>
        /// Constructor
        /// </summary>
        public CapkTable() {
            this.T2_RID = new PinpadFixedLengthProperty<HexadecimalData>("T2_RID", 10, false, DefaultStringFormatter.HexadecimalStringFormatter, DefaultStringParser.HexadecimalStringParser);
            this.T2_CAPKIDX = new PinpadFixedLengthProperty<HexadecimalData>("T2_CAPKIDX", 2, false, DefaultStringFormatter.HexadecimalStringFormatter, DefaultStringParser.HexadecimalStringParser);
            this.T2_RUF1 = new PinpadFixedLengthProperty<int?>("T2_RUF1", 2, false, DefaultStringFormatter.IntegerStringFormatter, DefaultStringParser.IntegerStringParser, null, 0);
            this.T2_EXP = new VariableLengthProperty<HexadecimalData>("T2_EXP", 1, 6, 1.0f / 2, true, false, DefaultStringFormatter.HexadecimalStringFormatter, DefaultStringParser.HexadecimalStringParser);
            this.T2_MOD = new VariableLengthProperty<HexadecimalData>("T2_MOD", 3, 496, 1.0f / 2, true, false, DefaultStringFormatter.HexadecimalStringFormatter, DefaultStringParser.HexadecimalStringParser);
            this.T2_CHKSTAT = new SimpleProperty<bool?>("T2_CHKSTAT", false, DefaultStringFormatter.BooleanStringFormatter, DefaultStringParser.BooleanStringParser);
            this.T2_CHECKSUM = new PinpadFixedLengthProperty<HexadecimalData>("T2_CHECKSUM", 40, false, DefaultStringFormatter.HexadecimalStringFormatter, DefaultStringParser.HexadecimalStringParser);
            this.T2_RUF2 = new PinpadFixedLengthProperty<HexadecimalData>("T2_RUF2", 42, false, DefaultStringFormatter.HexadecimalStringFormatter, DefaultStringParser.HexadecimalStringParser, null, new HexadecimalData(new byte[0]));

            {//PinPadBaseTableController starts the region and doesn't close
                this.AddProperty(this.T2_RID);
                this.AddProperty(this.T2_CAPKIDX);
                this.AddProperty(this.T2_RUF1);
                this.AddProperty(this.T2_EXP);
                this.AddProperty(this.T2_MOD);
                this.AddProperty(this.T2_CHKSTAT);
                this.AddProperty(this.T2_CHECKSUM);
                this.AddProperty(this.T2_RUF2);
            }//PinPadBaseTableController starts the region and doesn't close
            this.EndLastRegion();
        }

        /// <summary>
        /// Table Identifier
        /// </summary>
        public override EmvTableType TAB_ID {
            get {
                return EmvTableType.Capk;
            }
        }

        /// <summary>
        /// Registered Application provider Identifier.
        /// 10 first bytes of AID.
        /// </summary>
        public PinpadFixedLengthProperty<HexadecimalData> T2_RID { get; private set; }

        /// <summary>
        /// Certification Authority Public Key Index
        /// </summary>
        public PinpadFixedLengthProperty<HexadecimalData> T2_CAPKIDX { get; private set; }

        /// <summary>
        /// Reserved for future use, default 0.
        /// </summary>
        public PinpadFixedLengthProperty<Nullable<int>> T2_RUF1 { get; private set; }

        /// <summary>
        /// Certification Authority Public Key Exponent
        /// </summary>
        public VariableLengthProperty<HexadecimalData> T2_EXP { get; private set; }

        /// <summary>
        /// Certification Authority Public Key Modulus
        /// </summary>
        public VariableLengthProperty<HexadecimalData> T2_MOD { get; private set; }

        /// <summary>
        /// Should verify Checksum?
        /// </summary>
        public SimpleProperty<Nullable<bool>> T2_CHKSTAT { get; private set; }

        /// <summary>
        /// Certification Authority Public Key Checksum
        /// </summary>
        public PinpadFixedLengthProperty<HexadecimalData> T2_CHECKSUM { get; private set; }

        /// <summary>
        /// Reserved for future use, default 0.
        /// </summary>
        public PinpadFixedLengthProperty<HexadecimalData> T2_RUF2 { get; private set; }
    }
}
