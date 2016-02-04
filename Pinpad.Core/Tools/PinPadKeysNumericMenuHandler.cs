using PinPadSDK.Commands;
using PinPadSDK.Controllers;
using PinPadSDK.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PinPadSDK.Tools
{
    public class PinPadKeysNumericMenuHandler
    {
        private PinPadConnectionController WriteTool;
        private int StartIndex;
        public int MenuDisplayPos
        {
            get { return StartIndex; }
            set
            {
                int Entries = Header.Count + MenuItems.Count;
                if (Entries <= 9)
                    value = 0;
                else if (value > Entries - 8)
                    value = Entries - 8;
                if (value < 0)
                    value = 0;
                StartIndex = value;
            }
        }
        public List<string> Header;
        public List<string> MenuItems;

        public PinPadKeysNumericMenuHandler(PinPadConnectionController writeTool, params string[] menuItems)
        {
            WriteTool = writeTool;
            StartIndex = 0;
            MenuItems = new List<string>();
            Header = new List<string>();
            if (menuItems.Length > 0)
            {
                if (menuItems.Length > 10)
                    MenuItems.AddRange(menuItems.Take(10));
                else
                    MenuItems.AddRange(menuItems);
            }
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
            if (MenuItems.Count <= 0)
                return null;
            PinPadKeysReader.ClearKeyExtendedBuffer(WriteTool);

            int Index = 0;

            DisplayItems();

            List<PinPadKeyEnum> KeyList = new List<PinPadKeyEnum>();
            PinPadKeyEnum NewKey;
            while ((NewKey = PinPadKeysReader.ReadKeyExtended(WriteTool)) != PinPadKeyEnum.CANCEL)
            {
                if (PinPadKeysTools.IsNumeric(NewKey))
                {
                    Index = Convert.ToInt32(PinPadKeysTools.GetLong(NewKey)) - 1;
                    if (Index < 0)
                        Index = 9;
                    if (Index < MenuItems.Count)
                        break;
                }
                else if (NewKey == PinPadKeyEnum.F1 || NewKey == PinPadKeyEnum.F3)
                {
                    MenuDisplayPos++;
                }
                else if (NewKey == PinPadKeyEnum.F2 || NewKey == PinPadKeyEnum.F4)
                {
                    MenuDisplayPos--;
                }

                DisplayItems();
            }
            PinPadKeysReader.ClearKeyExtendedBuffer(WriteTool);
            if (NewKey == PinPadKeyEnum.CANCEL)
                return null;
            return MenuItems[Index];
        }

        public void DisplayItems()
        {
            List<string> Lines = new List<string>();
            foreach (string line in Header)
                Lines.Add(line);
            for (int i = 1; i <= 10; i++)
            {
                int ItemIndex = i - 1;
                if (ItemIndex >= MenuItems.Count)
                    break;
                int DisplayIndex = (i == 10) ? 0 : i;
                Lines.Add(DisplayIndex.ToString() + ": " + MenuItems[ItemIndex]);
            }
            List<string> DisplayLines = new List<string>();
            if (Lines.Count > 9)
            {
                DisplayLines.AddRange(Lines.GetRange(StartIndex, 8));
                DisplayLines.Add("\\/ F1        F2 /\\");
            }
            else
                DisplayLines.AddRange(Lines);
            WriteTool.DisplayEx(DisplayLines.ToArray());
        }
    }
}
