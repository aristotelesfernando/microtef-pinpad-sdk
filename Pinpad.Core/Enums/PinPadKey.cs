using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PinPadSDK.Enums {
    /// Since undefined is 0 every value will be the actual code plus 1
    public enum PinPadKey {
        /// <summary>
        /// Null
        /// </summary>
        Undefined = 0,

        /// <summary>
        /// Ok/Return key
        /// </summary>
        Return = 1,

        /// <summary>
        /// Function 1 key
        /// </summary>
        Function1 = 5,

        /// <summary>
        /// Function 2 key
        /// </summary>
        Function2 = 6,

        /// <summary>
        /// Function 3 key
        /// </summary>
        Function3 = 7,

        /// <summary>
        /// Function 4 key
        /// </summary>
        Function4 = 8,

        /// <summary>
        /// Backspace key
        /// </summary>
        Backspace = 9,

        /// <summary>
        /// Cancel key
        /// </summary>
        Cancel = 14,

        /// <summary>
        /// Decimal 0 key
        /// </summary>
        Decimal0 = 49,

        /// <summary>
        /// Decimal 1 key
        /// </summary>
        Decimal1 = 50,

        /// <summary>
        /// Decimal 2 key
        /// </summary>
        Decimal2 = 51,

        /// <summary>
        /// Decimal 3 key
        /// </summary>
        Decimal3 = 52,

        /// <summary>
        /// Decimal 4 key
        /// </summary>
        Decimal4 = 53,

        /// <summary>
        /// Decimal 5 key
        /// </summary>
        Decimal5 = 54,

        /// <summary>
        /// Decimal 6 key
        /// </summary>
        Decimal6 = 55,

        /// <summary>
        /// Decimal 7 key
        /// </summary>
        Decimal7 = 56,

        /// <summary>
        /// Decimal 8 key
        /// </summary>
        Decimal8 = 57,

        /// <summary>
        /// Decimal 9 key
        /// </summary>
        Decimal9 = 58,
    }
}
