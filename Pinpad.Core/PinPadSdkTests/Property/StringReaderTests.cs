using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PinPadSDK.Property;

namespace PinPadSdkTests.Property {
    [TestClass]
    public class StringReaderTests {
        [TestMethod]
        public void ValidateStringReaderJump() {
            StringReader reader = new StringReader("10");
            reader.Jump(1);
            Assert.AreEqual("0", reader.PeekString(1));
            Assert.AreEqual("0", reader.ReadString(1));
            reader.Jump(-2);
            Assert.AreEqual("10", reader.PeekString(2));
            Assert.AreEqual("10", reader.ReadString(2));
        }
        [TestMethod]
        public void ValidateStringReaderOffset() {
            StringReader reader = new StringReader("1234567890");

            Assert.AreEqual(0, reader.Offset);
            Assert.AreEqual("12345", reader.PeekString(5));
            Assert.AreEqual(0, reader.Offset);
            Assert.AreEqual("12345", reader.ReadString(5));
            Assert.AreEqual(5, reader.Offset);
            Assert.AreEqual("67890", reader.PeekString(5));
            Assert.AreEqual(5, reader.Offset);
            Assert.AreEqual("67890", reader.ReadString(5));
            Assert.AreEqual(10, reader.Offset);
        }
        [TestMethod]
        public void ValidateStringReaderRemaining() {
            StringReader reader = new StringReader("1234567890");

            Assert.AreEqual(10, reader.Remaining);
            Assert.AreEqual("12345", reader.PeekString(5));
            Assert.AreEqual(10, reader.Remaining);
            Assert.AreEqual("12345", reader.ReadString(5));
            Assert.AreEqual(5, reader.Remaining);
            Assert.AreEqual("67890", reader.PeekString(5));
            Assert.AreEqual(5, reader.Remaining);
            Assert.AreEqual("67890", reader.ReadString(5));
            Assert.AreEqual(0, reader.Remaining);
        }
        [TestMethod]
        public void ValidateStringReaderIsOver() {
            StringReader reader = new StringReader("1234567890");

            Assert.IsFalse(reader.IsOver);
            Assert.AreEqual("12345", reader.PeekString(5));
            Assert.IsFalse(reader.IsOver);
            Assert.AreEqual("12345", reader.ReadString(5));
            Assert.IsFalse(reader.IsOver);
            Assert.AreEqual("67890", reader.PeekString(5));
            Assert.IsFalse(reader.IsOver);
            Assert.AreEqual("67890", reader.ReadString(5));
            Assert.IsTrue(reader.IsOver);
        }
        [TestMethod]
        public void ValidateStringReaderValue() {
            string value = "1234567890";
            StringReader reader = new StringReader(value);

            Assert.AreEqual(value, reader.Value);
            Assert.AreEqual("12345", reader.PeekString(5));
            Assert.AreEqual(value, reader.Value);
            Assert.AreEqual("12345", reader.ReadString(5));
            Assert.AreEqual(value, reader.Value);
            Assert.AreEqual("67890", reader.PeekString(5));
            Assert.AreEqual(value, reader.Value);
            Assert.AreEqual("67890", reader.ReadString(5));
            Assert.AreEqual(value, reader.Value);
        }
        [TestMethod]
        public void ValidateStringReaderLastReadString() {
            StringReader reader = new StringReader("10");
            Assert.AreEqual(String.Empty, reader.LastReadString);
            reader.Jump(1);
            Assert.AreEqual("0", reader.PeekString(1));
            Assert.AreEqual("0", reader.LastReadString);
            Assert.AreEqual("0", reader.ReadString(1));
            Assert.AreEqual("0", reader.LastReadString);

            try {
                reader.ReadBool();
                Assert.Fail();
            }
            catch { }
            Assert.AreEqual(String.Empty, reader.LastReadString);
        }
        [TestMethod]
        public void ValidateStringReaderString() {
            StringReader reader = new StringReader("1234567890");

            Assert.AreEqual("12345", reader.PeekString(5));
            Assert.AreEqual("12345", reader.ReadString(5));
            Assert.AreEqual("67890", reader.PeekString(5));
            Assert.AreEqual("67890", reader.ReadString(5));
        }
        [TestMethod]
        public void ValidateStringReaderLong() {
            StringReader reader = new StringReader("1234567890");

            Assert.AreEqual(12345, reader.PeekLong(5));
            Assert.AreEqual(12345, reader.ReadLong(5));
            Assert.AreEqual(67890, reader.PeekLong(5));
            Assert.AreEqual(67890, reader.ReadLong(5));

            reader = new StringReader(long.MaxValue.ToString());
            Assert.AreEqual(long.MaxValue, reader.PeekLong(reader.Remaining));
            Assert.AreEqual(long.MaxValue, reader.ReadLong(reader.Remaining));
        }
        [TestMethod]
        public void ValidateStringReaderInt() {
            StringReader reader = new StringReader("1234567890");

            Assert.AreEqual(12345, reader.PeekInt(5));
            Assert.AreEqual(12345, reader.ReadInt(5));
            Assert.AreEqual(67890, reader.PeekInt(5));
            Assert.AreEqual(67890, reader.ReadInt(5));

            reader = new StringReader(int.MaxValue.ToString());
            Assert.AreEqual(int.MaxValue, reader.PeekInt(reader.Remaining));
            Assert.AreEqual(int.MaxValue, reader.ReadInt(reader.Remaining));
        }
        [TestMethod]
        public void ValidateStringReaderBoolean() {
            StringReader reader = new StringReader("10");

            Assert.IsTrue(reader.PeekBool());
            Assert.IsTrue(reader.ReadBool());
            Assert.IsFalse(reader.PeekBool());
            Assert.IsFalse(reader.ReadBool());
        }
    }
}
