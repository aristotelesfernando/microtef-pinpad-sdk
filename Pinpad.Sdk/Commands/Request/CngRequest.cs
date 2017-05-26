using Pinpad.Sdk.PinpadProperties.Refactor.Command;
using Pinpad.Sdk.PinpadProperties.Refactor.Formatter;
using Pinpad.Sdk.PinpadProperties.Refactor.Parser;
using Pinpad.Sdk.PinpadProperties.Refactor.Property;

namespace Pinpad.Sdk.Commands
{
    /// <summary>
    /// CNG request
    /// Used to supply the PinPad with additional EMV tags to be used in the processing of GOC and FNC requests.
    /// The parameters may coincide with the ones present in the AID table, which will alter the values only in the current transaction.
    /// May also be used to supply proprietary parameters not listed in the EMV documentation.
    /// </summary>
    internal sealed class CngRequest : BaseCommand
	{
        /// <summary>
        /// Constructor
        /// </summary>
        public CngRequest()
		{
            this.CMD_LEN1 = new RegionProperty("CMD_LEN1", 3);

			this.CNG_EMVDAT = new VariableLengthProperty<HexadecimalData>("CNG_EMVDAT", 2, 198, 1.0f / 2, 
                false, false, StringFormatter.HexadecimalStringFormatter, StringParser.HexadecimalStringParser);

            this.StartRegion(this.CMD_LEN1);
            {
				this.AddProperty(this.CNG_EMVDAT);
            }
            this.EndLastRegion();
        }

        /// <summary>
        /// Name of the command
        /// </summary>
        public override string CommandName { get { return "CNG"; } }

        /// <summary>
        /// Length of the first region of the command
        /// </summary>
        public RegionProperty CMD_LEN1 { get; private set; }

		/// <summary>
		/// EMV data to be supplied in Tag/Length/Value format
		/// </summary>
		public VariableLengthProperty<HexadecimalData> CNG_EMVDAT { get; private set; }
    }
}
