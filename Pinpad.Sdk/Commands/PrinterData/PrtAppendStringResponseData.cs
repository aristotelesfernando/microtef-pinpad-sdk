using Pinpad.Sdk.TypeCode;

namespace Pinpad.Sdk.Commands
{
	/// <summary>
	/// Controller for PRT append string response action
	/// </summary>
	public class PrtAppendStringResponseData : BasePrtResponseData
	{
		/// <summary>
		/// Constructor
		/// </summary>
		public PrtAppendStringResponseData() { }

		/// <summary>
		/// Response Event
		/// </summary>
		public override PrinterActionCode PRT_ACTION { get { return PrinterActionCode.AppendString; } }
	}
}
