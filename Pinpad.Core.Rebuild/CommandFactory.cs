using Pinpad.Core.Rebuild.Abecs;
using System;

namespace Pinpad.Core.Rebuild
{
	internal class CommandFactory
	{
		public static ICommandRequest Create (CommandCode commandCode)
		{
			// TODO: implementar lógica de criação do comando correto.

			switch (commandCode)
			{
				case CommandCode.Open: return new OpnRequest();
				default: throw new NotImplementedException("Command not implemented.");
			}
		}
	}
}
