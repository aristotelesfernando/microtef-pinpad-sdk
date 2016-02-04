using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PinPadSDK.Controllers;
using PinPadSDK.Commands;
using PinPadSDK.Property;
using PinPadSDK.Enums;
using PinPadSDK.PinPad;
using PinPadSDK.Commands.Stone;

namespace PinPadSDK.PinPad {
    /// <summary>
    /// PinPad display tool
    /// </summary>
    public class PinPadDisplay {
        /// <summary>
        /// Owner Facade
        /// </summary>
        public PinPadFacade PinPad { get; private set; }

        private Lazy<bool> imagesSupported { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="pinPad">PinPad to use</param>
        public PinPadDisplay(PinPadFacade pinPad) {
            this.PinPad = pinPad;
            this.imagesSupported = new Lazy<bool>(this.AreImagesSupported);
        }

        private bool AreImagesSupported( ) {
            return this.PinPad.Communication.StoneVersion >= new DsiRequest( ).MinimumStoneVersion;
        }

        /// <summary>
        /// Are image related commands supported?
        /// </summary>
        public bool ImagesSupported {
            get {
                return this.imagesSupported.Value;
            }
        }

        /// <summary>
        /// Displays a message in the PinPad display with the default, safe, method
        /// </summary>
        /// <param name="message">Message to display</param>
        /// <returns>true if message is displayed in the PinPad</returns>
        public bool DisplayMessage(SimpleMessage message) {
            DspRequest request = new DspRequest( );
            request.DSP_MSG.Value = message;

            return this.PinPad.Communication.SendRequestAndVerifyResponseCode(request);
        }

        /// <summary>
        /// Displays a message in the PinPad display with the unmanaged, unsafe, method
        /// The message is not garanteed to be displayed as expected, as each PinPad may have a different display size
        /// </summary>
        /// <param name="message">Message to display</param>
        /// <returns>true if message is displayed in the PinPad</returns>
        public bool DisplayMessage(MultilineMessage message) {
            DexRequest request = new DexRequest( );
            request.DEX_MSG.Value = message;

            return this.PinPad.Communication.SendRequestAndVerifyResponseCode(request);
        }

        /// <summary>
        /// Display a previously loaded image
        /// </summary>
        /// <param name="imageName">Image name, up to 15 characters</param>
        /// <returns>true if the message was displayed</returns>
        public bool DisplayImage(string imageName) {
            DsiRequest request = new DsiRequest( );
            request.DSI_IMGNAME.Value = imageName;

            return this.PinPad.Communication.SendRequestAndVerifyResponseCode(request);
        }
    }
}
