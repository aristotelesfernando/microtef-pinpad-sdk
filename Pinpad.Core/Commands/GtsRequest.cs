using Pinpad.Core.Properties;
using System;

/* WARNING!
 * 
 * DEPRECATED.
 * MUST BE REFACTORED.
 * 
 */

namespace Pinpad.Core.Commands 
{
    /// <summary>
    /// GTS request
    /// </summary>
    public class GtsRequest : BaseCommand 
	{
		// Members
        /// <summary>
        /// Name of the command
        /// </summary>
        public override string CommandName { get { return "GTS"; } }
        /// <summary>
        /// Length of the first region of the command
        /// </summary>
        public RegionProperty CMD_LEN1 { get; private set; }
        /// <summary>
        /// EMV Table Acquirer Index to request version or 0 when using a single table for all acquirers
        /// </summary>
        public PinpadFixedLengthProperty<Nullable<int>> GTS_ACQIDX { get; private set; }
        
		// Constructor
		/// <summary>
        /// Constructor
        /// </summary>
        public GtsRequest() 
		{
            this.CMD_LEN1 = new RegionProperty("CMD_LEN1", 3);
            this.GTS_ACQIDX = new PinpadFixedLengthProperty<int?>("GTS_ACQIDX", 2, false, DefaultStringFormatter.IntegerStringFormatter, DefaultStringParser.IntegerStringParser);

            this.StartRegion(this.CMD_LEN1);
            {
                this.AddProperty(this.GTS_ACQIDX);
            }
            this.EndLastRegion();
        }
    }
}
