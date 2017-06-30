using MicroPos.CrossPlatform;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pinpad.Sdk.Model.Pinpad;

namespace Pinpad.Sdk.Test.CommunicationPinpad
{
    [TestClass]
    public class PinpadCommunicationTest
    {
        [TestInitialize]
        public void Setup()
        {
            MicroPos.Platform.Desktop.DesktopInitializer.Initialize();
        }

        [TestMethod]
        public void PinpadCommunication_SetTimemout_ShouldReturnEquals()
        {
            IPinpadCommunication comm = new PinpadCommunicationMock();

            comm.SetTimeout(4000, 4000);

            Assert.AreEqual(comm.Connection.WriteTimeout, 4000);
            Assert.AreEqual(comm.Connection.ReadTimeout, 4000);
        }
    }
}
