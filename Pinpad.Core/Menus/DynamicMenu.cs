using PinPadSDK.Controllers;
using PinPadSDK.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PinPadSDK.PinPad;
using PinPadSDK.Property;

namespace PinPadSDK.Menus
{
    /// <summary>
    /// Menu Handler that iterates through items in a dynamic way until the user selects one or cancels the operation
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DynamicMenu<T>{
        /// <summary>
        /// PinPad used
        /// </summary>
        public PinPadFacade PinPad { get; private set; }

        private const string navigationString = "< F1          F2 >";

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="pinPad">PinPad to use</param>
        public DynamicMenu(PinPadFacade pinPad)
        {
            this.PinPad = pinPad;
        }

        /// <summary>
        /// Makes the user iterates through items until he selects one or cancel the operation
        /// </summary>
        /// <param name="firstItem">First item to display</param>
        /// <param name="getNextItem">Function to get the next item</param>
        /// <param name="getPreviousItem">Function to get the previous item</param>
        /// <param name="getItemDisplay">Function to get the item display</param>
        /// <param name="headerLineCollection">Item display header</param>
        /// <returns>Selected item or default value if cancelled by the user</returns>
        public T ReadInput(T firstItem, Func<T, T> getNextItem, Func<T, T> getPreviousItem, Func<T, string[]> getItemDisplay, params string[] headerLineCollection) {
            T lastItem = firstItem;
            T item = firstItem;
            Display(item, getItemDisplay, headerLineCollection);

            PinPadKey key;
            while ((key = this.PinPad.Keyboard.GetKey( )) != PinPadKey.Return && key != PinPadKey.Cancel) {
                lastItem = item;
                if (key == PinPadKey.Function1) {
                    item = getPreviousItem(item);
                }
                else if (key == PinPadKey.Function2) {
                    item = getNextItem(item);
                }

                Display(item, getItemDisplay, headerLineCollection);
            }

            if (key == PinPadKey.Cancel) {
                return default(T);
            }
            else {
                return item;
            }
        }

        private void Display(T item, Func<T, string[]> getItemDisplay, string[] headerLineCollection) {
            List<string> Lines = new List<string>( );
            Lines.AddRange(headerLineCollection);
            Lines.AddRange(getItemDisplay(item));

            while (Lines.Count < 8) {
                Lines.Add("");
            }

            Lines.Add(DynamicMenu<T>.navigationString);

            List<string> DisplayRange = Lines.GetRange(Lines.Count - 9, 9);

            this.PinPad.Display.DisplayMessage(new MultilineMessage(DisplayRange.ToArray( )));
        }
    }
}
