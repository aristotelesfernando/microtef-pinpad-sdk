using CrossPlatformBase;
using Dlp.Buy4.Switch.Utils;
using PinPadSDK.Commands;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace PinPadSDK.Tools
{
    public class PinPadSecureCommandController
    {
        private static byte[] StoneTDESKey = {0x6A, 0x82, 0xE5, 0x6E, 0xBE, 0x60, 0x32, 0x4C, 0x10, 0x83, 0x47, 0x6D, 0xA9, 0x93, 0x83, 0xFB, 0xD7, 0x6B, 0x9C, 0xD4, 0x0D, 0x42, 0x33, 0xCD};

        private static byte[] EncryptTDES(string str)
        {
            return EncryptTDES(HexUtils.ToBytes(str));
        }

        private static byte[] EncryptTDES(byte[] data)
        {
            //null generates a ECB encryption out of CBC
            return CrossPlatformController.EncryptionController.EncryptTDES(data, StoneTDESKey, null);
        }

        private static byte[] DecryptTDES(string str)
        {
            return DecryptTDES(HexUtils.ToBytes(str));
        }

        private static byte[] DecryptTDES(byte[] data)
        {
            //null generates a ECB encryption out of CBC
            return CrossPlatformController.EncryptionController.DecryptTDES(data, StoneTDESKey, null);
        }

        public static SECRequest WrapRequest(CMDRequestBase Request)
        {
            SECRequest SecureRequest = new SECRequest();
            SecureRequest.SEC_ACQIDX = 16;

            byte[] RequestData = Request.GetBytes(false);
            byte[] EncryptedRequest = EncryptTDES(RequestData);
            byte[] DecryptedRequest = DecryptTDES(EncryptedRequest);

            string EncryptedRequestString = HexUtils.FromBytes(EncryptedRequest);

            SecureRequest.SEC_CMDBLK = EncryptedRequestString;

            return SecureRequest;
        }

        public static string UnwrapResponse(SECResponse Response)
        {
            byte[] DecryptedData = DecryptTDES(Response.SEC_CMDBLK);
            string DecryptedResponse = CrossPlatformController.TextEncodingController.GetString(TextEncoding.ASCII, DecryptedData);
            string UnpaddedDecryptedResponse = DecryptedResponse.TrimEnd('\0');
            return UnpaddedDecryptedResponse;
        }
    }
}
