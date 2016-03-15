using Pinpad.Core.Commands.Context;
using Pinpad.Core.Properties;
using Pinpad.Core.Utilities;
using System;

namespace Pinpad.Core.Commands
{
	public class GertecEx07Request : BaseCommand
	{
		public override string CommandName { get { return "EX07"; } }
		public PinpadFixedLengthProperty<GertecEx07NumberFormat> NumericInputType { get; set; }
		public PinpadFixedLengthProperty<GertecEx07TextFormat> TextInputType { get; set; }
		public PinpadFixedLengthProperty<GertecEx07MessageInFirstLine> LabelFirstLine { get; set; }
		public PinpadFixedLengthProperty<GertecEx07MessageInSecondLine> LabelSecondLine { get; set; }
		public PinpadFixedLengthProperty<Nullable<int>> MaximumCharacterLength { get; set; }
		public PinpadFixedLengthProperty<Nullable<int>> MinimumCharacterLength { get; set; }
		public PinpadFixedLengthProperty<Nullable<int>> TimeOut { get; set; }
		public PinpadFixedLengthProperty<Nullable<int>> TimeIdle { get; set; }

		public GertecEx07Request ()
			: base(new GertecContext())
		{
			this.NumericInputType = new PinpadFixedLengthProperty<GertecEx07NumberFormat>("NumericInputType", 1, false, DefaultStringFormatter.EnumStringFormatter<GertecEx07NumberFormat>, DefaultStringParser.EnumStringParser<GertecEx07NumberFormat>);
			this.TextInputType = new PinpadFixedLengthProperty<GertecEx07TextFormat>("TextInputType", 1, false, DefaultStringFormatter.EnumStringFormatter<GertecEx07TextFormat>, DefaultStringParser.EnumStringParser<GertecEx07TextFormat>);
			this.LabelFirstLine = new PinpadFixedLengthProperty<GertecEx07MessageInFirstLine>("LabelFirstLine", 2, false, DefaultStringFormatter.EnumStringFormatter<GertecEx07MessageInFirstLine>, DefaultStringParser.EnumStringParser<GertecEx07MessageInFirstLine>);
			this.LabelSecondLine = new PinpadFixedLengthProperty<GertecEx07MessageInSecondLine>("LabelSecondLine", 2, false, DefaultStringFormatter.EnumStringFormatter<GertecEx07MessageInSecondLine>, DefaultStringParser.EnumStringParser<GertecEx07MessageInSecondLine>);

			this.MaximumCharacterLength = new PinpadFixedLengthProperty<Nullable<int>>("MaximumCharacterLength", 2, false, DefaultStringFormatter.IntegerStringFormatter, DefaultStringParser.IntegerStringParser);
			this.MinimumCharacterLength = new PinpadFixedLengthProperty<Nullable<int>>("MinimumCharacterLength", 2, false, DefaultStringFormatter.IntegerStringFormatter, DefaultStringParser.IntegerStringParser);
			this.TimeOut = new PinpadFixedLengthProperty<Nullable<int>>("TimeOut", 3, false, DefaultStringFormatter.IntegerStringFormatter, DefaultStringParser.IntegerStringParser);
			this.TimeIdle = new PinpadFixedLengthProperty<Nullable<int>>("TimeIdle", 3, false, DefaultStringFormatter.IntegerStringFormatter, DefaultStringParser.IntegerStringParser);
			
			{
				this.AddProperty(this.NumericInputType);
				this.AddProperty(this.TextInputType);
				this.AddProperty(this.LabelFirstLine);
				this.AddProperty(this.LabelSecondLine);
				this.AddProperty(this.MaximumCharacterLength);
				this.AddProperty(this.MinimumCharacterLength);
				this.AddProperty(this.TimeOut);
				this.AddProperty(this.TimeIdle);
			}
		}
	}
}
