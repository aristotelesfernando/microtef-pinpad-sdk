using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PinPadSDK.Property;
using PinPadSDK.Enums;
using System.Reflection;

namespace PinPadSdkTests.Property {
    [TestClass]
    public class SimpleMessageTests {
        [TestMethod]
        public void ValidateSimpleMessageFullString() {
            string value = BasePropertyTestUtils.GetStringWithSpecifiedLength(32);
            SimpleMessage message = new SimpleMessage(value);
            Assert.AreEqual(value, message.CommandString);
            Assert.AreEqual(value, message.FullValue);
        }
        [TestMethod]
        public void ValidateSimpleMessageFullStringWithLeftPadding() {
            string value = BasePropertyTestUtils.GetStringWithSpecifiedLength(16);
            SimpleMessage message = new SimpleMessage(value, PaddingType.Left);
            Assert.AreEqual(value + "                ", message.CommandString);
            Assert.AreEqual(value + "                ", message.FullValue);
        }
        [TestMethod]
        public void ValidateSimpleMessageFullStringWithRightPadding() {
            string value = BasePropertyTestUtils.GetStringWithSpecifiedLength(16);
            SimpleMessage message = new SimpleMessage(value, PaddingType.Right);
            Assert.AreEqual("                " + value, message.CommandString);
            Assert.AreEqual("                " + value, message.FullValue);
        }
        [TestMethod]
        public void ValidateSimpleMessageFullStringWithCenterPadding() {
            string value = BasePropertyTestUtils.GetStringWithSpecifiedLength(16);
            SimpleMessage message = new SimpleMessage(value, PaddingType.Center);
            Assert.AreEqual("        " + value + "        ", message.CommandString);
            Assert.AreEqual("        " + value + "        ", message.FullValue);
        }
        [TestMethod]
        public void ValidateSimpleMessageFullStringNull() {
            string value = null;
            try {
                SimpleMessage message = new SimpleMessage(value);
                Assert.Fail();
            }
            catch (ArgumentOutOfRangeException) { }
        }
        [TestMethod]
        public void ValidateSimpleMessageFullStringNullWithLeftPadding() {
            SimpleMessage message = new SimpleMessage(null, PaddingType.Left);
            Assert.AreEqual("                                ", message.CommandString);
        }
        [TestMethod]
        public void ValidateSimpleMessageFullStringNullWithRightPadding() {
            SimpleMessage message = new SimpleMessage(null, PaddingType.Right);
            Assert.AreEqual("                                ", message.CommandString);
        }
        [TestMethod]
        public void ValidateSimpleMessageFullStringNullWithCenterPadding() {
            SimpleMessage message = new SimpleMessage(null, PaddingType.Center);
            Assert.AreEqual("                                ", message.CommandString);
        }
        [TestMethod]
        public void ValidateSimpleMessageFullStringTooSmall() {
            string value = BasePropertyTestUtils.GetStringWithSpecifiedLength(31);
            try {
                SimpleMessage message = new SimpleMessage(value);
                Assert.Fail();
            }
            catch (ArgumentOutOfRangeException) { }
        }
        [TestMethod]
        public void ValidateSimpleMessageFullStringTooBig() {
            string value = BasePropertyTestUtils.GetStringWithSpecifiedLength(33);
            try {
                SimpleMessage message = new SimpleMessage(value);
                Assert.Fail();
            }
            catch (ArgumentOutOfRangeException) { }
        }
        [TestMethod]
        public void ValidateSimpleMessageLines() {
            string lineValue = BasePropertyTestUtils.GetStringWithSpecifiedLength(16);
            string fullValue = lineValue + lineValue;
            SimpleMessage message = new SimpleMessage(fullValue);
            Assert.AreEqual(fullValue, message.CommandString);
            Assert.AreEqual(fullValue, message.FullValue);
            Assert.AreEqual(lineValue, message.FirstLine);
            Assert.AreEqual(lineValue, message.SecondLine);
        }
        [TestMethod]
        public void ValidateSimpleMessageLinesLeftPadding() {
            string lineValue = BasePropertyTestUtils.GetStringWithSpecifiedLength(8);
            SimpleMessage message = new SimpleMessage(lineValue, lineValue, PaddingType.Left);
            Assert.AreEqual(lineValue + "        ", message.FirstLine);
            Assert.AreEqual(lineValue + "        ", message.SecondLine);
        }
        [TestMethod]
        public void ValidateSimpleMessageLinesRightPadding() {
            string lineValue = BasePropertyTestUtils.GetStringWithSpecifiedLength(8);
            SimpleMessage message = new SimpleMessage(lineValue, lineValue, PaddingType.Right);
            Assert.AreEqual("        " + lineValue, message.FirstLine);
            Assert.AreEqual("        " + lineValue, message.SecondLine);
        }
        [TestMethod]
        public void ValidateSimpleMessageLinesCenterPadding() {
            string lineValue = BasePropertyTestUtils.GetStringWithSpecifiedLength(8);
            SimpleMessage message = new SimpleMessage(lineValue, lineValue, PaddingType.Center);
            Assert.AreEqual("    " + lineValue + "    ", message.FirstLine);
            Assert.AreEqual("    " + lineValue + "    ", message.SecondLine);
        }
        [TestMethod]
        public void ValidateSimpleMessageLinesLeftToRightPadding() {
            string lineValue = BasePropertyTestUtils.GetStringWithSpecifiedLength(8);
            SimpleMessage message = new SimpleMessage(lineValue, lineValue, PaddingType.Left);
            Assert.AreEqual(lineValue + "        ", message.FirstLine);
            Assert.AreEqual(lineValue + "        ", message.SecondLine);
            message.Padding = PaddingType.Right;
            Assert.AreEqual("        " + lineValue, message.FirstLine);
            Assert.AreEqual("        " + lineValue, message.SecondLine);
        }
        [TestMethod]
        public void ValidateSimpleMessageLinesCenterToRightPadding() {
            string lineValue = BasePropertyTestUtils.GetStringWithSpecifiedLength(8);
            SimpleMessage message = new SimpleMessage(lineValue, lineValue, PaddingType.Center);
            Assert.AreEqual("    " + lineValue + "    ", message.FirstLine);
            Assert.AreEqual("    " + lineValue + "    ", message.SecondLine);
            message.Padding = PaddingType.Right;
            Assert.AreEqual("        " + lineValue, message.FirstLine);
            Assert.AreEqual("        " + lineValue, message.SecondLine);
        }
        [TestMethod]
        public void ValidateSimpleMessageLinesLeftToCenterPadding() {
            string lineValue = BasePropertyTestUtils.GetStringWithSpecifiedLength(8);
            SimpleMessage message = new SimpleMessage(lineValue, lineValue, PaddingType.Left);
            Assert.AreEqual(lineValue + "        ", message.FirstLine);
            Assert.AreEqual(lineValue + "        ", message.SecondLine);
            message.Padding = PaddingType.Center;
            Assert.AreEqual("    " + lineValue + "    ", message.FirstLine);
            Assert.AreEqual("    " + lineValue + "    ", message.SecondLine);
        }
        [TestMethod]
        public void ValidateSimpleMessageLinesRightToCenterPadding() {
            string lineValue = BasePropertyTestUtils.GetStringWithSpecifiedLength(8);
            SimpleMessage message = new SimpleMessage(lineValue, lineValue, PaddingType.Right);
            Assert.AreEqual("        " + lineValue, message.FirstLine);
            Assert.AreEqual("        " + lineValue, message.SecondLine);
            message.Padding = PaddingType.Center;
            Assert.AreEqual("    " + lineValue + "    ", message.FirstLine);
            Assert.AreEqual("    " + lineValue + "    ", message.SecondLine);
        }
        [TestMethod]
        public void ValidateSimpleMessageLinesRightToLeftPadding() {
            string lineValue = BasePropertyTestUtils.GetStringWithSpecifiedLength(8);
            SimpleMessage message = new SimpleMessage(lineValue, lineValue, PaddingType.Right);
            Assert.AreEqual("        " + lineValue, message.FirstLine);
            Assert.AreEqual("        " + lineValue, message.SecondLine);
            message.Padding = PaddingType.Left;
            Assert.AreEqual(lineValue + "        ", message.FirstLine);
            Assert.AreEqual(lineValue + "        ", message.SecondLine);
        }
        [TestMethod]
        public void ValidateSimpleMessageLinesCenterToLeftPadding() {
            string lineValue = BasePropertyTestUtils.GetStringWithSpecifiedLength(8);
            SimpleMessage message = new SimpleMessage(lineValue, lineValue, PaddingType.Center);
            Assert.AreEqual("    " + lineValue + "    ", message.FirstLine);
            Assert.AreEqual("    " + lineValue + "    ", message.SecondLine);
            message.Padding = PaddingType.Left;
            Assert.AreEqual(lineValue + "        ", message.FirstLine);
            Assert.AreEqual(lineValue + "        ", message.SecondLine);
        }
        [TestMethod]
        public void ValidateSimpleMessageFirstLineNullWithPadding() {
            SimpleMessage message = new SimpleMessage();
            message.Padding = PaddingType.Left;
            message.FirstLine = null;
            Assert.AreEqual("                ", message.FirstLine);
        }
        [TestMethod]
        public void ValidateSimpleMessageFirstLineNullWithoutPadding() {
            SimpleMessage message = new SimpleMessage();
            try {
                message.Padding = PaddingType.Undefined;
                message.FirstLine = null;
                Assert.Fail();
            }
            catch (ArgumentOutOfRangeException) { }
        }
        [TestMethod]
        public void ValidateSimpleMessageFirstLineTooSmallWithoutPadding() {
            string lineValue = BasePropertyTestUtils.GetStringWithSpecifiedLength(15);
            SimpleMessage message = new SimpleMessage();
            try {
                message.Padding = PaddingType.Undefined;
                message.FirstLine = lineValue;
                Assert.Fail();
            }
            catch (ArgumentOutOfRangeException) { }
        }
        [TestMethod]
        public void ValidateSimpleMessageFirstLineTooBig() {
            string lineValue = BasePropertyTestUtils.GetStringWithSpecifiedLength(17);
            SimpleMessage message = new SimpleMessage();
            try {
                message.FirstLine = lineValue;
                Assert.Fail();
            }
            catch (ArgumentOutOfRangeException) { }
        }
        [TestMethod]
        public void ValidateSimpleMessageSecondLineNullWithPadding() {
            SimpleMessage message = new SimpleMessage();
            message.Padding = PaddingType.Left;
            message.SecondLine = null;
            Assert.AreEqual("                ", message.SecondLine);
        }
        [TestMethod]
        public void ValidateSimpleMessageSecondLineNullWithoutPadding() {
            SimpleMessage message = new SimpleMessage();
            try {
                message.Padding = PaddingType.Undefined;
                message.SecondLine = null;
                Assert.Fail();
            }
            catch (ArgumentOutOfRangeException) { }
        }
        [TestMethod]
        public void ValidateSimpleMessageSecondLineTooSmallWithoutPadding() {
            string lineValue = BasePropertyTestUtils.GetStringWithSpecifiedLength(15);
            SimpleMessage message = new SimpleMessage();
            try {
                message.Padding = PaddingType.Undefined;
                message.SecondLine = lineValue;
                Assert.Fail();
            }
            catch (ArgumentOutOfRangeException) { }
        }
        [TestMethod]
        public void ValidateSimpleMessageSecondLineTooBig() {
            string lineValue = BasePropertyTestUtils.GetStringWithSpecifiedLength(17);
            SimpleMessage message = new SimpleMessage();
            try {
                message.SecondLine = lineValue;
                Assert.Fail();
            }
            catch (ArgumentOutOfRangeException) { }
        }
        [TestMethod]
        public void ValidateSimpleMessagePadLineWithoutPadding() {
            SimpleMessage message = new SimpleMessage();
            MethodInfo padLineMethod = message.GetType().GetMethod("PadLine", BindingFlags.Instance | BindingFlags.NonPublic);
            try {
                padLineMethod.Invoke(message, new object[] { String.Empty, 16 });
                Assert.Fail();
            }
            catch (TargetInvocationException ex) {
                Assert.IsInstanceOfType(ex.InnerException, typeof(InvalidOperationException));
            }
        }
    }
}
