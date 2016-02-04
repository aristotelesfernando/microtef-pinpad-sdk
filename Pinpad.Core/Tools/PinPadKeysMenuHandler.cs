using PinPadSDK.Commands;
using PinPadSDK.Controllers;
using PinPadSDK.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PinPadSDK.Tools
{
    public class PinPadKeysMenuHandler
    {
        private PinPadConnectionController WriteTool;
        public List<string> MenuItems;

        private const string NavigationString = "< F1        F2 >";

        public PinPadKeysMenuHandler(PinPadConnectionController writeTool, params string[] menuItems)
        {
            WriteTool = writeTool;
            MenuItems = new List<string>();
            if(menuItems.Length > 0)
                MenuItems.AddRange(menuItems);
        }

        public string ReadInput()
        {
            if (MenuItems.Count <= 0)
                return null;

            int Index = 0;

            WriteTool.Display(DataTools.CenterString(MenuItems[Index], 16), NavigationString);

            List<PinPadKeyEnum> KeyList = new List<PinPadKeyEnum>();
            PinPadKeyEnum NewKey;
            while ((NewKey = PinPadKeysReader.ReadKey(WriteTool)) != PinPadKeyEnum.OK && NewKey != PinPadKeyEnum.CANCEL)
            {
                if (NewKey == PinPadKeyEnum.F1)
                {
                    Index--;
                    if (Index < 0)
                        Index = MenuItems.Count - 1;
                }
                else if (NewKey == PinPadKeyEnum.F2)
                {
                    Index++;
                    if (Index >= MenuItems.Count)
                        Index = 0;
                }

                WriteTool.Display(DataTools.CenterString(MenuItems[Index], 16), NavigationString);
            }
            if (NewKey == PinPadKeyEnum.CANCEL)
                return null;
            return MenuItems[Index];
        }
    }
}
