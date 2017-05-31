using Pinpad.Sdk.PinpadProperties.Refactor.Property;

namespace Pinpad.Sdk.Commands
{
	internal class GertecBaseResponse : BaseResponse
	{
		public override string CommandName
		{
			get
			{
				if (this.CMD_ID == null)
				{
					return null;
				}
				else
				{
					return this.CMD_ID.Value;
				}
			}
		}
		public override bool IsBlockingCommand
		{
			get
			{
				return false;
			}
		}
		public new FixedLengthProperty<GertecResponseCode> RSP_STAT { get; private set; }
	}
}
