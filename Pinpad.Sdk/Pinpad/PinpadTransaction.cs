using Pinpad.Sdk.Commands;
using Pinpad.Sdk.Properties;
using Pinpad.Sdk.Transaction;
using Pinpad.Sdk.Transaction.Mapper;
using Pinpad.Sdk.Exceptions;
using Pinpad.Sdk.Model;
using Pinpad.Sdk.Model.Exceptions;
using System;
using System.Diagnostics;
using System.Linq;

namespace Pinpad.Sdk
{
	/// <summary>
	/// Responsible for authorization operations.
	/// </summary>
	public class PinpadTransaction : IPinpadTransaction
	{
		/// <summary>
		/// Responsible for read card password itself, depending on card type.
		/// </summary>
		private PinReader pinReader;
		/// <summary>
		/// Responsible for sending requests to the pinpad.
		/// </summary>
		private PinpadCommunication pinpadCommunication;
		/// <summary>
		/// Pinpad Emv table handler, that is, responsible for all operations related to table (CAPK and AID tables) controlling.
		/// </summary>
		public PinpadTable EmvTable { get; private set; }
		/// <summary>
		/// The status of the last operation made.
		/// </summary>
		public ResponseStatus LastCommandStatus { get; private set; }

		// Constructor
		/// <summary>
		/// Constructor that
		/// </summary>
		/// <param name="pinpadCommunication"></param>
		public PinpadTransaction (PinpadCommunication pinpadCommunication)
		{
			this.pinReader = new PinReader(pinpadCommunication);
			this.pinpadCommunication = pinpadCommunication;
			this.EmvTable = new PinpadTable(pinpadCommunication);
		}

		// Transaction Methods
		/// <summary>
		/// On Pinpad screen, alternates between "RETIRE O CARTÃO" and parameter 'message' received, until card removal.
		/// </summary>
		/// <param name="message">Message to be shown on pinpad screen. Must not exceed 16 characters. This message remains on Pinpad screen after card removal.</param>
		/// <param name="padding">Message alignment.</param>
		/// <returns></returns>
		public bool RemoveCard (string message, DisplayPaddingType padding)
		{
			RmcRequest request = new RmcRequest();

			// Assemblies RMC command.
			request.RMC_MSG.Value = new SimpleMessage(message, padding);

			// Sends command and receive response
			GenericResponse response = null;
			while (response == null)
			{
				response = this.pinpadCommunication.SendRequestAndReceiveResponse<GenericResponse>(request);
			}

			// Getting legacy response status code:
			AbecsResponseStatus legacyStatus = AbecsResponseStatus.ST_OK;

			// Mapping legacy status code into Pinpad.Sdk response status code.
			this.LastCommandStatus = ResponseStatusMapper.MapLegacyResponseStatus(legacyStatus);

			// Verifies if the command was executed.
			if (this.LastCommandStatus == ResponseStatus.Ok)
			{ return true; }
			else
			{ return false; }
		}
		/// <summary>
		/// Read basic card information, that is, brand id, card type, card primary account number (PAN), cardholder name and expiration date.
		/// If the card is removed in the middle of the process, returns CANCEL status.
		/// </summary>
		/// <param name="transactionType">Transaction type, that is, debit/credit.</param>
		/// <param name="amount">Transaction amount.</param>
		/// <param name="newTransactionType">New transaction type read throgh ABECS protocol.</param>
		/// <returns>Card basic info.</returns>
		/// <exception cref="ExpiredCardException">When an expired card is read.</exception>
		public CardEntry ReadCard (TransactionType transactionType, decimal amount, out TransactionType newTransactionType)
		{
			AbecsResponseStatus status;
			CardEntry cardRead;
			
				status = this.PerformCardReading(transactionType, amount, out cardRead, out newTransactionType);
			this.LastCommandStatus = ResponseStatusMapper.MapLegacyResponseStatus(status);

			// EMV tables are incompatible. Recharging tables:
			if (status == AbecsResponseStatus.ST_TABVERDIF ||
				status == AbecsResponseStatus.ST_CARDAPPNAV)
			{
				// TODO: FAZER UM TRATAMENTO DESCENTE
				return null;
			}
			else if (status == AbecsResponseStatus.ST_TIMEOUT && this.pinpadCommunication.OpenPinpadConnection() == false)
			{
				// Conection loss
				throw new PinpadDisconnectedException();
			}
			else if (status == AbecsResponseStatus.ST_TABERR)
			{
				throw new InvalidTableException("EMV table version could not be found.");
			}
			else if (status == (AbecsResponseStatus) 23)
			{
				this.LastCommandStatus = ResponseStatus.InvalidEmvTable;
			}

			return cardRead;
		}
		/// <summary>
		/// If cardholder card needs password, than prompts it. Otherwise, nothing is done. 
		/// </summary>
		/// <param name="amount">Transaction amount in cents.</param>
		/// <param name="pan">Primary Account Number (PAN).</param>
		/// <param name="readingMode">Card type, defining how the card must be read.</param>
		/// <returns>An object Pin, containing pin block (cardholder password), Key Serial Number (KSN, determining pin block DUKPT encryption key) and if pin verification is online. </returns>
		public Pin ReadPassword (decimal amount, string pan = "", CardType readingMode = CardType.Emv)
		{
			Debug.WriteLine("Readig mode <{0}>.", readingMode);

			// Gets Pin:
			Pin pin = pinReader.Read(readingMode, amount, pan);

			Debug.WriteLine("PIN read. Response <{0}>.", pinReader.CommandStatus);

			// Saving last command status:
			this.LastCommandStatus = pinReader.CommandStatus;

			return pin;
		}
		/// <summary>
		/// Creates and sends a FNC to the pinpad. This command ends an authorization process on EMV module.
		/// </summary>
		/// <param name="issuerEmvData">EMV script to be send to the card.</param>
		/// <returns>FNC command status.</returns>
		public AbecsResponseStatus FinishTransaction (IssuerEmvDataEntry issuerEmvData)
		{
			FncRequest fnc = new FncRequest();
			fnc.FNC_COMMST.Value = issuerEmvData.AuthorizationStatus;
			fnc.FNC_ISSMODE.Value = 0;
			fnc.FNC_ARC.Value = issuerEmvData.AuthorizationResponseCode;

			if (string.IsNullOrEmpty(issuerEmvData.IssuerRelatedData) == false && issuerEmvData.AuthorizationStatus != AcquirerCommunicationStatus.ConnectionError)
			{
				fnc.FNC_ISSDAT.Value = new HexadecimalData(issuerEmvData.IssuerRelatedData);
				fnc.FNC_TAGS.Value = new HexadecimalData(EmvTag.GetEmvTagsRequired());
			}
			else
			{
				fnc.FNC_ISSDAT.Value = new HexadecimalData("");
				fnc.FNC_TAGS.Value = new HexadecimalData("");
			}

			return this.pinpadCommunication.SendRequestAndReceiveResponse<FncResponse>(fnc).RSP_STAT.Value;
		}

