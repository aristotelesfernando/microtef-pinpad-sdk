using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PinPadSDK.Enums {
    /// <summary>
    /// Enumerator for acquirer connection status, to be used at FNC_COMMST
    /// Since undefined is 0 every value will be the actual code plus 1
    /// </summary>
    public enum AcquirerCommunicationStatus {
        /// <summary>
        /// Null
        /// </summary>
        Undefined = 0,

        /// <summary>
        /// Connection was successful and a valid response was received
        /// </summary>
        ConnectionSuccessful = 1,

        /// <summary>
        /// Connection failed
        /// </summary>
        ConnectionError = 2,

        /// <summary>
        /// Connection was successful, transaction was approved but Authorization Response Code was different than "00"
        /// </summary>
        ConnectionSuccessfulWithDifferentARC = 10
    }
}
