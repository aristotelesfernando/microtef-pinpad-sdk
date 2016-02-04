using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PinPadSDK.PinPad;
using PinPadSDK.Menus;
using PinPadSDK.Enums;

namespace PinPadSdkTests.Menus {
    [TestClass]
    public class FunctionInputTests {
        [TestInitialize]
        public void TestSetup() {
            CrossPlatformDesktop.CrossPlatformDesktopInitializer.Initialize_CrossPlatformDesktop();
            //CrossPlatformController.SendMailController = new SendMailControllerMock();
        }
        [TestMethod]
        public void ValidateFunctionInput() {
            QueuedKeysPinPad queuedKeysPinPad = new QueuedKeysPinPad();
            queuedKeysPinPad.OnMultilineMessageReceived = (message) => {
                Assert.AreEqual("header1", message.LineCollection[0]);
                Assert.AreEqual("header2", message.LineCollection[1]);
                Assert.AreEqual("header3", message.LineCollection[2]);
                Assert.AreEqual("header4", message.LineCollection[3]);
                Assert.AreEqual("header5", message.LineCollection[4]);
                Assert.AreEqual("F1: item1", message.LineCollection[5]);
                Assert.AreEqual("F2: item2", message.LineCollection[6]);
                Assert.AreEqual("F3: item3", message.LineCollection[7]);
                Assert.AreEqual("F4: item4", message.LineCollection[8]);

            };
            PinPadFacade pinPad = new PinPadFacade(queuedKeysPinPad.PinPadConnection);
            FunctionInput menu = new FunctionInput(pinPad);

            queuedKeysPinPad.KeyCollection.Enqueue(PinPadKey.Function1);
            Assert.AreEqual("item1", menu.ReadInput("item1", "item2", "item3", "item4", "header1", "header2", "header3", "header4", "header5"));

            queuedKeysPinPad.KeyCollection.Enqueue(PinPadKey.Function2);
            Assert.AreEqual("item2", menu.ReadInput("item1", "item2", "item3", "item4", "header1", "header2", "header3", "header4", "header5"));

            queuedKeysPinPad.KeyCollection.Enqueue(PinPadKey.Function3);
            Assert.AreEqual("item3", menu.ReadInput("item1", "item2", "item3", "item4", "header1", "header2", "header3", "header4", "header5"));

            queuedKeysPinPad.KeyCollection.Enqueue(PinPadKey.Function4);
            Assert.AreEqual("item4", menu.ReadInput("item1", "item2", "item3", "item4", "header1", "header2", "header3", "header4", "header5"));

            queuedKeysPinPad.KeyCollection.Enqueue(PinPadKey.Cancel);
            Assert.IsNull(menu.ReadInput("item1", "item2", "item3", "item4", "header1", "header2", "header3", "header4", "header5"));

            foreach (PinPadKey key in new PinPadKey[] { PinPadKey.Backspace, PinPadKey.Decimal0, PinPadKey.Decimal1, PinPadKey.Decimal2, PinPadKey.Decimal3, PinPadKey.Decimal4, PinPadKey.Decimal5, PinPadKey.Decimal6, PinPadKey.Decimal7, PinPadKey.Decimal8, PinPadKey.Decimal9, PinPadKey.Return, PinPadKey.Function1 }) {
                queuedKeysPinPad.KeyCollection.Enqueue(key);
            }
            Assert.AreEqual("item1", menu.ReadInput("item1", "item2", "item3", "item4", "header1", "header2", "header3", "header4", "header5"));
        }
        [TestMethod]
        public void ValidateFunctionInputInvalidChoice() {
            QueuedKeysPinPad queuedKeysPinPad = new QueuedKeysPinPad();
            PinPadFacade pinPad = new PinPadFacade(queuedKeysPinPad.PinPadConnection);
            FunctionInput menu = new FunctionInput(pinPad);

            queuedKeysPinPad.KeyCollection.Enqueue(PinPadKey.Function2);
            queuedKeysPinPad.KeyCollection.Enqueue(PinPadKey.Function1);
            Assert.AreEqual("item1", menu.ReadInput("item1", null, "item3", "item4"));

        }
        [TestMethod]
        public void ValidateFunctionInputAmountOfItems() {
            QueuedKeysPinPad queuedKeysPinPad = new QueuedKeysPinPad();
            PinPadFacade pinPad = new PinPadFacade(queuedKeysPinPad.PinPadConnection);
            FunctionInput menu = new FunctionInput(pinPad);

            Assert.IsTrue(BasePropertyTestUtils.IsActionThrowingException<InvalidOperationException>(() => {
                menu.ReadInput(null, null, null, null);
            }));
            Assert.IsTrue(BasePropertyTestUtils.IsActionThrowingException<InvalidOperationException>(() => {
                menu.ReadInput(" ", null, null, null);
            }));
            Assert.IsTrue(BasePropertyTestUtils.IsActionThrowingException<InvalidOperationException>(() => {
                menu.ReadInput(null, " ", null, null);
            }));
            Assert.IsTrue(BasePropertyTestUtils.IsActionThrowingException<InvalidOperationException>(() => {
                menu.ReadInput(null, null, " ", null);
            }));
            Assert.IsTrue(BasePropertyTestUtils.IsActionThrowingException<InvalidOperationException>(() => {
                menu.ReadInput(null, null, null, " ");
            }));
            queuedKeysPinPad.KeyCollection.Enqueue(PinPadKey.Function1);
            Assert.IsFalse(BasePropertyTestUtils.IsActionThrowingException<InvalidOperationException>(() => {
                menu.ReadInput(" ", " ", null, null);
            }));
            queuedKeysPinPad.KeyCollection.Enqueue(PinPadKey.Function1);
            Assert.IsFalse(BasePropertyTestUtils.IsActionThrowingException<InvalidOperationException>(() => {
                menu.ReadInput(" ", null, " ", null);
            }));
            queuedKeysPinPad.KeyCollection.Enqueue(PinPadKey.Function1);
            Assert.IsFalse(BasePropertyTestUtils.IsActionThrowingException<InvalidOperationException>(() => {
                menu.ReadInput(" ", null, null, " ");
            }));
            queuedKeysPinPad.KeyCollection.Enqueue(PinPadKey.Function2);
            Assert.IsFalse(BasePropertyTestUtils.IsActionThrowingException<InvalidOperationException>(() => {
                menu.ReadInput(null, " ", " ", null);
            }));
            queuedKeysPinPad.KeyCollection.Enqueue(PinPadKey.Function2);
            Assert.IsFalse(BasePropertyTestUtils.IsActionThrowingException<InvalidOperationException>(() => {
                menu.ReadInput(null, " ", null, " ");
            }));
            queuedKeysPinPad.KeyCollection.Enqueue(PinPadKey.Function3);
            Assert.IsFalse(BasePropertyTestUtils.IsActionThrowingException<InvalidOperationException>(() => {
                menu.ReadInput(null, null, " ", " ");
            }));
            queuedKeysPinPad.KeyCollection.Enqueue(PinPadKey.Function1);
            Assert.IsFalse(BasePropertyTestUtils.IsActionThrowingException<InvalidOperationException>(() => {
                menu.ReadInput(" ", " ", " ", null);
            }));
            queuedKeysPinPad.KeyCollection.Enqueue(PinPadKey.Function1);
            Assert.IsFalse(BasePropertyTestUtils.IsActionThrowingException<InvalidOperationException>(() => {
                menu.ReadInput(" ", " ", null, " ");
            }));
            queuedKeysPinPad.KeyCollection.Enqueue(PinPadKey.Function2);
            Assert.IsFalse(BasePropertyTestUtils.IsActionThrowingException<InvalidOperationException>(() => {
                menu.ReadInput(null, " ", " ", " ");
            }));
            queuedKeysPinPad.KeyCollection.Enqueue(PinPadKey.Function1);
            Assert.IsFalse(BasePropertyTestUtils.IsActionThrowingException<InvalidOperationException>(() => {
                menu.ReadInput(" ", " ", " ", " ");
            }));
        }
    }
}
