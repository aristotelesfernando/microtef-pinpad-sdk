using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Pinpad.Sdk.Model;
using Pinpad.Sdk.Model.Utilities;
using Pinpad.Sdk.Utilities;
using System;

namespace Pinpad.Sdk.Test.Utilities
{
    [TestClass]
    public class DataPickerTest
    {
        [TestMethod]
        public void DataPicker_should_not_return_null()
        {
            // Arrange
            var keyboard = new Mock<IPinpadKeyboard>();
            var infos = new Mock<IPinpadInfos>();
            var display = new Mock<IPinpadDisplay>();
            // Act
            IDataPicker picker = new DataPicker(keyboard.Object, infos.Object, display.Object);
            // Assert
            Assert.IsNotNull(picker);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SetUpAndDownKey_should_throws_argumentexception_when_key_up_is_undefined()
        {
            // Arrange
            var keyboard = new Mock<IPinpadKeyboard>();
            var infos = new Mock<IPinpadInfos>();
            var display = new Mock<IPinpadDisplay>();
            IDataPicker picker = new DataPicker(keyboard.Object, infos.Object, display.Object);
            // Act
            picker.DataPickerKeys = new DataPickerKeys { Up = PinpadKeyCode.Undefined, Down = PinpadKeyCode.Function1 };
            // Assert
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SetUpAndDownKey_should_throws_argumentexception_when_key_down_is_undefined()
        {
            // Arrange
            var keyboard = new Mock<IPinpadKeyboard>();
            var infos = new Mock<IPinpadInfos>();
            var display = new Mock<IPinpadDisplay>();
            IDataPicker picker = new DataPicker(keyboard.Object, infos.Object, display.Object);
            // Act
            picker.DataPickerKeys = new DataPickerKeys { Up = PinpadKeyCode.Function1, Down = PinpadKeyCode.Undefined };
            // Assert
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetNumericValue_should_throws_argumentexception_when_label_is_empty()
        {
            // Arrange
            var keyboard = new Mock<IPinpadKeyboard>();
            var infos = new Mock<IPinpadInfos>();
            var display = new Mock<IPinpadDisplay>();
            IDataPicker picker = new DataPicker(keyboard.Object, infos.Object, display.Object);
            // Act
            picker.GetNumericValue("", 0, 1);
            // Assert
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetNumericValue_should_throws_argumentexception_when_label_is_null()
        {
            // Arrange
            var keyboard = new Mock<IPinpadKeyboard>();
            var infos = new Mock<IPinpadInfos>();
            var display = new Mock<IPinpadDisplay>();
            IDataPicker picker = new DataPicker(keyboard.Object, infos.Object, display.Object);
            // Act
            picker.GetNumericValue(null, 0, 1);
            // Assert
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetNumericValue_should_throws_argumentexception_when_minumunvalue_is_greather_than_maximunvalue()
        {
            // Arrange
            var keyboard = new Mock<IPinpadKeyboard>();
            var infos = new Mock<IPinpadInfos>();
            var display = new Mock<IPinpadDisplay>();
            IDataPicker picker = new DataPicker(keyboard.Object, infos.Object, display.Object);
            // Act
            picker.GetNumericValue("label", 1, 0);
            // Assert
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetNumericValue_should_()
        {
            // Arrange
            var keyboard = new Mock<IPinpadKeyboard>();
            var infos = new Mock<IPinpadInfos>();
            var display = new Mock<IPinpadDisplay>();
            IDataPicker picker = new DataPicker(keyboard.Object, infos.Object, display.Object);
            // Act
            picker.GetNumericValue("label", 1, 0);
            // Assert
        }
    }
}
