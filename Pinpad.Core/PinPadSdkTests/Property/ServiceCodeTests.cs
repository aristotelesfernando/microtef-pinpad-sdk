using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PinPadSDK.Property;
using PinPadSDK.Enums;

namespace PinPadSdkTests.Property {
    [TestClass]
    public class ServiceCodeTests {
        [TestMethod]
        public void ValidateServiceCodeNullValue() {
            try {
                ServiceCode serviceCode = new ServiceCode(null);
                Assert.Fail();
            }
            catch (InvalidOperationException) { }
        }
        [TestMethod]
        public void ValidateServiceCodeTooSmall() {
            try {
                ServiceCode serviceCode = new ServiceCode("00");
                Assert.Fail();
            }
            catch (InvalidOperationException) { }
        }
        [TestMethod]
        public void ValidateServiceCodeTooBig() {
            try {
                ServiceCode serviceCode = new ServiceCode("0000");
                Assert.Fail();
            }
            catch (InvalidOperationException) { }
        }
        [TestMethod]
        public void ValidateServiceCodeValue() {
                ServiceCode serviceCode = new ServiceCode("000");
                Assert.AreEqual("000", serviceCode.Code);
        }
        [TestMethod]
        public void ValidateServiceCodeInterchangeValues() {
            Assert.AreEqual(InterchangeType.Undefined, new ServiceCode("000").Interchange);
            Assert.AreEqual(InterchangeType.International, new ServiceCode("100").Interchange);
            Assert.AreEqual(InterchangeType.International, new ServiceCode("200").Interchange);
            Assert.AreEqual(InterchangeType.Undefined, new ServiceCode("300").Interchange);
            Assert.AreEqual(InterchangeType.Undefined, new ServiceCode("400").Interchange);
            Assert.AreEqual(InterchangeType.National, new ServiceCode("500").Interchange);
            Assert.AreEqual(InterchangeType.National, new ServiceCode("600").Interchange);
            Assert.AreEqual(InterchangeType.Private, new ServiceCode("700").Interchange);
            Assert.AreEqual(InterchangeType.Undefined, new ServiceCode("800").Interchange);
            Assert.AreEqual(InterchangeType.Test, new ServiceCode("900").Interchange);
        }
        [TestMethod]
        public void ValidateServiceCodeEmvValues() {
            Assert.IsFalse(new ServiceCode("000").IsEMV);
            Assert.IsFalse(new ServiceCode("100").IsEMV);
            Assert.IsTrue(new ServiceCode("200").IsEMV);
            Assert.IsFalse(new ServiceCode("300").IsEMV);
            Assert.IsFalse(new ServiceCode("400").IsEMV);
            Assert.IsFalse(new ServiceCode("500").IsEMV);
            Assert.IsTrue(new ServiceCode("600").IsEMV);
            Assert.IsFalse(new ServiceCode("700").IsEMV);
            Assert.IsFalse(new ServiceCode("800").IsEMV);
            Assert.IsFalse(new ServiceCode("900").IsEMV);
        }
        [TestMethod]
        public void ValidateServiceCodePinRequiredValues() {
            Assert.IsTrue(new ServiceCode("000").IsPinRequired);
            Assert.IsFalse(new ServiceCode("001").IsPinRequired);
            Assert.IsFalse(new ServiceCode("002").IsPinRequired);
            Assert.IsTrue(new ServiceCode("003").IsPinRequired);
            Assert.IsFalse(new ServiceCode("004").IsPinRequired);
            Assert.IsTrue(new ServiceCode("005").IsPinRequired);
            Assert.IsTrue(new ServiceCode("006").IsPinRequired);
            Assert.IsTrue(new ServiceCode("007").IsPinRequired);
            Assert.IsFalse(new ServiceCode("008").IsPinRequired);
            Assert.IsFalse(new ServiceCode("009").IsPinRequired);
        }
        [TestMethod]
        public void ValidateServiceCodePinMandatoryValues() {
            Assert.IsTrue(new ServiceCode("000").IsPinMandatory);
            Assert.IsFalse(new ServiceCode("001").IsPinMandatory);
            Assert.IsFalse(new ServiceCode("002").IsPinMandatory);
            Assert.IsTrue(new ServiceCode("003").IsPinMandatory);
            Assert.IsFalse(new ServiceCode("004").IsPinMandatory);
            Assert.IsTrue(new ServiceCode("005").IsPinMandatory);
            Assert.IsFalse(new ServiceCode("006").IsPinMandatory);
            Assert.IsFalse(new ServiceCode("007").IsPinMandatory);
            Assert.IsFalse(new ServiceCode("008").IsPinMandatory);
            Assert.IsFalse(new ServiceCode("009").IsPinMandatory);
        }
        [TestMethod]
        public void ValidateServiceCodeStringParser() {
            Assert.IsNull(ServiceCode.StringParser(new StringReader("abc")));
            Assert.AreEqual("123", ServiceCode.StringParser(new StringReader("123")).Code);
        }
    }
}
