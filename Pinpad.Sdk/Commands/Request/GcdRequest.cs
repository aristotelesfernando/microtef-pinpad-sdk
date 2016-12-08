using Pinpad.Sdk.Model;
using Pinpad.Sdk.Properties;
using System;

namespace Pinpad.Sdk.Commands
{
	internal sealed class GcdRequest : BaseCommand
	{
		public override string CommandName
		{
			get { return "GCD"; }
		}

		public RegionProperty CMD_LEN1 { get; private set; }
		public PinpadFixedLengthProperty<KeyboardMessageCode> SPE_MSGIDX { get; private set; }
		public PinpadFixedLengthProperty<Nullable<int>> SPE_MINDIG { get; private set; }
		public PinpadFixedLengthProperty<Nullable<int>> SPE_MAXDIG { get; private set; }

		public GcdRequest()
		{
			this.CMD_LEN1 = new RegionProperty("CMD_LEN1", 3, false, true);
			this.SPE_MSGIDX = new PinpadFixedLengthProperty<KeyboardMessageCode>("SPE_MSGIDX", 4, false, DefaultStringFormatter.EnumStringFormatter<KeyboardMessageCode>, DefaultStringParser.EnumStringParser<KeyboardMessageCode>);

			this.SPE_MINDIG = new PinpadFixedLengthProperty<Nullable<int>>("SPE_MINDIG", 2, false, DefaultStringFormatter.IntegerStringFormatter, DefaultStringParser.IntegerStringParser);

			this.SPE_MAXDIG = new PinpadFixedLengthProperty<Nullable<int>>("SPE_MAXDIG", 2, false, DefaultStringFormatter.IntegerStringFormatter, DefaultStringParser.IntegerStringParser);

			this.AddProperty(this.SPE_MSGIDX);
			this.AddProperty(this.SPE_MINDIG);
			this.AddProperty(this.SPE_MAXDIG);
		}
	}
}
