using NUnit.Framework;
using Moq;
using Pinpad.Sdk.Model;
using Pinpad.Sdk.Model.Utilities;
using Pinpad.Sdk.Utilities;

namespace Pinpad.Sdk.Test.Utilities
{
    [TestFixture]
    public class DataPickerKeysFactoryTest
    {
        [Test]
        public void DataPickerKeysFactory_GetUpAndDownKeys_ShouldReturnGertecKeys_WhenNullInfosArePassed ()
        {
            // Arrange
            IPinpadInfos infos = new Mock<IPinpadInfos>().Object;

            // Act
            DataPickerKeys keys = infos.GetUpAndDownKeys();

            // Assert
            Assert.IsNotNull(keys);
            Assert.AreEqual(PinpadKeyCode.Function3, keys.UpKey);
            Assert.AreEqual(PinpadKeyCode.Function4, keys.DownKey);
        }
        [Test]
        public void DataPickerKeysFactory_GetUpAndDownKeys_ShouldReturnGertecKeysWhenGertecInfosArePassed ()
        {
            // Arrange
            var mock = new Mock<IPinpadInfos>();
            mock.SetupGet(x => x.ManufacturerName).Returns("GERTEC");
            mock.SetupGet(x => x.Model).Returns("PPC920");
            IPinpadInfos infos = mock.Object;

            // Act
            DataPickerKeys keys = infos.GetUpAndDownKeys();

            // Assert
            Assert.IsNotNull(keys);
            Assert.AreEqual(PinpadKeyCode.Function3, keys.UpKey);
            Assert.AreEqual(PinpadKeyCode.Function4, keys.DownKey);
        }
        [Test]
        public void DataPickerKeysFactory_GetUpAndDownKeys_ShouldReturnIngenicoKeys_WhenIngenicoInfosArePassed ()
        {
            // Arrange
            var mock = new Mock<IPinpadInfos>();
            mock.SetupGet(x => x.ManufacturerName).Returns("INGENICO");
            mock.SetupGet(x => x.Model).Returns("IPP320");
            IPinpadInfos infos = mock.Object;

            // Act
            DataPickerKeys keys = infos.GetUpAndDownKeys();

            // Assert
            Assert.IsNotNull(keys);
            Assert.AreEqual(PinpadKeyCode.Function3, keys.UpKey);
            Assert.AreEqual(PinpadKeyCode.Function2, keys.DownKey);
        }
        [Test]
        public void DataPickerKeysFactory_GetUpAndDownKeys_ShouldReturnVerifoneKeys_WhenVerifoneInfosArePassed ()
        {
            // Arrange
            var mock = new Mock<IPinpadInfos>();
            mock.SetupGet(x => x.ManufacturerName).Returns("VERIFONE");
            mock.SetupGet(x => x.Model).Returns("VX820");
            IPinpadInfos infos = mock.Object;

            // Act
            DataPickerKeys keys = infos.GetUpAndDownKeys();

            // Assert
            Assert.IsNotNull(keys);
            Assert.AreEqual(PinpadKeyCode.Function1, keys.UpKey);
            Assert.AreEqual(PinpadKeyCode.Function3, keys.DownKey);
        }
    }
}
