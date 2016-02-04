using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PinPadSDK.Menus;
using PinPadSDK.PinPad;
using PinPadSDK.Enums;
using PinPadSDK.Property;

namespace PinPadSdkTests.Menus {
    [TestClass]
    public class BooleanInputTests {
        [TestInitialize]
        public void TestSetup() {
            CrossPlatformDesktop.CrossPlatformDesktopInitializer.Initialize_CrossPlatformDesktop();
            //CrossPlatformController.SendMailController = new SendMailControllerMock();
        }
        [TestMethod]
        public void ValidateBooleanInput() {
            SimpleMessage message = new SimpleMessage("1234567890123456", "1234567890123456");
            QueuedKeysPinPad queuedKeysPinPad = new QueuedKeysPinPad();
            queuedKeysPinPad.OnSimpleMessageReceived = (receivedMessage) =>{
                Assert.AreEqual(message.FullValue, receivedMessage.FullValue);
            };
            PinPadFacade pinPad = new PinPadFacade(queuedKeysPinPad.PinPadConnection);
            BooleanInput menu = new BooleanInput(pinPad);

            queuedKeysPinPad.KeyCollection.Enqueue(PinPadKey.Return);
            Assert.IsTrue(menu.ReadInput(message));

            queuedKeysPinPad.KeyCollection.Enqueue(PinPadKey.Cancel);
            Assert.IsFalse(menu.ReadInput(message));

            foreach (PinPadKey key in new PinPadKey[] { PinPadKey.Backspace, PinPadKey.Decimal0, PinPadKey.Decimal1, PinPadKey.Decimal2, PinPadKey.Decimal3, PinPadKey.Decimal4, PinPadKey.Decimal5, PinPadKey.Decimal6, PinPadKey.Decimal7, PinPadKey.Decimal8, PinPadKey.Decimal9, PinPadKey.Function1, PinPadKey.Function2, PinPadKey.Function3, PinPadKey.Function4, PinPadKey.Return }) {
                queuedKeysPinPad.KeyCollection.Enqueue(key);
            }
            Assert.IsTrue(menu.ReadInput(message));

            foreach (PinPadKey key in new PinPadKey[] { PinPadKey.Backspace, PinPadKey.Decimal0, PinPadKey.Decimal1, PinPadKey.Decimal2, PinPadKey.Decimal3, PinPadKey.Decimal4, PinPadKey.Decimal5, PinPadKey.Decimal6, PinPadKey.Decimal7, PinPadKey.Decimal8, PinPadKey.Decimal9, PinPadKey.Function1, PinPadKey.Function2, PinPadKey.Function3, PinPadKey.Function4, PinPadKey.Cancel }) {
                queuedKeysPinPad.KeyCollection.Enqueue(key);
            }
            Assert.IsFalse(menu.ReadInput(message));
        }
    }
}
