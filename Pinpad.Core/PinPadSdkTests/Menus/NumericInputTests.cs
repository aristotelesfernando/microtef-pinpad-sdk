using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PinPadSDK.PinPad;
using PinPadSDK.Menus;
using PinPadSDK.Enums;

namespace PinPadSdkTests.Menus {
    [TestClass]
    public class NumericInputTests {
        [TestInitialize]
        public void TestSetup() {
            CrossPlatformDesktop.CrossPlatformDesktopInitializer.Initialize_CrossPlatformDesktop();
            //CrossPlatformController.SendMailController = new SendMailControllerMock();
        }
        [TestMethod]
        public void ValidateNumericInputWithoutStoneApp() {
            QueuedKeysPinPad queuedKeysPinPad = new QueuedKeysPinPad();
            queuedKeysPinPad.OnMultilineMessageReceived = (message) => {
                Assert.AreEqual("(USE F1 e F2)", message.LineCollection[1]);

            };
            PinPadFacade pinPad = new PinPadFacade(queuedKeysPinPad.PinPadConnection);
            NumericInput menu = new NumericInput(pinPad);

            int value = 123;
            for (int i = 0; i < value; i++) {
                queuedKeysPinPad.KeyCollection.Enqueue(PinPadKey.Function2);
            }
            queuedKeysPinPad.KeyCollection.Enqueue(PinPadKey.Return);
            queuedKeysPinPad.KeyCollection.Enqueue(PinPadKey.Return);
            Assert.AreEqual(value, menu.ReadInput(NumericInput.DefaultIntegerFormatHandler));

            //123 using return keys to confirm each digit
            queuedKeysPinPad.KeyCollection.Enqueue(PinPadKey.Function2);
            queuedKeysPinPad.KeyCollection.Enqueue(PinPadKey.Return);
            queuedKeysPinPad.KeyCollection.Enqueue(PinPadKey.Function2);
            queuedKeysPinPad.KeyCollection.Enqueue(PinPadKey.Function2);
            queuedKeysPinPad.KeyCollection.Enqueue(PinPadKey.Return);
            queuedKeysPinPad.KeyCollection.Enqueue(PinPadKey.Function2);
            queuedKeysPinPad.KeyCollection.Enqueue(PinPadKey.Function2);
            queuedKeysPinPad.KeyCollection.Enqueue(PinPadKey.Function2);
            queuedKeysPinPad.KeyCollection.Enqueue(PinPadKey.Return);
            queuedKeysPinPad.KeyCollection.Enqueue(PinPadKey.Return);
            Assert.AreEqual(123, menu.ReadInput(NumericInput.DefaultIntegerFormatHandler));
        }
        [TestMethod]
        public void ValidateNumericInputWithoutStoneAppCancel() {
            QueuedKeysPinPad queuedKeysPinPad = new QueuedKeysPinPad();
            queuedKeysPinPad.OnMultilineMessageReceived = (message) => {
                Assert.AreEqual("(USE F1 e F2)", message.LineCollection[1]);

            };
            PinPadFacade pinPad = new PinPadFacade(queuedKeysPinPad.PinPadConnection);
            NumericInput menu = new NumericInput(pinPad);

            //123 using return keys to confirm each digit
            queuedKeysPinPad.KeyCollection.Enqueue(PinPadKey.Function2);
            queuedKeysPinPad.KeyCollection.Enqueue(PinPadKey.Return);
            queuedKeysPinPad.KeyCollection.Enqueue(PinPadKey.Function2);
            queuedKeysPinPad.KeyCollection.Enqueue(PinPadKey.Function2);
            queuedKeysPinPad.KeyCollection.Enqueue(PinPadKey.Cancel);
            Assert.IsNull(menu.ReadInput(NumericInput.DefaultIntegerFormatHandler));
        }
        [TestMethod]
        public void ValidateNumericInputWithoutStoneAppMaxValue() {
            QueuedKeysPinPad queuedKeysPinPad = new QueuedKeysPinPad();
            queuedKeysPinPad.OnMultilineMessageReceived = (message) => {
                Assert.AreEqual("(USE F1 e F2)", message.LineCollection[1]);

            };
            PinPadFacade pinPad = new PinPadFacade(queuedKeysPinPad.PinPadConnection);
            NumericInput menu = new NumericInput(pinPad);

            for (int i = 0; i < 123; i++) {
                queuedKeysPinPad.KeyCollection.Enqueue(PinPadKey.Function2);
            }
            queuedKeysPinPad.KeyCollection.Enqueue(PinPadKey.Return);
            queuedKeysPinPad.KeyCollection.Enqueue(PinPadKey.Return);
            Assert.AreEqual(100, menu.ReadInput(NumericInput.DefaultIntegerFormatHandler, 0, 100));

            //123 using return keys to confirm each digit
            queuedKeysPinPad.KeyCollection.Enqueue(PinPadKey.Function2);
            queuedKeysPinPad.KeyCollection.Enqueue(PinPadKey.Return);
            queuedKeysPinPad.KeyCollection.Enqueue(PinPadKey.Function2);
            queuedKeysPinPad.KeyCollection.Enqueue(PinPadKey.Function2);
            queuedKeysPinPad.KeyCollection.Enqueue(PinPadKey.Return);
            queuedKeysPinPad.KeyCollection.Enqueue(PinPadKey.Function2);
            queuedKeysPinPad.KeyCollection.Enqueue(PinPadKey.Function2);
            queuedKeysPinPad.KeyCollection.Enqueue(PinPadKey.Function2);
            queuedKeysPinPad.KeyCollection.Enqueue(PinPadKey.Return);
            queuedKeysPinPad.KeyCollection.Enqueue(PinPadKey.Return);
            Assert.AreEqual(100, menu.ReadInput(NumericInput.DefaultIntegerFormatHandler, 0, 100));
        }
        [TestMethod]
        public void ValidateNumericInputWithoutStoneAppMinValue() {
            QueuedKeysPinPad queuedKeysPinPad = new QueuedKeysPinPad();
            queuedKeysPinPad.OnMultilineMessageReceived = (message) => {
                Assert.AreEqual("(USE F1 e F2)", message.LineCollection[1]);

            };
            PinPadFacade pinPad = new PinPadFacade(queuedKeysPinPad.PinPadConnection);
            NumericInput menu = new NumericInput(pinPad);

            queuedKeysPinPad.KeyCollection.Enqueue(PinPadKey.Function1);
            queuedKeysPinPad.KeyCollection.Enqueue(PinPadKey.Function1);
            queuedKeysPinPad.KeyCollection.Enqueue(PinPadKey.Function1);
            queuedKeysPinPad.KeyCollection.Enqueue(PinPadKey.Return);
            queuedKeysPinPad.KeyCollection.Enqueue(PinPadKey.Return);
            Assert.AreEqual(0, menu.ReadInput(NumericInput.DefaultIntegerFormatHandler, 0, 100));

            queuedKeysPinPad.KeyCollection.Enqueue(PinPadKey.Backspace);
            queuedKeysPinPad.KeyCollection.Enqueue(PinPadKey.Backspace);
            queuedKeysPinPad.KeyCollection.Enqueue(PinPadKey.Backspace);
            queuedKeysPinPad.KeyCollection.Enqueue(PinPadKey.Return);
            queuedKeysPinPad.KeyCollection.Enqueue(PinPadKey.Return);
            Assert.AreEqual(0, menu.ReadInput(NumericInput.DefaultIntegerFormatHandler, 0, 100));
        }
        [TestMethod]
        public void ValidateNumericInputWithoutStoneAppBackspace() {
            QueuedKeysPinPad queuedKeysPinPad = new QueuedKeysPinPad();
            queuedKeysPinPad.OnMultilineMessageReceived = (message) => {
                Assert.AreEqual("(USE F1 e F2)", message.LineCollection[1]);

            };
            PinPadFacade pinPad = new PinPadFacade(queuedKeysPinPad.PinPadConnection);
            NumericInput menu = new NumericInput(pinPad);

            for (int i = 0; i < 123; i++) {
                queuedKeysPinPad.KeyCollection.Enqueue(PinPadKey.Function2);
            }
            queuedKeysPinPad.KeyCollection.Enqueue(PinPadKey.Backspace);
            queuedKeysPinPad.KeyCollection.Enqueue(PinPadKey.Return);
            queuedKeysPinPad.KeyCollection.Enqueue(PinPadKey.Return);
            Assert.AreEqual(12, menu.ReadInput(NumericInput.DefaultIntegerFormatHandler));

            //123 using return keys to confirm each digit
            queuedKeysPinPad.KeyCollection.Enqueue(PinPadKey.Function2);
            queuedKeysPinPad.KeyCollection.Enqueue(PinPadKey.Return);
            queuedKeysPinPad.KeyCollection.Enqueue(PinPadKey.Function2);
            queuedKeysPinPad.KeyCollection.Enqueue(PinPadKey.Function2);
            queuedKeysPinPad.KeyCollection.Enqueue(PinPadKey.Return);
            queuedKeysPinPad.KeyCollection.Enqueue(PinPadKey.Function2);
            queuedKeysPinPad.KeyCollection.Enqueue(PinPadKey.Function2);
            queuedKeysPinPad.KeyCollection.Enqueue(PinPadKey.Function2);
            queuedKeysPinPad.KeyCollection.Enqueue(PinPadKey.Backspace);
            queuedKeysPinPad.KeyCollection.Enqueue(PinPadKey.Return);
            queuedKeysPinPad.KeyCollection.Enqueue(PinPadKey.Return);
            Assert.AreEqual(12, menu.ReadInput(NumericInput.DefaultIntegerFormatHandler));
        }
        [TestMethod]
        public void ValidateNumericInputWithStoneApp() {
            QueuedKeysPinPad queuedKeysPinPad = new QueuedKeysPinPad();
            queuedKeysPinPad.SupportStone = true;
            PinPadFacade pinPad = new PinPadFacade(queuedKeysPinPad.PinPadConnection);
            NumericInput menu = new NumericInput(pinPad);

            int value = 123;
            for (int i = 0; i < value; i++) {
                queuedKeysPinPad.KeyCollection.Enqueue(PinPadKey.Function2);
            }
            queuedKeysPinPad.KeyCollection.Enqueue(PinPadKey.Return);
            Assert.AreEqual(value, menu.ReadInput(NumericInput.DefaultStoneIntegerFormatHandler));

            queuedKeysPinPad.KeyCollection.Enqueue(PinPadKey.Decimal1);
            queuedKeysPinPad.KeyCollection.Enqueue(PinPadKey.Decimal2);
            queuedKeysPinPad.KeyCollection.Enqueue(PinPadKey.Decimal3);
            queuedKeysPinPad.KeyCollection.Enqueue(PinPadKey.Return);
            Assert.AreEqual(123, menu.ReadInput(NumericInput.DefaultStoneIntegerFormatHandler));
        }

