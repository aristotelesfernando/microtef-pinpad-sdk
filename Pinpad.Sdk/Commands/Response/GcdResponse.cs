namespace Pinpad.Sdk.Commands
{
	internal class GcdResponse : BaseResponse
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
