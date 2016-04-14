using Pinpad.Sdk.TypeCode;

namespace Pinpad.Sdk.Commands
{
	/// <summary>
	/// Controller for PRT end request action
	/// </summary>
	public class PrtEndRequestData : BasePrtRequestData
	{
		/// <summary>
		/// Constructor
		/// </summary>
		public PrtEndRequestData() { }

		/// <summary>
		/// Response Event
		/// </summary>
		public override PrinterActionCode PRT_ACTION { get { return PrinterActionCode.End; } }
	}
}
