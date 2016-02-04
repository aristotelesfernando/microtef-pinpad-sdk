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
    /// Uses the default, safe, display to iterate through items using F1 and F2, Return to select a item and Cancel to cancel the operation
    /// </summary>
    public class MenuHandler {
        /// <summary>
        /// PinPad used
        /// </summary>
        public PinPadFacade PinPad { get; private set; }

        private const string navigationString = "< F1        F2 >";

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="pinPad">PinPad to use</param>
        public MenuHandler(PinPadFacade pinPad)
        {
            this.PinPad = pinPad;
        }

        /// <summary>
        /// Prompts user to select a item
        /// </summary>
        /// <param name="itemCollection">items to choose from</param>
        /// <returns>selected item or null if cancelled</returns>
        public string ReadInput(params string[] itemCollection)
        {
            if (itemCollection.Length <= 1) {
                throw new ArgumentOutOfRangeException("At least one item must be provided");
            }

            int index = 0;
            PinPadKey key;

            this.PinPad.Display.DisplayMessage(new SimpleMessage(itemCollection[index], navigationString));

            while ((key = this.PinPad.Keyboard.GetKey()) != PinPadKey.Return && key != PinPadKey.Cancel)
            {
                switch (key) {
                    case PinPadKey.Function1:
                        index--;
                        if (index < 0) {
                            index = itemCollection.Length - 1;
                        }
                        break;

                    case PinPadKey.Function2:
                        index++;
                        if (index >= itemCollection.Length) {
                            index = 0;
                        } break;
                }

                this.PinPad.Display.DisplayMessage(new SimpleMessage(itemCollection[index], navigationString));
            }

            if (key == PinPadKey.Cancel) {
                return null;
            }

            return itemCollection[index];
        }
    }
}
