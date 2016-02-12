using Pinpad.Core.Properties;
using Pinpad.Core.TypeCode;

/* WARNING!
 * 
 * DEPRECATED.
 * MUST BE REFACTORED.
 * 
 */

namespace Pinpad.Core.Commands
{
	/// <summary>
	/// Controller for PRT append string request action
	/// </summary>
	public class PrtAppendStringRequestData : BasePrtRequestData 
	{
		// Members
		/// <summary>
		/// Response Event
		/// </summary>
		public override PrinterActionCode PRT_ACTION { get { return PrinterActionCode.AppendString; } }
		/// <summary>
		/// Size of the font to use
		/// </summary>
		public PinpadFixedLengthProperty<PrinterStringSize> PRT_SIZE { get; private set; }
		/// <summary>
		/// Message alignment
		/// </summary>
		public PinpadFixedLengthProperty<PrinterAlignmentCode> PRT_ALIGNMENT { get; private set; }
		/// <summary>
		/// Message to append
		/// </summary>
		public VariableLengthProperty<string> PRT_MSG { get; private set; }
		
		// Constructor
		/// <summary>
		/// Constructor
		/// </summary>
		public PrtAppendStringRequestData() 
		{
			this.PRT_SIZE = new PinpadFixedLengthProperty<PrinterStringSize>("PRT_SIZE", 1, false, DefaultStringFormatter.EnumStringFormatter<PrinterStringSize>, DefaultStringParser.EnumStringParser<PrinterStringSize>);
			this.PRT_ALIGNMENT = new PinpadFixedLengthProperty<PrinterAlignmentCode>("PRT_ALIGNMENT", 1, false, DefaultStringFormatter.EnumStringFormatter<PrinterAlignmentCode>, DefaultStringParser.EnumStringParser<PrinterAlignmentCode>);
			this.PRT_MSG = new VariableLengthProperty<string>("PRT_MSG", 3, 512, 1.0f, false, false, DefaultStringFormatter.StringStringFormatter, DefaultStringParser.StringStringParser);

			this.AddProperty(this.PRT_SIZE);
			this.AddProperty(this.PRT_ALIGNMENT);
			this.AddProperty(this.PRT_MSG);
		}
	}
}
