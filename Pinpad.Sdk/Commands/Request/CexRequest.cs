using Pinpad.Sdk.Commands.DataSet;
using Pinpad.Sdk.PinpadProperties.Refactor.Command;
using Pinpad.Sdk.PinpadProperties.Refactor.Formatter;
using Pinpad.Sdk.PinpadProperties.Refactor.Parser;
using Pinpad.Sdk.PinpadProperties.Refactor.Property;

namespace Pinpad.Sdk.Commands.Request
{
    /// <summary>
    /// CEX Request
    /// </summary>
    internal sealed class CexRequest : BaseCommand
    {
        // Members
        /// <summary>
        /// Name of the command.
        /// </summary>
        public override string CommandName { get { return "CEX"; } }
        /// <summary>
		/// Length of the first region of the command
		/// </summary>
		public RegionProperty CMD_LEN1 { get; private set; }
        /// <summary>
		/// Event to be verified by the pinpad.
        /// Supported events listed at <see cref="CexOptions"/>.
		/// </summary>
		public FixedLengthProperty<string> SPE_CEXOPT { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public CexRequest()
        {
            CMD_LEN1 = new RegionProperty("CMD_LEN1", 3);
            SPE_CEXOPT = new FixedLengthProperty<string>("SPE_CEXOPT", 6, false,
                StringFormatter.StringStringFormatter, StringParser.StringStringParser);

            this.StartRegion(CMD_LEN1);
            {
                this.AddProperty(SPE_CEXOPT);
            }
            this.EndLastRegion();
        }
    }
}
