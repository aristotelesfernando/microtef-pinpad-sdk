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
	/// OPN response containing the Stone Application Version
	/// </summary>
	public class OpnResponse : BaseResponse 
	{
		// Members
		/// <summary>
		/// Is this a blocking command?
		/// </summary>
		public override bool IsBlockingCommand { get { return false; } }
		/// <summary>
		/// Name of the command
		/// </summary>
		public override string CommandName { get { return "OPN"; } }
		/// <summary>
		/// Length of the first region of the command
		/// </summary>
		public RegionProperty RSP_LEN1 { get; private set; }
		/// <summary>
		/// Version of the Stone App in the PinPad
		/// </summary>
		public PinpadFixedLengthProperty<Nullable<int>> OPN_STONEVER { get; private set; }
		
		// Constructor
		/// <summary>
		/// Controller
		/// </summary>
		public OpnResponse() 
		{
			this.RSP_LEN1 = new RegionProperty("RSP_LEN1", 3, false, true);
			this.OPN_STONEVER = new PinpadFixedLengthProperty<Nullable<int>>("OPN_STONEVER", 3, true, DefaultStringFormatter.IntegerStringFormatter, DefaultStringParser.IntegerStringParser);

			this.StartRegion(this.RSP_LEN1);
			{
				this.AddProperty(this.OPN_STONEVER);
			}
			this.EndLastRegion();
		}
	}
}
