using Pinpad.Sdk.Commands.TypeCode;
using Pinpad.Sdk.Model;
using Pinpad.Sdk.Properties;
using System;

namespace Pinpad.Sdk.Commands.Request
{
    // TODO: Documentar.
    internal sealed class PrtRequest : BaseCommand
    {
        public override string CommandName { get { return "PRT"; } }
        public RegionProperty CMD_LEN1 { get; private set; }
        public PinpadFixedLengthProperty<IngenicoPrinterAction> PRT_Action { get; private set; }
        public PinpadFixedLengthProperty<PrinterFontSize> PRT_Size { get; private set; }
        public PinpadFixedLengthProperty<PrinterAlignmentCode> PRT_Alignment { get; private set; }
        public VariableLengthProperty<string> PRT_DATA { get; private set; }
        public PinpadFixedLengthProperty<Nullable<int>> PRT_Steps { get; private set; }
        public PinpadFixedLengthProperty<Nullable<int>> PRT_Horizontal { get; private set; }

        public PrtRequest()
        {
            this.CMD_LEN1 = new RegionProperty("CMD_LEN", 3);
            this.PRT_Action = new PinpadFixedLengthProperty<IngenicoPrinterAction>(
                "PRT_Action", 1, false, 
                DefaultStringFormatter.EnumStringFormatter<IngenicoPrinterAction>, 
                DefaultStringParser.EnumStringParser<IngenicoPrinterAction>);
            this.PRT_Size = new PinpadFixedLengthProperty<PrinterFontSize>(
                "PRT_Size", 1, true,
                DefaultStringFormatter.EnumStringFormatter<PrinterFontSize>,
                DefaultStringParser.EnumStringParser<PrinterFontSize>);
            this.PRT_Alignment = new PinpadFixedLengthProperty<PrinterAlignmentCode>(
                "PRT_Alignment", 1, true,
                DefaultStringFormatter.EnumStringFormatter<PrinterAlignmentCode>,
                DefaultStringParser.EnumStringParser<PrinterAlignmentCode>);
            this.PRT_DATA = new VariableLengthProperty<string>("PRT_DATA",
                3, 128, 1, false, true,
                DefaultStringFormatter.StringStringFormatter,
                DefaultStringParser.StringStringParser, null);
            this.PRT_Steps = new PinpadFixedLengthProperty<Nullable<int>>("PRT_Steps",
                4, true,
                DefaultStringFormatter.IntegerStringFormatter,
                DefaultStringParser.IntegerStringParser);
            this.PRT_Horizontal = new PinpadFixedLengthProperty<Nullable<int>>(
                "PRT_Horizontal", 4, true,
                DefaultStringFormatter.IntegerStringFormatter,
                DefaultStringParser.IntegerStringParser);

            this.StartRegion(this.CMD_LEN1);
            {
                this.AddProperty(this.PRT_Action);
                this.AddProperty(this.PRT_Size);
                this.AddProperty(this.PRT_Alignment);
                this.AddProperty(this.PRT_Steps);
                this.AddProperty(this.PRT_Horizontal);
                this.AddProperty(this.PRT_DATA);
            }
            this.EndLastRegion();
        }
    }
}
