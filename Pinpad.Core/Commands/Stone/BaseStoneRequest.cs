using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PinPadSDK.Commands.Stone {
    /// <summary>
    /// Base Controller for Stone command Requests
    /// </summary>
    public abstract class BaseStoneRequest : BaseCommand {
        /// <summary>
        /// Minimum stone version required for the request
        /// </summary>
        public abstract int MinimumStoneVersion { get; }
    }
}
