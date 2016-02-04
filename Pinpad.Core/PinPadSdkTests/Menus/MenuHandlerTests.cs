using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PinPadSDK.PinPad;
using PinPadSDK.Menus;
using PinPadSDK.Enums;

namespace PinPadSdkTests.Menus {
    [TestClass]
    public class MenuHandlerTests {
        [TestInitialize]
        public void TestSetup() {
            CrossPlatformDesktop.CrossPlatformDesktopInitializer.Initialize_CrossPlatformDesktop();
            //CrossPlatformController.SendMailController = new SendMailControllerMock();
        }
        [TestMethod]
        public void ValidateMenuHandler() {
            QueuedKeysPinPad queuedKeysPinPad = new QueuedKeysPinPad();
            queuedKeysPinPad.OnSimpleMessageReceived = (receivedMessage) => {
                Assert.AreEqual("< F1        F2 >", receivedMessage.SecondLine);
            };
            PinPadFacade pinPad = new PinPadFacade(queuedKeysPinPad.PinPadConnection);
            MenuHandler menu = new MenuHandler(pinPad);

            queuedKeysPinPad.KeyCollection.Enqueue(PinPadKey.Return);
            Assert.AreEqual("option1", menu.ReadInput("option1", "option2"));

            queuedKeysPinPad.KeyCollection.Enqueue(PinPadKey.Function1);
            queuedKeysPinPad.KeyCollection.Enqueue(PinPadKey.Return);
            Assert.AreEqual("option2", menu.ReadInput("option1", "option2"));

            queuedKeysPinPad.KeyCollection.Enqueue(PinPadKey.Function2);
            queuedKeysPinPad.KeyCollection.Enqueue(PinPadKey.Return);
            Assert.AreEqual("option2", menu.ReadInput("option1", "option2"));

            queuedKeysPinPad.KeyCollection.Enqueue(PinPadKey.Function1);
            queuedKeysPinPad.KeyCollection.Enqueue(PinPadKey.Function1);
            queuedKeysPinPad.KeyCollection.Enqueue(PinPadKey.Return);
            Assert.AreEqual("option1", menu.ReadInput("option1", "option2"));

            queuedKeysPinPad.KeyCollection.Enqueue(PinPadKey.Function2);
            queuedKeysPinPad.KeyCollection.Enqueue(PinPadKey.Function2);
            queuedKeysPinPad.KeyCollection.Enqueue(PinPadKey.Return);
            Assert.AreEqual("option1", menu.ReadInput("option1", "option2"));

            queuedKeysPinPad.KeyCollection.Enqueue(PinPadKey.Cancel);
            Assert.IsNull(menu.ReadInput("option1", "option2"));

            Assert.IsTrue(BasePropertyTestUtils.IsActionThrowingException<ArgumentOutOfRangeException>(() => {
                menu.ReadInput("option1");
            }), "Permitiu menu de apenas 1 item");
        }
    }
}
