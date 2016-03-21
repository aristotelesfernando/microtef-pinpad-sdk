using Pinpad.Sdk.Connection;
using Pinpad.Sdk.Model.TypeCode;
using Pinpad.Sdk.EmvTable;
using Pinpad.Sdk.Model;
using Pinpad.Sdk.Mapper;
using Pinpad.Sdk.Transaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ResponseStatus = Pinpad.Sdk.Model.TypeCode.ResponseStatus;
using Pinpad.Sdk.Exceptions;
using Pinpad.Core.Pinpad;
using Pinpad.Core.TypeCode;
using Pinpad.Core.Commands;
using Pinpad.Core.Properties;
using System.Diagnostics;
using Pinpad.Core;
using Pinpad.Sdk.Model.Exceptions;

namespace Pinpad.Sdk
{
	/// <summary>
	/// Pinpad controller, containing all methods needed to perform a transaction.
	/// </summary>
	public class PinpadController
	{
		/// <summary>
		/// Facade through which pinpad communication is made.
		/// </summary>
		private PinpadFacade pinpadFacade;
		private PinReader pinReader;
		
		/// <summary>
		/// Connection handler, is responsible for specifying the connection through which the pinpad will be looked for.
		/// </summary>
		public BasePinpadConnection PinpadConnection { get; private set; }
		/// <summary>
		/// Display handler, responsible for presenting messages in a pinpad device.
		/// </summary>
		public IPinpadDisplay Display { get; private set; }
		/// <summary>
		/// Keyboard handler, responsible for getting inputs from pinpad keyboard.
		/// </summary>
		public IPinpadKeyboard Keyboard { get; private set; }
		/// <summary>
		/// Information about the pinpad device connected.
		/// </summary>
		public IPinpadInfos Infos { get; private set; }
		/// <summary>
		/// Pinpad Emv table handler, that is, responsible for all operations related to table (CAPK and AID tables) controlling.
		/// </summary>
		public IPinpadTable EmvTable { get; private set; }
		/// <summary>
		/// Last status returned from a pinpad command.
		/// </summary>
		public ResponseStatus LastCommandStatus { get; private set; }

		// Constructors
		/// <summary>
		/// Primary constructor, setter of all mandatory parameters and responsible for data validation.
		/// </summary>
		/// <param name="pinpadConnection">Connection through which the pinpad will be looked for.</param>
		/// <param name="pinpadDisplay">Display handler.</param>
		/// <param name="pinpadTable">Pinpad emv table handler.</param>
		public PinpadController(BasePinpadConnection pinpadConnection)
		{
			if (pinpadConnection == null)
			{
				this.LastCommandStatus = ResponseStatus.InvalidParameter;
				throw new ArgumentNullException("pinpadConnection");
			}

			this.LastCommandStatus = ResponseStatus.Ok;

			this.PinpadConnection = pinpadConnection;

			this.pinpadFacade = new PinpadFacade(this.PinpadConnection.PlatformPinpadConnection);
			this.Display = this.pinpadFacade.Display;
			this.Keyboard = this.pinpadFacade.Keyboard;
			this.Infos = this.pinpadFacade.Infos;
			this.EmvTable = this.pinpadFacade.Table;

			this.pinReader = new PinReader(this.pinpadFacade);
		}

