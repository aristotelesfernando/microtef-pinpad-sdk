using Pinpad.Sdk.PinpadProperties.Refactor.Formatter;
using Pinpad.Sdk.PinpadProperties.Refactor.Parser;
using Pinpad.Sdk.PinpadProperties.Refactor.Property;

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
		public FixedLengthProperty<string> GDU_KSN { get; private set; }
		/// <summary>
		/// If stone is supported on this pinpad.
		/// </summary>
		public bool IsStoneSupported
		{
			get
			{
				if (this.RSP_STAT.Value == AbecsResponseStatus.ST_OK && this.GDU_KSN.HasValue && string.IsNullOrEmpty(this.GDU_KSN.Value) == false)
				{
					return true;
				}
				else
				{
					return false;
				}
			}
		}

		/// <summary>
		/// Creates GDU response command with all properties and regions.
		/// </summary>
		public GduResponse ()
		{
			this.CMD_LEN1 = new RegionProperty("CMD_LEN1", 3);
			this.GDU_KSN = new FixedLengthProperty<string>("GDU_KSN", 20, false, 
                StringFormatter.StringStringFormatter, StringParser.StringStringParser);

			this.StartRegion(CMD_LEN1);
			{
				this.AddProperty(this.GDU_KSN);
			}
			this.EndLastRegion();
		}
	}
}
