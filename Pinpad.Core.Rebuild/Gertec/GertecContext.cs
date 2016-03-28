using System;

namespace Pinpad.Core.Rebuild.Gertec
{
	/// <summary>
	/// Gertec command context.
	/// Contains low details about the Link Layer, accor
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
		/// Byte that indicates the beggining of the command.
		/// </summary>
		public byte StartByte { get { return STX_BYTE; } }
		/// <summary>
		/// Byte that indicates the end of the command.
		/// </summary>
		public byte EndByte { get { return ETX_BYTE; } }

		/// <summary>
		/// Generates the LRC (Longitudinal Redundancy Check – XOR of all bytes).
		/// </summary>
		/// <param name="data">Data to be shifted into LRC.</param>
		/// <returns>One byte corresponding to the LRC.</returns>
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
		/// Compare two bytes as integrity code.
		/// </summary>
		/// <param name="firstCode">First LRC.</param>
		/// <param name="secondCode">Second LRC.</param>
		/// <returns>If both bytes received are equal.</returns>
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
