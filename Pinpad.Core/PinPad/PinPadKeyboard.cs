﻿using Pinpad.Core.Commands;
using Pinpad.Core.Commands.Gpn;
using Pinpad.Core.Commands.Stone;
using Pinpad.Core.Properties;
using Pinpad.Core.TypeCode;
using Pinpad.Core.Utilities;
using Pinpad.Sdk.Model;
using Pinpad.Sdk.Model.TypeCode;
using System;

namespace Pinpad.Core.Pinpad
{
	/// <summary>
	/// Pinpad keyboard tool
	/// </summary>
	public class PinpadKeyboard : IPinpadKeyboard
	{
		// Members:
		/// <summary>
		/// Pinpad communication adapter
		/// </summary>
		public PinpadCommunication Communication { get; private set; }
		internal PinpadInfos Informations { get; private set; }
		private Lazy<bool> extendedKeysSupport;
		/// <summary>
		/// Are the Extended key and clear buffer functions supported in the Pinpad?
		/// </summary>
		public bool ExtendedKeySupported { get { return extendedKeysSupport.Value; } }

		// Constructor:
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="pinPad">Pinpad to use</param>
		public PinpadKeyboard(PinpadCommunication communication, PinpadInfos infos)
		{
			this.Communication = communication;
			this.Informations = infos;
			this.extendedKeysSupport = new Lazy<bool>(this.IsExtendedKeySupported);
		}

