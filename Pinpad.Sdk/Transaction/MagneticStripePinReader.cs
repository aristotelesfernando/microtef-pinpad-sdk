using Pinpad.Sdk.Commands;
using System;
using Pinpad.Sdk.Model;
using Pinpad.Sdk.Properties;
using Pinpad.Sdk.Transaction;

namespace Pinpad.Sdk
{
	internal class MagneticStripePinReader
	{
		// Constants
		/// <summary>
		/// Amount label presented on pinpad display.
		/// </summary>
		private const string AMOUNT_LABEL   = "VALOR:";
		/// <summary>
		/// Password label presented on pinpad display.
		/// </summary>
		private const string PASSWORD_LABEL = "SENHA:";
		/// <summary>
		/// Brazilian language culture.
		/// </summary>
		private const string BRAZILIAN_CULTURE = "pt-BR";

		// Methods
		/// <summary>
		/// Read security information when card has no chip - magnetic stripe only.
		/// </summary>
		/// <param name="pan">Primary Account Number printed on the card.</param>
		/// <param name="amount">Transaction amount.</param>
		/// <returns>Wheter is an online transaction or not.</returns>
		/// <exception cref="System.InvalidOperationException">Thrown when parameter validation fails.</exception>
		internal ResponseStatus Read(PinpadCommunication pinpadCommunication, string pan, decimal amount, out Pin pin)
		{
			pin = new Pin();

			// Validating data
			try { this.Validate(pinpadCommunication, amount, pan); }
			catch (Exception ex)
			{
				throw new InvalidOperationException("Error while trying to read security info using magnetic stripe. Verify inner exception.", ex);
			}

			// Using ABECS GPN command to get pin block & KSN.
			GpnResponse response = this.SendGpn(pinpadCommunication, pan, amount);

			// Saving command response status:
			AbecsResponseStatus legacyStatus = response.RSP_STAT.Value;
			ResponseStatus status = ResponseStatusMapper.MapLegacyResponseStatus(legacyStatus);

			if (status == ResponseStatus.Ok)
			{
				// Magnetic stripe always validates pin online.
				pin.PinBlock = response.GPN_PINBLK.Value.DataString;
				pin.KeySerialNumber = response.GPN_KSN.Value.DataString;
				pin.IsOnline = true;
			}

			pin.ApplicationCryptogram = null;

			return status;
		}
		/// <summary>
		/// Sends reading command using ABECS in case of a magnetic stripe card.
		/// Unexpected behavior if amount is too large (over quadrillion).
		/// </summary>
		/// <param name="pan">Primary Account Number printed on the card.</param>
		/// <param name="amount">Transaction amount.</param>
		/// <returns>ABECS GPN command response.</returns>
		private GpnResponse SendGpn(PinpadCommunication pinpadCommunication, string pan, decimal amount)
		{
			GpnRequest request = new GpnRequest();

			// Assembling GPN command.
			request.GPN_METHOD.Value = new CryptographyMethod(KeyManagementMode.DerivedUniqueKeyPerTransaction, CryptographyMode.TripleDataEncryptionStandard);
			request.GPN_KEYIDX.Value = (int)StoneIndexCode.EncryptionKey;
			request.GPN_WKENC.Value = new HexadecimalData("");
			request.GPN_PAN.Value = pan;

			// Assembling GPN mandatory entries to GPN command.
			GpnPinEntryRequest pinEntry = new GpnPinEntryRequest();
			
			pinEntry.GPN_MIN.Value = PinReader.PASSWORD_MINIMUM_LENGTH;
			pinEntry.GPN_MAX.Value = PinReader.PASSWORD_MAXIMUM_LENGTH;
			pinEntry.GPN_MSG.Value.Padding = DisplayPaddingType.Left;
			pinEntry.GPN_MSG.Value.FirstLine = this.GetAmountLabel(amount);
			pinEntry.GPN_MSG.Value.SecondLine = PASSWORD_LABEL;

			// Adds the entry to list of entries supported by a GPN command.
			request.GPN_ENTRIES.Value.Add(pinEntry);

			GpnResponse response = pinpadCommunication.SendRequestAndReceiveResponse<GpnResponse>(request);
			return response;
		}
		/// <summary>
		/// Responsible for returning a formatted string to be shown on the pinpad screen when the cardholder should input his password.
		/// If the formatted string is too large so it overtake pinpad linewidth, then returns only the transaction amount.
		/// This method DOES NOT treat this situation: if the amount itself overtakes pinpads linewidth. (behavior unexpected)
		/// </summary>
		/// <param name="amount">Transaction amount, on decimal format.</param>
		/// <returns>Formatted string to be shown on pinpad screen when the cardholder should input his password.</returns>
		private string GetAmountLabel(decimal amount)
		{
			string amountLabel = AMOUNT_LABEL;
			
			// Gets formatted amount, based on BRAZIL culture.
			string amountStr = amount.ToString("0.00");

			// Gets the number of white spaces needed to align the amount on right side of pinpad screen.
			int alignmentLength = PinpadDisplay.DISPLAY_LINE_WIDTH - (AMOUNT_LABEL.Length + amountStr.Length);

			if (alignmentLength < 0)
			{
				// Amount label consists only on amount transaction (the amount is too high to show "VALOR:" label).
				amountLabel = amountStr;
			}
			else
			{
				// Amount label contains "VALOR:" label followed by transaction amount.
				amountLabel = amountLabel.PadRight(amountLabel.Length + alignmentLength, ' ');
				amountLabel = amountLabel.Insert(amountLabel.Length, amountStr);
			}

			return amountLabel;
		}

		// Validation
		/// <summary>
		/// Validates parameters used on internal processing.
		/// </summary>
		/// <exception cref="System.ArgumentNullException">When one parameter is null.</exception>
		/// <exception cref="System.ArgumentException">When one parameter is not null, but contains invalid data.</exception>
		private void Validate(PinpadCommunication pinpadCommunication, decimal amount, string pan)
		{
			if (pinpadCommunication == null) { throw new ArgumentNullException("pinpadFacade cannot be null. Unable to communicate with the pinpad."); }
			if (amount <= 0) { throw new ArgumentException("amount should be greater than 0."); }
			if (pan == null) { throw new ArgumentNullException("pan"); }
			if (pan.Length <= 0) { throw new ArgumentException("pan should not be empty."); }
		}
	}
}
