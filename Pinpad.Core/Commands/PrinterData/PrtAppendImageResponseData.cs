using Pinpad.Core.TypeCode;

namespace Pinpad.Core.Commands
{
	/// <summary>
	/// Controller for PRT append image response action
	/// </summary>
	public class PrtAppendImageResponseData : BasePrtResponseData
    {
		/// <summary>
		/// Constructor
		/// </summary>
		public PrtAppendImageResponseData() { }

		/// <summary>
		/// Response Event
		/// </summary>
		public override PrinterActionCode PRT_ACTION { get { return PrinterActionCode.AppendImage; } }
	}
}
