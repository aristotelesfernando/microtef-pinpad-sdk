using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PinPadSDK.Property;

namespace PinPadSdkTests.Property {
    [TestClass]
    public class MultilineMessageTests {
        [TestMethod]
        public void ValidateMultilineMessageString() {
            MultilineMessage message = new MultilineMessage();
            message.LineCollection.Add("12345");
            message.LineCollection.Add("67890");
            Assert.AreEqual("12345\r67890", message.CommandString);
        }
        [TestMethod]
        public void ValidateMultilineMessageConstructor() {
            MultilineMessage message = new MultilineMessage("12345", "67890");
            Assert.AreEqual("12345\r67890", message.CommandString);
        }
        [TestMethod]
        public void ValidateMultilineMessageStringParse() {
            MultilineMessage message = new MultilineMessage();
            message.CommandString = "12345\r67890";
            Assert.AreEqual("12345\r67890", message.CommandString);
            Assert.AreEqual("12345", message.LineCollection[0]);
            Assert.AreEqual("67890", message.LineCollection[1]);
        }
        [TestMethod]
        public void ValidateMultilineMessageNoLines() {
            MultilineMessage message = new MultilineMessage();
            Assert.AreEqual(String.Empty, message.CommandString);
        }
    }
}
