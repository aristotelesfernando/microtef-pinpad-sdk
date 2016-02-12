using CrossPlatformBase;
using Pinpad.Core.Commands;
using Pinpad.Core.Utilities;

namespace Pinpad.Core.Pinpad
{
	/// <summary>
	/// Controller for Stone Secure Command
	/// </summary>
	public class PinpadEncryption
	{
		private static PinpadEncryption instance = new PinpadEncryption( );

		private PinpadEncryption() {  }

		/// <summary>
		/// Gets the Singleton of PinpadEncryption
		/// </summary>
		public static PinpadEncryption Instance {
			get {
				return PinpadEncryption.instance;
			}
		}

		private byte[] StoneTDESKey = { 0x6A, 0x82, 0xE5, 0x6E, 0xBE, 0x60, 0x32, 0x4C, 0x10, 0x83, 0x47, 0x6D, 0xA9, 0x93, 0x83, 0xFB, 0xD7, 0x6B, 0x9C, 0xD4, 0x0D, 0x42, 0x33, 0xCD };

		private byte[] EncryptTDES(HexadecimalData value) {
			//null generates a ECB encryption out of CBC
			return CrossPlatformController.EncryptionController.EncryptTDES(value.Data, StoneTDESKey, null);
		}

		private byte[] DecryptTDES(HexadecimalData value) {
			//null generates a ECB encryption out of CBC
			return CrossPlatformController.EncryptionController.DecryptTDES(value.Data, StoneTDESKey, null);
		}

		/// <summary>
		/// Wraps a command into SecureRequest controller
		/// </summary>
		/// <param name="request">request string</param>
		/// <returns>PinpadSecRequestController</returns>
		public SecRequest WrapRequest(string request) {
			SecRequest secureRequest = new SecRequest( );
			secureRequest.SEC_ACQIDX.Value = 16;

			byte[] requestBytes = CrossPlatformController.TextEncodingController.GetBytes(TextEncoding.ASCII, request);
			byte[] encryptedRequestBytes = EncryptTDES(new HexadecimalData(requestBytes));

			secureRequest.SEC_CMDBLK.Value = new HexadecimalData(encryptedRequestBytes);

			return secureRequest;
		}

		/// <summary>
		/// Unwraps a command from a SecureResponse controller
		/// </summary>
		/// <param name="response">SecureResponse controller</param>
		/// <returns>command string</returns>
		public string UnwrapResponse(SecResponse response) {
			byte[] DecryptedData = DecryptTDES(response.SEC_CMDBLK.Value);
			string DecryptedResponse = CrossPlatformController.TextEncodingController.GetString(TextEncoding.ASCII, DecryptedData);
			string UnpaddedDecryptedResponse = DecryptedResponse.TrimEnd('\0'); //Trim null bytes
			return UnpaddedDecryptedResponse;
		}
	}
}
