using Pinpad.Sdk.Commands;
using System;
using Pinpad.Sdk.Model;
using System.Globalization;
using Pinpad.Sdk.Properties;
using Pinpad.Sdk.Transaction;
using Pinpad.Sdk.Exceptions;
using Pinpad.Sdk.Model.TypeCode;

namespace Pinpad.Sdk
{
    /// <summary>
    /// Reads a password from a card which contains EMV chip.
    /// </summary>
	internal class EmvPinReader
	{
        // Constants
        /// <summary>
        /// This value is used on bit operation, to verifie if all bits from a byte are on.
        /// In this case, the value corresponds to 0xF1, which corresponds to the ASCII value of 1, which corresponds to the logical value of true.
        /// </summary>
        internal const byte BITS_ARE_ON = 0x1F; 

        // Methods
        /// <summary>
        /// Reads the card.
        /// </summary>
        /// <param name="communication">Pinpad facade, responsible for pinpad communication and plus.</param>
        /// <param name="amount">Transaction amount.</param>
        /// <param name="pin">Pin information read.</param>
        /// <returns>Operation status.</returns>
		internal ResponseStatus Read(PinpadCommunication communication, decimal amount, out Pin pin)
		{
			pin = null;

			// Validating data
			this.Validate(communication, amount);

			// Using ABECS GOC command to communicate with pinpad.
			GocResponse commandResponse = this.SendGoc(communication, amount);

			if (commandResponse == null)
			{
				if (communication.Ping() == true)
				{
					// Pinpad is connected. Time out.
					return ResponseStatus.TimeOut;
				}
				else
				{
					// Pinpad loss conection.
					throw new PinpadDisconnectedException("Não foi possível ler a senha.\nVerifique a conexão com o pinpad.");
				}
			}

			// Saving command response status:
			AbecsResponseStatus legacyStatus = commandResponse.RSP_STAT.Value;
			ResponseStatus status = ResponseStatusMapper.MapLegacyResponseStatus(legacyStatus);

			if (status != ResponseStatus.Ok)
			{
				return status;
			}
			//if (status == ResponseStatus.OperationCancelled) { return status; }

			pin = new Pin();
			pin.ApplicationCryptogram = this.GetValueFromEmvData(
                commandResponse.GOC_EMVDAT.Value.DataString, EmvTagCode.ApplicationCryptogram);
            pin.CardholderNameExtended = this.GetValueFromEmvData(
                commandResponse.GOC_EMVDAT.Value.DataString, EmvTagCode.CardholderNameExtended);

			// Whether is a pin online authentication or not.
			if (commandResponse.GOC_PINONL.Value.HasValue == true)
			{
				pin.IsOnline = commandResponse.GOC_PINONL.Value.Value;
			}

            // If EMV data war not returned from the command:
            if (commandResponse.GOC_EMVDAT.HasValue == false) { return ResponseStatus.PinBusy; }

			// Savind EMV data:
			if (commandResponse.GOC_EMVDAT.Value != null)
			{
				pin.EmvData = commandResponse.GOC_EMVDAT.Value.DataString;
			}

			if (pin.IsOnline == true && commandResponse.GOC_DECISION.Value == OfflineTransactionStatus.RequiresAuthorization)
			{
				// If it's an online transaction, that is, needs pin and ksn emv validation:
				pin.PinBlock = commandResponse.GOC_PINBLK.Value.DataString;
				pin.KeySerialNumber = commandResponse.GOC_KSN.Value.DataString;
			}

			// Returns read pin block.
			return status;
		}

