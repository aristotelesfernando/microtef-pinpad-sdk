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
            Assert.AreEqual(PinpadKeyCode.Function3, keys.Up);
            Assert.AreEqual(PinpadKeyCode.Function4, keys.Down);
        }
        [TestMethod]
        public void DataPickerKeys_should_not_return_null_when_parameters_was_passed()
        {
            // Arrange
            DataPickerKeys keys = null;
            // Act
            keys = new DataPickerKeys { Up = PinpadKeyCode.Function3, Down = PinpadKeyCode.Function2 };
            // Assert
            Assert.IsNotNull(keys);
            Assert.AreEqual(PinpadKeyCode.Function3, keys.Up);
            Assert.AreEqual(PinpadKeyCode.Function2, keys.Down);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void DataPickerKeys_should_throws_exception_when_up_and_down_key_are_equal()
        {
            // Arrange
            DataPickerKeys keys = null;
            // Act
            keys = new DataPickerKeys { Up = PinpadKeyCode.Function1, Down = PinpadKeyCode.Function1 };
            // Assert
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void DataPickerKeys_should_throws_exception_when_undefied_key_is_set_as_up_key()
        {
            // Arrange
            DataPickerKeys keys = null;
            // Act
            keys = new DataPickerKeys { Up = PinpadKeyCode.Undefined };
            // Assert
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void DataPickerKeys_should_throws_exception_when_undefied_key_is_set_as_down_key()
        {
            // Arrange
            DataPickerKeys keys = null;
            // Act
            keys = new DataPickerKeys { Down = PinpadKeyCode.Undefined };
            // Assert
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void DataPickerKeys_should_throws_exception_when_cancel_key_is_set_as_up_key()
        {
            // Arrange
            DataPickerKeys keys = null;
            // Act
            keys = new DataPickerKeys { Up = PinpadKeyCode.Cancel };
            // Assert
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void DataPickerKeys_should_throws_exception_when_cancel_key_is_set_as_down_key()
        {
            // Arrange
            DataPickerKeys keys = null;
            // Act
            keys = new DataPickerKeys { Down = PinpadKeyCode.Cancel };
            // Assert
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void DataPickerKeys_should_throws_exception_when_return_key_is_set_as_up_key()
        {
            // Arrange
            DataPickerKeys keys = null;
            // Act
            keys = new DataPickerKeys { Up = PinpadKeyCode.Return };
            // Assert
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void DataPickerKeys_should_throws_exception_when_return_key_is_set_as_down_key()
        {
            // Arrange
            DataPickerKeys keys = null;
            // Act
            keys = new DataPickerKeys { Down = PinpadKeyCode.Return };
            // Assert
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void DataPickerKeys_should_throws_exception_when_backspace_key_is_set_as_up_key()
        {
            // Arrange
            DataPickerKeys keys = null;
            // Act
            keys = new DataPickerKeys { Up = PinpadKeyCode.Backspace };
            // Assert
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void DataPickerKeys_should_throws_exception_when_backspace_key_is_set_as_down_key()
        {
            // Arrange
            DataPickerKeys keys = null;
            // Act
            keys = new DataPickerKeys { Down = PinpadKeyCode.Backspace };
            // Assert
        }
    }
}
