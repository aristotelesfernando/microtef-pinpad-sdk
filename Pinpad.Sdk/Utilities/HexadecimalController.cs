using System;

namespace Pinpad.Sdk 
{
	/// <summary>
	/// Contains utilities for hexadecimal manipulation.
	/// </summary>
	public class HexadecimalController
	{

		/// <summary>
		/// Converts a stream of bytes into a hexadecimal representation.
		/// </summary>
		/// <param name="buffer">Stream of bytes to convert.</param>
		/// <param name="count">Number fo bytes to convert.</param>
		/// <param name="offset">Offset to start reading.</param>
		/// <returns>Hexadecimal representation.</returns>
		public static string FromBytes(byte[] buffer, int count, int offset = 0) {
			if (buffer == null) return "";

			// Check for negative values.
			if (count < 0)
				throw new IndexOutOfRangeException("'count' cannot be negative.");
			if (offset < 0)
				throw new IndexOutOfRangeException("'offset' cannot be negative.");

			// Check for huge values.
			if (count + offset > buffer.Length)
				throw new IndexOutOfRangeException("'count' and 'offset' combination is out of range.");

			char[] hex = new char[count * 2];
			for (int i = 0; i < count; ++i) {
				hex[2 * i] = hexCharacters[(buffer[i + offset] >> 4) & 0xF];
				hex[2 * i + 1] = hexCharacters[buffer[i + offset] & 0xF];
			}

			return new string(hex);
		}

		/// <summary>
		/// Converts a stream of bytes into a hexadecimal representation.
		/// </summary>
		/// <param name="buffer">Stream of bytes to convert.</param>
		/// <returns>Hexadecimal representation.</returns>
		public static string FromBytes(byte[] buffer) {
			if (buffer == null) return "";

			return FromBytes(buffer, buffer.Length, 0);
		}

		/// <summary>
		/// Converts a hexadecimal string to a stream of bytes.
		/// </summary>
		/// <param name="hex">Hexadecimal string to convert.</param>
		/// <param name="count">Number fo bytes to convert.</param>
		/// <param name="offset">Offset to start reading.</param>
		/// <returns>Stream of bytes.</returns>
		public static byte[] ToBytes(string hex, int count, int offset = 0) {
			if (string.IsNullOrEmpty(hex))
				return new byte[0];

			// Check for negative values.
			if (count < 0)
				throw new IndexOutOfRangeException("'count' cannot be negative.");
			if (offset < 0)
				throw new IndexOutOfRangeException("'offset' cannot be negative.");

			// Check for huge values.
			if (2 * count + offset > hex.Length)
				throw new IndexOutOfRangeException("'count' and 'offset' combination is out of range.");

			byte[] buffer = new byte[count];

			for (int i = 0; i < buffer.Length; ++i) {
				char b1 = hex[(i << 1) + offset];
				char b2 = hex[(i << 1) + offset + 1];
				if (b1 < '0' || b2 < '0' || b1 > 'f' || b2 > 'f')
					break;
				buffer[i] = (byte)(hexValues[b2 - '0'] | (hexValues[b1 - '0'] << 4));
			}

			return buffer;
		}

		/// <summary>
		/// Converts a hexadecimal string to a stream of bytes.
		/// </summary>
		/// <param name="hex">Hexadecimal string to convert.</param>
		/// <returns>Stream of bytes.</returns>
		public static byte[] ToBytes(string hex) {
			if (string.IsNullOrEmpty(hex))
				return new byte[0];

			return ToBytes(hex, hex.Length / 2, 0);
		}


		/// <summary>
		/// Vector with hexadecimal characters.
		/// </summary>
		static char[] hexCharacters = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F' };

		/// <summary>
		/// Vector with values for each hexadecimal character.
		/// </summary>
		static byte[] hexValues = {
			0,1,2,3,4,5,6,7,8,9,0,0,0,0,0,0,0,10,11,12,13,14,15,0,0,0,0,
			0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,10,11,12,13,14,15
		};
	}
}
