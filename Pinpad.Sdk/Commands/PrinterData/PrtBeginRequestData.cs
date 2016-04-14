using Pinpad.Sdk.TypeCode;

namespace Pinpad.Sdk.Commands
{
	/// <summary>
	/// Controller for PRT begin request action
	/// </summary>
	public class PrtBeginRequestData : BasePrtRequestData 
	{
		/// <summary>
		/// Response Event
		/// </summary>
		public override PrinterActionCode PRT_ACTION { get { return PrinterActionCode.Begin; } }

		/// <summary>
		/// Constructor
		/// </summary>
		public PrtBeginRequestData() { }
	}
}
