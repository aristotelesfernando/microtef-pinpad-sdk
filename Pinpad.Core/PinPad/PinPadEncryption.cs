using MicroPos.CrossPlatform;
using MicroPos.CrossPlatform.TypeCode;
using Pinpad.Core.Commands;
using Pinpad.Core.Utilities;

namespace Pinpad.Core.Pinpad
{
	/// <summary>
	/// Controller for Stone Secure Command
	/// </summary>
	public class PinpadEncryption
	{
		// Constants:
		public const short STONE_KEY_INDEX = 16;

		// Members:
		private static PinpadEncryption instance = new PinpadEncryption();
		/// <summary>
		/// Gets the Singleton of PinpadEncryption
		/// </summary>
		public static PinpadEncryption Instance { get { return PinpadEncryption.instance; } }
		/// <summary>
		/// Stone TripleDES key, used to encrypt and decrypt packages whithin pinpad communication.
		/// </summary>
		private byte[] StoneTDESKey = { 0x6A, 0x82, 0xE5, 0x6E, 0xBE, 0x60, 0x32, 0x4C, 0x10, 0x83, 0x47, 0x6D, 0xA9, 0x93, 0x83, 0xFB, 0xD7, 0x6B, 0x9C, 0xD4, 0x0D, 0x42, 0x33, 0xCD };

		// Constructor:
		private PinpadEncryption() {  }
		
		// Methods:        
		/// <summary>
		/// Encrypt a package.
		/// </summary>
		/// <param name="value">Package to be encrypted.</param>
		/// <returns>Encrypted value.</returns>
		private byte[] EncryptTDES(HexadecimalData value)
		{
			// Null generates a ECB encryption out of CBC.
			return CrossPlatformController.EncryptionController.EncryptTDES(value.Data, StoneTDESKey, null);
		}
		/// <summary>
		/// Debrypt a package.
		/// </summary>
		/// <param name="value">Package to be decrypted.</param>
		/// <returns>Decrypted value.</returns>
		private byte[] DecryptTDES(HexadecimalData value)
		{
			// Null generates a ECB encryption out of CBC.
			return CrossPlatformController.EncryptionController.DecryptTDES(value.Data, StoneTDESKey, null);
		}
		/// <summary>
		/// Encrypts the request and wraps is into a secure command (SEC).
		/// </summary>
		/// <param name="request">Request as string.</param>
		/// <returns>Secure request.</returns>
		public SecRequest WrapRequest(string request)
		{
			// Creates the secure request:
			SecRequest secureRequest = new SecRequest();
			secureRequest.SEC_ACQIDX.Value = STONE_KEY_INDEX;

			// Turns the text into bytes:
			byte [] requestBytes = CrossPlatformController.TextEncodingController.GetBytes(TextEncodingType.Ascii, request);

			// Encrypts it:
			byte [] encryptedRequestBytes = EncryptTDES(new HexadecimalData(requestBytes));

			// Generates hexadecimal data based on the encrypted request:
			secureRequest.SEC_CMDBLK.Value = new HexadecimalData(encryptedRequestBytes);

			return secureRequest;
		}
		/// <summary>
		/// Unwraps a command from a SecureResponse controller.
		/// </summary>
		/// <param name="response">SecureResponse.</param>
		/// <returns>Unwrappod value of RecureResponse as a string.</returns>
		public string UnwrapResponse(SecResponse response)
		{
			// Decrypt the reponse:
			byte[] DecryptedData = DecryptTDES(response.SEC_CMDBLK.Value);

			// Turns the response into text:
			string DecryptedResponse = CrossPlatformController.TextEncodingController.GetString(TextEncodingType.Ascii, DecryptedData);

			// Trim null bytes:
			string UnpaddedDecryptedResponse = DecryptedResponse.TrimEnd('\0');

			return UnpaddedDecryptedResponse;
		}
	}
}
