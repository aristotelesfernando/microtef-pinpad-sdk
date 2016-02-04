using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PinPadSDK.Enums {
    /// <summary>
    /// Enumerator for online transaction status
    /// Since undefined is 0 every value will be the actual code plus 1
    /// </summary>
    public enum OnlineTransactionStatus {
        /// <summary>
        /// Null
        /// </summary>
        Undefined = 0,

        /// <summary>
        /// Transaction was approved
        /// </summary>
        Approved = 1,

        /// <summary>
        /// Transaction was denied by the card
        /// </summary>
        DeniedByCard = 2,

        /// <summary>
        /// Transaction was denied by the acquirer
        /// </summary>
        DeniedByAcquirer = 3,
    }
}
