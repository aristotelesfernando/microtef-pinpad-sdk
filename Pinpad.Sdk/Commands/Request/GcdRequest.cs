using Pinpad.Sdk.Model;
using Pinpad.Sdk.PinpadProperties.Refactor.Formatter;
using Pinpad.Sdk.PinpadProperties.Refactor.Parser;
using Pinpad.Sdk.PinpadProperties.Refactor.Property;
using System;

namespace Pinpad.Sdk.Commands
{
	internal sealed class GcdRequest : PinpadProperties.Refactor.BaseCommand
	{
		public override string CommandName
		{
			get { return "GCD"; }
		}

		public RegionProperty CMD_LEN1 { get; private set; }
		public FixedLengthProperty<KeyboardMessageCode> SPE_MSGIDX { get; private set; }
		public FixedLengthProperty<Nullable<int>> SPE_MINDIG { get; private set; }
		public FixedLengthProperty<Nullable<int>> SPE_MAXDIG { get; private set; }

		public GcdRequest()
		{
			this.CMD_LEN1 = new RegionProperty("CMD_LEN1", 3, false, true);
			this.SPE_MSGIDX = new FixedLengthProperty<KeyboardMessageCode>("SPE_MSGIDX", 4, false, 
                StringFormatter.EnumStringFormatter<KeyboardMessageCode>, 
                StringParser.EnumStringParser<KeyboardMessageCode>);
			this.SPE_MINDIG = new FixedLengthProperty<Nullable<int>>("SPE_MINDIG", 2, false, 
                StringFormatter.IntegerStringFormatter, StringParser.IntegerStringParser);
			this.SPE_MAXDIG = new FixedLengthProperty<Nullable<int>>("SPE_MAXDIG", 2, false, 
                StringFormatter.IntegerStringFormatter, StringParser.IntegerStringParser);

			this.AddProperty(this.SPE_MSGIDX);
			this.AddProperty(this.SPE_MINDIG);
			this.AddProperty(this.SPE_MAXDIG);
		}
	}
}
