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
    /// Uses Stone Extended Keys to select a item
    /// </summary>
    public class NumericMenu {
        /// <summary>
        /// PinPad used
        /// </summary>
        public PinPadFacade PinPad { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="pinPad">PinPad to use</param>
        public NumericMenu(PinPadFacade pinPad)
        {
            this.PinPad = pinPad;
            if (this.PinPad.Keyboard.ExtendedKeySupported == false) {
                throw new InvalidOperationException("Extended Keys are not Supported for this PinPad");
            }
        }

        /// <summary>
        /// Prompts the user to select one of the items using a numeric key
        /// </summary>
        /// <param name="itemCollection">Items to choose from</param>
        /// <param name="headerLineCollection">Message header</param>
        /// <returns>selected item or null if cancelled by the user</returns>
        public string ReadInput(string[] itemCollection, params string[] headerLineCollection) {
            if (itemCollection.Length < 2) {
                throw new ArgumentOutOfRangeException("At least two items must be passed");
            }

            if (itemCollection.Length > 10) {
                throw new ArgumentOutOfRangeException("Only up to 10 items are supported.");
            }

            int index = 0;
            int menuPosition = 0;

            this.PinPad.Keyboard.ClearKeyBuffer( );
            DisplayItems(menuPosition, itemCollection, headerLineCollection);

            PinPadKey key;
            while ((key = this.PinPad.Keyboard.GetKeyExtended( )) != PinPadKey.Cancel) {
                if (key.IsNumeric( )) {
                    index = Convert.ToInt32(key.GetLong( )) - 1;
                    if (index == -1) {
                        index = 9;
                    }
                    if (index >= itemCollection.Length) {
                        continue;
                    }
                    else {
                        break;
                    }
                }
                else if (key == PinPadKey.Function1 || key == PinPadKey.Function3) {
                    menuPosition = UpdateMenuPosition(menuPosition+1, itemCollection, headerLineCollection);
                }
                else if (key == PinPadKey.Function2 || key == PinPadKey.Function4) {
                    menuPosition = UpdateMenuPosition(menuPosition-1, itemCollection, headerLineCollection);
                }

                DisplayItems(menuPosition, itemCollection, headerLineCollection);
            }

            if (key == PinPadKey.Cancel) {
                return null;
            }
            else {
                return itemCollection[index];
            }
        }

        private int UpdateMenuPosition(int value, string[] itemCollection, string[] headerLineCollection) {

            int fixedValue = value;

            int entryCount = headerLineCollection.Length + itemCollection.Length;

            if (entryCount <= 9) {
                fixedValue = 0;
            }
            else if (fixedValue > entryCount - 8) {
                fixedValue = entryCount - 8;
            }

            if (fixedValue < 0) {
                fixedValue = 0;
            }

            return fixedValue;
        }

        private void DisplayItems(int menuPosition, string[] itemCollection, string[] headerLineCollection)
        {
            List<string> lineCollection = new List<string>();

            foreach (string line in headerLineCollection) {
                lineCollection.Add(line);
            }

            for (int i = 1; i <= itemCollection.Length; i++)
            {
                int index = i - 1;
                int displayIndex = (i == 10) ? 0 : i;
                lineCollection.Add(displayIndex + ": " + itemCollection[index]);
            }

            List<string> displayLineCollection = new List<string>();
            if (lineCollection.Count > 9) {
                displayLineCollection.AddRange(lineCollection.GetRange(menuPosition, 8));
                displayLineCollection.Add("\\/ F1        F2 /\\");
            }
            else {
                displayLineCollection.AddRange(lineCollection);
            }

            this.PinPad.Display.DisplayMessage(new MultilineMessage(displayLineCollection.ToArray( )));
        }
    }
}
