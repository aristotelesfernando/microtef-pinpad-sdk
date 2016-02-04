using PinPadSDK.Commands;
using PinPadSDK.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PinPadSDK.Tools
{
    public class PinPadKeysTools
    {
        public static string TranslateKeyCode(PinPadKeyEnum KeyCode)
        {
            switch (KeyCode)
            {
                case PinPadKeyEnum.OK: return "OK/ENTRA";
                case PinPadKeyEnum.F1: return "F1";
                case PinPadKeyEnum.F2: return "F2";
                case PinPadKeyEnum.F3: return "F3";
                case PinPadKeyEnum.F4: return "F4";
                case PinPadKeyEnum.BACK: return "LIMPA";
                case PinPadKeyEnum.CANCEL: return "CANCELA";
                case PinPadKeyEnum.D0: return "0";
                case PinPadKeyEnum.D1: return "1";
                case PinPadKeyEnum.D2: return "2";
                case PinPadKeyEnum.D3: return "3";
                case PinPadKeyEnum.D4: return "4";
                case PinPadKeyEnum.D5: return "5";
                case PinPadKeyEnum.D6: return "6";
                case PinPadKeyEnum.D7: return "7";
                case PinPadKeyEnum.D8: return "8";
                case PinPadKeyEnum.D9: return "9";
                default: throw new UnknownPinPadKeysException(KeyCode);
            }
        }

        public static bool IsNumeric(PinPadKeyEnum KeyCode)
        {
            switch (KeyCode)
            {
                case PinPadKeyEnum.D0:
                case PinPadKeyEnum.D1:
                case PinPadKeyEnum.D2:
                case PinPadKeyEnum.D3:
                case PinPadKeyEnum.D4:
                case PinPadKeyEnum.D5:
                case PinPadKeyEnum.D6:
                case PinPadKeyEnum.D7:
                case PinPadKeyEnum.D8:
                case PinPadKeyEnum.D9:
                    return true;
                default:
                    return false;
            }
        }

        public static long GetLong(PinPadKeyEnum KeyCode)
        {
            switch (KeyCode)
            {
                case PinPadKeyEnum.D0: return 0;
                case PinPadKeyEnum.D1: return 1;
                case PinPadKeyEnum.D2: return 2;
                case PinPadKeyEnum.D3: return 3;
                case PinPadKeyEnum.D4: return 4;
                case PinPadKeyEnum.D5: return 5;
                case PinPadKeyEnum.D6: return 6;
                case PinPadKeyEnum.D7: return 7;
                case PinPadKeyEnum.D8: return 8;
                case PinPadKeyEnum.D9: return 9;
                default: throw new UnknownPinPadKeysException(KeyCode);
            }
        }

        public static long GetLong(IList<PinPadKeyEnum> KeyCodes)
        {
            long Output = 0;

            for (int i = KeyCodes.Count - 1; i >= 0; i--)
            {
                Output += GetLong(KeyCodes[i]) * Convert.ToInt64(Math.Pow(10, KeyCodes.Count - i - 1));
            }

            return Output;
        }

        public static long GetLong(params PinPadKeyEnum[] KeyCodes)
        {
            long Output = 0;

            for (int i = KeyCodes.Length - 1; i >= 0; i--)
            {
                Output += GetLong(KeyCodes[i]) * Convert.ToInt64(Math.Pow(10, KeyCodes.Length - i - 1));
            }

            return Output;
        }

        public static string GetString(PinPadKeyEnum KeyCode)
        {
            switch (KeyCode)
            {
                case PinPadKeyEnum.D0: return "0";
                case PinPadKeyEnum.D1: return "1";
                case PinPadKeyEnum.D2: return "2";
                case PinPadKeyEnum.D3: return "3";
                case PinPadKeyEnum.D4: return "4";
                case PinPadKeyEnum.D5: return "5";
                case PinPadKeyEnum.D6: return "6";
                case PinPadKeyEnum.D7: return "7";
                case PinPadKeyEnum.D8: return "8";
                case PinPadKeyEnum.D9: return "9";
                default: throw new UnknownPinPadKeysException(KeyCode);
            }
        }

        public static string GetString(IList<PinPadKeyEnum> KeyCodes)
        {
            string Output = "";

            for (int i = KeyCodes.Count - 1; i >= 0; i--)
            {
                Output = GetString(KeyCodes[i]) + Output;
            }

            return Output;
        }

        public static string GetString(params PinPadKeyEnum[] KeyCodes)
        {
            string Output = "";

            for (int i = KeyCodes.Length - 1; i >= 0; i--)
            {
                Output = GetString(KeyCodes[i]) + Output;
            }

            return Output;
        }
    }

    public class UnknownPinPadKeysException : System.Exception
    {
        public UnknownPinPadKeysException() : base() { }
        public UnknownPinPadKeysException(string message) : base(message) { }
        public UnknownPinPadKeysException(string message, System.Exception inner) : base(message, inner) { }
        public UnknownPinPadKeysException(PinPadKeyEnum KeyCode) : base("Unknown PinPadKeys: " + ((int)KeyCode).ToString()) { }
    }
}
