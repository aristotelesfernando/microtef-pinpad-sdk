using Pinpad.Core.Properties;
using Pinpad.Core.Utilities;
using System;

/* WARNING!
 * 
 * DEPRECATED.
 * MUST BE REFACTORED.
 * 
 */

namespace Pinpad.Core.Properties
{
	/// <summary>
	/// Contains commonly used StringFormatters
	/// </summary>
	public class DefaultStringFormatter 
	{
		/// <summary>
		/// Default integer string formatter
		/// </summary>
		/// <param name="obj">value to convert</param>
		/// <param name="length">length for the value as string</param>
		/// <returns>Value of the property as string</returns>
		public static string IntegerStringFormatter(Nullable<int> obj, int length) 
		{
			string value;
			if (obj.HasValue == false) {
				throw new InvalidOperationException("Can not parse a null value");
			}
			else {
				value = obj.ToString().PadLeft(length, '0');
			}
			return value;
		}
		/// <summary>
		/// Default long integer string formatter
		/// </summary>
		/// <param name="obj">value to convert</param>
		/// <param name="length">length for the value as string</param>
		/// <returns>Value of the property as string</returns>
		public static string LongIntegerStringFormatter(Nullable<long> obj, int length) {
			string value;
			if (obj.HasValue == false) {
				throw new InvalidOperationException("Can not parse a null value");
			}
			else {
				value = obj.ToString().PadLeft(length, '0');
			}
			return value;
		}
		/// <summary>
		/// Default hexadecimal numeric string formatter
		/// </summary>
		/// <param name="obj">value to convert</param>
		/// <returns>Value of the property as string</returns>
		public static string HexadecimalStringFormatter(HexadecimalData obj) {
			if (obj == null) {
				throw new InvalidOperationException("Can not parse a null value");
			}
			string value = obj.DataString;
			return value;
		}
		/// <summary>
		/// Default hexadecimal numeric string formatter
		/// </summary>
		/// <param name="obj">value to convert</param>
		/// <param name="length">length for the value as string</param>
		/// <returns>Value of the property as string</returns>
		public static string HexadecimalStringFormatter(HexadecimalData obj, int length) {
			string value = DefaultStringFormatter.HexadecimalStringFormatter(obj).PadLeft(length, '0');
			return value;
		}
		/// <summary>
		/// Default hexadecimal numeric string formatter with padding in the right side
		/// </summary>
		/// <param name="obj">value to convert</param>
		/// <param name="length">length for the value as string</param>
		/// <returns>Value of the property as string</returns>
		public static string HexadecimalRightPaddingStringFormatter(HexadecimalData obj, int length) {
			string value = DefaultStringFormatter.HexadecimalStringFormatter(obj).PadRight(length, '0');
			return value;
		}
		/// <summary>
		/// Default nullable boolean string formatter
		/// </summary>
		/// <param name="obj">value to convert</param>
		/// <returns>Value of the property as string or InvalidOperationException if null</returns>
		public static string BooleanStringFormatter(Nullable<bool> obj) {
			string value;

			if (obj.HasValue == false) {
				throw new InvalidOperationException("Can not parse a null value");
			}
			else if (obj.Value == true) {
				value = "1";
			}
			else {
				value = "0";
			}

			return value;
		}
		/// <summary>
		/// Default string formatter
		/// </summary>
		/// <param name="obj">string</param>
		/// <returns>Value of the property as string</returns>
		public static string StringStringFormatter(string obj) {
			if (obj == null) {
				throw new InvalidOperationException("Can not parse a null value");
			}
			return obj;
		}
		/// <summary>
		/// Default string formatter
		/// </summary>
		/// <param name="obj">string</param>
		/// <param name="length">length for the value as string, ignored</param>
		/// <returns>Value of the property as string</returns>
		public static string StringStringFormatter(string obj, int length) {
			string value = DefaultStringFormatter.StringStringFormatter(obj);
			if (value.Length != length) {
				throw new InvalidOperationException("StringStringFormatter(string obj, int length) should not be used when obj does not have the string length of length");
			}
			return value;
		}
		/// <summary>
		/// Default string formatter with right space padding
		/// </summary>
		/// <param name="obj">object to convert</param>
		/// <param name="length">length for the value as string</param>
		/// <returns>Value of the property as string</returns>
		public static string RightPaddingWithSpacesStringFormatter(string obj, int length) {
			string value = StringStringFormatter(obj).PadRight(length, ' ');
			return value;
		}
		/// <summary>
		/// Default string formatter with left space padding
		/// </summary>
		/// <param name="obj">object to convert</param>
		/// <param name="length">length for the value as string</param>
		/// <returns>Value of the property as string</returns>
		public static string LeftPaddingWithSpacesStringFormatter(string obj, int length) {
			string value = StringStringFormatter(obj).PadLeft(length, ' ');
			return value;
		}
		/// <summary>
		/// Default character string formatter
		/// </summary>
		/// <param name="obj">object to convert</param>
		/// <returns>Value of the property as string</returns>
		public static string CharStringFormatter(Nullable<char> obj) {
			if (obj.HasValue == false) {
				throw new InvalidOperationException("Can not parse a null value");
			}
			else {
				string value = obj.ToString();
				return value;
			}
		}
		/// <summary>
		/// Default date string formatter (yyMMdd)
		/// </summary>
		/// <param name="obj">object to convert</param>
		/// <returns>Value of the property as string</returns>
		public static string DateStringFormatter(Nullable<DateTime> obj) {
			if (obj.HasValue == false) {
				throw new InvalidOperationException("Can not parse a null value");
			}
			else {
				string value = obj.Value.ToString("yyMMdd");
				return value;
			}
		}
		/// <summary>
		/// Default time string formatter (HHmmss)
		/// </summary>
		/// <param name="obj">object to convert</param>
		/// <returns>Value of the property as string</returns>
		public static string TimeStringFormatter(Nullable<DateTime> obj) {
			if (obj.HasValue == false) {
				throw new InvalidOperationException("Can not parse a null value");
			}
			else {
				string value = obj.Value.ToString("HHmmss");
				return value;
			}
		}
		/// <summary>
		/// Default date and time string formatter (yyMMddHHmmss)
		/// </summary>
		/// <param name="obj">object to convert</param>
		/// <returns>Value of the property as string</returns>
		public static string DateTimeStringFormatter(Nullable<DateTime> obj) {
			if (obj.HasValue == false) {
				throw new InvalidOperationException("Can not parse a null value");
			}
			else {
				string value = obj.Value.ToString("yyMMddHHmmss");
				return value;
			}
		}
		/// <summary>
		/// Default string formatter for enums where the index 0 is undefined therefore all values have 1 added to them.
		/// 
		/// Example: RSP_STAT : ST_OK = 0
		/// PinPadCommandResponseStatusEnum : 0 : Undefined
		/// PinPadCommandResponseStatusEnum : 1 : ST_OK
		/// </summary>
		/// <typeparam name="enumType">enum type to use, throws InvalidOperationException when used with a type that is not a enum</typeparam>
		/// <param name="obj">object to convert</param>
		/// <param name="length">length for the value as string</param>
		/// <returns>Value of the property as string</returns>
		public static string EnumStringFormatter<enumType>(enumType obj, int length) where enumType : struct {
			if (typeof(enumType).IsEnum == false) {
				throw new InvalidOperationException(typeof(enumType).Name + " is not a Enum.");
			}
			else if (Enum.GetNames(typeof(enumType))[0] != "Undefined") {
				throw new InvalidOperationException(typeof(enumType).Name + " default value is not Undefined.");
			}

			int realIntValue = Convert.ToInt32(obj);
			if (realIntValue == 0) {
				throw new InvalidOperationException("Can not parse a null value");
			}
			else {
				int intValue = realIntValue - 1;
				string value = DefaultStringFormatter.IntegerStringFormatter(intValue, length);
				return value;
			}
		}
		/// <summary>
		/// Default string formatter for properties controllers
		/// </summary>
		/// <param name="obj">object to convert</param>
		/// <returns>Value of the property as string</returns>
		public static string PropertyControllerStringFormatter(BaseProperty obj) {
			if (obj == null) {
				throw new InvalidOperationException("Can not parse a null value");
			}
			else {
				string value = obj.CommandString;
				return value;
			}
		}
		/// <summary>
		/// Default string formatter for properties controllers
		/// </summary>
		/// <param name="obj">object to convert</param>
		/// <param name="length">ignored</param>
		/// <returns>Value of the property as string</returns>
		public static string PropertyControllerStringFormatter(BaseProperty obj, int length) {
			string value = DefaultStringFormatter.PropertyControllerStringFormatter(obj);
			if (value.Length != length) {
				throw new InvalidOperationException("PropertyControllerStringFormatter(BaseProperty obj, int length) should not be used when obj does not have the string length of length");
			}
			return value;
		}
	}
}
