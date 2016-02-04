using PinPadSDK.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PinPadSDK.Exceptions;

namespace PinPadSDK.Menus
{
    /// <summary>
    /// Extension methods for PinPadKey
    /// </summary>
    public static class PinPadKeyExtensionMethods
    {
        /// <summary>
        /// Translate a PinPadKey to a string
        /// </summary>
        /// <param name="key">PinPadKey</param>
        /// <returns>PinPadKey string</returns>
        public static string Translate(this PinPadKey key)
        {
            switch (key)
            {
                case PinPadKey.Return: return "OK/ENTRA";
                case PinPadKey.Function1: return "F1";
                case PinPadKey.Function2: return "F2";
                case PinPadKey.Function3: return "F3";
                case PinPadKey.Function4: return "F4";
                case PinPadKey.Backspace: return "LIMPA";
                case PinPadKey.Cancel: return "CANCELA";
                case PinPadKey.Decimal0: return "0";
                case PinPadKey.Decimal1: return "1";
                case PinPadKey.Decimal2: return "2";
                case PinPadKey.Decimal3: return "3";
                case PinPadKey.Decimal4: return "4";
                case PinPadKey.Decimal5: return "5";
                case PinPadKey.Decimal6: return "6";
                case PinPadKey.Decimal7: return "7";
                case PinPadKey.Decimal8: return "8";
                case PinPadKey.Decimal9: return "9";
                default: throw new UnknownPinPadKeyException(key);
            }
        }

        /// <summary>
        /// Is the PinPadKey numeric?
        /// </summary>
        /// <param name="key">PinPadKey</param>
        /// <returns>true if numeric</returns>
        public static bool IsNumeric(this PinPadKey key)
        {
            switch (key)
            {
                case PinPadKey.Decimal0:
                case PinPadKey.Decimal1:
                case PinPadKey.Decimal2:
                case PinPadKey.Decimal3:
                case PinPadKey.Decimal4:
                case PinPadKey.Decimal5:
                case PinPadKey.Decimal6:
                case PinPadKey.Decimal7:
                case PinPadKey.Decimal8:
                case PinPadKey.Decimal9:
                    return true;
                default:
                    return false;
            }
        }

        /// <summary>
        /// Gets the long value of the PinPadKey
        /// </summary>
        /// <param name="key">PinPadKey</param>
        /// <returns>long</returns>
        public static long GetLong(this PinPadKey key)
        {
            switch (key)
            {
                case PinPadKey.Decimal0: return 0;
                case PinPadKey.Decimal1: return 1;
                case PinPadKey.Decimal2: return 2;
                case PinPadKey.Decimal3: return 3;
                case PinPadKey.Decimal4: return 4;
                case PinPadKey.Decimal5: return 5;
                case PinPadKey.Decimal6: return 6;
                case PinPadKey.Decimal7: return 7;
                case PinPadKey.Decimal8: return 8;
                case PinPadKey.Decimal9: return 9;
                default: throw new UnknownPinPadKeyException(key);
            }
        }

        /// <summary>
        /// Get the long value of a array of PinPadKeys
        /// </summary>
        /// <param name="keyCollection">PinPadKey array</param>
        /// <returns>long</returns>
        public static long GetLong(this PinPadKey[] keyCollection)
        {
            long Output = 0;

            for (int i = keyCollection.Length - 1; i >= 0; i--)
            {
                Output += GetLong(keyCollection[i]) * Convert.ToInt64(Math.Pow(10, keyCollection.Length - i - 1));
            }

            return Output;
        }

        /// <summary>
        /// Gets the function key index
        /// </summary>
        /// <param name="key">PinPadKey</param>
        /// <returns>int</returns>
        public static int GetFunctionKeyIndex(this PinPadKey key) {
            switch(key){
                case PinPadKey.Function1:
                    return 1;

                case PinPadKey.Function2:
                    return 2;

                case PinPadKey.Function3:
                    return 3;

                case PinPadKey.Function4:
                    return 4;

                default:
                    throw new UnknownPinPadKeyException(key);
            }
        }

        /// <summary>
        /// Is the PinPadKey a function key?
        /// </summary>
        /// <param name="key">PinPadKey</param>
        /// <returns>true if function key</returns>
        public static bool IsFunctionKey(this PinPadKey key) {
            switch (key) {
                case PinPadKey.Function1:
                case PinPadKey.Function2:
                case PinPadKey.Function3:
                case PinPadKey.Function4:
                    return true;

                default:
                    return false;
            }
        }

        /// <summary>
        /// Gets the string value of a numeric PinPadKey
        /// </summary>
        /// <param name="key">PinPadKey</param>
        /// <returns>string</returns>
        public static string GetString(this PinPadKey key)
        {
            switch (key)
            {
                case PinPadKey.Decimal0: return "0";
                case PinPadKey.Decimal1: return "1";
                case PinPadKey.Decimal2: return "2";
                case PinPadKey.Decimal3: return "3";
                case PinPadKey.Decimal4: return "4";
                case PinPadKey.Decimal5: return "5";
                case PinPadKey.Decimal6: return "6";
                case PinPadKey.Decimal7: return "7";
                case PinPadKey.Decimal8: return "8";
                case PinPadKey.Decimal9: return "9";
                default: throw new UnknownPinPadKeyException(key);
            }
        }
        
        /// <summary>
        /// Gets the string value of a array of PinPadKeys
        /// </summary>
        /// <param name="keyCollection">PinPadKey array</param>
        /// <returns>string</returns>
        public static string GetString(this PinPadKey[] keyCollection)
        {
            StringBuilder stringBuilder = new StringBuilder();

            for (int i = keyCollection.Length - 1; i >= 0; i--)
            {
                stringBuilder.Insert(0, GetString(keyCollection[i]));
            }

            return stringBuilder.ToString();
        }
    }

}
