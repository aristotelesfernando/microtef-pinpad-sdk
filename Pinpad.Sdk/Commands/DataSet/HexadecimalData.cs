using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

/* WARNING!
 * 
 * DEPRECATED.
 * MUST BE REFACTORED.
 * 
 */

namespace Pinpad.Sdk.Commands 
{
	/// <summary>
	/// Controller for Hexadecimal data
	/// </summary>
	public class HexadecimalData 
	{
		// Members
		/// <summary>
		/// Gets and Sets the data as a hexadecimal string
		/// </summary>
		public string DataString 
		{
			get {
				if (this.Data == null) {
					return null;
				}

				StringBuilder stringBuilder = new StringBuilder(Data.Length * 2);
				foreach (byte b in this.Data) {
					stringBuilder.AppendFormat("{0:X2}", b);
				}
				return stringBuilder.ToString();
			}
			private set {
				MatchCollection matchCollection = Regex.Matches(value, "[^0-9a-fA-F]");
				if (matchCollection.Count > 0) {
					List<string> invalidCharCollection = new List<string>();
					foreach (Match match in matchCollection) {
						if (invalidCharCollection.Contains(match.Value) == false) {
							invalidCharCollection.Add(match.Value);
						}
					}
					invalidCharCollection.Sort();
					throw new InvalidCastException("Invalid Hexadecimal String: \"" + value + "\"; Detected Invalid Chars: \"" + String.Join("", invalidCharCollection.ToArray()) + "\"");
				}
				else if (value.Length % 2 == 1) {
					throw new InvalidCastException("Invalid Hexadecimal String: \"" + value + "\", odd hexadecimal strings are not supported.");
				}

				byte[] data = new byte[value.Length / 2];
				for (int i = 0; i < data.Length; i++) {
					string byteString = value.Substring(i * 2, 2);
					byte b = Convert.ToByte(byteString, 16);
					data[i] = b;
				}
				this.Data = data;
			}
		}
		/// <summary>
		/// Gets and sets the data as byte array
		/// </summary>
		public byte[] Data { get; private set; }

		// Constructors
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="dataString">Hexadecimal string</param>
		public HexadecimalData(string dataString) 
		{
			this.DataString = dataString;
		}
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="data">byte array</param>
		public HexadecimalData(byte[] data) 
		{
			this.Data = data;
		}
	}
}
