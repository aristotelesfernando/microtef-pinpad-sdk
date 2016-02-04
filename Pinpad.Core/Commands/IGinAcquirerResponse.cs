using PinPadSDK.Controllers;
using PinPadSDK.Property;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PinPadSDK.Commands {
    /// <summary>
    /// Interface for a GIN Response for requiring Acquirer informations
    /// </summary>
    public interface IGinAcquirerResponse {
        /// <summary>
        /// Name of the acquirer at the requested index
        /// </summary>
        PinPadFixedLengthPropertyController<string> GIN_ACQNAME { get; }

        /// <summary>
        /// ABECS application version with format "VVV.VV YYMMDD"
        /// </summary>
        PinPadFixedLengthPropertyController<string> GIN_APPVERS { get; }

        /// <summary>
        /// Specification version with format "V.vv" or "VvvA"
        /// Where V is the major version
        /// v is the minor version
        /// A is the alphanumeric modifier
        /// Example: "108a", "1.07"
        /// </summary>
        PinPadFixedLengthPropertyController<string> GIN_SPECVER { get; }
    }
}
