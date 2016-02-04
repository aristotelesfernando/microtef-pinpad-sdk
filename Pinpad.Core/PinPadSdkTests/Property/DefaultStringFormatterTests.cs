using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PinPadSDK.Property;
using StonePortableUtils;
using System.Globalization;

namespace PinPadSdkTests.Property {
    [TestClass]
    public class DefaultStringFormatterTests {
        [TestMethod]
        public void ValidateDefaultStringFormatterTestsIntegerStringFormatter() {
            Assert.IsTrue(BasePropertyTestUtils.IsActionThrowingException<InvalidOperationException>(() => {
                DefaultStringFormatter.IntegerStringFormatter(null, 1);
            }));
            Assert.AreEqual("123", DefaultStringFormatter.IntegerStringFormatter(123, 3));
            Assert.AreEqual("0123", DefaultStringFormatter.IntegerStringFormatter(123, 4));
        }
        [TestMethod]
        public void ValidateDefaultStringFormatterTestsLongIntegerStringFormatter() {
            Assert.IsTrue(BasePropertyTestUtils.IsActionThrowingException<InvalidOperationException>(() => {
                DefaultStringFormatter.LongIntegerStringFormatter(null, 1);
            }));
            Assert.AreEqual("123", DefaultStringFormatter.LongIntegerStringFormatter(123, 3));
            Assert.AreEqual("0123", DefaultStringFormatter.LongIntegerStringFormatter(123, 4));
        }
        [TestMethod]
        public void ValidateDefaultStringFormatterTestsHexadecimalStringFormatter() {
            Assert.IsTrue(BasePropertyTestUtils.IsActionThrowingException<InvalidOperationException>(() => {
                DefaultStringFormatter.HexadecimalStringFormatter(null);
            }));
            Assert.AreEqual("1234567890", DefaultStringFormatter.HexadecimalStringFormatter(new HexadecimalData("1234567890")));
        }
        [TestMethod]
        public void ValidateDefaultStringFormatterTestsHexadecimalStringFormatterWithLength() {
            Assert.IsTrue(BasePropertyTestUtils.IsActionThrowingException<InvalidOperationException>(() => {
                DefaultStringFormatter.HexadecimalStringFormatter(null, 10);
            }));
            Assert.AreEqual("1234567890", DefaultStringFormatter.HexadecimalStringFormatter(new HexadecimalData("1234567890"), 10));
            Assert.AreEqual("001234567890", DefaultStringFormatter.HexadecimalStringFormatter(new HexadecimalData("1234567890"), 12));
        }
        [TestMethod]
        public void ValidateDefaultStringFormatterTestsHexadecimalRightPaddingStringFormatter() {
            Assert.IsTrue(BasePropertyTestUtils.IsActionThrowingException<InvalidOperationException>(() => {
                DefaultStringFormatter.HexadecimalRightPaddingStringFormatter(null, 10);
            }));
            Assert.AreEqual("1234567890", DefaultStringFormatter.HexadecimalRightPaddingStringFormatter(new HexadecimalData("1234567890"), 10));
            Assert.AreEqual("123456789000", DefaultStringFormatter.HexadecimalRightPaddingStringFormatter(new HexadecimalData("1234567890"), 12));
        }
        [TestMethod]
        public void ValidateDefaultStringFormatterTestsBooleanStringFormatter() {
            Assert.IsTrue(BasePropertyTestUtils.IsActionThrowingException<InvalidOperationException>(() => {
                DefaultStringFormatter.BooleanStringFormatter(null);
            }));
            Assert.AreEqual("1", DefaultStringFormatter.BooleanStringFormatter(true));
            Assert.AreEqual("0", DefaultStringFormatter.BooleanStringFormatter(false));
        }
        [TestMethod]
        public void ValidateDefaultStringFormatterTestsStringStringFormatter() {
            Assert.IsTrue(BasePropertyTestUtils.IsActionThrowingException<InvalidOperationException>(() => {
                DefaultStringFormatter.StringStringFormatter(null);
            }));
            Assert.AreEqual("1234567890", DefaultStringFormatter.StringStringFormatter("1234567890"));
        }
        [TestMethod]
        public void ValidateDefaultStringFormatterTestsStringStringFormatterWithLength() {
            Assert.IsTrue(BasePropertyTestUtils.IsActionThrowingException<InvalidOperationException>(() => {
                DefaultStringFormatter.StringStringFormatter(null, 10);
            }));
            Assert.AreEqual("1234567890", DefaultStringFormatter.StringStringFormatter("1234567890", 10));
            Assert.IsTrue(BasePropertyTestUtils.IsActionThrowingException<InvalidOperationException>(() => {
                DefaultStringFormatter.StringStringFormatter("1234567890", 11);
            }));
            Assert.IsTrue(BasePropertyTestUtils.IsActionThrowingException<InvalidOperationException>(() => {
                DefaultStringFormatter.StringStringFormatter("1234567890", 9);
            }));
        }
        [TestMethod]
        public void ValidateDefaultStringFormatterTestsRightPaddingWithSpacesStringFormatter() {
            Assert.IsTrue(BasePropertyTestUtils.IsActionThrowingException<InvalidOperationException>(() => {
                DefaultStringFormatter.RightPaddingWithSpacesStringFormatter(null, 10);
            }));
            Assert.AreEqual("1234567890", DefaultStringFormatter.RightPaddingWithSpacesStringFormatter("1234567890", 10));
            Assert.AreEqual("1234567890  ", DefaultStringFormatter.RightPaddingWithSpacesStringFormatter("1234567890", 12));
        }
        [TestMethod]
        public void ValidateDefaultStringFormatterTestsLeftPaddingWithSpacesStringFormatter() {
            Assert.IsTrue(BasePropertyTestUtils.IsActionThrowingException<InvalidOperationException>(() => {
                DefaultStringFormatter.LeftPaddingWithSpacesStringFormatter(null, 10);
            }));
            Assert.AreEqual("1234567890", DefaultStringFormatter.LeftPaddingWithSpacesStringFormatter("1234567890", 10));
            Assert.AreEqual("  1234567890", DefaultStringFormatter.LeftPaddingWithSpacesStringFormatter("1234567890", 12));
        }
        [TestMethod]
        public void ValidateDefaultStringFormatterTestsCharStringFormatter() {
            Assert.IsTrue(BasePropertyTestUtils.IsActionThrowingException<InvalidOperationException>(() => {
                DefaultStringFormatter.CharStringFormatter(null);
            }));
            Assert.AreEqual("a", DefaultStringFormatter.CharStringFormatter('a'));
        }
        [TestMethod]
        public void ValidateDefaultStringFormatterTestsDateStringFormatter() {
            Assert.IsTrue(BasePropertyTestUtils.IsActionThrowingException<InvalidOperationException>(() => {
                DefaultStringFormatter.DateStringFormatter(null);
            }));
            string dateString = "150102";
            DateTime value = DateTime.ParseExact(dateString, "yyMMdd", CultureInfo.InvariantCulture);
            Assert.AreEqual(dateString, DefaultStringFormatter.DateStringFormatter(value));
        }
        [TestMethod]
        public void ValidateDefaultStringFormatterTestsTimeStringFormatter() {
            Assert.IsTrue(BasePropertyTestUtils.IsActionThrowingException<InvalidOperationException>(() => {
                DefaultStringFormatter.TimeStringFormatter(null);
            }));
            string dateString = "123456";
            DateTime value = DateTime.ParseExact(dateString, "HHmmss", CultureInfo.InvariantCulture);
            Assert.AreEqual(dateString, DefaultStringFormatter.TimeStringFormatter(value));
        }
        [TestMethod]
        public void ValidateDefaultStringFormatterTestsDateTimeStringFormatter() {
            Assert.IsTrue(BasePropertyTestUtils.IsActionThrowingException<InvalidOperationException>(() => {
                DefaultStringFormatter.DateTimeStringFormatter(null);
            }));
            string dateString = "150102123456";
            DateTime value = DateTime.ParseExact(dateString, "yyMMddHHmmss", CultureInfo.InvariantCulture);
            Assert.AreEqual(dateString, DefaultStringFormatter.DateTimeStringFormatter(value));
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
        public void ValidateDefaultStringFormatterTestsEnumStringFormatter() {
            Assert.IsTrue(BasePropertyTestUtils.IsActionThrowingException<InvalidOperationException>(() => {
                DefaultStringFormatter.EnumStringFormatter<int>(0, 10);
            }));
            Assert.IsTrue(BasePropertyTestUtils.IsActionThrowingException<InvalidOperationException>(() => {
                DefaultStringFormatter.EnumStringFormatter<invalidEnum>(invalidEnum.Value1, 10);
            }));
            Assert.AreEqual("0", DefaultStringFormatter.EnumStringFormatter<validEnum>(validEnum.Value1, 1));
            Assert.AreEqual("1", DefaultStringFormatter.EnumStringFormatter<validEnum>(validEnum.Value2, 1));
            Assert.IsTrue(BasePropertyTestUtils.IsActionThrowingException<InvalidOperationException>(() => {
                DefaultStringFormatter.EnumStringFormatter<validEnum>(validEnum.Undefined, 10);
            }));
        }
        class validBaseProperty : BaseProperty {
            public override string CommandString {
                get {
                    return "1234567890";
                }
                set {}
            }
        }
        [TestMethod]
        public void ValidateDefaultStringFormatterTestsPropertyControllerStringFormatter() {
            Assert.IsTrue(BasePropertyTestUtils.IsActionThrowingException<InvalidOperationException>(() => {
                DefaultStringFormatter.PropertyControllerStringFormatter(null);
            }));
            Assert.AreEqual("1234567890", DefaultStringFormatter.PropertyControllerStringFormatter(new validBaseProperty()));
        }
        [TestMethod]
        public void ValidateDefaultStringFormatterTestsPropertyControllerStringFormatterWithLength() {
            Assert.IsTrue(BasePropertyTestUtils.IsActionThrowingException<InvalidOperationException>(() => {
                DefaultStringFormatter.PropertyControllerStringFormatter(null, 10);
            }));
            Assert.AreEqual("1234567890", DefaultStringFormatter.PropertyControllerStringFormatter(new validBaseProperty(), 10));
            Assert.IsTrue(BasePropertyTestUtils.IsActionThrowingException<InvalidOperationException>(() => {
                DefaultStringFormatter.PropertyControllerStringFormatter(new validBaseProperty(), 11);
            }));
            Assert.IsTrue(BasePropertyTestUtils.IsActionThrowingException<InvalidOperationException>(() => {
                DefaultStringFormatter.PropertyControllerStringFormatter(new validBaseProperty(), 9);
            }));
        }
    }
}
