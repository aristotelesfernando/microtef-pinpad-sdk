using Pinpad.Sdk.TypeCode;

namespace Pinpad.Sdk.Commands
{
	/// <summary>
	/// Controller for PRT end response action
	/// </summary>
	public class PrtEndResponseData : BasePrtResponseData 
	{
		/// <summary>
		/// Response Event
		/// </summary>
		public override PrinterActionCode PRT_ACTION { get { return PrinterActionCode.End; } }

		/// <summary>
		/// Constructor
		/// </summary>
		public PrtEndResponseData() { }

	}
}
