using Pinpad.Sdk.Commands;
using Pinpad.Sdk.Model;
using Pinpad.Sdk.PinpadProperties.Refactor.Formatter;
using Pinpad.Sdk.PinpadProperties.Refactor.Parser;
using Pinpad.Sdk.PinpadProperties.Refactor.Property;
using System;

namespace Pinpad.Sdk
{
    /// <summary>
    /// Certification Authority Public Key table
    /// CAPKs are used by EMV cards for offline autentication and pin cryptography
    /// </summary>
    public sealed class CapkTable : BaseTable
    {
        /// <summary>
        /// Table Identifier
        /// </summary>
        public override EmvTableType TAB_ID
        {
            get
            {
                return EmvTableType.Capk;
            }
        }
        /// <summary>
        /// Registered Application provider Identifier.
        /// 10 first bytes of AID.
        /// </summary>
        public FixedLengthProperty<HexadecimalData> T2_RID { get; private set; }
        /// <summary>
        /// Certification Authority Public Key Index
        /// </summary>
        public FixedLengthProperty<HexadecimalData> T2_CAPKIDX { get; private set; }
        /// <summary>
        /// Reserved for future use, default 0.
        /// </summary>
        public FixedLengthProperty<Nullable<int>> T2_RUF1 { get; private set; }
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
        public TextProperty<Nullable<bool>> T2_CHKSTAT { get; private set; }
        /// <summary>
        /// Certification Authority Public Key Checksum
        /// </summary>
        public FixedLengthProperty<HexadecimalData> T2_CHECKSUM { get; private set; }
        /// <summary>
        /// Reserved for future use, default 0.
        /// </summary>
        public FixedLengthProperty<HexadecimalData> T2_RUF2 { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public CapkTable() {
            this.T2_RID = new FixedLengthProperty<HexadecimalData>("T2_RID", 10, false, 
                StringFormatter.HexadecimalStringFormatter, StringParser.HexadecimalStringParser);
            this.T2_CAPKIDX = new FixedLengthProperty<HexadecimalData>("T2_CAPKIDX", 2, false, 
                StringFormatter.HexadecimalStringFormatter, StringParser.HexadecimalStringParser);
            this.T2_RUF1 = new FixedLengthProperty<int?>("T2_RUF1", 2, false, 
                StringFormatter.IntegerStringFormatter, StringParser.IntegerStringParser, null, 0);
            this.T2_EXP = new VariableLengthProperty<HexadecimalData>("T2_EXP", 1, 6, 1.0f / 2, true, false, 
                StringFormatter.HexadecimalStringFormatter, StringParser.HexadecimalStringParser);
            this.T2_MOD = new VariableLengthProperty<HexadecimalData>("T2_MOD", 3, 496, 1.0f / 2, true, false, 
                StringFormatter.HexadecimalStringFormatter, StringParser.HexadecimalStringParser);
            this.T2_CHKSTAT = new TextProperty<bool?>("T2_CHKSTAT", false, 
                StringFormatter.BooleanStringFormatter, StringParser.BooleanStringParser);
            this.T2_CHECKSUM = new FixedLengthProperty<HexadecimalData>("T2_CHECKSUM", 40, false, 
                StringFormatter.HexadecimalStringFormatter, StringParser.HexadecimalStringParser);
            this.T2_RUF2 = new FixedLengthProperty<HexadecimalData>("T2_RUF2", 42, false, 
                StringFormatter.HexadecimalStringFormatter, StringParser.HexadecimalStringParser, null, new HexadecimalData(new byte[0]));

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
    }
}
