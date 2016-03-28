using Pinpad.Core.Rebuild.Gertec;
using Pinpad.Core.Rebuild.Property;
using System.Linq;

namespace Pinpad.Core.Rebuild
{
	public class CommandBuilder
	{
		private ICommandRequest command;

		public CommandBuilder (CommandCode commandCode)
		{
			this.command = CommandFactory.Create(commandCode);
		}

		// Building methods:
		public CommandBuilder Add (PropertyCode propertyCode, IProperty value)
		{
			this.command.Properties[propertyCode] = value;
			return this;
		}
		
		public ICommandRequest Build ()
		{
			// TODO: verificar se falta alguma propriedade mandatória do comando.
			return this.command;
		}
	}
}
