using PinPadSDK.Commands;
using PinPadSDK.Controllers;
using PinPadSDK.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PinPadSDK.Tools
{
    public delegate string[] LongFormatHandler(long value);

    public class PinPadKeysNumericInput
    {
        private PinPadConnectionController WriteTool;
        public long MinValue;
        public long MaxValue;

        public PinPadKeysNumericInput(PinPadConnectionController writeTool, long minValue = Int64.MinValue, long maxValue = Int64.MaxValue)
        {
            WriteTool = writeTool;
            MinValue = minValue;
            MaxValue = maxValue;
        }

        public static string[] DefaultIntegerFormatHandler(long value)
        {
            return DataTools.BreakString(value.ToString(), 18).ToArray();
        }

        public long? ReadInput()
        {
            return ReadInput(DefaultIntegerFormatHandler);
        }

        public long? ReadInput(LongFormatHandler integerFormatHandler)
        {
            WriteTool.DisplayEx(integerFormatHandler(0));
            PinPadKeysReader.ClearKeyExtendedBuffer(WriteTool);

            List<PinPadKeyEnum> KeyList = new List<PinPadKeyEnum>();
            PinPadKeyEnum NewKey;
            while (((NewKey = PinPadKeysReader.ReadKeyExtended(WriteTool)) != PinPadKeyEnum.OK || PinPadKeysTools.GetLong(KeyList) < MinValue) && NewKey != PinPadKeyEnum.CANCEL)
            {
                if (PinPadKeysTools.IsNumeric(NewKey))
                {
                    KeyList.Add(NewKey);
                    try
                    {
                        if (PinPadKeysTools.GetLong(KeyList) > MaxValue)
                            KeyList.RemoveAt(KeyList.Count - 1);
                    }
                    catch (OverflowException)
                    {
                        KeyList.RemoveAt(KeyList.Count - 1);
                    }
                }
                else if (NewKey == PinPadKeyEnum.BACK)
                    if(KeyList.Count > 0)
                        KeyList.RemoveAt(KeyList.Count - 1);

                WriteTool.DisplayEx(integerFormatHandler(PinPadKeysTools.GetLong(KeyList)));
            }
            PinPadKeysReader.ClearKeyExtendedBuffer(WriteTool);
            if (NewKey == PinPadKeyEnum.CANCEL)
                return null;
            return PinPadKeysTools.GetLong(KeyList);
        }
    }
}
