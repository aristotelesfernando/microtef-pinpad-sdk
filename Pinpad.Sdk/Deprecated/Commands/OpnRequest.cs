using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PinPadSDK.Commands {
    /// <summary>
    /// OPN request
    /// </summary>
    public class OpnRequest : BaseCommand {
        /// <summary>
        /// Constructor
        /// </summary>
        public OpnRequest() {
        }

        /// <summary>
        /// Name of the command
        /// </summary>
        public override string CommandName { get { return "OPN"; } }
    }
}
