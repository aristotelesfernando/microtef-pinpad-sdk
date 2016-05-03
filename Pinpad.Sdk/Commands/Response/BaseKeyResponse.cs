using Pinpad.Sdk.Model;
using Pinpad.Sdk.TypeCode;
using System;

namespace Pinpad.Sdk.Commands 
{
	/// <summary>
	/// Base Controller for PinPad responses that have a PinpadKey embedded into the RSP_STAT.
	/// </summary>
	public abstract class BaseKeyResponse : BaseResponse
	{
		/// <summary>
		/// Gets or Sets the key returned from the command
		/// </summary>
		public PinpadKeyCode PressedKey
		{
			get
			{
				int ResponseCode = (int)this.RSP_STAT.Value;

				if (Enum.IsDefined(typeof(PinpadKeyCode), ResponseCode) == true)
				{
					return (PinpadKeyCode)this.RSP_STAT.Value;
				}
				else
				{
					return PinpadKeyCode.Undefined;
				}
			}
			set
			{
				this.RSP_STAT.Value = (AbecsResponseStatus)value;
			}
		}
	}
}
