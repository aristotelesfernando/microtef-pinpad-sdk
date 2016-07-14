using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pinpad.Sdk.Model;
using Pinpad.Sdk.Model.Utilities;
using System;

namespace Pinpad.Sdk.Test.Utilities
{
    [TestClass]
    public class DataPickerKeysTest
    {
        [TestMethod]
        public void DataPickerKeys_should_not_return_null_when_no_parameters_are_passed()
        {
            // Arrange
            DataPickerKeys keys = null;
            // Act
            keys = new DataPickerKeys();
            // Assert
            Assert.IsNotNull(keys);
            Assert.AreEqual(PinpadKeyCode.Function3, keys.UpKey);
            Assert.AreEqual(PinpadKeyCode.Function4, keys.DownKey);
        }
        [TestMethod]
        public void DataPickerKeys_should_not_return_null_when_parameters_was_passed()
        {
            // Arrange
            DataPickerKeys keys = null;
            // Act
            keys = new DataPickerKeys { UpKey = PinpadKeyCode.Function3, DownKey = PinpadKeyCode.Function2 };
            // Assert
            Assert.IsNotNull(keys);
            Assert.AreEqual(PinpadKeyCode.Function3, keys.UpKey);
            Assert.AreEqual(PinpadKeyCode.Function2, keys.DownKey);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void DataPickerKeys_should_throws_exception_when_up_and_down_key_are_equal()
        {
            // Arrange
            DataPickerKeys keys = null;
            // Act
            keys = new DataPickerKeys { UpKey = PinpadKeyCode.Function1, DownKey = PinpadKeyCode.Function1 };
            // Assert
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void DataPickerKeys_should_throws_exception_when_undefied_key_is_set_as_up_key()
        {
            // Arrange
            DataPickerKeys keys = null;
            // Act
            keys = new DataPickerKeys { UpKey = PinpadKeyCode.Undefined };
            // Assert
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void DataPickerKeys_should_throws_exception_when_undefied_key_is_set_as_down_key()
        {
            // Arrange
            DataPickerKeys keys = null;
            // Act
            keys = new DataPickerKeys { DownKey = PinpadKeyCode.Undefined };
            // Assert
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void DataPickerKeys_should_throws_exception_when_cancel_key_is_set_as_up_key()
        {
            // Arrange
            DataPickerKeys keys = null;
            // Act
            keys = new DataPickerKeys { UpKey = PinpadKeyCode.Cancel };
            // Assert
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void DataPickerKeys_should_throws_exception_when_cancel_key_is_set_as_down_key()
        {
            // Arrange
            DataPickerKeys keys = null;
            // Act
            keys = new DataPickerKeys { DownKey = PinpadKeyCode.Cancel };
            // Assert
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void DataPickerKeys_should_throws_exception_when_return_key_is_set_as_up_key()
        {
            // Arrange
            DataPickerKeys keys = null;
            // Act
            keys = new DataPickerKeys { UpKey = PinpadKeyCode.Return };
            // Assert
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void DataPickerKeys_should_throws_exception_when_return_key_is_set_as_down_key()
        {
            // Arrange
            DataPickerKeys keys = null;
            // Act
            keys = new DataPickerKeys { DownKey = PinpadKeyCode.Return };
            // Assert
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void DataPickerKeys_should_throws_exception_when_backspace_key_is_set_as_up_key()
        {
            // Arrange
            DataPickerKeys keys = null;
            // Act
            keys = new DataPickerKeys { UpKey = PinpadKeyCode.Backspace };
            // Assert
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void DataPickerKeys_should_throws_exception_when_backspace_key_is_set_as_down_key()
        {
            // Arrange
            DataPickerKeys keys = null;
            // Act
            keys = new DataPickerKeys { DownKey = PinpadKeyCode.Backspace };
            // Assert
        }
    }
}
