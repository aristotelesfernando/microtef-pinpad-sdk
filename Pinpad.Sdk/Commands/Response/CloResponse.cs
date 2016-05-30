using System;

namespace Pinpad.Sdk.Commands
{
	internal class CloResponse : BaseResponse
	{
		public override string CommandName
		{
			get
			{
				return "CLO";
			}
		}

		public override bool IsBlockingCommand
		{
			get
			{
				return false;
			}
		}

		public CloResponse ()
		{
		}
	}
}
