using Pinpad.Sdk.Properties;
using Pinpad.Sdk.TypeCode;

/* WARNING!
 * 
 * DEPRECATED.
 * MUST BE REFACTORED.
 * 
 */

namespace Pinpad.Sdk.Commands
{
	/// <summary>
	/// Base Prt Request Action
	/// </summary>
	public class BasePrtRequestData : BaseProperty 
	{
		// Members
		/// <summary>
		/// Action
		/// </summary>
		public virtual PrinterActionCode PRT_ACTION 
		{
			get 
			{
				if (this._PRT_ACTION != null) 
				{
					return this._PRT_ACTION.Value;
				}
				else 
				{
					return PrinterActionCode.Undefined;
				}
			}
		}
		private PinpadFixedLengthProperty<PrinterActionCode> _PRT_ACTION { get; set; }

		// Constructor
		/// <summary>
		/// Constructor
		/// </summary>
		public BasePrtRequestData() 
		{
			this._PRT_ACTION = new PinpadFixedLengthProperty<PrinterActionCode>("PRT_ACTION", 1, false, DefaultStringFormatter.EnumStringFormatter<PrinterActionCode>, DefaultStringParser.EnumStringParser<PrinterActionCode>, null, this.PRT_ACTION);

			this.AddProperty(this._PRT_ACTION);
		}
	}
}