        // Used internally
        /// <summary>
		/// Sends reading command using ABECS when card have a chip.
		/// </summary>
		/// <param name="pinpadCommunication">Pinpad communication, through which the communication with the pinpad is made.</param>
		/// <param name="amount">Transaction amount.</param>
		/// <returns>ABECS GOC command response.</returns>
		private GocResponse SendGoc(PinpadCommunication pinpadCommunication, decimal amount)
        {
            GocRequest request = new GocRequest();

            request.GOC_AMOUNT.Value = Convert.ToInt64(amount * 100);
            request.GOC_CASHBACK.Value = 0;
            request.GOC_EXCLIST.Value = false;
            request.GOC_CONNECT.Value = true;
            request.GOC_METHOD.Value = new CryptographyMethod(KeyManagementMode.DerivedUniqueKeyPerTransaction, 
                CryptographyMode.TripleDataEncryptionStandard);
            request.GOC_KEYIDX.Value = (int)StoneIndexCode.EncryptionKey;
            request.GOC_WKENC.Value = new HexadecimalData("00000000000000000000000000000000");
            request.GOC_RISKMAN.Value = false;
            request.GOC_FLRLIMIT.Value = new HexadecimalData("00000000");
            request.GOC_TPBRS.Value = 0;
            request.GOC_TVBRS.Value = new HexadecimalData("00000000");
            request.GOC_MTPBRS.Value = 0;
            request.GOC_TAGS1.Value = new HexadecimalData(EmvTag.GetEmvTagsRequired());
            request.GOC_TAGS2.Value = new HexadecimalData(string.Empty);

            GocResponse response = pinpadCommunication.SendRequestAndReceiveResponse<GocResponse>(request);

            return response;
        }
        /// <summary>
        /// Gets a specified tag from the whole EMV tag field.
        /// concatenated).
        /// </summary>
        /// <param name="emvData">EMV data (string with all EMV tags and values concatenated).</param>
        /// <param name="emvKey">EMV key to be extracted from EMV Data.</param>
        /// <returns>
        ///     The value of the specified tag or null if the tag searched was not found.
        /// </returns>
        /// <exception cref="IndexOutOfRangeException">
        ///     If an error occurred while processing the EMV Data.
        /// </exception>
		private string GetValueFromEmvData(string emvData, EmvTagCode emvKey)
        {
            string data = emvData;
            string searchedKeyName = ((int)emvKey).ToString("X");

            for (int i = 0; i < data.Length; /*nothing here, the increment is dynamic*/)
            {
                try
                {
                    // Gets the key
                    string key = data.Substring(i, 2);
                    i += 2;

                    // Verifies whether all bits from a byte are on or not.
                    if (this.AreAllBitsOn(key) == true)
                    {
                        // Adding an extra byte for the TAG field:
                        key += data.Substring(i, 2);
                        i += 2;
                    }

                    // Gets the value
                    string len = data.Substring(i, 2);
                    i += 2;
                    int length = Int32.Parse(len, NumberStyles.HexNumber);

                    if (length > 127)
                    {
                        // More than 1 byte for lenth
                        int bytesLength = length - 128;

                        len = data.Substring(i, bytesLength * 2);
                        i += bytesLength * 2;
                        length = Int32.Parse(len, NumberStyles.HexNumber);
                    }

                    length *= 2;

                    if (key == searchedKeyName)
                    {
                        return data.Substring(i, length);
                    }

                    i += length;
                }
                catch (ArgumentOutOfRangeException e)
                {
                    throw new IndexOutOfRangeException("Error processing field", e);
                }
                catch (IndexOutOfRangeException e)
                {
                    throw new IndexOutOfRangeException("Error processing field", e);
                }
            }

            return null;
        }
        /// <summary>
        /// Converts a string into an integer, as hexadecimal.
        /// Verifies whether all bits from a byte are on or not.
        /// Used as a macro.
        /// </summary>
        /// <returns>Returns whether all bits from a byte aro on or not.</returns>
        private bool AreAllBitsOn(string key)
        {
            return (Int32.Parse(key, NumberStyles.HexNumber) & BITS_ARE_ON) == BITS_ARE_ON;
        }
        
        // Validation
        /// <summary>
        /// Validate all mandatory parameters.
        /// Throws an exception if the parameter is null or invalid.
        /// </summary>
        /// <param name="communication">Pinpad facade, through which pinpad communication are made.</param>
        /// <param name="amount">Transaction amount.</param>
        private void Validate(PinpadCommunication communication, decimal amount)
        {
            if (communication == null) { throw new ArgumentNullException("pinpadCommunication cannot be null. Unable to communicate with the pinpad."); }
            if (amount <= 0) { throw new ArgumentException("amount shall be greater than 0."); }
        }
    }
}