		// Transaction Methods
		/// <summary>
		/// On Pinpad screen, alternates between "RETIRE O CARTÃO" and parameter 'message' received, until card removal.
		/// </summary>
		/// <param name="message">Message to be shown on pinpad screen. Must not exceed 16 characters. This message remains on Pinpad screen after card removal.</param>
		/// <param name="padding">Message alignment.</param>
		/// <returns></returns>
		public bool RemoveCard(string message, DisplayPaddingType padding)
		{
			RmcRequest request = new RmcRequest();
			
			// Assemblies RMC command.
			request.RMC_MSG.Value = new SimpleMessage(message, padding);

			// Sends command and receive response
			GenericResponse response = null;
			while (response == null)
			{
				response = this.pinpadFacade.Communication.SendRequestAndReceiveResponse<GenericResponse>(request);
			}
			
			// Getting legacy response status code:
			AbecsResponseStatus legacyStatus = AbecsResponseStatus.ST_OK;

			// Mapping legacy status code into Pinpad.Sdk response status code.
			this.LastCommandStatus = ResponseStatusMapper.MapLegacyResponseStatus(legacyStatus);
			
			// Verifies if the command was executed.
			if (this.LastCommandStatus == ResponseStatus.Ok) { return true; }
			else { return false; }
		}
		/// <summary>
		/// Read basic card information, that is, brand id, card type, card primary account number (PAN), cardholder name and expiration date.
		/// If the card is removed in the middle of the process, returns CANCEL status.
		/// </summary>
		/// <param name="transactionType">Transaction type, that is, debit/credit.</param>
		/// <returns>Card basic info.</returns>
		/// <exception cref="ExpiredCardException">When an expired card is read.</exception>
		public CardEntry ReadCard(TransactionType transactionType, decimal amount, out TransactionType newTransactionType)
		{
			AbecsResponseStatus status;
			CardEntry cardRead;

			do
			{
				status = PerformCardReading(transactionType, amount, out cardRead, out newTransactionType);
				this.LastCommandStatus = ResponseStatusMapper.MapLegacyResponseStatus(status);

				// EMV tables are incompatible. Recharging tables:
				if (status == AbecsResponseStatus.ST_TABVERDIF || 
					status == AbecsResponseStatus.ST_CARDAPPNAV)
				{
					// TODO: FAZER UM TRATAMENTO DESCENTE
					return null;
				}
				else if (status == AbecsResponseStatus.ST_TIMEOUT)
				{
					throw new PinpadDisconnectedException();
				}
				else if (status == AbecsResponseStatus.ST_TABERR)
				{
					throw new InvalidTableException("EMV table version could not be found.");
				}

			} while (status != AbecsResponseStatus.ST_OK && status != AbecsResponseStatus.ST_CANCEL);

			return cardRead;
		}
		private AbecsResponseStatus PerformCardReading(TransactionType transactionType, decimal amount, out CardEntry cardRead, out TransactionType newTransactionType)
		{
			cardRead = new CardEntry();

			newTransactionType = transactionType;

			// Assembling GCR command request:
			GcrRequest request = new GcrRequest();

			// TODO: flag de acquirer.
			//request.GCR_ACQIDXREQ.Value = (int)StoneIndexCode.Application;
			request.GCR_ACQIDXREQ.Value = 00;

			if (transactionType != TransactionType.Undefined)
			{
				request.GCR_APPTYPREQ.Value = (int)transactionType;
			}
			else
			{
				request.GCR_APPTYPREQ.Value = 99;
			}

			request.GCR_AMOUNT.Value = Convert.ToInt64(amount * 100);
			request.GCR_DATE_TIME.Value = DateTime.Now;

			// Retieving current EMV table version from pinpad:
			string emvTableVersion = this.EmvTable.GetEmvTableVersion();
			Debug.WriteLine("EMV table version: <{0}>", emvTableVersion);
			
			if (emvTableVersion == null)
			{
				// There's no table version, therefore tables cannot be reached.
				return AbecsResponseStatus.ST_TABERR;
			}

			// If it's a valid EMV table version, then adds it to the command:
			request.GCR_TABVER.Value = emvTableVersion;

			// Sending and receiving response.
			Debug.WriteLine("Sending GCR command <{0}>", request.CommandString);
			GcrResponse response = this.pinpadFacade.Communication.SendRequestAndReceiveResponse<GcrResponse>(request);

			// If timeout was reached:
			if (response == null)
			{
				return AbecsResponseStatus.ST_TIMEOUT;
			}
			//If the card was removed at the middle of reading process:
			else if (response.RSP_STAT.Value == AbecsResponseStatus.ST_NOCARD)
			{
				return AbecsResponseStatus.ST_CANCEL;
			}
			// If the card has expired:
			else if (response.GCR_CARDEXP.HasValue == false)
			{
				throw new ExpiredCardException();
			}

			// If it's OK with card reading:
			if (response.RSP_STAT.Value != AbecsResponseStatus.ST_OK)
			{
				
				return response.RSP_STAT.Value;
			}

			// Saving the transaction type. This is handy if you have initiated the transaction without knowing the transaction type.
			// In this situation, the user will select transaction type throgh the pinpad.
			newTransactionType = (TransactionType) response.GCR_APPTYPE.Value;

			// Saving command response status.
			// Getting legacy response status code:
			AbecsResponseStatus legacyStatus = response.RSP_STAT.Value;
			
			// Mapping legacy status code into Pinpad.Sdk response status code.
			this.LastCommandStatus = ResponseStatusMapper.MapLegacyResponseStatus(legacyStatus);

			// Get card information and return it:
			cardRead = CardMapper.MapCardFromTracks(response);

			if (cardRead.Type == CardType.Emv)
			{
				this.EmvTable.RefreshFromPinpad();

				if (this.EmvTable.AidTable.Count <= 0)
				{
					throw new InvalidTableException("AID table is empty.");
				}

				string brandId = cardRead.BrandId.ToString();
				var aidVar = this.EmvTable.AidTable.First(a => a.AidIndex == brandId);

				cardRead.ApplicationId = aidVar.ApplicationId;
				cardRead.BrandName = EmvTrackMapper.GetBrandByAid(cardRead.ApplicationId);
			}

			return AbecsResponseStatus.ST_OK;
		}
		/// <summary>
		/// If cardholder card needs password, than prompts it. Otherwise, nothing is done. 
		/// </summary>
		/// <param name="amount">Transaction amount in cents.</param>
		/// <param name="pan">Primary Account Number (PAN).</param>
		/// <param name="readingMode">Card type, defining how the card must be read.</param>
		/// <returns>An object Pin, containing pin block (cardholder password), Key Serial Number (KSN, determining pin block DUKPT encryption key) and if pin verification is online. </returns>
		public Pin ReadPassword(decimal amount, string pan = "", CardType readingMode = CardType.Emv)
		{
			Debug.WriteLine("Readig mode <{0}>.", readingMode);
			
			// Gets Pin:
			Pin pin = pinReader.Read(readingMode, amount, pan);

			Debug.WriteLine("PIN read. Response <{0}>.", pinReader.CommandStatus);

			// Saving last command status:
			this.LastCommandStatus = pinReader.CommandStatus;

			return pin;
		}

		// Pinpad utils methods
		public bool OpenConnection()
		{
			return this.pinpadFacade.Communication.SendRequestAndVerifyResponseCode(new OpnRequest());
		}

		// Validations
		/// <summary>
		/// Responsible for validating mandatory parameters.
		/// </summary>
		/// <param name="pinpadConnection">Connection through which the pinpad will be looked for.</param>
		/// <param name="pinpadDisplay">Display handler.</param>
		/// <param name="pinpadTable">Pinpad emv table handler.</param>
		/// <exception cref="System.ArgumentNullException">Thrown when a parameter is null (specifies which is).</exception>
		private void Validate(BasePinpadConnection pinpadConnection, IPinpadDisplay pinpadDisplay, IPinpadTable pinpadTable)
		{
			if (pinpadConnection == null) { throw new ArgumentNullException("pinpadConnection"); }
			if (pinpadDisplay == null) { throw new ArgumentNullException("pinpadDisplay"); }
			if (pinpadTable == null) { throw new ArgumentNullException("pinpadTable"); }
		}
	}
}
