using PinPadSDK.Commands;
using PinPadSDK.Controllers;
using PinPadSDK.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PinPadSDK.Tools
{
    public class PinPadKeysBooleanInput
    {
        private PinPadConnectionController WriteTool;
        public string Display;

        public PinPadKeysBooleanInput(PinPadConnectionController writeTool, string display, string display2 = null)
        {
            WriteTool = writeTool;
            if (display2 != null)
                Display = display.PadRight(16, ' ').Substring(0, 16) + display2;
            else
                Display = display;
        }

        public bool ReadInput()
        {
            WriteTool.Display(Display);

            List<PinPadKeyEnum> KeyList = new List<PinPadKeyEnum>();
            PinPadKeyEnum NewKey;
            while ((NewKey = PinPadKeysReader.ReadKey(WriteTool)) != PinPadKeyEnum.OK && NewKey != PinPadKeyEnum.CANCEL)
            {
                WriteTool.Display(Display);
            }
            if (NewKey == PinPadKeyEnum.OK)
                return true;
            else
                return false;
        }
    }
}
