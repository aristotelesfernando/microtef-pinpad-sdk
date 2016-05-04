using Pinpad.Sdk.Model;
using Pinpad.Sdk.TypeCode;
using System;

namespace Pinpad.Sdk.Commands
{
    /// <summary>
    /// GKY response
    /// </summary>
    internal class GkyResponse : BaseResponse
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public GkyResponse() {  }

        /// <summary>
        /// Is this a blocking command?
        /// </summary>
        public override bool IsBlockingCommand { get { return true; } }

        /// <summary>
        /// Name of the command
        /// </summary>
        public override string CommandName { get { return "GKY"; } }

		/// <summary>
		/// Gets or Sets the key returned from the command
		/// </summary>
		public PinpadKeyCode PressedKey
		{
			get
			{
				int ResponseCode = (int) this.RSP_STAT.Value;

				if (Enum.IsDefined(typeof(PinpadKeyCode), ResponseCode) == true)
				{
					return (PinpadKeyCode) this.RSP_STAT.Value;
				}
				else
				{
					return PinpadKeyCode.Undefined;
				}
			}
			set
			{
				this.RSP_STAT.Value = (AbecsResponseStatus) value;
			}
		}
	}
}

