using PinPadSDK.Commands;
using PinPadSDK.Commands.CkeEventsData;
using PinPadSDK.Controllers.Tables;
using PinPadSDK.Enums;
using PinPadSDK.Factories;
using StonePortableUtils;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace PinPadSDK.Property {
    /// <summary>
    /// Contains commonly used String Parsers
    /// </summary>
    public class DefaultStringParser {
        /// <summary>
        /// Parses a long integer with the specified string length
        /// </summary>
        /// <param name="reader">string reader</param>
        /// <param name="length">string length</param>
        /// <returns>long</returns>
        public static Nullable<long> LongIntegerStringParser(StringReader reader, int length) {
            long value = reader.ReadLong(length);
            return value;
        }

        /// <summary>
        /// Parses a integer with the specified string length
        /// </summary>
        /// <param name="reader">string reader</param>
        /// <param name="length">string length</param>
        /// <returns>integer</returns>
        public static Nullable<int> IntegerStringParser(StringReader reader, int length) {
            int value = reader.ReadInt(length);
            return value;
        }

        /// <summary>
        /// Parses a hexadecimal string with the specified string length
        /// </summary>
        /// <param name="reader">string reader</param>
        /// <param name="length">string length</param>
        /// <returns>HexadecimalDataController</returns>
        public static HexadecimalData HexadecimalStringParser(StringReader reader, int length) {
            string data = reader.ReadString(length);
            HexadecimalData value = new HexadecimalData(data.Trim());
            return value;
        }

        /// <summary>
        /// Parses a hexadecimal string with the specified string length and trim zero bytes at the right side
        /// </summary>
        /// <param name="reader">string reader</param>
        /// <param name="length">string length</param>
        /// <returns>HexadecimalDataController</returns>
        public static HexadecimalData HexadecimalRightPaddingStringParser(StringReader reader, int length) {
            string data = reader.ReadString(length);
            while (data.EndsWith("00") == true) {
                data = data.Remove(data.Length - 2, 2);
            }
            HexadecimalData value = new HexadecimalData(data);
            return value;
        }

        /// <summary>
        /// Parses a nullable boolean string
        /// </summary>
        /// <param name="reader">string reader</param>
        /// <returns>boolean value</returns>
        public static Nullable<bool> BooleanStringParser(StringReader reader) {
            bool value = reader.ReadBool();
            return value;
        }

        /// <summary>
        /// Parses a string with the remaining data
        /// </summary>
        /// <param name="reader">string reader</param>
        /// <returns>string</returns>
        public static string StringStringParser(StringReader reader) {
            return DefaultStringParser.StringStringParser(reader, reader.Remaining);
        }

        /// <summary>
        /// Parses a string with the specified string length
        /// </summary>
        /// <param name="reader">string reader</param>
        /// <param name="length">string length</param>
        /// <returns>string</returns>
        public static string StringStringParser(StringReader reader, int length) {
            string value = reader.ReadString(length);
            return value;
        }

        /// <summary>
        /// Parses a character
        /// </summary>
        /// <param name="reader">string reader</param>
        /// <returns>char</returns>
        public static Nullable<char> CharStringParser(StringReader reader) {
            return Convert.ToChar(DefaultStringParser.StringStringParser(reader, 1));
        }

        /// <summary>
        /// Parses a Date with the default format (yyMMdd)
        /// </summary>
        /// <param name="reader">string reader</param>
        /// <returns>DateTime</returns>
        public static Nullable<DateTime> DateStringParser(StringReader reader) {
            string substring = reader.ReadString(6);
            if (substring == "000000") {
                return null;
            }
            else {
                DateTime value = DateTime.ParseExact(substring, "yyMMdd", CultureInfo.InvariantCulture);
                return value;
            }
        }

        /// <summary>
        /// Parses a Time with the default format (HHmmss)
        /// </summary>
        /// <param name="reader">string reader</param>
        /// <returns>DateTime</returns>
        public static Nullable<DateTime> TimeStringParser(StringReader reader) {
            string substring = reader.ReadString(6);
            if (substring == "000000") {
                return null;
            }
            else {
                DateTime value = DateTime.ParseExact(substring, "HHmmss", CultureInfo.InvariantCulture);
                return value;
            }
        }

        /// <summary>
        /// Parses a Date and Time with the default format (yyMMddHHmmss)
        /// </summary>
        /// <param name="reader">string reader</param>
        /// <returns>DateTime</returns>
        public static Nullable<DateTime> DateTimeStringParser(StringReader reader) {
            string substring = reader.ReadString(12);
            if (substring == "000000000000") {
                return null;
            }
            else {
                DateTime value = DateTime.ParseExact(substring, "yyMMddHHmmss", CultureInfo.InvariantCulture);
                return value;
            }
        }

        /// <summary>
        /// Default string parser for enums where the index 0 is undefined therefore all values have 1 added to them.
        /// 
        /// Example: RSP_STAT : ST_OK = 0
        /// PinPadCommandResponseStatusEnum : 0 : Undefined
        /// PinPadCommandResponseStatusEnum : 1 : ST_OK
        /// </summary>
        /// <typeparam name="enumType">enum type to use, throws InvalidOperationException when used with a type that is not a enum</typeparam>
        /// <param name="reader">string reader</param>
        /// <param name="length">string length</param>
        /// <returns>Enum value</returns>
        public static enumType EnumStringParser<enumType>(StringReader reader, int length) where enumType : struct {
            if (typeof(enumType).IsEnum == false) {
                throw new InvalidOperationException(typeof(enumType).Name + " is not a Enum.");
            }
            else if (Enum.GetNames(typeof(enumType))[0] != "Undefined") {
                throw new InvalidOperationException(typeof(enumType).Name + " default value is not Undefined.");
            }

            int intValue = DefaultStringParser.IntegerStringParser(reader, length).Value + 1;
            enumType value = (enumType)Enum.ToObject(typeof(enumType), intValue);
            return value;
        }

        /// <summary>
        /// Default string parser for properties controllers
        /// </summary>
        /// <typeparam name="propertyControllerType">property controller type to use</typeparam>
        /// <param name="reader">string reader</param>
        /// <param name="length">string length</param>
        /// <returns>propertyControllerType</returns>
        public static propertyControllerType PropertyControllerStringParser<propertyControllerType>(StringReader reader, int length) where propertyControllerType : BaseProperty, new() {
            string data = DefaultStringParser.StringStringParser(reader, length);

            propertyControllerType value = new propertyControllerType();
            value.CommandString = data;
            return value;
        }
    }
}
