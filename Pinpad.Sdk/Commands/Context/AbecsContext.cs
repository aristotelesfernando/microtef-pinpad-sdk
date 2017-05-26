using System;
using System.Linq;
using System.Collections.Generic;
using Pinpad.Sdk.PinpadProperties.Refactor.Command;

namespace Pinpad.Sdk.Commands
{
    /// <summary>
    /// Command context accordingly to abecs.
    /// </summary>
    internal class AbecsContext : IContext
	{
		// Constants
		/// <summary>
		/// Cyclic Redundance Check.
		/// Is a technique for detecting errors in digital data.
		/// </summary>
		const UInt16 CRC_MASK = 0x1021;
		/// <summary>
		/// ETB bytes indicates the end of a package.
		/// </summary>
		public const byte ETB_BYTE = 0x17;
		/// <summary>
		/// SYN byte indicates the begining of a package.
		/// </summary>
		public const byte SYN_BYTE = 0x16;

		// Members
		/// <summary>
		/// Abecs commands use SYN byte to indicate beggining of a package.
		/// </summary>
		public byte StartByte { get { return SYN_BYTE; } }
		/// <summary>
		/// Abecs commands use ETB byte to indicate the end of a package.
		/// </summary>
		public byte EndByte { get { return ETB_BYTE; } }
		/// <summary>
		/// Names of Abecs commands have 3 characters.
		/// </summary>
		public short CommandNameLength { get { return 3; } }
		/// <summary>
		/// Abecs response status.
		/// </summary>
		public Type StatusType { get { return typeof(AbecsResponseStatus); } }

		public short StatusLength
		{
			get
			{
				return 3;
			}
		}

		public short IntegrityCodeLength { get { return 2; } }

		public bool HasToIncludeFirstByte { get { return false; } }

		// Methods
		/// <summary>
		/// Generates the data checksum accordingly to the ABECS specification.
		/// </summary>
		/// <param name="data">Message bytes.</param>
		/// <returns>Checksum.</returns>
		public byte [] GetIntegrityCode (byte [] data)
		{
			UInt16 wData, wCRC = 0;

			for (int i = 0; i < data.Length; i++)
			{
				wData = (UInt16) (data [i] << 8);
				for (int d = 0; d < 8; d++, wData <<= 1)
				{
					if (((wCRC ^ wData) & 0x8000) != 0)
					{
						wCRC = (UInt16) ((wCRC << 1) ^ CRC_MASK);
					}
					else
					{
						wCRC <<= 1;
					}
				}
			}

			return BitConverter.GetBytes(wCRC).Reverse().ToArray();
		}
		/// <summary>
		/// Includes bytes of beggining and end of a command, and includes CheckSum, accordingly to Abecs.
		/// </summary>
		/// <param name="request">Request to be sent.</param>
		/// <returns>List of bytes ready to be sent to the pinpad.</returns>
        public List<byte> GetRequestBody(BaseCommand request)
        {
            List<byte> requestBody = new List<byte>();

            requestBody.AddRange(request.CommandTrack);

            // Add ETB (indicating the end of a package):
            requestBody.Add(ETB_BYTE);

            // Generate checksum:
            requestBody.AddRange(this.GetIntegrityCode(requestBody.ToArray()));

            // Add SYN (indication the begining of a package):
            requestBody.Insert(0, SYN_BYTE);

            return requestBody;
        }

        /// <summary>
        /// Nothing to format.
        /// Do nothing.
        /// </summary>
        /// <param name="response">Response received from SPE.</param>
        public void FormatResponse (List<byte> response) { }
		public bool IsIntegrityCodeValid (byte [] firstCode, byte [] secondByte)
		{
			if (firstCode [0] != secondByte [0] || firstCode [1] != secondByte [1])
			{
				return false;
			}

			return true;
		}
	}
}
