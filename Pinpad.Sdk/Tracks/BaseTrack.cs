using Pinpad.Sdk.Properties;
using System;
using System.Globalization;
using System.Text;

/* WARNING!
 * 
 * DEPRECATED.
 * MUST BE REFACTORED.
 * 
 */

namespace Pinpad.Sdk.Tracks 
{
	/// <summary>
	/// Controller for Track1 data
	/// </summary>
	public abstract class BaseTrack : BaseProperty 
	{
		/// <summary>
		/// Separator used in the track data
		/// </summary>
		protected abstract string FieldSeparator { get; }

		/// <summary>
		/// String Formatter for Year and Month pattern
		/// </summary>
		/// <param name="obj">DateTime</param>
		/// <returns>string</returns>
		protected string YearAndMonthStringFormatter(Nullable<DateTime> obj) 
		{
			//Even though it's a nullable type this code is never reached with null
			string value = obj.Value.ToString("yyMM");
			return value;
		}

		/// <summary>
		/// String Parser for Year and Month pattern
		/// </summary>
		/// <param name="reader">StringReader</param>
		/// <returns>DateTime</returns>
		protected Nullable<DateTime> YearAndMonthStringParser(StringReader reader) 
		{
			if (reader.PeekString(1) == FieldSeparator) 
			{
				reader.Jump(1);
				return null;
			}
			else 
			{
				string substring = reader.ReadString(4);
				DateTime value = DateTime.ParseExact(substring, "yyMM", CultureInfo.InvariantCulture);
				
				return value;
			}
		}

		/// <summary>
		/// String Formatter for Strings with separator
		/// </summary>
		/// <param name="obj">string</param>
		/// <returns>string with separator</returns>
		protected string StringWithSeparatorStringFormatter(string obj) 
		{
			string value = obj.ToString() + FieldSeparator;
			
			return value;
		}

		/// <summary>
		/// String Parser for strings with separator
		/// </summary>
		/// <param name="reader">StringReader</param>
		/// <returns>string</returns>
		protected string StringWithSeparatorStringParser(StringReader reader) 
		{
			StringBuilder stringBuilder = new StringBuilder();
			string value = reader.ReadString(1);
			
			while (value != FieldSeparator) {
				stringBuilder.Append(value);
				value = reader.ReadString(1);
			}
			
			return stringBuilder.ToString();
		}
	}
}
