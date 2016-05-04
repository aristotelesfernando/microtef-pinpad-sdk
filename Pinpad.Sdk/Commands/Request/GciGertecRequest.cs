using Pinpad.Sdk.Properties;
using System;
using Pinpad.Sdk.Model;

namespace Pinpad.Sdk.Commands
{
	internal class GciGertecRequest : BaseCommand
	{
		public override string CommandName { get { return "EX07"; } }
		public PinpadFixedLengthProperty<KeyboardNumberFormat> NumericInputType { get; set; }
		public PinpadFixedLengthProperty<KeyboardTextFormat> TextInputType { get; set; }
		public PinpadFixedLengthProperty<FirstLineLabelCode> LabelFirstLine { get; set; }
		public PinpadFixedLengthProperty<SecondLineLabelCode> LabelSecondLine { get; set; }
		public PinpadFixedLengthProperty<Nullable<int>> MaximumCharacterLength { get; set; }
		public PinpadFixedLengthProperty<Nullable<int>> MinimumCharacterLength { get; set; }
		public PinpadFixedLengthProperty<Nullable<int>> TimeOut { get; set; }
		public PinpadFixedLengthProperty<Nullable<int>> TimeIdle { get; set; }

		public GciGertecRequest ()
			: base(new GertecContext())
		{
			this.NumericInputType = new PinpadFixedLengthProperty<KeyboardNumberFormat>("NumericInputType", 1, false, DefaultStringFormatter.EnumStringFormatter<KeyboardNumberFormat>, DefaultStringParser.EnumStringParser<KeyboardNumberFormat>);
			this.TextInputType = new PinpadFixedLengthProperty<KeyboardTextFormat>("TextInputType", 1, false, DefaultStringFormatter.EnumStringFormatter<KeyboardTextFormat>, DefaultStringParser.EnumStringParser<KeyboardTextFormat>);
			this.LabelFirstLine = new PinpadFixedLengthProperty<FirstLineLabelCode>("LabelFirstLine", 2, false, DefaultStringFormatter.EnumStringFormatter<FirstLineLabelCode>, DefaultStringParser.EnumStringParser<FirstLineLabelCode>);
			this.LabelSecondLine = new PinpadFixedLengthProperty<SecondLineLabelCode>("LabelSecondLine", 2, false, DefaultStringFormatter.EnumStringFormatter<SecondLineLabelCode>, DefaultStringParser.EnumStringParser<SecondLineLabelCode>);

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

		public static bool IsSupported (string manufacturerName, string model, string manufacturerVersion)
		{
			int v1, v2, v3;

			if (manufacturerName.Contains("GERTEC") == false) { return false; }

			if (model.Contains("MOBI PIN 10") == true)
			{
				string [] v = manufacturerVersion.Trim().Split('.', ' ');

				if (v.Length != 3) { return false; }

				if (Int32.TryParse(v [0], out v1) == true && Int32.TryParse(v [1], out v2) == true && Int32.TryParse(v [2], out v3) == true)
				{
					if (v1 < 1 && v2 < 11 && v3 < 160324) { return false; }
				}
				else { return false; }
			}
			else { return false; }

			return true;
		}
	}
}
