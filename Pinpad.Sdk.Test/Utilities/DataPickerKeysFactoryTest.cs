﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Pinpad.Sdk.Model;
using Pinpad.Sdk.Model.Utilities;
using Pinpad.Sdk.Utilities;

namespace Pinpad.Sdk.Test.Utilities
{
    [TestClass]
    public class DataPickerKeysFactoryTest
    {
        [TestMethod]
        public void Create_should_return_gertec_keys_when_null_infos_was_passed()
        {
            // Arrange
            var mock = new Mock<IPinpadInfos>();
            IPinpadInfos infos = mock.Object;
            // Act
            DataPickerKeys keys = DataPickerKeysFactory.Create(infos);
            // Assert
            Assert.IsNotNull(keys);
            Assert.AreEqual(PinpadKeyCode.Function3, keys.Up);
            Assert.AreEqual(PinpadKeyCode.Function4, keys.Down);
        }
        [TestMethod]
        public void Create_should_return_gertec_keys_when_gertec_infos_was_passed()
        {
            // Arrange
            var mock = new Mock<IPinpadInfos>();
            mock.SetupGet(x => x.ManufacturerName).Returns("GERTEC");
            mock.SetupGet(x => x.Model).Returns("PPC920");
            IPinpadInfos infos = mock.Object;
            // Act
            DataPickerKeys keys = DataPickerKeysFactory.Create(infos);
            // Assert
            Assert.IsNotNull(keys);
            Assert.AreEqual(PinpadKeyCode.Function3, keys.Up);
            Assert.AreEqual(PinpadKeyCode.Function4, keys.Down);
        }
        [TestMethod]
        public void Create_should_return_ingenico_keys_when_ingenco_infos_was_passed()
        {
            // Arrange
            var mock = new Mock<IPinpadInfos>();
            mock.SetupGet(x => x.ManufacturerName).Returns("INGENICO");
            mock.SetupGet(x => x.Model).Returns("IPP320");
            IPinpadInfos infos = mock.Object;
            // Act
            DataPickerKeys keys = DataPickerKeysFactory.Create(infos);
            // Assert
            Assert.IsNotNull(keys);
            Assert.AreEqual(PinpadKeyCode.Function3, keys.Up);
            Assert.AreEqual(PinpadKeyCode.Function2, keys.Down);
        }
        [TestMethod]
        public void Create_should_return_verifone_keys_when_verifone_infos_was_passed()
        {
            // Arrange
            var mock = new Mock<IPinpadInfos>();
            mock.SetupGet(x => x.ManufacturerName).Returns("VERIFONE");
            mock.SetupGet(x => x.Model).Returns("VX820");
            IPinpadInfos infos = mock.Object;
            // Act
            DataPickerKeys keys = DataPickerKeysFactory.Create(infos);
            // Assert
            Assert.IsNotNull(keys);
            Assert.AreEqual(PinpadKeyCode.Function1, keys.Up);
            Assert.AreEqual(PinpadKeyCode.Function3, keys.Down);
        }

        [TestMethod]
        public void Create_should_not_return_null()
        {
            // Arrange
            DataPickerKeys keys = null;
            // Act
            keys = new DataPickerKeys();
            // Assert
            Assert.IsNotNull(keys);
        }
    }
}