        [TestMethod]
        public void ValidateNumericInputWithStoneAppCancel() {
            QueuedKeysPinPad queuedKeysPinPad = new QueuedKeysPinPad();
            queuedKeysPinPad.SupportStone = true;
            PinPadFacade pinPad = new PinPadFacade(queuedKeysPinPad.PinPadConnection);
            NumericInput menu = new NumericInput(pinPad);

            queuedKeysPinPad.KeyCollection.Enqueue(PinPadKey.Decimal1);
            queuedKeysPinPad.KeyCollection.Enqueue(PinPadKey.Decimal2);
            queuedKeysPinPad.KeyCollection.Enqueue(PinPadKey.Decimal3);
            queuedKeysPinPad.KeyCollection.Enqueue(PinPadKey.Cancel);
            Assert.IsNull(menu.ReadInput(NumericInput.DefaultStoneIntegerFormatHandler));
        }
        [TestMethod]
        public void ValidateNumericInputWithStoneAppMaxValue() {
            QueuedKeysPinPad queuedKeysPinPad = new QueuedKeysPinPad();
            queuedKeysPinPad.SupportStone = true;
            PinPadFacade pinPad = new PinPadFacade(queuedKeysPinPad.PinPadConnection);
            NumericInput menu = new NumericInput(pinPad);

            for (int i = 0; i < 123; i++) {
                queuedKeysPinPad.KeyCollection.Enqueue(PinPadKey.Function2);
            }
            queuedKeysPinPad.KeyCollection.Enqueue(PinPadKey.Return);
            Assert.AreEqual(100, menu.ReadInput(NumericInput.DefaultStoneIntegerFormatHandler, 0, 100));

            //123 using return keys to confirm each digit
            queuedKeysPinPad.KeyCollection.Enqueue(PinPadKey.Decimal1);
            queuedKeysPinPad.KeyCollection.Enqueue(PinPadKey.Decimal2);
            queuedKeysPinPad.KeyCollection.Enqueue(PinPadKey.Decimal3);
            queuedKeysPinPad.KeyCollection.Enqueue(PinPadKey.Return);
            Assert.AreEqual(12, menu.ReadInput(NumericInput.DefaultStoneIntegerFormatHandler, 0, 100));
        }
        [TestMethod]
        public void ValidateNumericInputWithStoneAppMinValue() {
            QueuedKeysPinPad queuedKeysPinPad = new QueuedKeysPinPad();
            queuedKeysPinPad.SupportStone = true;
            PinPadFacade pinPad = new PinPadFacade(queuedKeysPinPad.PinPadConnection);
            NumericInput menu = new NumericInput(pinPad);

            queuedKeysPinPad.KeyCollection.Enqueue(PinPadKey.Function1);
            queuedKeysPinPad.KeyCollection.Enqueue(PinPadKey.Function1);
            queuedKeysPinPad.KeyCollection.Enqueue(PinPadKey.Function1);
            queuedKeysPinPad.KeyCollection.Enqueue(PinPadKey.Return);
            Assert.AreEqual(0, menu.ReadInput(NumericInput.DefaultStoneIntegerFormatHandler, 0, 100));

            queuedKeysPinPad.KeyCollection.Enqueue(PinPadKey.Backspace);
            queuedKeysPinPad.KeyCollection.Enqueue(PinPadKey.Backspace);
            queuedKeysPinPad.KeyCollection.Enqueue(PinPadKey.Backspace);
            queuedKeysPinPad.KeyCollection.Enqueue(PinPadKey.Return);
            Assert.AreEqual(0, menu.ReadInput(NumericInput.DefaultStoneIntegerFormatHandler, 0, 100));
        }
        [TestMethod]
        public void ValidateNumericInputWithStoneAppBackspace() {
            QueuedKeysPinPad queuedKeysPinPad = new QueuedKeysPinPad();
            queuedKeysPinPad.SupportStone = true;
            PinPadFacade pinPad = new PinPadFacade(queuedKeysPinPad.PinPadConnection);
            NumericInput menu = new NumericInput(pinPad);

            for (int i = 0; i < 123; i++) {
                queuedKeysPinPad.KeyCollection.Enqueue(PinPadKey.Function2);
            }
            queuedKeysPinPad.KeyCollection.Enqueue(PinPadKey.Backspace);
            queuedKeysPinPad.KeyCollection.Enqueue(PinPadKey.Return);
            Assert.AreEqual(12, menu.ReadInput(NumericInput.DefaultStoneIntegerFormatHandler));

            queuedKeysPinPad.KeyCollection.Enqueue(PinPadKey.Decimal1);
            queuedKeysPinPad.KeyCollection.Enqueue(PinPadKey.Decimal2);
            queuedKeysPinPad.KeyCollection.Enqueue(PinPadKey.Decimal3);
            queuedKeysPinPad.KeyCollection.Enqueue(PinPadKey.Backspace);
            queuedKeysPinPad.KeyCollection.Enqueue(PinPadKey.Return);
            Assert.AreEqual(12, menu.ReadInput(NumericInput.DefaultStoneIntegerFormatHandler));
        }
    }
}
