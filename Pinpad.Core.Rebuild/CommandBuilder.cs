using Pinpad.Core.Rebuild.Gertec;

namespace Pinpad.Core.Rebuild
{
	internal class CommandBuilder
	{
		private ICommandRequest command;

		public CommandBuilder (CommandCode commandCode)
		{
			this.command = CommandFactory.Create(commandCode);
		}

		// Building methods:
		public CommandBuilder AddLabel (string label)
		{
			// TODO: implementar lógica.
			return this;
		}
		public CommandBuilder AddLabel (GertecFirstLabelCode labelCode)
		{
			// TODO: implementar lógica.
			return this;
		}
		public CommandBuilder AddLabel (GertecSecondLabelCode labelCode)
		{
			// TODO: implementar lógica.
			return this;
		}
		public CommandBuilder ShouldReadKeys (bool shouldReadKeys)
		{
			// TODO: implementar lógica.
			return this;
		}
		public CommandBuilder ShouldReadMagneticStripe (bool shouldReadMagneticStripe)
		{
			// TODO: implementar lógica.
			return this;
		}
		public CommandBuilder ShouldReadIcc (bool shouldReadIcc)
		{
			// TODO: implementar lógica.
			return this;
		}
		public CommandBuilder AddNumericFormatting (GertecNumberFormatCode formattingCode)
		{
			// TODO: implementar lógica.
			return this;
		}
		public CommandBuilder AddTextFormatting (GertecTextFormatCode formattingCode)
		{
			// TODO: implementar lógica.
			return this;
		}
		public CommandBuilder AddAmount (decimal amount)
		{
			// TODO: implementar lógica.
			return this;
		}
		public CommandBuilder AddTransactionType (TransactionType transactionType)
		{
			// TODO: implementar lógica.
			return this;
		}
		public CommandBuilder AddEmvTable ()
		{
			// TODO: implementar lógica.
			return this;
		}
		
		public ICommandRequest Build ()
		{
			return this.command;
		}
	}
}
