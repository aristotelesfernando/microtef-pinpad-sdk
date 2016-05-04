﻿using Pinpad.Sdk.Commands;
using Pinpad.Sdk.Properties;
using Pinpad.Sdk.Model;
using System;

namespace Pinpad.Sdk
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
		internal IPinpadInfos Informations { get; private set; }

		// Constructor:
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="pinPad">Pinpad to use</param>
		public PinpadKeyboard(PinpadCommunication communication, IPinpadInfos infos)
		{
			this.Communication = communication;
			this.Informations = infos;
		}

		// Methods:
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
		public string GetNumericInput (FirstLineLabelCode firstLine, SecondLineLabelCode secondLine, int minimumLength, int maximumLength, int timeOut)
		{
			if (GciGertecRequest.IsSupported(this.Informations.ManufacturerName, this.Informations.Model, this.Informations.ManufacturerVersion) == false) { return null; }

			if (minimumLength < 0) { minimumLength = 0; }

			if (maximumLength > 35)
			{
				throw new InvalidOperationException("Invalid maximumLength. The maximum length is up to 32 characters.");
			}

			GciGertecRequest request = new GciGertecRequest();

			request.NumericInputType.Value = KeyboardNumberFormat.Decimal;
			request.TextInputType.Value = KeyboardTextFormat.None;
			request.LabelFirstLine.Value = firstLine;
			request.LabelSecondLine.Value = secondLine;
			request.MaximumCharacterLength.Value = minimumLength;
			request.MinimumCharacterLength.Value = maximumLength;
			request.TimeOut.Value = timeOut;
			request.TimeIdle.Value = 0;

			GertecEx07Response response = this.Communication.SendRequestAndReceiveResponse<GertecEx07Response>(request);

			if (response.RSP_STAT.Value != AbecsResponseStatus.ST_OK) { return null; }

			if (response.RSP_RESULT.HasValue == true)
			{
				return response.RSP_RESULT.Value;
			}

			return null;
		}

		public string GetText (KeyboardNumberFormat numericInput, KeyboardTextFormat textInput, FirstLineLabelCode firstLine, SecondLineLabelCode secondLine, int minimumLength, int maximumLength, int timeOut)
		{
			if (GciGertecRequest.IsSupported(this.Informations.ManufacturerName, this.Informations.Model, this.Informations.ManufacturerVersion) == false)
			{ return null; }

			if (minimumLength < 0)
			{ minimumLength = 0; }

			if (maximumLength > 35)
			{
				throw new InvalidOperationException("Invalid maximumLength. The maximum length is up to 32 characters.");
			}

			GciGertecRequest request = new GciGertecRequest();

			request.NumericInputType.Value = numericInput;
			request.TextInputType.Value = textInput;
			request.LabelFirstLine.Value = firstLine;
			request.LabelSecondLine.Value = secondLine;
			request.MaximumCharacterLength.Value = minimumLength;
			request.MinimumCharacterLength.Value = maximumLength;
			request.TimeOut.Value = timeOut;
			request.TimeIdle.Value = 0;

			GertecEx07Response response = this.Communication.SendRequestAndReceiveResponse<GertecEx07Response>(request);

			if (response.RSP_STAT.Value != AbecsResponseStatus.ST_OK)
			{ return null; }

			if (response.RSP_RESULT.HasValue == true)
			{
				return response.RSP_RESULT.Value;
			}

			return null;
		}
	}
}
