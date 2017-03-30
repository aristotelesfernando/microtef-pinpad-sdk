using Pinpad.Sdk.Commands;
using Pinpad.Sdk.Properties;
using Pinpad.Sdk.Model;
using System;
using System.Globalization;
using Pinpad.Sdk.Utilities;
using Pinpad.Sdk.Model.Utilities;
using Pinpad.Sdk.Model.TypeCode;

namespace Pinpad.Sdk
{
    /// <summary>
    /// Pinpad keyboard tool
    /// </summary>
    public sealed class PinpadKeyboard : IPinpadKeyboard
	{
        // Properties
        /// <summary>
        /// It contains methods to select data through the pinpad.
        /// </summary>
        public IDataPicker DataPicker { get; set; }

        // Members:
        /// <summary>
        /// Pinpad communication adapter
        /// </summary>
        public PinpadCommunication Communication { get; private set; }
		internal IPinpadInfos Informations { get; private set; }

        // Constructor:
        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="communication">Pinpad communication, through which is possible to communicate with the pinpad.</param>
        /// <param name="infos">Pinpad informations.</param>
        /// <param name="display">Pinpad display, through which is possible to use with the display operations in data picker.</param>
        public PinpadKeyboard(PinpadCommunication communication, IPinpadInfos infos, IPinpadDisplay display)
        {
            this.Communication = communication;
            this.Informations = infos;
            this.DataPicker = new DataPicker(this, infos, display);
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
		/// <param name="minimumLength">Minimum number of characters typed.</param>
		/// <param name="maximumLength">Maximum number of characters typed.</param>
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

			if (response == null || response.RSP_STAT.Value != AbecsResponseStatus.ST_OK) { return null; }

			if (response.RSP_RESULT.HasValue == true)
			{
				return response.RSP_RESULT.Value;
			}

			return null;
		}
		/// <summary>
		/// Get's a generic input.
		/// </summary>
		/// <param name="numericInput">Numeric input code.</param>
		/// <param name="textInput">Text input code.</param>
		/// <param name="firstLine">First line label.</param>
		/// <param name="secondLine">Second line label.</param>
		/// <param name="minimumLength">Minimum number of characters typed.</param>
		/// <param name="maximumLength">Maximum number of characters typed (up to 32 chars).</param>
		/// <param name="timeOut">Time out.</param>
		/// <returns>Text typed.</returns>
		public string GetInput (KeyboardNumberFormat numericInput, KeyboardTextFormat textInput, FirstLineLabelCode firstLine, SecondLineLabelCode secondLine, int minimumLength, int maximumLength, int timeOut)
		{
			if (GciGertecRequest.IsSupported(this.Informations.ManufacturerName, this.Informations.Model, this.Informations.ManufacturerVersion) == false)
			{ return null; }

			if (minimumLength < 0)
			{ minimumLength = 0; }

			if (maximumLength > 32)
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

			if (response == null || response.RSP_STAT.Value != AbecsResponseStatus.ST_OK) { return null; }

			if (response.RSP_RESULT.HasValue == true)
			{
				return response.RSP_RESULT.Value;
			}

			return null;
		}
		/// <summary>
		/// Gets a decimal amount.
		/// The amount shall be typed in the followed format: 
		///    - "1,99"
		///    - "0,00"
		/// Containing always at least 4 chars.
		/// </summary>
		/// <param name="currency">Amount currency, i. e. R$, US$.</param>
		/// <returns>The amount if a valid amount was typed. Null if: timeout, user cancelled, amount was typed on an invalid format (example: 1.7,2).</returns>
		public Nullable<decimal> GetAmount (AmountCurrencyCode currency)
		{
			if (GciGertecRequest.IsSupported(this.Informations.ManufacturerName, this.Informations.Model, this.Informations.ManufacturerVersion) == false) { return null; }

			if (currency == AmountCurrencyCode.Undefined || Enum.IsDefined(typeof(AmountCurrencyCode), (int) currency) == false)
			{
				throw new ArgumentException("currency has an invalid value.");
			}

			GciGertecRequest request = new GciGertecRequest();

			request.NumericInputType.Value = KeyboardNumberFormat.Decimal;
			request.TextInputType.Value = KeyboardTextFormat.Symbols;
			request.LabelFirstLine.Value = FirstLineLabelCode.Type;
			request.LabelSecondLine.Value = (SecondLineLabelCode) (currency + 30);
			request.MaximumCharacterLength.Value = 4;
			request.MinimumCharacterLength.Value = 10;
			request.TimeOut.Value = 120;
			request.TimeIdle.Value = 0;

			GertecEx07Response response = this.Communication.SendRequestAndReceiveResponse<GertecEx07Response>(request);

			if (response == null || response.RSP_STAT.Value != AbecsResponseStatus.ST_OK) { return null; }

			if (response.RSP_RESULT.HasValue == true)
			{
				return this.GetAmount(response.RSP_RESULT.Value);
			}

			return null;
		}

		private Nullable<decimal> GetAmount (string amountValue)
		{
			CultureInfo c = new CultureInfo("en-US");
			amountValue = amountValue.Replace(',', '.');
			decimal amount;

			if (Decimal.TryParse(amountValue, NumberStyles.Currency, c, out amount) == true)
			{
				return amount;
			}

			return null;
		}
    }
}
