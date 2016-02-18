using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pinpad.Sdk.Model.TypeCode
{
    public enum PinpadContactlessMode
    {
        /// <summary>
        /// Null
        /// </summary>
        Undefined = 0,
        /// <summary>
        /// CTLS mode not supported
        /// </summary>
        Unsupported = 1,
        /// <summary>
        /// Visa Magnetic Stripe
        /// </summary>
        VisaMagneticStripe = 2,
        /// <summary>
        /// Visa chip
        /// </summary>
        VisaChip = 3,
        /// <summary>
        /// MasterCard PayPass for Magnetic Stripe
        /// </summary>
        MasterMagneticStripe = 4,
        /// <summary>
        /// MasterCard PayPass for m/Chip
        /// </summary>
        MasterChip = 5,
        /// <summary>
        /// Amex Expresspay for Magnetic Stripe
        /// </summary>
        AmexMagneticStripe = 6,
        /// <summary>
        /// Amex Expresspay for EMV
        /// </summary>
        AmexChip = 7
    }
}
