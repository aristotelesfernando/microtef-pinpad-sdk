﻿using Pinpad.Core.TypeCode;

namespace Pinpad.Core.Commands
{
	/// <summary>
	/// Controller for PRT step response action
	/// </summary>
	public class PrtStepResponseData : BasePrtResponseData
	{
		/// <summary>
		/// Constructor
		/// </summary>
		public PrtStepResponseData() { }

		/// <summary>
		/// Response Event
		/// </summary>
		public override PrinterActionCode PRT_ACTION { get { return PrinterActionCode.Step; } }
	}
}
