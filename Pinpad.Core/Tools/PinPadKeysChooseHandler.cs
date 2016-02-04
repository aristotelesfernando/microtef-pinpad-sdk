using PinPadSDK.Commands;
using PinPadSDK.Controllers;
using PinPadSDK.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PinPadSDK.Tools
{
    public class PinPadKeysChooseHandler
    {
        private PinPadConnectionController WriteTool;
        public List<string> Header;
        public string[] MenuItems;

        public PinPadKeysChooseHandler(PinPadConnectionController writeTool, string Item1, string Item2)
        {
            WriteTool = writeTool;

            Header = new List<string>();

            MenuItems = new string[2];
            MenuItems[0] = Item1;
            MenuItems[1] = Item2;
        }

        public void SetHeader(params string[] headerLines)
        {
            Header.Clear();
            if (headerLines.Length > 0)
            {
                Header.AddRange(headerLines);
            }
        }

        public string ReadInput()
        {
            DisplayItems();

            List<PinPadKeyEnum> KeyList = new List<PinPadKeyEnum>();
            PinPadKeyEnum NewKey;
            while ((NewKey = PinPadKeysReader.ReadKey(WriteTool)) != PinPadKeyEnum.CANCEL)
            {
                if (NewKey == PinPadKeyEnum.F1)
                {
                    return MenuItems[0];
                }
                else if (NewKey == PinPadKeyEnum.F2)
                {
                    return MenuItems[1];
                }

                DisplayItems();
            }
            return null;
        }

        public void DisplayItems()
        {
            List<string> Lines = new List<string>();
            foreach (string line in Header)
                Lines.Add(line);
            Lines.Add("F1: " + MenuItems[0]);
            Lines.Add("F2: " + MenuItems[1]);
            WriteTool.DisplayEx(Lines.ToArray());
        }
    }
}
