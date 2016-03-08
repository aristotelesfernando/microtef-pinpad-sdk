﻿using Pinpad.Sdk.Mapper;
using Pinpad.Sdk.Model.TypeCode;
using Pinpad.Core.Commands;
using Pinpad.Core.TypeCode;
using System;
using ResponseStatus = Pinpad.Sdk.Model.TypeCode.ResponseStatus;
using LegacyResponseStatus = Pinpad.Core.TypeCode.AbecsResponseStatus;
using Pinpad.Sdk.Model;
using System.Globalization;
using Pinpad.Sdk.EmvTable;
using Pinpad.Core.Utilities;
using Pinpad.Core.Properties;
using Pinpad.Core;

namespace Pinpad.Sdk.Transaction
{
    /// <summary>
    /// Reads a password from a card which contains EMV chip.
    /// </summary>
	internal class EmvPinReader
	{
        // Constants
        /// <summary>
        /// Application Cryptogram (ARQC) EMV tag. All tags can be verified on EMV lab or EMV book.
        /// </summary>
        internal const string ARQC_EMV_TAG = "9F26";
        /// <summary>
        /// This value is used on bit operation, to verifie if all bits from a byte are on.
        /// In this case, the value corresponds to 0xF1, which corresponds to the ASCII value of 1, which corresponds to the logical value of true.
        /// </summary>
        internal const byte BITS_ARE_ON = 0x1F; 

        // Methods
        /// <summary>
        /// Reads the card.
        /// </summary>
        /// <param name="facade">Pinpad facade, responsible for pinpad communication and plus.</param>
        /// <param name="amount">Transaction amount.</param>
        /// <param name="pin">Pin information read.</param>
        /// <returns>Operation status.</returns>
		internal ResponseStatus Read(IPinpadFacade facade, decimal amount, out Pin pin)
		{
			pin = new Pin();

			// Validating data
			this.Validate(facade, amount);

			// Using ABECS GOC command to communicate with pinpad.
			GocResponse commandResponse = this.SendGoc(facade, amount);

			// Saving command response status:
			LegacyResponseStatus legacyStatus = commandResponse.RSP_STAT.Value;
			ResponseStatus status = ResponseStatusMapper.MapLegacyResponseStatus(legacyStatus);
			if (status == ResponseStatus.OperationCancelled) { return status; }

			pin.ApplicationCryptogram = this.MapApplicationCryptogram(commandResponse.GOC_EMVDAT.Value.DataString);

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
		/// <param name="amount">Transaction amount.</param>
		/// <returns>ABECS GOC command response.</returns>
		private GocResponse SendGoc(IPinpadFacade pinpadFacade, decimal amount)
        {
            GocRequest request = new GocRequest();

            request.GOC_AMOUNT.Value = Convert.ToInt64(amount * 100);
            request.GOC_CASHBACK.Value = 0;
            request.GOC_EXCLIST.Value = false;
            request.GOC_CONNECT.Value = true;
            request.GOC_METHOD.Value = new CryptographyMethod(KeyManagementMode.DerivedUniqueKeyPerTransaction, CryptographyMode.TripleDataEncryptionStandard);
            request.GOC_KEYIDX.Value = (int)StoneIndexCode.EncryptionKey;
            request.GOC_WKENC.Value = new HexadecimalData("00000000000000000000000000000000");
            request.GOC_RISKMAN.Value = false;
            request.GOC_FLRLIMIT.Value = new HexadecimalData("00000000");
            request.GOC_TPBRS.Value = 0;
            request.GOC_TVBRS.Value = new HexadecimalData("00000000");
            request.GOC_MTPBRS.Value = 0;
            request.GOC_TAGS1.Value = new HexadecimalData(EmvTag.GetEmvTagsRequired());
            request.GOC_TAGS2.Value = new HexadecimalData("");

            GocResponse response = pinpadFacade.Communication.SendRequestAndReceiveResponse<GocResponse>(request);

            return response;
        }
        /// <summary>
        /// Gets the Application Cryptogram (ARQC) from EMV data (string with all EMV tags and values concatenated).
        /// </summary>
        /// <param name="emvData">EMV data (string with all EMV tags and values concatenated).</param>
        /// <returns>Application Cryptogram (ARQC).</returns>
		private string MapApplicationCryptogram(string emvData)
        {
            string data = emvData;

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

                    if (key == ARQC_EMV_TAG)
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
        /// <param name="facade">Pinpad facade, through which pinpad communication are made.</param>
        /// <param name="amount">Transaction amount.</param>
        private void Validate(IPinpadFacade facade, decimal amount)
        {
            if (facade == null) { throw new ArgumentNullException("pinpadFacade cannot be null. Unable to communicate with the pinpad."); }
            if (amount <= 0) { throw new ArgumentException("amount shall be greater than 0."); }
        }
    }
}
