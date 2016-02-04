using PinPadSDK.Commands;
using PinPadSDK.Controllers;
using PinPadSDK.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PinPadSDK.Tools
{
    public delegate string[] StringFormatHandler(string value);

    public class PinPadKeysStringInput
    {
        private PinPadConnectionController WriteTool;
        public int MinLength;
        public int MaxLength;

        public PinPadKeysStringInput(PinPadConnectionController writeTool, int minLength = 0, int maxLength = Int32.MaxValue)
        {
            WriteTool = writeTool;
            MinLength = minLength;
            MaxLength = maxLength;
        }

        public static string[] DefaultStringFormatHandler(string value)
        {
            return DataTools.BreakString(value, 18).ToArray();
        }

        public string ReadInput()
        {
            return ReadInput(DefaultStringFormatHandler);
        }

        public string ReadInput(StringFormatHandler stringFormatHandler)
        {
            WriteTool.DisplayEx(stringFormatHandler(""));

            List<PinPadKeyEnum> KeyList = new List<PinPadKeyEnum>();
            PinPadKeyEnum NewKey;
            while (((NewKey = PinPadKeysReader.ReadKeyExtended(WriteTool)) != PinPadKeyEnum.OK || PinPadKeysTools.GetString(KeyList).Length < MinLength) && NewKey != PinPadKeyEnum.CANCEL)
            {
                if (PinPadKeysTools.IsNumeric(NewKey))
                {
                    KeyList.Add(NewKey);
                    try
                    {
                        if (PinPadKeysTools.GetString(KeyList).Length > MaxLength)
                            KeyList.RemoveAt(KeyList.Count - 1);
                    }
                    catch (OverflowException)
                    {
                        KeyList.RemoveAt(KeyList.Count - 1);
                    }
                }
                else if (NewKey == PinPadKeyEnum.BACK)
                    if (KeyList.Count > 0)
                        KeyList.RemoveAt(KeyList.Count - 1);

                WriteTool.DisplayEx(stringFormatHandler(PinPadKeysTools.GetString(KeyList)));
            }
            if (NewKey == PinPadKeyEnum.CANCEL)
                return null;
            string Value = PinPadKeysTools.GetString(KeyList);
            return Value;
        }
    }
}
