using Pinpad.Sdk.Commands;
using System;
using System.Globalization;

/* WARNING!
 * 
 * DEPRECATED.
 * MUST BE REFACTORED.
 * 
 */

namespace Pinpad.Sdk.Properties 
{
	/// <summary>
	/// Contains commonly used String Parsers.
	/// </summary>
	public class DefaultStringParser 
	{
		/// <summary>
		/// Parses a long integer with the specified string length.
		/// </summary>
		/// <param name="reader">String reader.</param>
		/// <param name="length">String length.</param>
		/// <returns>Long value.</returns>
		public static Nullable<long> LongIntegerStringParser(StringReader reader, int length)
		{
			return reader.ReadLong(length);
		}
		/// <summary>
		/// Parses a integer with the specified string length.
		/// </summary>
		/// <param name="reader">String reader.</param>
		/// <param name="length">String length.</param>
		/// <returns>Integer value.</returns>
		public static Nullable<int> IntegerStringParser(StringReader reader, int length)
		{
			return reader.ReadInt(length);
		}
		/// <summary>
		/// Parses a hexadecimal string with the specified string length.
		/// </summary>
		/// <param name="reader">String reader.</param>
		/// <param name="length">String length.</param>
		/// <returns>HexadecimalData value.</returns>
		public static HexadecimalData HexadecimalStringParser(StringReader reader, int length)
		{
			string data = reader.ReadString(length);
			return new HexadecimalData(data.Trim());
		}
		/// <summary>
		/// Parses a hexadecimal string with the specified string length and trim zero bytes at the right side.
		/// </summary>
		/// <param name="reader">String reader.</param>
		/// <param name="length">String length.</param>
		/// <returns>HexadecimalData value.</returns>
		public static HexadecimalData HexadecimalRightPaddingStringParser(StringReader reader, int length)
		{
			string data = reader.ReadString(length);

			while (data.EndsWith("00") == true)
			{
				data = data.Remove(data.Length - 2, 2);
			}

			return new HexadecimalData(data);
		}
		/// <summary>
		/// Parses a nullable boolean string.
		/// </summary>
		/// <param name="reader">String reader.</param>
		/// <returns>Boolean value.</returns>
		public static Nullable<bool> BooleanStringParser(StringReader reader)
		{
			return reader.ReadBool();
		}
		/// <summary>
		/// Parses a string with the remaining data.
		/// </summary>
		/// <param name="reader">String reader.</param>
		/// <returns>String value.</returns>
		public static string StringStringParser(StringReader reader)
		{
			return DefaultStringParser.StringStringParser(reader, reader.Remaining);
		}
		/// <summary>
		/// Parses a string with the specified string length.
		/// </summary>
		/// <param name="reader">String reader.</param>
		/// <param name="length">String length.</param>
		/// <returns>String value.</returns>
		public static string StringStringParser(StringReader reader, int length)
		{
			return reader.ReadString(length);
		}
		/// <summary>
		/// Parses a character.
		/// </summary>
		/// <param name="reader">String reader.</param>
		/// <returns>Char value.</returns>
		public static Nullable<char> CharStringParser(StringReader reader)
		{
			return Convert.ToChar(DefaultStringParser.StringStringParser(reader, 1));
		}
		/// <summary>
		/// Parses a Date with the default format (yyMMdd).
		/// </summary>
		/// <param name="reader">String reader.</param>
		/// <returns>DateTime value on format (yyMMdd).</returns>
		public static Nullable<DateTime> DateStringParser(StringReader reader)
		{
			string substring = reader.ReadString(6);

			// Validation:
			if (substring == "000000") { return null; }

			return DateTime.ParseExact(substring, "yyMMdd", CultureInfo.InvariantCulture);
		}
		/// <summary>
		/// Parses a Time with the default format (HHmmss).
		/// </summary>
		/// <param name="reader">String reader.</param>
		/// <returns>DateTime value on format (HHmmss).</returns>
		public static Nullable<DateTime> TimeStringParser(StringReader reader)
		{
			string substring = reader.ReadString(6);

			// Validation:
			if (substring == "000000") { return null; }

			return DateTime.ParseExact(substring, "HHmmss", CultureInfo.InvariantCulture);
		}
		/// <summary>
		/// Parses a Date and Time with the default format (yyMMddHHmmss).
		/// </summary>
		/// <param name="reader">String reader.</param>
		/// <returns>DateTime value on format (yyMMddHHmmss).</returns>
		public static Nullable<DateTime> DateTimeStringParser(StringReader reader)
		{
			string substring = reader.ReadString(12);

			// Validation:
			if (substring == "000000000000") { return null; }

			return DateTime.ParseExact(substring, "yyMMddHHmmss", CultureInfo.InvariantCulture);
		}
		/// <summary>
		/// Default string parser for enums where the index 0 is undefined therefore all values have 1 added to them.
		/// 
		/// Example: RSP_STAT : ST_OK = 0
		/// PinPadCommandResponseStatusEnum : 0 : Undefined
		/// PinPadCommandResponseStatusEnum : 1 : ST_OK
		/// </summary>
		/// <typeparam name="enumType">Enum type to use, throws InvalidOperationException when used with a type that is not an enum.</typeparam>
		/// <param name="reader">String reader.</param>
		/// <param name="length">String length.</param>
		/// <returns>Enum value.</returns>
		public static enumType EnumStringParser<enumType>(StringReader reader, int length) where enumType 
			: struct
		{
			// Validation:
			if (typeof(enumType).IsEnum == false)
			{
				throw new InvalidOperationException(typeof(enumType).Name + " is not a Enum.");
			}
			else if (Enum.GetNames(typeof(enumType))[0] != "Undefined")
			{
				throw new InvalidOperationException(typeof(enumType).Name + " default value is not Undefined.");
			}
			
			int intValue = DefaultStringParser.IntegerStringParser(reader, length).Value + 1;
			return (enumType)Enum.ToObject(typeof(enumType), intValue);
		}
		/// <summary>
		/// Default string parser for properties controllers.
		/// </summary>
		/// <typeparam name="propertyControllerType">Property controller type to use.</typeparam>
		/// <param name="reader">String reader.</param>
		/// <param name="length">String length.</param>
		/// <returns>propertyControllerType value.</returns>
		public static propertyControllerType PropertyControllerStringParser<propertyControllerType>(StringReader reader, int length) 
			where propertyControllerType : BaseProperty, new()
		{
			string data = DefaultStringParser.StringStringParser(reader, length);

			propertyControllerType value = new propertyControllerType();
			value.CommandString = data;
			return value;
		}
	}
}
