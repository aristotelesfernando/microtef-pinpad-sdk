using Pinpad.Sdk.Properties;
using System;

namespace Pinpad.Sdk.Commands
{
	/// <summary>
	/// This command is the GDU response.
	/// The CDU command btains the KSN ("Key Serial Number") of a specific index on acquirer table.
	/// This operation depends on encryption methods such as: DUKPT/DES/PIN or DUKPT/TDES/PIN.
	/// </summary>
	internal class GduResponse : BaseResponse
	{
		/// <summary>
		/// Command name. In this case, GDU command response.
		/// </summary>
		public override string CommandName { get { return "GDU"; } }
		/// <summary>
		/// If the command have to wait for an extern interaction.
		/// In this case, the command only depends on the pinpad.
		/// </summary>
		public override bool IsBlockingCommand { get { return false; } }
		/// <summary>
		/// Length of the first region of the command.
		/// </summary>
		public RegionProperty CMD_LEN1 { get; private set; }
		/// <summary>
		/// KSN obtained.
		/// </summary>
		public PinpadFixedLengthProperty<string> GDU_KSN { get; private set; }

		/// <summary>
		/// Creates GDU response command with all properties and regions.
		/// </summary>
		public GduResponse ()
		{
			this.CMD_LEN1 = new RegionProperty("CMD_LEN1", 3);
			this.GDU_KSN = new PinpadFixedLengthProperty<string>("GDU_KSN", 20, false, DefaultStringFormatter.StringStringFormatter, DefaultStringParser.StringStringParser);

			this.StartRegion(CMD_LEN1);
			{
				this.AddProperty(this.GDU_KSN);
			}
			this.EndLastRegion();
		}
	}
}
