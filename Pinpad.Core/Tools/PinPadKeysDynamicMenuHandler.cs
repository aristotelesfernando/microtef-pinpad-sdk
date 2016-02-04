using PinPadSDK.Commands;
using PinPadSDK.Controllers;
using PinPadSDK.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PinPadSDK.Tools
{
    public class PinPadKeysDynamicMenuHandler<T>
        where T : class
    {
        private PinPadConnectionController WriteTool;
        public List<string> Header;

        private Func<T, T> GetNextItem;
        private Func<T, T> GetPreviousItem;
        private Func<T, string[]> GetItemDisplay;

        private const string NavigationString = "< F1          F2 >";

        public PinPadKeysDynamicMenuHandler(PinPadConnectionController WriteTool, Func<T, T> GetNextItem, Func<T, T> GetPreviousItem, Func<T, string[]> GetItemDisplay)
        {
            this.WriteTool = WriteTool;
            this.GetNextItem = GetNextItem;
            this.GetPreviousItem = GetPreviousItem;
            this.GetItemDisplay = GetItemDisplay;

            Header = new List<string>();
        }

        public void Display(T item)
        {
            List<string> Lines = new List<string>();
            Lines.AddRange(Header);
            Lines.AddRange(GetItemDisplay(item));

            while (Lines.Count < 8)
                Lines.Add("");

            Lines.Add(NavigationString);

            List<string> DisplayRange = Lines.GetRange(Lines.Count - 9, 9);

            WriteTool.DisplayEx(DisplayRange.ToArray());
        }

        public T ReadInput(T FirstItem)
        {
            T LastItem = FirstItem;
            T Item = FirstItem;
            Display(Item);

            PinPadKeyEnum NewKey;
            while ((NewKey = PinPadKeysReader.ReadKey(WriteTool)) != PinPadKeyEnum.OK && NewKey != PinPadKeyEnum.CANCEL)
            {
                LastItem = Item;
                if (NewKey == PinPadKeyEnum.F1)
                {
                    Item = GetPreviousItem(Item);
                }
                else if (NewKey == PinPadKeyEnum.F2)
                {
                    Item = GetNextItem(Item);
                }
                if (Item == null)
                    Item = LastItem;

                Display(Item);
            }
            if (NewKey == PinPadKeyEnum.CANCEL)
                return null;
            return Item;
        }
    }
}
