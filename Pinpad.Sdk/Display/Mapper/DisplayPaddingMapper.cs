﻿using Pinpad.Core.TypeCode;
using Pinpad.Sdk.Model.TypeCode;

namespace Pinpad.Sdk.Display.Mapper
{
    internal class DisplayPaddingMapper
    {
        /// <summary>
        /// Translates enumerator from Pinpad.Core to Pinpad.Sdk enumerator.
        /// </summary>
        /// <param name="paddingType"><see cref="Pinpad.Sdk.Display.DisplayPaddingType">Pinpad.Sdk padding enumerator.</see></param>
        /// <returns><see cref="Pinpad.Core.Enums.PaddingType">Pinpad.Core corresponding enumerator.</see></returns>
        internal static PaddingType MapPaddingType(DisplayPaddingType paddingType)
        {
            switch (paddingType)
            {
                case DisplayPaddingType.Left: return PaddingType.Left;
                case DisplayPaddingType.Center: return PaddingType.Center;
                case DisplayPaddingType.Right: return PaddingType.Right;

                default: return PaddingType.Undefined;
            }
        }
    }
}
