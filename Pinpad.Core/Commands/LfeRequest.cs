using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pinpad.Core.Commands {
    /// <summary>
    /// LFE request
    /// </summary>
    public class LfeRequest : BaseStoneRequest {
        /// <summary>
        /// Constructor
        /// </summary>
        public LfeRequest() {
        }

        /// <summary>
        /// Minimum stone version required for the request
        /// </summary>
        public override int MinimumStoneVersion { get { return 1; } }

        /// <summary>
        /// Name of the command
        /// </summary>
        public override string CommandName { get { return "LFE"; } }
    }
}
