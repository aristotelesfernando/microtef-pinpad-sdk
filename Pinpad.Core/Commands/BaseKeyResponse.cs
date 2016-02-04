using PinPadSDK.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PinPadSDK.Commands {
    /// <summary>
    /// Base Controller for PinPad responses that have a PinPadKey embedded into the RSP_STAT
    /// </summary>
    public abstract class BaseKeyResponse : BaseResponse {
        /// <summary>
        /// Gets or Sets the key returned from the command
        /// </summary>
        public PinPadKey PressedKey {
            get {
                int ResponseCode = (int)this.RSP_STAT.Value;
                if (Enum.IsDefined(typeof(PinPadKey), ResponseCode) == true) {
                    return (PinPadKey)this.RSP_STAT.Value;
                }
                else {
                    return PinPadKey.Undefined;
                }
            }
            set {
                this.RSP_STAT.Value = (ResponseStatus)value;
            }
        }
    }
}
