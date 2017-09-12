using Pinpad.Sdk.Commands.DataSet;
using Pinpad.Sdk.PinpadProperties.Refactor.Command;
using Pinpad.Sdk.PinpadProperties.Refactor.Formatter;
using Pinpad.Sdk.PinpadProperties.Refactor.Parser;
using Pinpad.Sdk.PinpadProperties.Refactor.Property;
using System;

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
        /// Parameter identifier.
        /// </summary>
        public BinaryProperty<byte[]> CMD_PAR0ID{ get; private set; }
        /// <summary>
        /// Parameter length.
        /// </summary>
        public BinaryProperty<byte[]> CMD_PAR0LEN { get; private set; }
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
            this.CMD_LEN1 = new RegionProperty("CMD_LEN1", 3);
            this.CMD_PAR0ID = new BinaryProperty<byte[]>("CMD_PAR0ID", false, new byte[] { 0x00, 0x06 });
            this.CMD_PAR0LEN = new BinaryProperty<byte[]>("CMD_PAR0LEN", false, new byte[] { 0x00, 0x06 });
            this.SPE_CEXOPT = new FixedLengthProperty<string>("SPE_CEXOPT", 6, false,
                StringFormatter.StringStringFormatter, StringParser.StringStringParser);
            
            this.StartRegion(this.CMD_LEN1);
            {
                this.AddProperty(this.CMD_PAR0ID);
                this.AddProperty(this.CMD_PAR0LEN);
                this.AddProperty(this.SPE_CEXOPT);
            }
            this.EndLastRegion(); 
        }
    }
}
