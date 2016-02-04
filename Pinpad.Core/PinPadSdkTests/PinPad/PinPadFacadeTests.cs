using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PinPadSDK.PinPad;

namespace PinPadSdkTests.PinPad {
    [TestClass]
    public class PinPadFacadeTests {
        [TestMethod]
        public void ValidatePinPadFacade() {
            PinPadConnectionMock connection = new PinPadConnectionMock();

            PinPadFacade facade = new PinPadFacade(connection);

            Assert.IsNotNull(facade.Communication);
            Assert.IsNotNull(facade.Display);
            Assert.IsNotNull(facade.Infos);
            Assert.IsNotNull(facade.Keyboard);
            Assert.IsNotNull(facade.Printer);
            Assert.IsNotNull(facade.Storage);
            Assert.IsNotNull(facade.Table);
        }
        [TestMethod]
        public void ValidatePinPadFacadeInvalidConnection() {
            try {
                PinPadFacade facade = new PinPadFacade(null);
                Assert.Fail("Did not complain about null PinPadConnection");
            }
            catch (InvalidOperationException) { }
        }
    }
}
