using PinPadSDK.Commands;
using PinPadSDK.Controllers;
using PinPadSDK.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PinPadSDK.Tools
{
    public class PinPadKeysNumericUpDown
    {
        private PinPadConnectionController WriteTool;
        public long MinValue;
        public long MaxValue;

        public PinPadKeysNumericUpDown(PinPadConnectionController writeTool, long minValue = Int64.MinValue, long maxValue = Int64.MaxValue)
        {
            WriteTool = writeTool;
            MinValue = minValue;
            MaxValue = maxValue;
        }

        public static string[] DefaultIntegerFormatHandler(long value)
        {
            List<string> Lines = new List<string>();
            Lines.AddRange(DataTools.BreakString(value.ToString(), 18));
            Lines.Add("(USE F1 e F2)");
            return Lines.ToArray();
        }

        public long? ReadInput()
        {
            return ReadInput(DefaultIntegerFormatHandler);
        }

        public long? ReadInput(LongFormatHandler integerFormatHandler)
        {
            if (WriteTool.GetStoneVersion() > 0)
                return ReadStoneInput(integerFormatHandler);
            else
                return ReadNonStoneInput(integerFormatHandler);
        }

        public long? ReadNonStoneInput(LongFormatHandler integerFormatHandler)
        {
            long CurrentValue = MinValue;
            WriteTool.DisplayEx(integerFormatHandler(CurrentValue));

            PinPadKeyEnum NewKey;
            while ((NewKey = PinPadKeysReader.ReadKey(WriteTool)) != PinPadKeyEnum.OK && NewKey != PinPadKeyEnum.CANCEL)
            {
                if (NewKey == PinPadKeyEnum.F1)
                {
                    CurrentValue--;
                    if (CurrentValue < MinValue)
                        CurrentValue = MinValue;
                }
                else if (NewKey == PinPadKeyEnum.F2)
                {
                    CurrentValue++;
                    if (CurrentValue > MaxValue)
                        CurrentValue = MaxValue;
                }

                WriteTool.DisplayEx(integerFormatHandler(CurrentValue));
            }
            if (NewKey == PinPadKeyEnum.CANCEL)
                return null;
            return CurrentValue;
        }

        public long? ReadStoneInput(LongFormatHandler integerFormatHandler)
        {
            long CurrentValue = 0;
            WriteTool.DisplayEx(integerFormatHandler(CurrentValue));

            PinPadKeyEnum NewKey;
            while (((NewKey = PinPadKeysReader.ReadKeyExtended(WriteTool)) != PinPadKeyEnum.OK || CurrentValue < MinValue) && NewKey != PinPadKeyEnum.CANCEL)
            {
                if (NewKey == PinPadKeyEnum.F1)
                {
                    CurrentValue--;
                    if (CurrentValue < MinValue)
                        CurrentValue = MinValue;
                }
                else if (NewKey == PinPadKeyEnum.F2)
                {
                    CurrentValue++;
                    if (CurrentValue > MaxValue)
                        CurrentValue = MaxValue;
                }
                else if (NewKey == PinPadKeyEnum.BACK)
                {
                    CurrentValue /= 10;
                    if (CurrentValue < 0)
                        CurrentValue = 0;
                }
                else if (PinPadKeysTools.IsNumeric(NewKey))
                {
                    long OldValue = CurrentValue;
                    CurrentValue *= 10;
                    CurrentValue += PinPadKeysTools.GetLong(NewKey);
                    if (CurrentValue > MaxValue)
                        CurrentValue = OldValue;
                }

                WriteTool.DisplayEx(integerFormatHandler(CurrentValue));
            }
            if (NewKey == PinPadKeyEnum.CANCEL)
                return null;
            return CurrentValue;
        }
    }
}
