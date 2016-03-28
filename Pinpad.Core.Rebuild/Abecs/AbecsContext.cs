using System;
using System.Linq;

namespace Pinpad.Core.Rebuild.Abecs
{
	public class AbecsContext : IContext
	{
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

		/// <summary>
		/// Abecs commands use SYN byte to indicate beggining of a package.
		/// </summary>
		public byte StartByte { get { return SYN_BYTE; } }
		/// <summary>
		/// Abecs commands use ETB byte to indicate the end of a package.
		/// </summary>
		public byte EndByte { get { return ETB_BYTE; } }

		/// <summary>
		/// Generates the data checksum accordingly to the ABECS specification.
		/// </summary>
		/// <param name="data">Message bytes.</param>
		/// <returns>Two bytes of checksum.</returns>
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
		/// Compare bytes as integrity code.
		/// </summary>
		/// <param name="firstCode">First two bytes of checksum.</param>
		/// <param name="secondCode">Second two bytes of checksum (confirmation).</param>
		/// <returns>If both checksum received are equal.</returns>
		public bool IsIntegrityCodeValid (byte [] firstCode, byte [] secondCode)
		{
			if (firstCode [0] != secondCode [0] || firstCode [1] != secondCode [1])
			{
				return false;
			}

			return true;
		}
	}
}
