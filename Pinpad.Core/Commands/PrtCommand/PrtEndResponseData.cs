using Pinpad.Core.TypeCode;

namespace Pinpad.Core.Commands
{
	/// <summary>
	/// Controller for PRT end response action
	/// </summary>
	public class PrtEndResponseData : BasePrtResponseData 
	{
		/// <summary>
		/// Response Event
		/// </summary>
		public override PrinterActionCode PRT_ACTION {
			get {
				return PrinterActionCode.End;
			}
		}

		/// <summary>
		/// Constructor
		/// </summary>
		public PrtEndResponseData() { }

	}
}
