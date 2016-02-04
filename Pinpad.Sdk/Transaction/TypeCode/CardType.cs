using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pinpad.Sdk.Transaction.TypeCode
{
    /// <summary>
    /// Enumerator specifying each card type supported by the application.
    /// </summary>
    public enum CardType
    {
        /// <summary>
        /// Undefined (that is, invalid) card type.
        /// </summary>
        Undefined = 0,
        /// <summary>
        /// Cartão com chip
        /// </summary>
        Emv = 1,
        /// <summary>
        /// Cartão com tarja magnética
        /// </summary>
        MagneticStripe = 2
    }
}
