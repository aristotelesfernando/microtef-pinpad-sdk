using Pinpad.Core.Pinpad;
using Pinpad.Sdk.Model;
using Pinpad.Sdk.Model.TypeCode;
using System;
using System.Diagnostics;
using ResponseStatus = Pinpad.Sdk.Model.TypeCode.ResponseStatus;

namespace Pinpad.Core.Transaction
{
	/// <summary>
	/// Responsible for getting pinpad security information (Pin Block and Key Serial Number(KSN)).
	/// </summary>
	internal class PinReader
	{
		// Constants
		/// <summary>
		/// Cardholder password minimum length.
		/// </summary>
		internal const short PASSWORD_MINIMUM_LENGTH = 4;
		/// <summary>
		/// Cardholder maximum length.
		/// </summary>
		internal const short PASSWORD_MAXIMUM_LENGTH = 12;

		// Members
		/// <summary>
		/// Facade through which pinpad communication is made.
		/// </summary>
		private PinpadCommunication pinpadCommunication;
		/// <summary>
		/// Defines how data is read, according to card type.
		/// </summary>
        private EmvPinReader chipReader;
        private MagneticStripePinReader magneticStripeReader;
		/// <summary>
		/// Pinpad command response status.
		/// </summary>
		internal ResponseStatus CommandStatus { get; private set; }
		/// <summary>
		/// EMV data, if card has chip.
		/// </summary>
		internal string EmvData { get; private set; }

		// Constructor
		/// <summary>
		/// Reads pinblock and ksn and sets it's value into this class property. 
		/// Is up to the user using these infos.
		/// </summary>
		/// <param name="pinpadCommunication">PinpadFacade mandatory to this class be able to communicate with the pinpad.</param>
		/// <param name="readingMode">Card type, that is, the method physical in which the card should be read.</param>
		/// <exception cref="System.InvalidOperationException">Thrown when one parameter validation fails.</exception>
		internal PinReader(PinpadCommunication pinpadCommunication)
		{
			try { this.Validate(pinpadCommunication); }
			catch (Exception ex)
			{
				this.CommandStatus = ResponseStatus.InvalidParameter;
				throw new InvalidOperationException("Could not read pinpad security infos. Verify inner exception.", ex);
			}

			this.pinpadCommunication = pinpadCommunication;
			this.CommandStatus = ResponseStatus.Ok;
			this.chipReader = new EmvPinReader();
			this.magneticStripeReader = new MagneticStripePinReader();
		}

		// Methods
		/// <summary>
		/// Reads Pin Block and Key Serial Number (KSN) from the card.
		/// If needed, prompts for cardholder password.
		/// </summary>
		/// <param name="pan">Primary Account Number, printed on the card.</param>
		/// <param name="amount">Transaction amount.</param>
		/// <returns>Whether is an online transaction or not.</returns>
		/// <exception cref="System.NotImplementedException">Thrown when a reading mode is unknown.</exception>
		internal Pin Read(CardType readingMode, decimal amount, string pan)
		{
			Pin pin;

            this.Validate(readingMode, amount, pan);

			if (readingMode == CardType.Emv)
			{
				Debug.WriteLine("EMV reading was selected.");
				this.CommandStatus = this.chipReader.Read(this.pinpadCommunication, amount, out pin);
			}

			else if (readingMode == CardType.MagneticStripe)
			{
				Debug.WriteLine("Magnetic stripe reading was selected.");
				this.CommandStatus = this.magneticStripeReader.Read(this.pinpadCommunication, pan, amount, out pin);
			}
			else
			{
				throw new NotImplementedException("Unknown Pin reading mode. Verify what kind of card (EMV/Magnetic Stripe) you are reading.");
			}

			return pin;
		}

		// Validation
		/// <summary>
		/// Validates parameters sent to the constructor.
		/// </summary>
		/// <exception cref="System.ArgumentNullException">When one parameter is null.</exception>
		/// <exception cref="System.ArgumentException">When one parameter is not null, but contains invalid data.</exception>
		private void Validate(PinpadCommunication pinpadFacade)
		{
			if (pinpadFacade == null)
			{
				throw new ArgumentNullException("pinpadCommunication");
			}
		}
        private void Validate(CardType readingMode, decimal amount, string pan)
        {
            // Card reading type validation:
            if (Enum.IsDefined(typeof(CardType), readingMode) == false)
            {
                throw new ArgumentException("readingMode is of invalid type.");
            }
            if (readingMode == CardType.Undefined) { throw new ArgumentException("readingMode cannot be undefined."); }

            // Amount validation:
            if (amount <= 0) { throw new ArgumentException("amount shall be greater than 0."); }

            // PAN validation:
            if (string.IsNullOrEmpty(pan) == true) { throw new ArgumentException("Primary Accouunt Number (PAN) cannot be null."); }
        }
	}
}