		/// <summary>
		/// Read basic card information, that is, brand id, card type, card primary account number (PAN), cardholder name and expiration date.
		/// If the card is removed in the middle of the process, returns CANCEL status. 
		/// </summary>
		/// <param name="transactionType">Transaction type, that is, debit/credit.</param>
		/// <param name="amount">Transaction amount.</param>
		/// <param name="cardRead">Card to be read.</param>
		/// <param name="newTransactionType">New transaction type read throgh ABECS protocol.</param>
		/// <returns>Operation status.</returns>
		private AbecsResponseStatus PerformCardReading (TransactionType transactionType, decimal amount, out CardEntry cardRead, out TransactionType newTransactionType)
		{
			cardRead = null;

			newTransactionType = transactionType;

			GcrRequest request = this.CreateGcrRequest(transactionType, amount);

			// There's no table version, therefore tables cannot be reached.
			if (request == null) { return AbecsResponseStatus.ST_TABERR; }

			// Sending and receiving response.
			Debug.WriteLine("Sending GCR command <{0}>", request.CommandString);
			GcrResponse response = this.pinpadCommunication.SendRequestAndReceiveResponse<GcrResponse>(request);

			// If timeout was reached:
			if (response == null)
			{
				return AbecsResponseStatus.ST_TIMEOUT;
			}
			// If an error occurred:
			else if (response.RSP_STAT.Value != AbecsResponseStatus.ST_OK)
			{
				return response.RSP_STAT.Value;
			}
			// If the card has expired:
			else if (response.GCR_CARDTYPE.Value == ApplicationType.MagneticStripe)
			{
				CardEntry tempCard;

				// Verify if it is really a magnetic stripe card:
				tempCard = MagneticStripeTrackMapper.GetCard(response);
				Debug.WriteLine("Tipo do cartão: " + tempCard.Type);

				// TODO: Incluir o fallback nessa condição.
				if (tempCard.Type != response.GCR_CARDTYPE.Value.ToCardType())
				{
					throw new CardHasChipException();
				}

				// Validate expired cards:
				DateTime expirationDate = tempCard.ExpirationDate;

				if (expirationDate < DateTime.Now)
				{
					throw new ExpiredCardException(expirationDate);
				}
			}
			else if (response.GCR_CARDEXP.HasValue == false)
			{
				throw new ExpiredCardException(DateTime.MinValue);
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
				if (this.EmvTable.AidTable.Count <= 0)
				{
					throw new InvalidTableException("AID table is empty.");
				}

				int brandId = cardRead.BrandId;
				var aidVar = this.EmvTable.AidTable.First(a => a.TAB_RECIDX.Value.Value == brandId);

				cardRead.ApplicationId = aidVar.T1_AID.Value.DataString;

				// If it is a EMV transaction, then the application SHOULD send a CNG to change EMV parameters:
				// TODO: Ver como o comando CNG funciona. Não retirar.
				//if (response.GCR_CARDTYPE.Value == ApplicationType.IccEmv)
				//{
				//	CngRequest cng = new CngRequest();
				//	cng.CNG_EMVDAT.Value = ((...))
				//	bool stats = this.pinpadCommunication.SendRequestAndVerifyResponseCode(cng);
				//}
			}

			return AbecsResponseStatus.ST_OK;
		}
		/// <summary>
		/// Create a request for GCR. <seealso cref="GcrRequest"/>
		/// </summary>
		/// <param name="transactionType">Transaction type, e. g. credit/debit</param>
		/// <param name="amount">Transaction amount</param>
		/// <returns>A corresponding <see cref="GcrRequest"/></returns>
		private GcrRequest CreateGcrRequest (TransactionType transactionType, decimal amount)
		{
			// Assembling GCR command request:
			GcrRequest request = new GcrRequest();

			// TODO: flag de acquirer.
			request.GCR_ACQIDXREQ.Value = (int) StoneIndexCode.Application;

			if (transactionType != TransactionType.Undefined)
			{
				request.GCR_APPTYPREQ.Value = (int) transactionType;
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
				return null;
			}

			// If it's a valid EMV table version, then adds it to the command:
			request.GCR_TABVER.Value = emvTableVersion;

			return request;
		}
	}
}
