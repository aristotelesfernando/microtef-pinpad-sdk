using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PinPadSDK.Controllers;
using PinPadSDK.Commands;
using PinPadSDK.Commands.Stone;
using PinPadSDK.Enums;
using PinPadSDK.PinPad;
using PinPadSDK.Property;
using PinPadSDK.Commands.Gpn;
using StonePortableUtils;

namespace PinPadSDK.PinPad {
    /// <summary>
    /// PinPad keyboard tool
    /// </summary>
    public class PinPadKeyboard {
        /// <summary>
        /// Owner Facade
        /// </summary>
        public PinPadFacade PinPad { get; private set; }

        private Lazy<bool> extendedKeysSupport;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="pinPad">PinPad to use</param>
        public PinPadKeyboard(PinPadFacade pinPad) {
            this.PinPad = pinPad;
            this.extendedKeysSupport = new Lazy<bool>(this.IsExtendedKeySupported);
        }

        /// <summary>
        /// Are the Extended key and clear buffer functions supported in the PinPad?
        /// </summary>
        public bool ExtendedKeySupported { get { return extendedKeysSupport.Value; } }

        private bool IsExtendedKeySupported( ) {
            if (this.PinPad.Communication.StoneVersion < new GkeRequest( ).MinimumStoneVersion) {
                return false;
            }
            else {
                return true;
            }
        }

        /// <summary>
        /// Gets the next Key pressed at the PinPad with the default, safe, method.
        /// Does not retrieve Numeric keys.
        /// </summary>
        /// <returns>PinPadKey or Undefined on failure</returns>
        public PinPadKey GetKey() {
            GkyResponse response = this.PinPad.Communication.SendRequestAndReceiveResponse<GkyResponse>(new GkyRequest());
            if (response == null) {
                return PinPadKey.Undefined;
            }
            else {
                return response.PressedKey;
            }
        }

        /// <summary>
        /// Gets the next Key at the PinPad buffer or the next Key pressed with the Stone proprietary command
        /// Only works with PinPads using Stone application
        /// Retrieves Numeric keys as well.
        /// </summary>
        /// <param name="keyboardLightOn">Turn the keyboard lights on if the PinPad has keyboard lights</param>
        /// <returns>PinPadKey or Undefined on failure</returns>
        public PinPadKey GetKeyExtended(bool keyboardLightOn = true) {
            GkeRequest request = new GkeRequest();

            if (keyboardLightOn == true) {
                request.GKE_ACTION.Value = GkeAction.GKE_ReadKey;
            }
            else {
                request.GKE_ACTION.Value = GkeAction.GKE_ReadKeyNoLight;
            }

            GkeResponse response = this.PinPad.Communication.SendRequestAndReceiveResponse<GkeResponse>(request);
            if (response == null) {
                return PinPadKey.Undefined;
            }
            else {
                return response.PressedKey;
            }
        }

        /// <summary>
        /// Clears the PinPad key buffer from previously pressed keys
        /// Only works with PinPads using Stone application
        /// </summary>
        /// <returns>true if the buffer was cleared</returns>
        public bool ClearKeyBuffer( ) {
            GkeRequest request = new GkeRequest( );
            request.GKE_ACTION.Value = GkeAction.GKE_ClearBuffer;

            return this.PinPad.Communication.SendRequestAndVerifyResponseCode(request);
        }

        /// <summary>
        /// Gets the PinBlock and KeySerialNumber of a card using DUKPT mode
        /// </summary>
        /// <param name="cryptographyMode">Cryptography Mode</param>
        /// <param name="keyIndex">PinPad Key Index</param>
        /// <param name="pan">Card Pan</param>
        /// <param name="pinMinLength">Card Pin Minimum length</param>
        /// <param name="pinMaxLength">Card Pin Maximum length</param>
        /// <param name="message">Pin Entry Message</param>
        /// <param name="pinBlock">Retrieved pin block or null on failure</param>
        /// <param name="ksn">Retrieved key serial number of null on failure</param>
        /// <returns>true on success, false on failure</returns>
        public bool GetDukptPin(CryptographyMode cryptographyMode, int keyIndex, string pan, int pinMinLength, int pinMaxLength, SimpleMessage message, out HexadecimalData pinBlock, out HexadecimalData ksn) {
            GpnRequest request = new GpnRequest();
            request.GPN_METHOD.Value = new CryptographyMethod(KeyManagementMode.DerivedUniqueKeyPerTransaction, cryptographyMode);
            request.GPN_KEYIDX.Value = keyIndex;
            request.GPN_WKENC.Value = new HexadecimalData(new byte[0]);
            request.GPN_PAN.Value = pan;

            GpnPinEntryRequest entry = new GpnPinEntryRequest();
            entry.GPN_MIN.Value = pinMinLength;
            entry.GPN_MAX.Value = pinMaxLength;
            entry.GPN_MSG.Value = message;

            request.GPN_ENTRIES.Value.Add(entry);

           GpnResponse response = this.PinPad.Communication.SendRequestAndReceiveResponse<GpnResponse>(request);
           if (response == null) {
               pinBlock = null;
               ksn = null;
               return false;
           }
           else {
               pinBlock = response.GPN_PINBLK.Value;
               ksn = response.GPN_KSN.Value;
               return true;
           }
        }
    }
}
