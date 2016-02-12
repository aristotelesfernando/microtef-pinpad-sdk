using Pinpad.Sdk.EmvTable;
using Pinpad.Sdk.Model;
using Pinpad.Sdk.Model.TypeCode;
using System;
using System.Diagnostics;
using ResponseStatus = Pinpad.Sdk.Model.TypeCode.ResponseStatus;

namespace Pinpad.Sdk.Transaction
{
	/// <summary>
	/// Responsible for getting pinpad security information (Pin Block and Key Serial Number(KSN)).
	/// </summary>
	internal class PinReader
	{
		/* CONSTANTS */
		/// <summary>
		/// MK index or DUKPT register.
		/// </summary>
		internal const short STONE_DUKPT_KEY_INDEX = 16;
		/// <summary>
		/// Cardholder password minimum length.
		/// </summary>
		internal const short PASSWORD_MINIMUM_LENGTH = 4;
		/// <summary>
		/// Cardholder maximum length.
		/// </summary>
		internal const short PASSWORD_MAXIMUM_LENGTH = 12;

		/* MEMBERS */
		/// <summary>
		/// Facade through which pinpad communication is made.
		/// </summary>
		private IPinpadFacade pinpadFacade;
		/// <summary>
		/// Defines how data is read, according to card type.
		/// </summary>
		private CardType readingMode;
		/// <summary>
		/// Pinpad command response status.
		/// </summary>
		internal ResponseStatus CommandStatus { get; private set; }
		/// <summary>
		/// EMV data, if card has chip.
		/// </summary>
		internal string EmvData { get; private set; }

		/* CONSTRUCTOR */
		/// <summary>
		/// Reads pinblock and ksn and sets it's value into this class property. 
		/// Is up to the user using these infos.
		/// </summary>
		/// <param name="pinpadFacade">PinpadFacade mandatory to this class be able to communicate with the pinpad.</param>
		/// <param name="readingMode">Card type, that is, the method physical in which the card should be read.</param>
		/// <exception cref="System.InvalidOperationException">Thrown when one parameter validation fails.</exception>
		internal PinReader(IPinpadFacade pinpadFacade, CardType readingMode)
		{
			try { Validate(pinpadFacade, readingMode); }
			catch (Exception ex)
			{
				this.CommandStatus = ResponseStatus.InvalidParameter;
				throw new InvalidOperationException("Could not read pinpad security infos. Verify inner exception.", ex);
			}

			this.pinpadFacade = pinpadFacade;
			this.readingMode = readingMode;
			this.CommandStatus = ResponseStatus.Ok;
		}

		/* METHODS */
		/// <summary>
		/// Reads Pin Block and Key Serial Number (KSN) from the card.
		/// If needed, prompts for cardholder password.
		/// </summary>
		/// <param name="pan">Primary Account Number, printed on the card.</param>
		/// <param name="amount">Transaction amount.</param>
		/// <returns>Whether is an online transaction or not.</returns>
		/// <exception cref="System.NotImplementedException">Thrown when a reading mode is unknown.</exception>
		internal Pin Read(decimal amount, string pan)
		{
			Pin pin;

			if (this.readingMode == CardType.Emv)
			{
				Debug.WriteLine("EMV reading was selected.");
				this.CommandStatus = EmvPinReader.Read(this.pinpadFacade, amount, out pin);
			}

			else if (this.readingMode == CardType.MagneticStripe)
			{
				Debug.WriteLine("Magnetic stripe reading was selected.");
				this.CommandStatus = MagneticStripePinReader.Read(this.pinpadFacade, pan, amount, out pin);
			}
			else
			{
				Debug.WriteLine("There was no valid reading mode <{0}>.", this.readingMode);
				throw new NotImplementedException("Unknown Pin reading mode. Verify what kind of card (EMV/Magnetic Stripe) you are reading.");
			}

			return pin;
		}

		/* VALIDATORS */
		/// <summary>
		/// Validates parameters sent to the constructor.
		/// </summary>
		/// <exception cref="System.ArgumentNullException">When one parameter is null.</exception>
		/// <exception cref="System.ArgumentException">When one parameter is not null, but contains invalid data.</exception>
		private void Validate(IPinpadFacade pinpadFacade, CardType readingMode)
		{
			if (pinpadFacade == null)
			{
				throw new ArgumentNullException("pinpadFacade");
			}
			if (Enum.IsDefined(typeof(CardType), readingMode) == false)
			{
				throw new ArgumentException("readingMode is of invalid type.");
			}
			if (readingMode == CardType.Undefined)
			{
				throw new ArgumentException("readingMode cannot be undefined.");
			}
		}
	}
}
