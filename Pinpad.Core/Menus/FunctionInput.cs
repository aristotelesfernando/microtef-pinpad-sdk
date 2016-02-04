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
    /// Function key based input handler
    /// </summary>
    public class FunctionInput {
        /// <summary>
        /// PinPad used
        /// </summary>
        public PinPadFacade PinPad { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="pinPad">PinPadFacade</param>
        public FunctionInput(PinPadFacade pinPad)
        {
            this.PinPad = pinPad;
        }

        /// <summary>
        /// Reads the input using Function keys to directly select the item
        /// For example: F1 would return item1, if item1 is not null, otherwise it will prompt for a key again
        /// If cancel key is pressed null is returned
        /// </summary>
        /// <param name="item1">First item, or null to skip</param>
        /// <param name="item2">Second item, or null to skip</param>
        /// <param name="item3">Third item, or null to skip</param>
        /// <param name="item4">Forth item, or null to skip</param>
        /// <param name="headerLineCollection">Display Header lines</param>
        /// <returns>Selected item or null if cancelled</returns>
        public string ReadInput(
            string item1 = null,
            string item2 = null,
            string item3 = null,
            string item4 = null,
            params string[] headerLineCollection
            ) {
            string[] choices = new string[4] { item1, item2, item3, item4 };

            int validChoices = choices.Count((item) => {
                return String.IsNullOrEmpty(item) == false;
            });

            if (validChoices < 2) {
                throw new InvalidOperationException("At least 2 valid items must be used");
            }

            DisplayItems(choices, headerLineCollection);

            List<PinPadKey> keyCollection = new List<PinPadKey>( );
            PinPadKey key;
            while ((key = this.PinPad.Keyboard.GetKey( )) != PinPadKey.Cancel) {
                if (PinPadKeyExtensionMethods.IsFunctionKey(key) == true) {
                    int index = PinPadKeyExtensionMethods.GetFunctionKeyIndex(key) - 1;
                    if (String.IsNullOrEmpty(choices[index]) == false) {
                        return choices[index];
                    }
                }

                DisplayItems(choices, headerLineCollection);
            }
            return null;
        }

        private void DisplayItems(string[] choices, string[] headerLineCollection) {
            List<string> lineCollection = new List<string>( );

            foreach (string line in headerLineCollection) {
                lineCollection.Add(line);
            }

            for (int i=0; i<4; i++) {
                if (String.IsNullOrEmpty(choices[i]) == false) {
                    lineCollection.Add("F" + (i+1).ToString("d1") + ": " + choices[i]);
                }
            }

            this.PinPad.Display.DisplayMessage(new MultilineMessage(lineCollection.ToArray( )));
        }
    }
}
