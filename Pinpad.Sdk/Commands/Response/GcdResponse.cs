namespace Pinpad.Sdk.Commands
{
	public class GcdResponse : BaseResponse
	{
		public override bool IsBlockingCommand
		{
			get { return true; }
		}

		public override string CommandName
		{
			get { return "GCD"; }
		}

		public GcdResponse()
		{
		}
	}
}
