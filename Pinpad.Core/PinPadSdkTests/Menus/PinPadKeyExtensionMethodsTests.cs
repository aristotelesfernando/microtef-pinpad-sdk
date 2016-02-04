using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PinPadSDK.Enums;
using PinPadSDK.Menus;
using PinPadSDK.Exceptions;

namespace PinPadSdkTests.Menus {
    [TestClass]
    public class PinPadKeyExtensionMethodsTests {
        [TestMethod]
        public void ValidatePinPadKeyExtensionMethodsTranslate() {
            Assert.IsTrue(BasePropertyTestUtils.IsActionThrowingException<UnknownPinPadKeyException>(() => {
                PinPadKey.Undefined.Translate();
            }));
            Assert.AreEqual("OK/ENTRA", PinPadKey.Return.Translate());
            Assert.AreEqual("F1", PinPadKey.Function1.Translate());
            Assert.AreEqual("F2", PinPadKey.Function2.Translate());
            Assert.AreEqual("F3", PinPadKey.Function3.Translate());
            Assert.AreEqual("F4", PinPadKey.Function4.Translate());
            Assert.AreEqual("LIMPA", PinPadKey.Backspace.Translate());
            Assert.AreEqual("CANCELA", PinPadKey.Cancel.Translate());
            Assert.AreEqual("0", PinPadKey.Decimal0.Translate());
            Assert.AreEqual("1", PinPadKey.Decimal1.Translate());
            Assert.AreEqual("2", PinPadKey.Decimal2.Translate());
            Assert.AreEqual("3", PinPadKey.Decimal3.Translate());
            Assert.AreEqual("4", PinPadKey.Decimal4.Translate());
            Assert.AreEqual("5", PinPadKey.Decimal5.Translate());
            Assert.AreEqual("6", PinPadKey.Decimal6.Translate());
            Assert.AreEqual("7", PinPadKey.Decimal7.Translate());
            Assert.AreEqual("8", PinPadKey.Decimal8.Translate());
            Assert.AreEqual("9", PinPadKey.Decimal9.Translate());
        }
        [TestMethod]
        public void ValidatePinPadKeyExtensionMethodsIsNumeric() {
            Assert.IsFalse(PinPadKey.Undefined.IsNumeric());
            Assert.IsFalse(PinPadKey.Return.IsNumeric());
            Assert.IsFalse(PinPadKey.Function1.IsNumeric());
            Assert.IsFalse(PinPadKey.Function2.IsNumeric());
            Assert.IsFalse(PinPadKey.Function3.IsNumeric());
            Assert.IsFalse(PinPadKey.Function4.IsNumeric());
            Assert.IsFalse(PinPadKey.Backspace.IsNumeric());
            Assert.IsFalse(PinPadKey.Cancel.IsNumeric());
            Assert.IsTrue(PinPadKey.Decimal0.IsNumeric());
            Assert.IsTrue(PinPadKey.Decimal1.IsNumeric());
            Assert.IsTrue(PinPadKey.Decimal2.IsNumeric());
            Assert.IsTrue(PinPadKey.Decimal3.IsNumeric());
            Assert.IsTrue(PinPadKey.Decimal4.IsNumeric());
            Assert.IsTrue(PinPadKey.Decimal5.IsNumeric());
            Assert.IsTrue(PinPadKey.Decimal6.IsNumeric());
            Assert.IsTrue(PinPadKey.Decimal7.IsNumeric());
            Assert.IsTrue(PinPadKey.Decimal8.IsNumeric());
            Assert.IsTrue(PinPadKey.Decimal9.IsNumeric());
        }
        [TestMethod]
        public void ValidatePinPadKeyExtensionMethodsGetLong() {
            Assert.IsTrue(BasePropertyTestUtils.IsActionThrowingException<UnknownPinPadKeyException>(() => {
                PinPadKey.Undefined.GetLong();
            }));
            Assert.IsTrue(BasePropertyTestUtils.IsActionThrowingException<UnknownPinPadKeyException>(() => {
                PinPadKey.Return.GetLong();
            }));
            Assert.IsTrue(BasePropertyTestUtils.IsActionThrowingException<UnknownPinPadKeyException>(() => {
                PinPadKey.Function1.GetLong();
            }));
            Assert.IsTrue(BasePropertyTestUtils.IsActionThrowingException<UnknownPinPadKeyException>(() => {
                PinPadKey.Function2.GetLong();
            }));
            Assert.IsTrue(BasePropertyTestUtils.IsActionThrowingException<UnknownPinPadKeyException>(() => {
                PinPadKey.Function3.GetLong();
            }));
            Assert.IsTrue(BasePropertyTestUtils.IsActionThrowingException<UnknownPinPadKeyException>(() => {
                PinPadKey.Function4.GetLong();
            }));
            Assert.IsTrue(BasePropertyTestUtils.IsActionThrowingException<UnknownPinPadKeyException>(() => {
                PinPadKey.Backspace.GetLong();
            }));
            Assert.IsTrue(BasePropertyTestUtils.IsActionThrowingException<UnknownPinPadKeyException>(() => {
                PinPadKey.Cancel.GetLong();
            }));
            Assert.AreEqual(0, PinPadKey.Decimal0.GetLong());
            Assert.AreEqual(1, PinPadKey.Decimal1.GetLong());
            Assert.AreEqual(2, PinPadKey.Decimal2.GetLong());
            Assert.AreEqual(3, PinPadKey.Decimal3.GetLong());
            Assert.AreEqual(4, PinPadKey.Decimal4.GetLong());
            Assert.AreEqual(5, PinPadKey.Decimal5.GetLong());
            Assert.AreEqual(6, PinPadKey.Decimal6.GetLong());
            Assert.AreEqual(7, PinPadKey.Decimal7.GetLong());
            Assert.AreEqual(8, PinPadKey.Decimal8.GetLong());
            Assert.AreEqual(9, PinPadKey.Decimal9.GetLong());

            PinPadKey[] keyCollection = new PinPadKey[]{PinPadKey.Decimal1,PinPadKey.Decimal2,PinPadKey.Decimal3,PinPadKey.Decimal4};
            Assert.AreEqual(1234, keyCollection.GetLong());
            
            keyCollection = new PinPadKey[]{PinPadKey.Decimal1,PinPadKey.Decimal2, PinPadKey.Cancel, PinPadKey.Decimal3,PinPadKey.Decimal4};
            Assert.IsTrue(BasePropertyTestUtils.IsActionThrowingException<UnknownPinPadKeyException>(() => {
                keyCollection.GetLong();
            }));
        }
        [TestMethod]
        public void ValidatePinPadKeyExtensionMethodsGetFunctionKeyIndex() {
            Assert.IsTrue(BasePropertyTestUtils.IsActionThrowingException<UnknownPinPadKeyException>(() => {
                PinPadKey.Undefined.GetFunctionKeyIndex();
            }));
            Assert.IsTrue(BasePropertyTestUtils.IsActionThrowingException<UnknownPinPadKeyException>(() => {
                PinPadKey.Return.GetFunctionKeyIndex();
            }));
            Assert.AreEqual(1, PinPadKey.Function1.GetFunctionKeyIndex());
            Assert.AreEqual(2, PinPadKey.Function2.GetFunctionKeyIndex());
            Assert.AreEqual(3, PinPadKey.Function3.GetFunctionKeyIndex());
            Assert.AreEqual(4, PinPadKey.Function4.GetFunctionKeyIndex());
            Assert.IsTrue(BasePropertyTestUtils.IsActionThrowingException<UnknownPinPadKeyException>(() => {
                PinPadKey.Backspace.GetFunctionKeyIndex();
            }));
            Assert.IsTrue(BasePropertyTestUtils.IsActionThrowingException<UnknownPinPadKeyException>(() => {
                PinPadKey.Cancel.GetFunctionKeyIndex();
            }));
            Assert.IsTrue(BasePropertyTestUtils.IsActionThrowingException<UnknownPinPadKeyException>(() => {
                PinPadKey.Decimal0.GetFunctionKeyIndex();
            }));
            Assert.IsTrue(BasePropertyTestUtils.IsActionThrowingException<UnknownPinPadKeyException>(() => {
                PinPadKey.Decimal1.GetFunctionKeyIndex();
            }));
            Assert.IsTrue(BasePropertyTestUtils.IsActionThrowingException<UnknownPinPadKeyException>(() => {
                PinPadKey.Decimal2.GetFunctionKeyIndex();
            }));
            Assert.IsTrue(BasePropertyTestUtils.IsActionThrowingException<UnknownPinPadKeyException>(() => {
                PinPadKey.Decimal3.GetFunctionKeyIndex();
            }));
            Assert.IsTrue(BasePropertyTestUtils.IsActionThrowingException<UnknownPinPadKeyException>(() => {
                PinPadKey.Decimal4.GetFunctionKeyIndex();
            }));
            Assert.IsTrue(BasePropertyTestUtils.IsActionThrowingException<UnknownPinPadKeyException>(() => {
                PinPadKey.Decimal5.GetFunctionKeyIndex();
            }));
            Assert.IsTrue(BasePropertyTestUtils.IsActionThrowingException<UnknownPinPadKeyException>(() => {
                PinPadKey.Decimal6.GetFunctionKeyIndex();
            }));
            Assert.IsTrue(BasePropertyTestUtils.IsActionThrowingException<UnknownPinPadKeyException>(() => {
                PinPadKey.Decimal7.GetFunctionKeyIndex();
            }));
            Assert.IsTrue(BasePropertyTestUtils.IsActionThrowingException<UnknownPinPadKeyException>(() => {
                PinPadKey.Decimal8.GetFunctionKeyIndex();
            }));
            Assert.IsTrue(BasePropertyTestUtils.IsActionThrowingException<UnknownPinPadKeyException>(() => {
                PinPadKey.Decimal9.GetFunctionKeyIndex();
            }));
        }
        [TestMethod]
        public void ValidatePinPadKeyExtensionMethodsIsFunctionKey() {
            Assert.IsFalse(PinPadKey.Undefined.IsFunctionKey());
            Assert.IsFalse(PinPadKey.Return.IsFunctionKey());
            Assert.IsTrue(PinPadKey.Function1.IsFunctionKey());
            Assert.IsTrue(PinPadKey.Function2.IsFunctionKey());
            Assert.IsTrue(PinPadKey.Function3.IsFunctionKey());
            Assert.IsTrue(PinPadKey.Function4.IsFunctionKey());
            Assert.IsFalse(PinPadKey.Backspace.IsFunctionKey());
            Assert.IsFalse(PinPadKey.Cancel.IsFunctionKey());
            Assert.IsFalse(PinPadKey.Decimal0.IsFunctionKey());
            Assert.IsFalse(PinPadKey.Decimal1.IsFunctionKey());
            Assert.IsFalse(PinPadKey.Decimal2.IsFunctionKey());
            Assert.IsFalse(PinPadKey.Decimal3.IsFunctionKey());
            Assert.IsFalse(PinPadKey.Decimal4.IsFunctionKey());
            Assert.IsFalse(PinPadKey.Decimal5.IsFunctionKey());
            Assert.IsFalse(PinPadKey.Decimal6.IsFunctionKey());
            Assert.IsFalse(PinPadKey.Decimal7.IsFunctionKey());
            Assert.IsFalse(PinPadKey.Decimal8.IsFunctionKey());
            Assert.IsFalse(PinPadKey.Decimal9.IsFunctionKey());
        }
        [TestMethod]
        public void ValidatePinPadKeyExtensionMethodsGetString() {
            Assert.IsTrue(BasePropertyTestUtils.IsActionThrowingException<UnknownPinPadKeyException>(() => {
                PinPadKey.Undefined.GetString();
            }));
            Assert.IsTrue(BasePropertyTestUtils.IsActionThrowingException<UnknownPinPadKeyException>(() => {
                PinPadKey.Return.GetString();
            }));
            Assert.IsTrue(BasePropertyTestUtils.IsActionThrowingException<UnknownPinPadKeyException>(() => {
                PinPadKey.Function1.GetString();
            }));
            Assert.IsTrue(BasePropertyTestUtils.IsActionThrowingException<UnknownPinPadKeyException>(() => {
                PinPadKey.Function2.GetString();
            }));
            Assert.IsTrue(BasePropertyTestUtils.IsActionThrowingException<UnknownPinPadKeyException>(() => {
                PinPadKey.Function3.GetString();
            }));
            Assert.IsTrue(BasePropertyTestUtils.IsActionThrowingException<UnknownPinPadKeyException>(() => {
                PinPadKey.Function4.GetString();
            }));
            Assert.IsTrue(BasePropertyTestUtils.IsActionThrowingException<UnknownPinPadKeyException>(() => {
                PinPadKey.Backspace.GetString();
            }));
            Assert.IsTrue(BasePropertyTestUtils.IsActionThrowingException<UnknownPinPadKeyException>(() => {
                PinPadKey.Cancel.GetString();
            }));
            Assert.AreEqual("0", PinPadKey.Decimal0.GetString());
            Assert.AreEqual("1", PinPadKey.Decimal1.GetString());
            Assert.AreEqual("2", PinPadKey.Decimal2.GetString());
            Assert.AreEqual("3", PinPadKey.Decimal3.GetString());
            Assert.AreEqual("4", PinPadKey.Decimal4.GetString());
            Assert.AreEqual("5", PinPadKey.Decimal5.GetString());
            Assert.AreEqual("6", PinPadKey.Decimal6.GetString());
            Assert.AreEqual("7", PinPadKey.Decimal7.GetString());
            Assert.AreEqual("8", PinPadKey.Decimal8.GetString());
            Assert.AreEqual("9", PinPadKey.Decimal9.GetString());

            PinPadKey[] keyCollection = new PinPadKey[] { PinPadKey.Decimal1, PinPadKey.Decimal2, PinPadKey.Decimal3, PinPadKey.Decimal4 };
            Assert.AreEqual("1234", keyCollection.GetString());

            keyCollection = new PinPadKey[] { PinPadKey.Decimal1, PinPadKey.Decimal2, PinPadKey.Cancel, PinPadKey.Decimal3, PinPadKey.Decimal4 };
            Assert.IsTrue(BasePropertyTestUtils.IsActionThrowingException<UnknownPinPadKeyException>(() => {
                keyCollection.GetString();
            }));
        }
    }
}
