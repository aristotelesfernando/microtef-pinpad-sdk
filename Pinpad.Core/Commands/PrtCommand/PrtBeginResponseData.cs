using Pinpad.Core.Properties;
using Pinpad.Core.TypeCode;
using Pinpad.Sdk.Model.TypeCode;

namespace Pinpad.Core.Commands {
	/// <summary>
	/// Controller for PRT begin response action
	/// </summary>
	public class PrtBeginResponseData : BasePrtResponseData {
		/// <summary>
		/// Constructor
		/// </summary>
		public PrtBeginResponseData() {
			this.PRT_STATUS = new PinpadFixedLengthProperty<PinpadPrinterStatus>("PRT_STATUS", 3, false, DefaultStringFormatter.EnumStringFormatter<PinpadPrinterStatus>, DefaultStringParser.EnumStringParser<PinpadPrinterStatus>);
		   
			this.AddProperty(this.PRT_STATUS);
		}

		/// <summary>
		/// Response Event
		/// </summary>
		public override PrinterActionCode PRT_ACTION {
			get {
				return PrinterActionCode.Begin;
			}
		}

		/// <summary>
		/// Adds to the vertical offset of the next print buffer append
		/// </summary>
		public PinpadFixedLengthProperty<PinpadPrinterStatus> PRT_STATUS { get; private set; }
	}
}
