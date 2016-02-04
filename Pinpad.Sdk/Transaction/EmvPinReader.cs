using Pinpad.Sdk.Mapper;
using Pinpad.Sdk.Model.TypeCode;
using PinPadSDK.Commands;
using PinPadSDK.Enums;
using PinPadSDK.PinPad;
using PinPadSDK.Property;
using StonePortableUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ResponseStatus = Pinpad.Sdk.Model.TypeCode.ResponseStatus;
using LegacyResponseStatus = PinPadSDK.Enums.ResponseStatus;
using Pinpad.Sdk.Model;
using System.Globalization;
using System.Diagnostics;

namespace Pinpad.Sdk.Transaction
{
	internal class EmvPinReader
	{
		internal static ResponseStatus Read(PinPadFacade facade, decimal amount, out Pin pin)
		{
			pin = new Pin();

			// Validating data
			try { EmvPinReader.Validate(amount); }
			catch (Exception ex)
			{
				throw new InvalidOperationException("Error while trying to read security info using EMV. Verify inner exception.", ex);
			}

			// Using ABECS GOC command to communicate with pinpad.
			Debug.WriteLine("Sending GOC command to pinpad controller...");
			GocResponse commandResponse = EmvPinReader.SendGoc(facade, amount);
			Debug.WriteLine("GOC response <{0}>.", commandResponse.RSP_STAT.Value);

			// Saving command response status:
			LegacyResponseStatus legacyStatus = commandResponse.RSP_STAT.Value;
			ResponseStatus status = ResponseStatusMapper.MapLegacyResponseStatus(legacyStatus);

			if (status == ResponseStatus.OperationCancelled)
			{
				return status;
			}

			pin.ApplicationCryptogram = MapApplicationCryptogram(commandResponse.GOC_EMVDAT.Value.DataString);
			Debug.WriteLine("ARQC <{0}>.", pin.ApplicationCryptogram);

			// Whether is a pin online authentication or not.
			if (commandResponse.GOC_PINONL.Value.HasValue)
			{
				pin.IsOnline = commandResponse.GOC_PINONL.Value.Value;
			}

			// Savind EMV data:
			if (commandResponse.GOC_EMVDAT.Value != null)
			{
				Debug.WriteLine("Has EMV data.");
				pin.EmvData = commandResponse.GOC_EMVDAT.Value.DataString;
			}

			if (pin.IsOnline == true && commandResponse.GOC_DECISION.Value == OfflineTransactionStatus.RequiresAuthorization)
			{
				Debug.WriteLine("Has online PIN validation and requires authorization.");

				// If it's an online transaction, that is, needs pin and ksn emv validation:
				pin.PinBlock = commandResponse.GOC_PINBLK.Value.DataString;
				pin.KeySerialNumber = commandResponse.GOC_KSN.Value.DataString;
			}

			// Returns read pin block.
			return status;
		}

		/// <summary>
		/// Sends reading command using ABECS when card have a chip.
		/// </summary>
		/// <param name="amount">Transaction amount.</param>
		/// <returns>ABECS GOC command response.</returns>
		private static GocResponse SendGoc(PinPadFacade pinpadFacade, decimal amount)
		{
			GocRequest request = new GocRequest();

			request.GOC_AMOUNT.Value = Convert.ToInt64(amount * 100);
			request.GOC_CASHBACK.Value = 0;
			request.GOC_EXCLIST.Value = false;
			request.GOC_CONNECT.Value = true;
			request.GOC_METHOD.Value = new CryptographyMethod(KeyManagementMode.DerivedUniqueKeyPerTransaction, CryptographyMode.TripleDataEncryptionStandard);
			request.GOC_KEYIDX.Value = PinReader.STONE_DUKPT_KEY_INDEX;
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
		/// Validates parameter used on internal processing.
		/// </summary>
		/// <exception cref="System.ArgumentNullException">When one parameter is null.</exception>
		/// <exception cref="System.ArgumentException">When one parameter is not null, but contains invalid data.</exception>
		private static void Validate(decimal amount)
		{
			if (amount <= 0) 
			{
				Debug.WriteLine("Invalid amount <{0}>.", amount);
				throw new ArgumentException("amount should be greater than 0."); 
			}
		}

		private static string MapApplicationCryptogram(string emvData)
		{
			string data = emvData;

			for (int i = 0; i < data.Length; )
			{
				try
				{
					string key = data.Substring(i, 2);
					i += 2;

					// TODO: retirar numeros magicos (0x1F)
					// Verifica se todos os 5 primeiros bits estao ligados:
					if ((Int32.Parse(key, NumberStyles.HexNumber) & 0x1F) == 0x1F)
					{
						// Adding an extra byte for the TAG field:
						key += data.Substring(i, 2);
						i += 2;
					}

					// Pega 
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

					if (key == "9F26") 
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
	}
}
