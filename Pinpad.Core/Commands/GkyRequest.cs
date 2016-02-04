using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PinPadSDK.Commands {
    /// <summary>
    /// GKY request
    /// </summary>
    public class GkyRequest : BaseCommand {
        /// <summary>
        /// Constructor
        /// </summary>
        public GkyRequest() {
        }

        /// <summary>
        /// Name of the command
        /// </summary>
        public override string CommandName { get { return "GKY"; } }
    }
}
