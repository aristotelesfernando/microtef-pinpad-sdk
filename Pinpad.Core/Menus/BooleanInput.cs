using PinPadSDK.Controllers;
using PinPadSDK.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PinPadSDK.Property;
using PinPadSDK.PinPad;

namespace PinPadSDK.Menus {
    /// <summary>
    /// Boolean Input Handler
    /// </summary>
    public class BooleanInput {
        /// <summary>
        /// PinPad used
        /// </summary>
        public PinPadFacade PinPad { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="pinPad">PinPadFacade</param>
        public BooleanInput(PinPadFacade pinPad) {
            this.PinPad = pinPad;
        }

        /// <summary>
        /// Prompts the user for a boolean input,
        /// Return, green button, for true
        /// Cancel, red button, for false
        /// </summary>
        /// <param name="message">Message to display to user</param>
        /// <returns>true for return button, false for cancel button</returns>
        public bool ReadInput(SimpleMessage message) {
            this.PinPad.Display.DisplayMessage(message);

            List<PinPadKey> KeyList = new List<PinPadKey>( );
            PinPadKey NewKey;
            while ((NewKey = this.PinPad.Keyboard.GetKey( )) != PinPadKey.Return && NewKey != PinPadKey.Cancel) {
                this.PinPad.Display.DisplayMessage(message);
            }
            if (NewKey == PinPadKey.Return) {
                return true;
            }
            else {
                return false;
            }
        }
    }
}
