using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PinPadSDK.Commands.Stone {
    /// <summary>
    /// GBS request
    /// </summary>
    public class GbsRequest : BaseStoneRequest {
        /// <summary>
        /// Constructor
        /// </summary>
        public GbsRequest() {
        }

        /// <summary>
        /// Minimum stone version required for the request
        /// </summary>
        public override int MinimumStoneVersion { get { return 1; } }

        /// <summary>
        /// Name of the command
        /// </summary>
        public override string CommandName { get { return "GBS"; } }
    }
}
