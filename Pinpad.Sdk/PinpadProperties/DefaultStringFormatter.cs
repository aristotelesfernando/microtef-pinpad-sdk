using Pinpad.Sdk.Commands;
using System;

/* WARNING!
 * 
 * DEPRECATED.
 * MUST BE REFACTORED.
 * 
 */

namespace Pinpad.Sdk.Properties
{
	/// <summary>
	/// Contains commonly used StringFormatters
	/// </summary>
	public sealed class DefaultStringFormatter 
	{
		/// <summary>
		/// Default integer string formatter.
		/// </summary>
		/// <param name="obj">Value to convert.</param>
		/// <param name="length">Length for the value as string.</param>
		/// <returns>Value of the property as string.</returns>
		public static string IntegerStringFormatter(Nullable<int> obj, int length) 
		{
			// Validation:
			if (obj.HasValue == false) { throw new InvalidOperationException("Can not parse a null value"); }
			
			return obj.ToString().PadLeft(length, '0');
		}
		/// <summary>
		/// Default long integer string formatter.
		/// </summary>
		/// <param name="obj">Value to convert.</param>
		/// <param name="length">Length for the value as string.</param>
		/// <returns>Value of the property as string.</returns>
		public static string LongIntegerStringFormatter(Nullable<long> obj, int length)
		{
			// Validation:
			if (obj.HasValue == false) { throw new InvalidOperationException("Can not parse a null value"); }
			
			return obj.ToString().PadLeft(length, '0');
		}
		/// <summary>
		/// Default hexadecimal numeric string formatter.
		/// </summary>
		/// <param name="obj">Value to convert.</param>
		/// <returns>Value of the property as string.</returns>
		public static string HexadecimalStringFormatter(HexadecimalData obj)
		{
			// Validation:
			if (obj == null) { throw new InvalidOperationException("Can not parse a null value"); }

			return obj.DataString;
		}
		/// <summary>
		/// Default hexadecimal numeric string formatter.
		/// </summary>
		/// <param name="obj">Value to convert.</param>
		/// <param name="length">Length for the value as string.</param>
		/// <returns>Value of the property as string.</returns>
		public static string HexadecimalStringFormatter(HexadecimalData obj, int length)
		{
			// Validation:
			if (obj == null) { throw new InvalidOperationException("Can not parse a null value"); }

			return DefaultStringFormatter.HexadecimalStringFormatter(obj).PadLeft(length, '0');
		}
		/// <summary>
		/// Default hexadecimal numeric string formatter with padding in the right side.
		/// </summary>
		/// <param name="obj">Value to convert.</param>
		/// <param name="length">Length for the value as string.</param>
		/// <returns>Value of the property as string.</returns>
		public static string HexadecimalRightPaddingStringFormatter(HexadecimalData obj, int length)
		{
			// Validation:
			if (obj == null) { throw new InvalidOperationException("Can not parse a null value"); }

			return DefaultStringFormatter.HexadecimalStringFormatter(obj).PadRight(length, '0');
		}
		/// <summary>
		/// Default nullable boolean string formatter.
		/// </summary>
		/// <param name="obj">Value to convert.</param>
		/// <returns>Value of the property as string or InvalidOperationException if null.</returns>
		public static string BooleanStringFormatter(Nullable<bool> obj)
		{
			// Validation:
			if (obj.HasValue == false) { throw new InvalidOperationException("Can not parse a null value"); }

			return (obj.Value == true) ? "1" : "0";
		}
		/// <summary>
		/// Default string formatter.
		/// </summary>
		/// <param name="obj">String.</param>
		/// <returns>Value of the property as string.</returns>
		public static string StringStringFormatter(string obj)
		{
			// Validation:
			if (obj == null) { throw new InvalidOperationException("Can not parse a null value"); }

			return obj;
		}
		/// <summary>
		/// Default string formatter.
		/// </summary>
		/// <param name="obj">String.</param>
		/// <param name="length">Length for the value as string.</param>
		/// <returns>Value of the property as string.</returns>
		public static string StringStringFormatter(string obj, int length)
		{
			string value = DefaultStringFormatter.StringStringFormatter(obj);

			// Validation:
			if (value.Length != length)
			{
				throw new InvalidOperationException("StringStringFormatter(string obj, int length) should not be used when obj does not have the string length of length");
			}

			return value;
		}
		/// <summary>
		/// Default string formatter with right space padding.
		/// </summary>
		/// <param name="obj">Object to convert.</param>
		/// <param name="length">Length for the value as string.</param>
		/// <returns>Value of the property as string.</returns>
		public static string RightPaddingWithSpacesStringFormatter(string obj, int length)
		{
			return StringStringFormatter(obj).PadRight(length, ' ');
		}
		/// <summary>
		/// Default string formatter with left space padding.
		/// </summary>
		/// <param name="obj">Object to convert.</param>
		/// <param name="length">Length for the value as string.</param>
		/// <returns>Value of the property as string.</returns>
		public static string LeftPaddingWithSpacesStringFormatter(string obj, int length)
		{
			return StringStringFormatter(obj).PadLeft(length, ' ');
		}
		/// <summary>
		/// Default character string formatter.
		/// </summary>
		/// <param name="obj">Object to convert.</param>
		/// <returns>Value of the property as string.</returns>
		public static string CharStringFormatter(Nullable<char> obj)
		{
			// Validation:
			if (obj.HasValue == false) { throw new InvalidOperationException("Can not parse a null value"); }

			return obj.ToString();
		}
		/// <summary>
		/// Default date string formatter (yyMMdd).
		/// </summary>
		/// <param name="obj">Object to convert.</param>
		/// <returns>Value of the property as string.</returns>
		public static string DateStringFormatter(Nullable<DateTime> obj)
		{
			// Validation:
			if (obj.HasValue == false) { throw new InvalidOperationException("Can not parse a null value"); }
			
			return obj.Value.ToString("yyMMdd");
		}
		/// <summary>
		/// Default time string formatter (HHmmss).
		/// </summary>
		/// <param name="obj">Object to convert.</param>
		/// <returns>Value of the property as string.</returns>
		public static string TimeStringFormatter(Nullable<DateTime> obj)
		{
			// Validation:
			if (obj.HasValue == false) { throw new InvalidOperationException("Can not parse a null value"); }

			return obj.Value.ToString("HHmmss");
		}
		/// <summary>
		/// Default date and time string formatter (yyMMddHHmmss).
		/// </summary>
		/// <param name="obj">Object to convert.</param>
		/// <returns>Value of the property as string.</returns>
		public static string DateTimeStringFormatter(Nullable<DateTime> obj)
		{
			// Validation:
			if (obj.HasValue == false) { throw new InvalidOperationException("Can not parse a null value"); }

			return obj.Value.ToString("yyMMddHHmmss");
		}
		/// <summary>
		/// Default string formatter for enums where the index 0 is undefined therefore all values have 1 added to them.
		/// 
		/// Example: RSP_STAT : ST_OK = 0
		/// PinPadCommandResponseStatusEnum : 0 : Undefined
		/// PinPadCommandResponseStatusEnum : 1 : ST_OK
		/// </summary>
		/// <typeparam name="enumType">Enum type to use, throws InvalidOperationException when used with a type that is not an enum.</typeparam>
		/// <param name="obj">Object to convert.</param>
		/// <param name="length">Length for the value as string.</param>
		/// <returns>Value of the property as string.</returns>
		public static string EnumStringFormatter<enumType>(enumType obj, int length) where enumType : struct
		{
			// Validation:
			if (typeof(enumType).IsEnum == false)
			{
				throw new InvalidOperationException(typeof(enumType).Name + " is not a Enum.");
			}
			if (Enum.GetNames(typeof(enumType))[0] != "Undefined") {
				throw new InvalidOperationException(typeof(enumType).Name + " default value is not Undefined.");
			}

			int realIntValue = Convert.ToInt32(obj);

			// Is zero if obj is null:
			if (realIntValue == 0) { throw new InvalidOperationException("Can not parse a null value"); }
			
			// Returns enum as integer:
			return DefaultStringFormatter.IntegerStringFormatter((realIntValue - 1), length);
		}
		/// <summary>
		/// Default string formatter for properties controllers.
		/// </summary>
		/// <param name="obj">Object to convert.</param>
		/// <returns>Value of the property as string.</returns>
		public static string PropertyControllerStringFormatter(BaseProperty obj)
		{
			// Validation:
			if (obj == null) { throw new InvalidOperationException("Can not parse a null value"); }

			return obj.CommandString;
		}
		/// <summary>
		/// Default string formatter for properties controllers.
		/// </summary>
		/// <param name="obj">Object to convert.</param>
		/// <param name="length">Length of the obj as string.</param>
		/// <returns>Value of the property as string.</returns>
		public static string PropertyControllerStringFormatter(BaseProperty obj, int length)
		{
			// Validation:
			if (obj == null) { throw new InvalidOperationException("Can not parse a null value"); }

			string value = DefaultStringFormatter.PropertyControllerStringFormatter(obj);

			if (value.Length != length)
			{
				throw new InvalidOperationException("PropertyControllerStringFormatter(BaseProperty obj, int length) should not be used when obj does not have the string length of length");
			}

			return value;
		}
	}
}
