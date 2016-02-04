using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PinPadSDK.Property;

namespace PinPadSdkTests.Property {
    [TestClass]
    public class DefaultStringParserTests {
        [TestMethod]
        public void ValidateDefaultStringParserLongIntegerStringParser() {
            Assert.AreEqual(123, DefaultStringParser.LongIntegerStringParser(new StringReader("123"), 3));
        }
        [TestMethod]
        public void ValidateDefaultStringParserIntegerStringParser() {
            Assert.AreEqual(123, DefaultStringParser.IntegerStringParser(new StringReader("123"), 3));
        }
        [TestMethod]
        public void ValidateDefaultStringParserHexadecimalStringParser() {
            Assert.AreEqual("1234567890ABCDEF", DefaultStringParser.HexadecimalStringParser(new StringReader("1234567890ABCDEF"), 16).DataString);
            Assert.AreEqual("1234", DefaultStringParser.HexadecimalStringParser(new StringReader("  1234"), 6).DataString);
            Assert.AreEqual("1234", DefaultStringParser.HexadecimalStringParser(new StringReader("1234  "), 6).DataString);
            Assert.AreEqual("1234", DefaultStringParser.HexadecimalStringParser(new StringReader(" 1234 "), 6).DataString);
        }
        [TestMethod]
        public void ValidateDefaultStringParserHexadecimalRightPaddingStringParser() {
            Assert.AreEqual("1234567890ABCDEF", DefaultStringParser.HexadecimalRightPaddingStringParser(new StringReader("1234567890ABCDEF"), 16).DataString);
            Assert.AreEqual("1234", DefaultStringParser.HexadecimalRightPaddingStringParser(new StringReader("123400"), 6).DataString);
        }
        [TestMethod]
        public void ValidateDefaultStringParserBooleanStringParser() {
            Assert.IsTrue(DefaultStringParser.BooleanStringParser(new StringReader("1")).Value);
            Assert.IsFalse(DefaultStringParser.BooleanStringParser(new StringReader("0")).Value);
        }
        [TestMethod]
        public void ValidateDefaultStringParserStringStringParser() {
            Assert.AreEqual("1234567890ABCDEF", DefaultStringParser.StringStringParser(new StringReader("1234567890ABCDEF")));
            Assert.AreEqual("1234567890", DefaultStringParser.StringStringParser(new StringReader("1234567890")));
        }
        [TestMethod]
        public void ValidateDefaultStringParserStringStringParserWithLength() {
            Assert.AreEqual("1234567890ABCDEF", DefaultStringParser.StringStringParser(new StringReader("1234567890ABCDEF"), 16));
            Assert.AreEqual("1234567890", DefaultStringParser.StringStringParser(new StringReader("1234567890ABCDEF"), 10));
        }
        [TestMethod]
        public void ValidateDefaultStringParserCharStringParser() {
            StringReader reader = new StringReader("12345");
            Assert.AreEqual('1', DefaultStringParser.CharStringParser(reader));
            Assert.AreEqual('2', DefaultStringParser.CharStringParser(reader));
            Assert.AreEqual('3', DefaultStringParser.CharStringParser(reader));
            Assert.AreEqual('4', DefaultStringParser.CharStringParser(reader));
            Assert.AreEqual('5', DefaultStringParser.CharStringParser(reader));
        }
        [TestMethod]
        public void ValidateDefaultStringParserDateStringParser() {
            string dateString = "150102";
            Assert.AreEqual(dateString, DefaultStringParser.DateStringParser(new StringReader(dateString)).Value.ToString("yyMMdd"));
            Assert.IsNull(DefaultStringParser.DateStringParser(new StringReader("000000")));
        }
        [TestMethod]
        public void ValidateDefaultStringParserTimeStringParser() {
            string timeString = "123456";
            Assert.AreEqual(timeString, DefaultStringParser.TimeStringParser(new StringReader(timeString)).Value.ToString("HHmmss"));
            Assert.IsNull(DefaultStringParser.TimeStringParser(new StringReader("000000")));
        }
        [TestMethod]
        public void ValidateDefaultStringParserDateTimeStringParser() {
            string timeString = "150102123456";
            Assert.AreEqual(timeString, DefaultStringParser.DateTimeStringParser(new StringReader(timeString)).Value.ToString("yyMMddHHmmss"));
            Assert.IsNull(DefaultStringParser.DateTimeStringParser(new StringReader("000000000000")));
        }
        enum validEnum {
            Undefined = 0,
            Value1,
            Value2
        }
        enum invalidEnum {
            Value1,
            Value2
        }
        [TestMethod]
        public void ValidateDefaultStringParserEnumStringParser() {
            Assert.IsTrue(BasePropertyTestUtils.IsActionThrowingException<InvalidOperationException>(() => {
                DefaultStringParser.EnumStringParser<int>(new StringReader("1"), 1);
            }));
            Assert.IsTrue(BasePropertyTestUtils.IsActionThrowingException<InvalidOperationException>(() => {
                DefaultStringParser.EnumStringParser<invalidEnum>(new StringReader("1"), 1);
            }));
            Assert.AreEqual(validEnum.Value1, DefaultStringParser.EnumStringParser<validEnum>(new StringReader("0"), 1));
            Assert.AreEqual(validEnum.Value2, DefaultStringParser.EnumStringParser<validEnum>(new StringReader("1"), 1));
            Assert.AreEqual((validEnum)3, DefaultStringParser.EnumStringParser<validEnum>(new StringReader("2"), 1));
        }
        class validBaseProperty : BaseProperty {
            private string _CommandString = null;
            public override string CommandString {
                get {
                    return this._CommandString;
                }
                set {
                    this._CommandString = value;
                }
            }
        }
        [TestMethod]
        public void ValidateDefaultStringParserPropertyControllerStringParser() {
            string value = "1234567890";
            Assert.AreEqual(value, DefaultStringParser.PropertyControllerStringParser < validBaseProperty>(new StringReader(value), value.Length).CommandString);
        }
    }
}
