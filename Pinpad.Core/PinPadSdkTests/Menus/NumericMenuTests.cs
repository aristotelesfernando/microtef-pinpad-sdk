using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PinPadSDK.PinPad;
using PinPadSDK.Menus;
using PinPadSDK.Enums;

namespace PinPadSdkTests.Menus {
    [TestClass]
    public class NumericMenuTests {
        [TestInitialize]
        public void TestSetup() {
            CrossPlatformDesktop.CrossPlatformDesktopInitializer.Initialize_CrossPlatformDesktop();
            //CrossPlatformController.SendMailController = new SendMailControllerMock();
        }
        [TestMethod]
        public void ValidateNumericMenu() {
            QueuedKeysPinPad queuedKeysPinPad = new QueuedKeysPinPad();
            queuedKeysPinPad.SupportStone = true;
            PinPadFacade pinPad = new PinPadFacade(queuedKeysPinPad.PinPadConnection);
            NumericMenu menu = new NumericMenu(pinPad);
            string[] options = new string[10] { "option1", "option2", "option3", "option4", "option5", "option6", "option7", "option8", "option9", "option0" };
            queuedKeysPinPad.OnMultilineMessageReceived = (message) => {
                Assert.AreEqual("header1", message.LineCollection[0]);
                Assert.AreEqual("header2", message.LineCollection[1]);
                Assert.AreEqual("1: " + options[0], message.LineCollection[2]);
                Assert.AreEqual("2: " + options[1], message.LineCollection[3]);
                Assert.AreEqual("3: " + options[2], message.LineCollection[4]);
                Assert.AreEqual("4: " + options[3], message.LineCollection[5]);
                Assert.AreEqual("5: " + options[4], message.LineCollection[6]);
                Assert.AreEqual("6: " + options[5], message.LineCollection[7]);
                Assert.AreEqual("\\/ F1        F2 /\\", message.LineCollection[8]);
            };

            queuedKeysPinPad.KeyCollection.Enqueue(PinPadKey.Decimal1);
            Assert.AreEqual("option1", menu.ReadInput(options, "header1", "header2"));

            queuedKeysPinPad.KeyCollection.Enqueue(PinPadKey.Decimal2);
            Assert.AreEqual("option2", menu.ReadInput(options, "header1", "header2"));

            queuedKeysPinPad.KeyCollection.Enqueue(PinPadKey.Decimal3);
            Assert.AreEqual("option3", menu.ReadInput(options, "header1", "header2"));

            queuedKeysPinPad.KeyCollection.Enqueue(PinPadKey.Decimal4);
            Assert.AreEqual("option4", menu.ReadInput(options, "header1", "header2"));

            queuedKeysPinPad.KeyCollection.Enqueue(PinPadKey.Decimal5);
            Assert.AreEqual("option5", menu.ReadInput(options, "header1", "header2"));

            queuedKeysPinPad.KeyCollection.Enqueue(PinPadKey.Decimal6);
            Assert.AreEqual("option6", menu.ReadInput(options, "header1", "header2"));

            queuedKeysPinPad.KeyCollection.Enqueue(PinPadKey.Decimal7);
            Assert.AreEqual("option7", menu.ReadInput(options, "header1", "header2"));

            queuedKeysPinPad.KeyCollection.Enqueue(PinPadKey.Decimal8);
            Assert.AreEqual("option8", menu.ReadInput(options, "header1", "header2"));

            queuedKeysPinPad.KeyCollection.Enqueue(PinPadKey.Decimal9);
            Assert.AreEqual("option9", menu.ReadInput(options, "header1", "header2"));

            queuedKeysPinPad.KeyCollection.Enqueue(PinPadKey.Decimal0);
            Assert.AreEqual("option0", menu.ReadInput(options, "header1", "header2"));

            queuedKeysPinPad.KeyCollection.Enqueue(PinPadKey.Cancel);
            Assert.IsNull(menu.ReadInput(options, "header1", "header2"));

            Assert.IsTrue(BasePropertyTestUtils.IsActionThrowingException<ArgumentOutOfRangeException>(() => {
                string[] fewOptions = new string[1] { "option1" };
                menu.ReadInput(fewOptions, "header1", "header2");
            }), "Permitiu menu de apenas 1 item");

            Assert.IsTrue(BasePropertyTestUtils.IsActionThrowingException<ArgumentOutOfRangeException>(() => {
                string[] fewOptions = new string[11] { "option1", "option2", "option3", "option4", "option5", "option6", "option7", "option8", "option9", "option10", "option11" };
                menu.ReadInput(fewOptions, "header1", "header2");
            }), "Permitiu menu de apenas 1 item");
        }
        [TestMethod]
        public void ValidateNumericMenuWithFewOptions() {
            QueuedKeysPinPad queuedKeysPinPad = new QueuedKeysPinPad();
            queuedKeysPinPad.SupportStone = true;
            PinPadFacade pinPad = new PinPadFacade(queuedKeysPinPad.PinPadConnection);
            NumericMenu menu = new NumericMenu(pinPad);
            string[] options = new string[7] { "option1", "option2", "option3", "option4", "option5", "option6", "option7" };
            queuedKeysPinPad.OnMultilineMessageReceived = (message) => {
                Assert.AreEqual("header1", message.LineCollection[0]);
                Assert.AreEqual("header2", message.LineCollection[1]);
                Assert.AreEqual("1: " + options[0], message.LineCollection[2]);
                Assert.AreEqual("2: " + options[1], message.LineCollection[3]);
                Assert.AreEqual("3: " + options[2], message.LineCollection[4]);
                Assert.AreEqual("4: " + options[3], message.LineCollection[5]);
                Assert.AreEqual("5: " + options[4], message.LineCollection[6]);
                Assert.AreEqual("6: " + options[5], message.LineCollection[7]);
                Assert.AreEqual("7: " + options[6], message.LineCollection[8]);
                Assert.AreEqual(9, message.LineCollection.Count);
            };

            queuedKeysPinPad.KeyCollection.Enqueue(PinPadKey.Decimal1);
            Assert.AreEqual("option1", menu.ReadInput(options, "header1", "header2"));

            queuedKeysPinPad.KeyCollection.Enqueue(PinPadKey.Decimal2);
            Assert.AreEqual("option2", menu.ReadInput(options, "header1", "header2"));

            queuedKeysPinPad.KeyCollection.Enqueue(PinPadKey.Decimal3);
            Assert.AreEqual("option3", menu.ReadInput(options, "header1", "header2"));

            queuedKeysPinPad.KeyCollection.Enqueue(PinPadKey.Decimal4);
            Assert.AreEqual("option4", menu.ReadInput(options, "header1", "header2"));

            queuedKeysPinPad.KeyCollection.Enqueue(PinPadKey.Decimal5);
            Assert.AreEqual("option5", menu.ReadInput(options, "header1", "header2"));

            queuedKeysPinPad.KeyCollection.Enqueue(PinPadKey.Decimal6);
            Assert.AreEqual("option6", menu.ReadInput(options, "header1", "header2"));

            queuedKeysPinPad.KeyCollection.Enqueue(PinPadKey.Decimal7);
            Assert.AreEqual("option7", menu.ReadInput(options, "header1", "header2"));

            queuedKeysPinPad.KeyCollection.Enqueue(PinPadKey.Decimal8);
            queuedKeysPinPad.KeyCollection.Enqueue(PinPadKey.Decimal1);
            Assert.AreEqual("option1", menu.ReadInput(options, "header1", "header2"));

            queuedKeysPinPad.KeyCollection.Enqueue(PinPadKey.Decimal9);
            queuedKeysPinPad.KeyCollection.Enqueue(PinPadKey.Decimal1);
            Assert.AreEqual("option1", menu.ReadInput(options, "header1", "header2"));

            queuedKeysPinPad.KeyCollection.Enqueue(PinPadKey.Decimal0);
            queuedKeysPinPad.KeyCollection.Enqueue(PinPadKey.Decimal1);
            Assert.AreEqual("option1", menu.ReadInput(options, "header1", "header2"));

            queuedKeysPinPad.KeyCollection.Enqueue(PinPadKey.Function1);
            queuedKeysPinPad.KeyCollection.Enqueue(PinPadKey.Function2);
            queuedKeysPinPad.KeyCollection.Enqueue(PinPadKey.Function3);
            queuedKeysPinPad.KeyCollection.Enqueue(PinPadKey.Function4);
            queuedKeysPinPad.KeyCollection.Enqueue(PinPadKey.Decimal1);
            Assert.AreEqual("option1", menu.ReadInput(options, "header1", "header2"));

            queuedKeysPinPad.KeyCollection.Enqueue(PinPadKey.Cancel);
            Assert.IsNull(menu.ReadInput(options, "header1", "header2"));
        }
        [TestMethod]
        public void ValidateNumericMenuWithFewerOptions() {
            QueuedKeysPinPad queuedKeysPinPad = new QueuedKeysPinPad();
            queuedKeysPinPad.SupportStone = true;
            PinPadFacade pinPad = new PinPadFacade(queuedKeysPinPad.PinPadConnection);
            NumericMenu menu = new NumericMenu(pinPad);
            string[] options = new string[5] { "option1", "option2", "option3", "option4", "option5" };
            queuedKeysPinPad.OnMultilineMessageReceived = (message) => {
                Assert.AreEqual("header1", message.LineCollection[0]);
                Assert.AreEqual("header2", message.LineCollection[1]);
                Assert.AreEqual("1: " + options[0], message.LineCollection[2]);
                Assert.AreEqual("2: " + options[1], message.LineCollection[3]);
                Assert.AreEqual("3: " + options[2], message.LineCollection[4]);
                Assert.AreEqual("4: " + options[3], message.LineCollection[5]);
                Assert.AreEqual("5: " + options[4], message.LineCollection[6]);
                Assert.AreEqual(7, message.LineCollection.Count);
            };

            queuedKeysPinPad.KeyCollection.Enqueue(PinPadKey.Decimal1);
            Assert.AreEqual("option1", menu.ReadInput(options, "header1", "header2"));

            queuedKeysPinPad.KeyCollection.Enqueue(PinPadKey.Decimal2);
            Assert.AreEqual("option2", menu.ReadInput(options, "header1", "header2"));

            queuedKeysPinPad.KeyCollection.Enqueue(PinPadKey.Decimal3);
            Assert.AreEqual("option3", menu.ReadInput(options, "header1", "header2"));

            queuedKeysPinPad.KeyCollection.Enqueue(PinPadKey.Decimal4);
            Assert.AreEqual("option4", menu.ReadInput(options, "header1", "header2"));

            queuedKeysPinPad.KeyCollection.Enqueue(PinPadKey.Decimal5);
            Assert.AreEqual("option5", menu.ReadInput(options, "header1", "header2"));

            queuedKeysPinPad.KeyCollection.Enqueue(PinPadKey.Decimal6);
            queuedKeysPinPad.KeyCollection.Enqueue(PinPadKey.Decimal1);
            Assert.AreEqual("option1", menu.ReadInput(options, "header1", "header2"));

            queuedKeysPinPad.KeyCollection.Enqueue(PinPadKey.Decimal7);
            queuedKeysPinPad.KeyCollection.Enqueue(PinPadKey.Decimal1);
            Assert.AreEqual("option1", menu.ReadInput(options, "header1", "header2"));

            queuedKeysPinPad.KeyCollection.Enqueue(PinPadKey.Decimal8);
            queuedKeysPinPad.KeyCollection.Enqueue(PinPadKey.Decimal1);
            Assert.AreEqual("option1", menu.ReadInput(options, "header1", "header2"));

            queuedKeysPinPad.KeyCollection.Enqueue(PinPadKey.Decimal9);
            queuedKeysPinPad.KeyCollection.Enqueue(PinPadKey.Decimal1);
            Assert.AreEqual("option1", menu.ReadInput(options, "header1", "header2"));

            queuedKeysPinPad.KeyCollection.Enqueue(PinPadKey.Decimal0);
            queuedKeysPinPad.KeyCollection.Enqueue(PinPadKey.Decimal1);
            Assert.AreEqual("option1", menu.ReadInput(options, "header1", "header2"));

            queuedKeysPinPad.KeyCollection.Enqueue(PinPadKey.Function1);
            queuedKeysPinPad.KeyCollection.Enqueue(PinPadKey.Function2);
            queuedKeysPinPad.KeyCollection.Enqueue(PinPadKey.Function3);
            queuedKeysPinPad.KeyCollection.Enqueue(PinPadKey.Function4);
            queuedKeysPinPad.KeyCollection.Enqueue(PinPadKey.Decimal1);
            Assert.AreEqual("option1", menu.ReadInput(options, "header1", "header2"));

            queuedKeysPinPad.KeyCollection.Enqueue(PinPadKey.Cancel);
            Assert.IsNull(menu.ReadInput(options, "header1", "header2"));
        }
        [TestMethod]
        public void ValidateNumericMenuPositions() {
            QueuedKeysPinPad queuedKeysPinPad = new QueuedKeysPinPad();
            queuedKeysPinPad.SupportStone = true;
            PinPadFacade pinPad = new PinPadFacade(queuedKeysPinPad.PinPadConnection);
            NumericMenu menu = new NumericMenu(pinPad);
            string[] options = new string[10] { "option1", "option2", "option3", "option4", "option5", "option6", "option7", "option8", "option9", "option0" };
            string[] fullMessage = new string[] { "header1", "header2", "1: " + options[0], "2: " + options[1], "3: " + options[2], "4: " + options[3], "5: " + options[4], "6: " + options[5], "7: " + options[6], "8: " + options[7], "9: " + options[8], "0: " + options[9] };
            int[] messageIndexScript = new int[] { 0, 1, 2, 3, 4, 4, 3, 2, 1, 0, 0 };
            int messageIndex = 0;
            queuedKeysPinPad.OnMultilineMessageReceived = (message) => {
                Assert.IsTrue(messageIndex < messageIndexScript.Length);
                Assert.AreEqual(fullMessage[messageIndexScript[messageIndex]], message.LineCollection[0]);
                Assert.AreEqual(fullMessage[messageIndexScript[messageIndex] + 1], message.LineCollection[1]);
                Assert.AreEqual(fullMessage[messageIndexScript[messageIndex] + 2], message.LineCollection[2]);
                Assert.AreEqual(fullMessage[messageIndexScript[messageIndex] + 3], message.LineCollection[3]);
                Assert.AreEqual(fullMessage[messageIndexScript[messageIndex] + 4], message.LineCollection[4]);
                Assert.AreEqual(fullMessage[messageIndexScript[messageIndex] + 5], message.LineCollection[5]);
                Assert.AreEqual(fullMessage[messageIndexScript[messageIndex] + 6], message.LineCollection[6]);
                Assert.AreEqual(fullMessage[messageIndexScript[messageIndex] + 7], message.LineCollection[7]);
                Assert.AreEqual("\\/ F1        F2 /\\", message.LineCollection[8]);
                messageIndex++;
            };

            queuedKeysPinPad.KeyCollection.Enqueue(PinPadKey.Function1);
            queuedKeysPinPad.KeyCollection.Enqueue(PinPadKey.Function3);
            queuedKeysPinPad.KeyCollection.Enqueue(PinPadKey.Function1);
            queuedKeysPinPad.KeyCollection.Enqueue(PinPadKey.Function3);
            queuedKeysPinPad.KeyCollection.Enqueue(PinPadKey.Function1);

            queuedKeysPinPad.KeyCollection.Enqueue(PinPadKey.Function2);
            queuedKeysPinPad.KeyCollection.Enqueue(PinPadKey.Function4);
            queuedKeysPinPad.KeyCollection.Enqueue(PinPadKey.Function2);
            queuedKeysPinPad.KeyCollection.Enqueue(PinPadKey.Function4);
            queuedKeysPinPad.KeyCollection.Enqueue(PinPadKey.Function2);

            queuedKeysPinPad.KeyCollection.Enqueue(PinPadKey.Decimal1);
            menu.ReadInput(options, "header1", "header2");
        }
        [TestMethod]
        public void ValidateNumericMenuWithoutExtendedKeySupport() {
            QueuedKeysPinPad queuedKeysPinPad = new QueuedKeysPinPad();
            PinPadFacade pinPad = new PinPadFacade(queuedKeysPinPad.PinPadConnection);
            Assert.IsTrue(BasePropertyTestUtils.IsActionThrowingException<InvalidOperationException>(() => {
                new NumericMenu(pinPad);
            }));
        }
    }
}
