using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pinpad.Sdk.Model;
using Pinpad.Sdk.Model.Utilities;
using Pinpad.Sdk.Utilities;

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
    }
}