		// Methods:
		private bool IsExtendedKeySupported()
		{
			if (this.Communication.StoneVersion < new GkeRequest( ).MinimumStoneVersion)
			{
				return false;
			}
			else { return true; }
		}
		/// <summary>
		/// Gets the next Key pressed at the Pinpad with the default, safe, method.
		/// Does not retrieve Numeric keys.
		/// </summary>
		/// <returns>PinpadKey or Undefined on failure</returns>
		public PinpadKeyCode GetKey()
		{
			// Tries to read a key from the keyboard:
			GkyResponse response = this.Communication.SendRequestAndReceiveResponse<GkyResponse>(new GkyRequest());

			// If did not receive any response, returns an undefined key code:
			if (response == null) { return PinpadKeyCode.Undefined; }

			// If a key was pressed, returns it's value:
			else { return response.PressedKey; }
		}
		/// <summary>
		/// Gets the next Key at the Pinpad buffer or the next Key pressed with the Stone proprietary command
		/// Only works with Pinpads using Stone application
		/// Retrieves Numeric keys as well.
		/// </summary>
		/// <param name="keyboardLightOn">Turn the keyboard lights on if the Pinpad has keyboard lights</param>
		/// <returns>PinpadKey or Undefined on failure</returns>
		public PinpadKeyCode GetKeyExtended(bool keyboardLightOn = true)
		{
			GkeRequest request = new GkeRequest();

			if (keyboardLightOn == true)
			{
				request.GKE_ACTION.Value = GkeActionCode.ReadKey;
			}
			else
			{
				request.GKE_ACTION.Value = GkeActionCode.ReadKeyNoLight;
			}

			GkeResponse response = this.Communication.SendRequestAndReceiveResponse<GkeResponse>(request);
			if (response == null)
			{
				return PinpadKeyCode.Undefined;
			}
			else
			{
				return response.PressedKey;
			}
		}
		/// <summary>
		/// Clears the Pinpad key buffer from previously pressed keys
		/// Only works with Pinpads using Stone application
		/// </summary>
		/// <returns>true if the buffer was cleared</returns>
		public bool ClearKeyBuffer()
		{
			// Create the reques to clear the buffer:
			GkeRequest request = new GkeRequest( );
			request.GKE_ACTION.Value = GkeActionCode.ClearBuffer;

			// Clear pinpad's buffer:
			return this.Communication.SendRequestAndVerifyResponseCode(request);
		}
		/// <summary>
		/// Gets the PinBlock and KeySerialNumber of a card using DUKPT mode
		/// </summary>
		/// <param name="cryptographyMode">Cryptography Mode</param>
		/// <param name="keyIndex">Pinpad Key Index</param>
		/// <param name="pan">Card Pan</param>
		/// <param name="pinMinLength">Card Pin Minimum length</param>
		/// <param name="pinMaxLength">Card Pin Maximum length</param>
		/// <param name="message">Pin Entry Message</param>
		/// <param name="pinBlock">Retrieved pin block or null on failure</param>
		/// <param name="ksn">Retrieved key serial number of null on failure</param>
		/// <returns>true on success, false on failure</returns>
		public bool GetDukptPin(CryptographyMode cryptographyMode, int keyIndex, string pan, int pinMinLength, int pinMaxLength, SimpleMessage message, out HexadecimalData pinBlock, out HexadecimalData ksn)
		{
			// Creater GPN request:
			GpnRequest request = new GpnRequest();
			request.GPN_METHOD.Value = new CryptographyMethod(KeyManagementMode.DerivedUniqueKeyPerTransaction, cryptographyMode);
			request.GPN_KEYIDX.Value = keyIndex;
			request.GPN_WKENC.Value = new HexadecimalData(new byte[0]);
			request.GPN_PAN.Value = pan;

			// Settings to capture PIN:
			GpnPinEntryRequest entry = new GpnPinEntryRequest();
			entry.GPN_MIN.Value = pinMinLength;
			entry.GPN_MAX.Value = pinMaxLength;
			entry.GPN_MSG.Value = message;

			request.GPN_ENTRIES.Value.Add(entry);

			// Sends the request and gets it's response:
			GpnResponse response = this.Communication.SendRequestAndReceiveResponse<GpnResponse>(request);

			// If none response war received:
			if (response == null)
			{
				pinBlock = null;
				ksn = null;
				return false;
			}

			// If a response was received:
			else
			{
				pinBlock = response.GPN_PINBLK.Value;
				ksn = response.GPN_KSN.Value;
				return true;
			}
		}
		/// <summary>
		/// Gets a numeric input from pinpad keyboard.
		/// Minimum length 1 character; maximum length 3 characters.
		/// </summary>
		/// <param name="firstLine">First line label.</param>
		/// <param name="secondLine">Second line label.</param>
		/// <param name="timeOut">Time out.</param>
		/// <returns>Input from the keyboard. Null if nothing was received, whether of timeout or cancellation.</returns>
		public Nullable<int> GetNumericInput(GertecMessageInFirstLineCode firstLine, GertecMessageInSecondLineCode secondLine, int timeOut)
		{
			int v1, v2, v3;

			if (this.Informations.ManufacturerName.Contains("GERTEC") == false) { return null; }

			if (this.Informations.Model.Contains("MOBI PIN 10") == true)
			{
				string [] v = this.Informations.ManufacturerVersion.Trim().Split('.', ' ');

				if (v.Length != 3) { return null; }

				if (Int32.TryParse(v [0], out v1) == true && Int32.TryParse(v [1], out v2) == true && Int32.TryParse(v [2], out v3) == true)
				{
					if (v1 < 1 && v2 < 11 && v3 < 160311)
					{
						return null;
					}
				}
				else
				{
					return null;
				}
			}
			else { return null; }

			GertecEx07Request request = new GertecEx07Request();

			request.NumericInputType.Value = GertecEx07NumberFormat.Decimal;
			request.TextInputType.Value = GertecEx07TextFormat.None;
			request.LabelFirstLine.Value = firstLine;
			request.LabelSecondLine.Value = secondLine;
			//request.MaximumCharacterLength.Value = minimumLength;
			//request.MinimumCharacterLength.Value = maximumLength;
			request.MaximumCharacterLength.Value = 1;
			request.MinimumCharacterLength.Value = 3;
			request.TimeOut.Value = timeOut;
			request.TimeIdle.Value = 0;

			GertecEx07Response response = this.Communication.SendRequestAndReceiveResponse<GertecEx07Response>(request);

			if (response.RSP_STAT.Value != AbecsResponseStatus.ST_OK) { return null; }

			if (response.RSP_RESULT.HasValue == true)
			{
				int input;

				if (Int32.TryParse(response.RSP_RESULT.Value, out input) == true)
				{
					return input;
				}

				return null;
			}

			return 0;
		}
	}
}
