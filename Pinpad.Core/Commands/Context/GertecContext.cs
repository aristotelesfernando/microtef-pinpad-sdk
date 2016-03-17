using System;
using System.Linq;
using System.Collections.Generic;
using MicroPos.CrossPlatform;
using MicroPos.CrossPlatform.TypeCode;
using Pinpad.Core.Pinpad;

namespace Pinpad.Core.Commands.Context
{
	/// <summary>
	/// A command context accordingly to Gertec specification.
	/// </summary>
	public class GertecContext : IContext
	{
		/// <summary>
		/// STX byte indicates the begining of a package, in a Gertec command.
		/// </summary>
		public const byte STX_BYTE = 0x02;
		/// <summary>
		/// ETX bytes indicates the end of a package, in a Gertec command.
		/// </summary>
		public const byte ETX_BYTE = 0x03;

		/// <summary>
		/// Command name fixed length.
		/// Names of Gertec commands have 4 characters.
		/// </summary>
		public short CommandNameLength { get { return 4; } }
		/// <summary>
		/// Gertec commands use STX byte to indicate beggining of a package.
		/// </summary>
		public byte StartByte { get { return STX_BYTE; } }
		/// <summary>
		/// Gertec commands use ETX byte to indicate end of a package.
		/// </summary>
		public byte EndByte { get { return ETX_BYTE; } }

		public short StatusLength
		{
			get
			{
				return 1;
			}
		}

		public short IntegrityCodeLength { get { return 1; } }

		public bool HasToIncludeFirstByte { get { return true; } }

		/// <summary>
		/// Generates the LRC (Longitudinal Redundancy Check – XOR of all bytes).
		/// </summary>
		/// <param name="data">Data to be shifted into LRC.</param>
		/// <returns>The LRC.</returns>
		public byte [] GetIntegrityCode (byte [] data)
		{
			byte lrc = 0;

			for (int i = 0; i < data.Length; i++)
			{
				lrc ^= data [i];
			}

			return new byte [] { lrc };
		}
		/// <summary>
		/// Includes bytes of beggining and end of a command, and includes LRC, accordingly to Gertec specifications.
		/// </summary>
		/// <param name="request">Request to be sent.</param>
		/// <returns>List of bytes ready to be sent to the pinpad.</returns>
		public List<byte> GetRequestBody (BaseCommand request)
		{
			List<byte> requestBody = new List<byte>(CrossPlatformController.TextEncodingController.GetBytes(TextEncodingType.Ascii, request.CommandString));

			// Add STX (indication the begining of a package):
			requestBody.Insert(0, STX_BYTE);

			// Inserts a null byte, accordingly to Gertec specs:
			requestBody.Insert(1, PinpadCommunication.NULL_BYTE);

			// Insert the length of the command, hardcoded, because it's fixed (25 characters):
			requestBody.Insert(2, 0x19);

			// Add ETX (indicating the end of a package):
			requestBody.Add(ETX_BYTE);

			// Add LRC(Longitudinal Redundancy Check – XOR of all bytes from STX to ETX) at the end of the command:
			requestBody.AddRange(this.GetIntegrityCode(requestBody.ToArray()));

			return requestBody;
		}
		public void FormatResponse (List<byte> response)
		{
			// Remove STX
			response.Remove(STX_BYTE);

			// Remove o byte nulo
			response.Remove(PinpadCommunication.NULL_BYTE);

			// Remove o byte de tamanho.
			response.Remove(0x0C);
		}
		public bool IsIntegrityCodeValid (byte [] firstCode, byte [] secondCode)
		{
			if (firstCode [0] != secondCode [0])
			{
				return false;
			}

			return true;
		}
	}
}
