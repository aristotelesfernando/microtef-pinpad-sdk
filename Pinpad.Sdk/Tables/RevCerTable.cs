using Pinpad.Sdk.Model;
using Pinpad.Sdk.Commands;
using Pinpad.Sdk.PinpadProperties.Refactor.Property;
using Pinpad.Sdk.PinpadProperties.Refactor.Formatter;
using Pinpad.Sdk.PinpadProperties.Refactor.Parser;

namespace Pinpad.Sdk
{
	/// <summary>
	/// Controller for CAPK revoked certificates
	/// </summary>
	public sealed class RevCerTable : BaseTable
    {
        /// <summary>
		/// Table Identifier
		/// </summary>
		public override EmvTableType TAB_ID
        {
            get
            {
                return EmvTableType.RevokedCertificate;
            }
        }
        /// <summary>
        /// Registered Application provider Identifier
        /// </summary>
        public FixedLengthProperty<HexadecimalData> T3_RID { get; private set; }
        /// <summary>
        /// Certification Authority Public Key Index
        /// </summary>
        public FixedLengthProperty<HexadecimalData> T3_CAPKIDX { get; private set; }
        /// <summary>
        /// Certificate Serial Number
        /// </summary>
        public FixedLengthProperty<HexadecimalData> T3_CERTSN { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public RevCerTable() {
			this.T3_RID = new FixedLengthProperty<HexadecimalData>("T3_RID", 10, false, 
                StringFormatter.HexadecimalStringFormatter, StringParser.HexadecimalStringParser);
			this.T3_CAPKIDX = new FixedLengthProperty<HexadecimalData>("T3_CAPKIDX", 2, false, 
                StringFormatter.HexadecimalStringFormatter, StringParser.HexadecimalStringParser);
			this.T3_CERTSN = new FixedLengthProperty<HexadecimalData>("T3_CERTSN", 6, false, 
                StringFormatter.HexadecimalStringFormatter, StringParser.HexadecimalStringParser);

			{//PinPadBaseTableController starts the region and doesn't close
				this.AddProperty(this.T3_RID);
				this.AddProperty(this.T3_CAPKIDX);
				this.AddProperty(this.T3_CERTSN);
			}//PinPadBaseTableController starts the region and doesn't close
			this.EndLastRegion();
		}
	}
}
