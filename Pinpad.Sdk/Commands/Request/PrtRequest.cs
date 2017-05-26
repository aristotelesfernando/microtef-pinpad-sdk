using Pinpad.Sdk.Commands.Context;
using Pinpad.Sdk.Commands.TypeCode;
using Pinpad.Sdk.Model;
using Pinpad.Sdk.PinpadProperties.Refactor.Formatter;
using Pinpad.Sdk.PinpadProperties.Refactor.Parser;
using Pinpad.Sdk.PinpadProperties.Refactor.Property;
using System;

namespace Pinpad.Sdk.Commands.Request
{
    /// <summary>
    /// Print something through pinpad thermal printer.
    /// </summary>
    internal sealed class PrtRequest : PinpadProperties.Refactor.BaseCommand
    {
        /// <summary>
        /// Command name, PRT in this case.
        /// </summary>
        public override string CommandName { get { return "PRT"; } }
        /// <summary>
        /// ommand length, excluding itself.
        /// </summary>
        public RegionProperty CMD_LEN1 { get; private set; }
        /// <summary>
        /// Type of data to print.
        /// </summary>
        public FixedLengthProperty<IngenicoPrinterAction> PRT_Action { get; private set; }
        /// <summary>
        /// Size of the font or QR code.
        /// </summary>
        public FixedLengthProperty<PrinterFontSize> PRT_Size { get; private set; }
        /// <summary>
        /// Text alignment on receipt.
        /// </summary>
        public FixedLengthProperty<PrinterAlignmentCode> PRT_Alignment { get; private set; }
        /// <summary>
        /// Data to print (text or image).
        /// </summary>
        public VariableLengthProperty<string> PRT_DATA { get; private set; }
        /// <summary>
        /// Steps to skip.
        /// </summary>
        public FixedLengthProperty<Nullable<int>> PRT_Steps { get; private set; }
        /// <summary>
        /// Padding to be added at the left side of an image or QR code.
        /// Could be used to align the image or QR code.
        /// </summary>
        public FixedLengthProperty<Nullable<int>> PRT_Horizontal { get; private set; }

        /// <summary>
        /// Creates a PRT request and it's properties.
        /// </summary>
        public PrtRequest()
            : base(new IngenicoContext())
        {
            this.CMD_LEN1 = new RegionProperty("CMD_LEN", 3);
            this.PRT_Action = new FixedLengthProperty<IngenicoPrinterAction>(
                "PRT_Action", 1, false, 
                StringFormatter.EnumStringFormatter<IngenicoPrinterAction>, 
                StringParser.EnumStringParser<IngenicoPrinterAction>);
            this.PRT_Size = new FixedLengthProperty<PrinterFontSize>(
                "PRT_Size", 1, true,
                StringFormatter.EnumStringFormatter<PrinterFontSize>,
                StringParser.EnumStringParser<PrinterFontSize>);
            this.PRT_Alignment = new FixedLengthProperty<PrinterAlignmentCode>(
                "PRT_Alignment", 1, true,
                StringFormatter.EnumStringFormatter<PrinterAlignmentCode>,
                StringParser.EnumStringParser<PrinterAlignmentCode>);
            this.PRT_DATA = new VariableLengthProperty<string>(
                "PRT_DATA", 3, 512, 1, false, true,
                StringFormatter.StringStringFormatter,
                StringParser.StringStringParser, null);
            this.PRT_Steps = new FixedLengthProperty<Nullable<int>>(
                "PRT_Steps", 4, true,
                StringFormatter.IntegerStringFormatter,
                StringParser.IntegerStringParser);
            this.PRT_Horizontal = new FixedLengthProperty<Nullable<int>>(
                "PRT_Horizontal", 1, true,
                StringFormatter.IntegerStringFormatter,
                StringParser.IntegerStringParser);

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
