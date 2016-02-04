using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pinpad.Sdk.Model.TypeCode
{
    /// <summary>
    /// Defines text position in an pinpad screen output.
    /// </summary>
    public enum DisplayPaddingType
    {
        /// <summary>
        /// Undefined type of text alignment on the pinpad screen.
        /// </summary>
        Undefined = 0,
        /// <summary>
        /// The text will be aligned at left side of the pinpad screen.
        /// </summary>
        Left = 1,
        /// <summary>
        /// The text will be centralized on the pinpad screen.
        /// </summary>
        Center = 2,
        /// <summary>
        /// The text will be aligned at right side of the pinpad screen.
        /// </summary>
        Right = 3
    }
}
