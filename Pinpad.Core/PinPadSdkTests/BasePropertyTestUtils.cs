using Microsoft.VisualStudio.TestTools.UnitTesting;
using PinPadSDK.Commands;
using PinPadSDK.Enums;
using PinPadSDK.Property;
using StonePortableUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PinPadSdkTests {
    internal class BasePropertyTestUtils {
        public static void TestCommandString<commandType>(string commandString) where commandType : BaseProperty, new() {
            commandType command = new commandType();
            command.CommandString = commandString;
            string generatedCommandString = command.CommandString;

            Assert.AreEqual(commandString, generatedCommandString);
        }
        public static void TestCommandString(BaseProperty command) {
            BaseProperty newCommand = (BaseProperty)Activator.CreateInstance(command.GetType());
            string commandString = command.CommandString;
            newCommand.CommandString = commandString;
            string generatedCommandString = newCommand.CommandString;

            Assert.AreEqual(commandString, generatedCommandString);
        }
        public static bool IsCommandStringThrowingException(BaseProperty command) {
            try {
                string temp = command.CommandString;
                return false;
            }
            catch {
                return true;
            }
        }
        public static bool IsCommandStringThrowingExceptionWith(BaseProperty command, string message) {
            try {
                string temp = command.CommandString;
                return false;
            }
            catch (Exception ex) {
                return ex.Message.Contains(message);
            }
        }
        public static bool IsActionThrowingExceptions(Action action) {
            try {
                action();
                return false;
            }
            catch {
                return true;
            }
        }
        public static bool IsActionThrowingException<exceptionType>(Action action)
            where exceptionType : Exception {
            try {
                action();
                return false;
            }
            catch (exceptionType) {
                return true;
            }
        }

        public static void TestBaseResponse(BaseResponse response) {
            BasePropertyTestUtils.TestProperty<ResponseStatus>(response, response.RSP_STAT, 3);

            Assert.IsTrue(BasePropertyTestUtils.IsActionThrowingExceptions(() => { response.RSP_STAT.Value = ResponseStatus.Undefined; }), "Did not complain about RSP_STAT's null value");
            
            response.RSP_STAT.Value = ResponseStatus.ST_CANCEL;
            Assert.AreEqual(response.CommandName + "013", response.CommandString, "Did not write command with RSP_STAT different than ST_OK");
            BasePropertyTestUtils.TestCommandString(response);

            response.RSP_STAT.Value = ResponseStatus.ST_OK;
        }
        public static void TestProperty(BaseProperty command, SimpleProperty<Nullable<long>> property, int length, bool optional = false) {
            if (optional == false && property.HasValue == false) {
                Assert.IsTrue(BasePropertyTestUtils.IsCommandStringThrowingExceptionWith(command, property.Name), "Did not complain about " + property.Name + ".");
            }
            long value = BasePropertyTestUtils.GetLongWithSpecifiedLength(length);
            property.Value = value;
            if (optional == false) {
                Assert.IsTrue(BasePropertyTestUtils.IsActionThrowingExceptions(() => { property.Value = null; }), "Did not complain about " + property.Name + "'s null value");
            }
            Assert.IsTrue(BasePropertyTestUtils.IsActionThrowingExceptions(() => { property.Value = BasePropertyTestUtils.GetLongWithSpecifiedLength(length + 1); }), "Did not complain about " + property.Name + "'s invalid value");
            Assert.AreEqual(value, property.Value, "Did not revert " + property.Name + "'s value after failed set");
        }
        public static void TestProperty(BaseProperty command, SimpleProperty<Nullable<int>> property, int length, bool optional = false) {
            if (optional == false && property.HasValue == false) {
                Assert.IsTrue(BasePropertyTestUtils.IsCommandStringThrowingExceptionWith(command, property.Name), "Did not complain about " + property.Name + ".");
            }
            int value = BasePropertyTestUtils.GetIntegerWithSpecifiedLength(length);
            property.Value = value;
            if (optional == false) {
                Assert.IsTrue(BasePropertyTestUtils.IsActionThrowingExceptions(() => { property.Value = null; }), "Did not complain about " + property.Name + "'s null value");
            }
            Assert.IsTrue(BasePropertyTestUtils.IsActionThrowingExceptions(() => { property.Value = BasePropertyTestUtils.GetIntegerWithSpecifiedLength(length + 1); }), "Did not complain about " + property.Name + "'s invalid value");
            Assert.AreEqual(value, property.Value, "Did not revert " + property.Name + "'s value after failed set");
        }
        public static void TestProperty(BaseProperty command, SimpleProperty<Nullable<DateTime>> property, DateTime value, bool optional = false) {
            if (optional == false && property.HasValue == false) {
                Assert.IsTrue(BasePropertyTestUtils.IsCommandStringThrowingExceptionWith(command, property.Name), "Did not complain about " + property.Name + ".");
            }
            property.Value = value;
            if (optional == false) {
                Assert.IsTrue(BasePropertyTestUtils.IsActionThrowingExceptions(() => { property.Value = null; }), "Did not complain about " + property.Name + "'s null value");
            }
            Assert.AreEqual(value, property.Value, "Did not revert " + property.Name + "'s value after failed set");
        }
        public static void TestProperty(BaseProperty command, SimpleProperty<SimpleMessage> property, bool optional = false) {
            if (optional == false && property.HasValue == false) {
                Assert.IsTrue(BasePropertyTestUtils.IsCommandStringThrowingExceptionWith(command, property.Name), "Did not complain about " + property.Name + ".");
            }
            SimpleMessage value = new SimpleMessage(BasePropertyTestUtils.GetStringWithSpecifiedLength(32));
            property.Value = value;
            if (optional == false) {
                Assert.IsTrue(BasePropertyTestUtils.IsActionThrowingExceptions(() => { property.Value = null; }), "Did not complain about " + property.Name + "'s null value");
            }
            Assert.AreEqual(value, property.Value, "Did not revert " + property.Name + "'s value after failed set");
        }
        public static void TestProperty(BaseProperty command, SimpleProperty<MultilineMessage> property, bool optional = false) {
            if (optional == false && property.HasValue == false) {
                Assert.IsTrue(BasePropertyTestUtils.IsCommandStringThrowingExceptionWith(command, property.Name), "Did not complain about " + property.Name + ".");
            }
            MultilineMessage value = new MultilineMessage(BasePropertyTestUtils.GetStringWithSpecifiedLength(16), BasePropertyTestUtils.GetStringWithSpecifiedLength(16));
            property.Value = value;
            if (optional == false) {
                Assert.IsTrue(BasePropertyTestUtils.IsActionThrowingExceptions(() => { property.Value = null; }), "Did not complain about " + property.Name + "'s null value");
            }
            Assert.AreEqual(value, property.Value, "Did not revert " + property.Name + "'s value after failed set");
        }
        public static void TestProperty(BaseProperty command, SimpleProperty<string> property, int length, bool optional = false) {
            if (optional == false && property.HasValue == false) {
                Assert.IsTrue(BasePropertyTestUtils.IsCommandStringThrowingExceptionWith(command, property.Name), "Did not complain about " + property.Name + ".");
            }
            string value = BasePropertyTestUtils.GetStringWithSpecifiedLength(length);
            property.Value = value;
            if (optional == false) {
                Assert.IsTrue(BasePropertyTestUtils.IsActionThrowingExceptions(() => { property.Value = null; }), "Did not complain about " + property.Name + "'s null value");
            }
            Assert.IsTrue(BasePropertyTestUtils.IsActionThrowingExceptions(() => { property.Value = BasePropertyTestUtils.GetStringWithSpecifiedLength(length + 1); }), "Did not complain about " + property.Name + "'s invalid value");
            Assert.AreEqual(value, property.Value, "Did not revert " + property.Name + "'s value after failed set");
        }
        public static void TestSimpleProperty(BaseProperty command, SimpleProperty<string> property, int length, bool optional = false) {
            if (optional == false && property.HasValue == false) {
                Assert.IsTrue(BasePropertyTestUtils.IsCommandStringThrowingExceptionWith(command, property.Name), "Did not complain about " + property.Name + ".");
            }
            string value = BasePropertyTestUtils.GetStringWithSpecifiedLength(length);
            property.Value = value;
            if (optional == false) {
                Assert.IsTrue(BasePropertyTestUtils.IsActionThrowingExceptions(() => { property.Value = null; }), "Did not complain about " + property.Name + "'s null value");
            }
            Assert.AreEqual(value, property.Value, "Did not revert " + property.Name + "'s value after failed set");
        }
        public static void TestProperty(BaseProperty command, SimpleProperty<HexadecimalData> property, int length, bool optional = false) {
            if (optional == false && property.HasValue == false) {
                Assert.IsTrue(BasePropertyTestUtils.IsCommandStringThrowingExceptionWith(command, property.Name), "Did not complain about " + property.Name + ".");
            }
            HexadecimalData value = BasePropertyTestUtils.GetHexadecimalWithSpecifiedLength(length / 2);
            property.Value = value;
            if (optional == false) {
                Assert.IsTrue(BasePropertyTestUtils.IsActionThrowingExceptions(() => { property.Value = null; }), "Did not complain about " + property.Name + "'s null value");
            }
            Assert.IsTrue(BasePropertyTestUtils.IsActionThrowingExceptions(() => { property.Value = BasePropertyTestUtils.GetHexadecimalWithSpecifiedLength(length/2 + 1); }), "Did not complain about " + property.Name + "'s invalid value");
            Assert.AreEqual(value, property.Value, "Did not revert " + property.Name + "'s value after failed set");
        }
        public static void TestSimpleProperty(BaseProperty command, SimpleProperty<HexadecimalData> property, int length, bool optional = false) {
            if (optional == false && property.HasValue == false) {
                Assert.IsTrue(BasePropertyTestUtils.IsCommandStringThrowingExceptionWith(command, property.Name), "Did not complain about " + property.Name + ".");
            }
            HexadecimalData value = BasePropertyTestUtils.GetHexadecimalWithSpecifiedLength(length / 2);
            property.Value = value;
            if (optional == false) {
                Assert.IsTrue(BasePropertyTestUtils.IsActionThrowingExceptions(() => { property.Value = null; }), "Did not complain about " + property.Name + "'s null value");
            }
            Assert.AreEqual(value, property.Value, "Did not revert " + property.Name + "'s value after failed set");
        }
        public static void TestProperty(BaseProperty command, SimpleProperty<Nullable<bool>> property, bool optional = false) {
            if (optional == false && property.HasValue == false) {
                Assert.IsTrue(BasePropertyTestUtils.IsCommandStringThrowingExceptionWith(command, property.Name), "Did not complain about " + property.Name + ".");
            }
            bool value = true;
            property.Value = value;
            if (optional == false) {
                Assert.IsTrue(BasePropertyTestUtils.IsActionThrowingExceptions(() => { property.Value = null; }), "Did not complain about " + property.Name + "'s null value");
            }
            Assert.AreEqual(value, property.Value, "Did not revert " + property.Name + "'s value after failed set");
        }
        public static void TestProperty(BaseProperty command, SimpleProperty<Nullable<char>> property, bool optional = false) {
            if (optional == false && property.HasValue == false) {
                Assert.IsTrue(BasePropertyTestUtils.IsCommandStringThrowingExceptionWith(command, property.Name), "Did not complain about " + property.Name + ".");
            }
            char value = 'C';
            property.Value = value;
            if (optional == false) {
                Assert.IsTrue(BasePropertyTestUtils.IsActionThrowingExceptions(() => { property.Value = null; }), "Did not complain about " + property.Name + "'s null value");
            }
            Assert.AreEqual(value, property.Value, "Did not revert " + property.Name + "'s value after failed set");
        }
        public static void TestFixedProperty(BaseProperty command, SimpleProperty<string> property, int length, bool optional = false) {
            if (optional == false && property.HasValue == false) {
                Assert.IsTrue(BasePropertyTestUtils.IsCommandStringThrowingExceptionWith(command, property.Name), "Did not complain about " + property.Name + ".");
            }
            string value = BasePropertyTestUtils.GetStringWithSpecifiedLength(length);
            property.Value = value;
            if (optional == false) {
                Assert.IsTrue(BasePropertyTestUtils.IsActionThrowingExceptions(() => { property.Value = null; }), "Did not complain about " + property.Name + "'s null value");
            }
            Assert.IsTrue(BasePropertyTestUtils.IsActionThrowingExceptions(() => { property.Value = BasePropertyTestUtils.GetStringWithSpecifiedLength(length - 1); }), "Did not complain about " + property.Name + "'s invalid lower value");
            Assert.IsTrue(BasePropertyTestUtils.IsActionThrowingExceptions(() => { property.Value = BasePropertyTestUtils.GetStringWithSpecifiedLength(length + 1); }), "Did not complain about " + property.Name + "'s invalid higher value");
            Assert.AreEqual(value, property.Value, "Did not revert " + property.Name + "'s value after failed set");
        }
        public static void TestProperty(BaseProperty command, SimpleProperty<ServiceCode> property, bool optional = false) {
            if (optional == false && property.HasValue == false) {
                Assert.IsTrue(BasePropertyTestUtils.IsCommandStringThrowingExceptionWith(command, property.Name), "Did not complain about " + property.Name + ".");
            }
            ServiceCode value = new ServiceCode(BasePropertyTestUtils.GetStringWithSpecifiedLength(3));
            property.Value = value;
            if (optional == false) {
                Assert.IsTrue(BasePropertyTestUtils.IsActionThrowingExceptions(() => { property.Value = null; }), "Did not complain about " + property.Name + "'s null value");
            }
            Assert.IsTrue(BasePropertyTestUtils.IsActionThrowingExceptions(() => { property.Value = new ServiceCode(BasePropertyTestUtils.GetStringWithSpecifiedLength(2)); }), "Did not complain about " + property.Name + "'s invalid lower value");
            Assert.IsTrue(BasePropertyTestUtils.IsActionThrowingExceptions(() => { property.Value = new ServiceCode(BasePropertyTestUtils.GetStringWithSpecifiedLength(4)); }), "Did not complain about " + property.Name + "'s invalid higher value");
            Assert.AreEqual(value, property.Value, "Did not revert " + property.Name + "'s value after failed set");
        }
        public static void TestProperty(BaseProperty command, SimpleProperty<CryptographyMethod> property, bool optional = false) {
            if (optional == false && property.HasValue == false) {
                Assert.IsTrue(BasePropertyTestUtils.IsCommandStringThrowingExceptionWith(command, property.Name), "Did not complain about " + property.Name + ".");
            }
            CryptographyMethod value = new CryptographyMethod(KeyManagementMode.DerivedUniqueKeyPerTransaction, CryptographyMode.TripleDataEncryptionStandard);
            property.Value = value;
            if (optional == false) {
                Assert.IsTrue(BasePropertyTestUtils.IsActionThrowingExceptions(() => { property.Value = null; }), "Did not complain about " + property.Name + "'s null value");
            }
            Assert.IsTrue(BasePropertyTestUtils.IsActionThrowingExceptions(() => { property.Value = new CryptographyMethod(KeyManagementMode.Undefined, CryptographyMode.DataEncryptionStandard); }), "Did not complain about " + property.Name + "'s invalid KeyManagementMode value");
            Assert.IsTrue(BasePropertyTestUtils.IsActionThrowingExceptions(() => { property.Value = new CryptographyMethod(KeyManagementMode.DerivedUniqueKeyPerTransaction, CryptographyMode.Undefined); }), "Did not complain about " + property.Name + "'s invalid CryptographyMode value");
            Assert.IsTrue(BasePropertyTestUtils.IsActionThrowingExceptions(() => { property.Value = new CryptographyMethod(KeyManagementMode.Undefined, CryptographyMode.Undefined); }), "Did not complain about " + property.Name + "'s invalid KeyManagementMode and CryptographyMode values");
            Assert.AreEqual(value, property.Value, "Did not revert " + property.Name + "'s value after failed set");
        }
        public static void TestProperty<enumType>(BaseProperty command, SimpleProperty<enumType> property, int length, bool optional = false) 
            where enumType : struct {
            if (typeof(enumType).IsEnum == false) {
                throw new InvalidOperationException(typeof(enumType).Name + " is not a Enum.");
            }

            Array enumValues = Enum.GetValues(typeof(enumType));
            string nullName = Enum.GetName(typeof(enumType), 0);
            Assert.AreEqual("Undefined", nullName, "Enum " + typeof(enumType).Name + " does not contain Undefined as 0");

            if (optional == false && property.HasValue == false) {
                Assert.IsTrue(BasePropertyTestUtils.IsCommandStringThrowingExceptionWith(command, property.Name), "Did not complain about " + property.Name + ".");
            }
            enumType value = (enumType)enumValues.GetValue(1); ;
            property.Value = value;
            if (optional == false) {
                Assert.IsTrue(BasePropertyTestUtils.IsActionThrowingExceptions(() => { property.Value = (enumType)Enum.ToObject(typeof(enumType), 0); }), "Did not complain about " + property.Name + "'s null value");
            }
            Assert.IsTrue(BasePropertyTestUtils.IsActionThrowingExceptions(() => { property.Value = (enumType)(object)(Math.Pow(10, length) + 1); }), "Did not complain about " + property.Name + "'s invalid value");
            Assert.AreEqual(value, property.Value, "Did not revert " + property.Name + "'s value after failed set");
        }
        public static long GetLongWithSpecifiedLength(int length) {
            long value = 0;
            int digit = 0;
            for (int i = length-1; i >= 0; i--) {
                digit = (digit + 1) % 10;
                value += digit * (long)Math.Pow(10, i);
            }
            return value;
        }
        public static int GetIntegerWithSpecifiedLength(int length) {
            int value = 0;
            int digit = 0;
            for (int i = length - 1; i >= 0; i--) {
                digit = (digit + 1) % 10;
                value += digit * (int)Math.Pow(10, i);
            }
            return value;
        }
        public static HexadecimalData GetHexadecimalWithSpecifiedLength(int length) {
            return new HexadecimalData(GetStringWithSpecifiedLength(length * 2));
        }
        public static string GetStringWithSpecifiedLength(int length) {
            StringBuilder value = new StringBuilder("1");
            int digit = 1;
            for (int i = 1; i < length; i++) {
                digit = (digit + 1) % 10;
                value.Append(digit);
            }
            return value.ToString();
        }
    }
}
