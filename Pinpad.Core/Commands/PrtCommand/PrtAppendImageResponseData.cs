using Pinpad.Core.TypeCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pinpad.Core.Commands {
	/// <summary>
	/// Controller for PRT append image response action
	/// </summary>
	public class PrtAppendImageResponseData : BasePrtResponseData {
		/// <summary>
		/// Constructor
		/// </summary>
		public PrtAppendImageResponseData() {
		}

		/// <summary>
		/// Response Event
		/// </summary>
		public override PrinterActionCode PRT_ACTION {
			get {
				return PrinterActionCode.AppendImage;
			}
		}
	}
}
