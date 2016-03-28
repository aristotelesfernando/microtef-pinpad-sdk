using System;

namespace Pinpad.Core.Rebuild.Abecs
{
	internal class OpnRequest : ICommandRequest
	{
		private IContext commandContext;
		public IContext CommandContext
		{
			get
			{
				return this.commandContext;
			}
		}

		public string Name
		{
			get
			{
				return "OPN";
			}
		}

		public OpnRequest ()
		{
			this.commandContext = new AbecsContext();
		}

		public void Send ()
		{
			// TODO: implementar!!
			throw new NotImplementedException();
		}
	}
}
