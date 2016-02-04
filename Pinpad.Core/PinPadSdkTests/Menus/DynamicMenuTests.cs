using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PinPadSDK.PinPad;
using PinPadSDK.Menus;
using PinPadSDK.Enums;
using System.Collections.Generic;

namespace PinPadSdkTests.Menus {
    [TestClass]
    public class DynamicMenuTests {
        [TestInitialize]
        public void TestSetup() {
            CrossPlatformDesktop.CrossPlatformDesktopInitializer.Initialize_CrossPlatformDesktop();
            //CrossPlatformController.SendMailController = new SendMailControllerMock();
        }
        [TestMethod]
        public void ValidateDynamicMenu() {
            QueuedKeysPinPad queuedKeysPinPad = new QueuedKeysPinPad();
            PinPadFacade pinPad = new PinPadFacade(queuedKeysPinPad.PinPadConnection);
            DynamicMenu<string> menu = new DynamicMenu<string>(pinPad);

            List<string> itemCollection = new List<string>() { "option1", "option2" };
            queuedKeysPinPad.OnMultilineMessageReceived = (receivedMessage) => {
                Assert.AreEqual("header1", receivedMessage.LineCollection[0]);
                Assert.AreEqual("header2", receivedMessage.LineCollection[1]);
                Assert.IsTrue(itemCollection.Contains(receivedMessage.LineCollection[2]));
                Assert.IsTrue(String.IsNullOrWhiteSpace(receivedMessage.LineCollection[3]));
                Assert.IsTrue(String.IsNullOrWhiteSpace(receivedMessage.LineCollection[4]));
                Assert.IsTrue(String.IsNullOrWhiteSpace(receivedMessage.LineCollection[5]));
                Assert.IsTrue(String.IsNullOrWhiteSpace(receivedMessage.LineCollection[6]));
                Assert.IsTrue(String.IsNullOrWhiteSpace(receivedMessage.LineCollection[7]));
                Assert.AreEqual("< F1          F2 >", receivedMessage.LineCollection[8]);
            };
            Func<string, string> nextItemFunc = (current) =>{
                int index = itemCollection.IndexOf(current);
                index++;
                if(index >= itemCollection.Count){
                    index = 0;
                }
                return itemCollection[index];
            };
            Func<string, string> prevItemFunc = (current) =>{
                int index = itemCollection.IndexOf(current);
                index--;
                if(index < 0){
                    index = itemCollection.Count - 1;
                }
                return itemCollection[index];
            };
            Func<string, string[]> itemDisplayFunc = (current) => {
                return new string[] { current };
            };

            queuedKeysPinPad.KeyCollection.Enqueue(PinPadKey.Return);
            Assert.AreEqual("option1", menu.ReadInput(itemCollection[0], nextItemFunc, prevItemFunc, itemDisplayFunc, "header1", "header2"));

            queuedKeysPinPad.KeyCollection.Enqueue(PinPadKey.Function1);
            queuedKeysPinPad.KeyCollection.Enqueue(PinPadKey.Return);
            Assert.AreEqual("option2", menu.ReadInput(itemCollection[0], nextItemFunc, prevItemFunc, itemDisplayFunc, "header1", "header2"));

            queuedKeysPinPad.KeyCollection.Enqueue(PinPadKey.Function2);
            queuedKeysPinPad.KeyCollection.Enqueue(PinPadKey.Return);
            Assert.AreEqual("option2", menu.ReadInput(itemCollection[0], nextItemFunc, prevItemFunc, itemDisplayFunc, "header1", "header2"));

            queuedKeysPinPad.KeyCollection.Enqueue(PinPadKey.Function1);
            queuedKeysPinPad.KeyCollection.Enqueue(PinPadKey.Function1);
            queuedKeysPinPad.KeyCollection.Enqueue(PinPadKey.Return);
            Assert.AreEqual("option1", menu.ReadInput(itemCollection[0], nextItemFunc, prevItemFunc, itemDisplayFunc, "header1", "header2"));

            queuedKeysPinPad.KeyCollection.Enqueue(PinPadKey.Function2);
            queuedKeysPinPad.KeyCollection.Enqueue(PinPadKey.Function2);
            queuedKeysPinPad.KeyCollection.Enqueue(PinPadKey.Return);
            Assert.AreEqual("option1", menu.ReadInput(itemCollection[0], nextItemFunc, prevItemFunc, itemDisplayFunc, "header1", "header2"));

            queuedKeysPinPad.KeyCollection.Enqueue(PinPadKey.Cancel);
            Assert.IsNull(menu.ReadInput(itemCollection[0], nextItemFunc, prevItemFunc, itemDisplayFunc, "header1", "header2"));
        }
    }
}
