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
            picker.DataPickerKeys = new DataPickerKeys { UpKey = PinpadKeyCode.Undefined, DownKey = PinpadKeyCode.Function1 };
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
            picker.DataPickerKeys = new DataPickerKeys { UpKey = PinpadKeyCode.Function1, DownKey = PinpadKeyCode.Undefined };
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
            picker.GetNumericValue(string.Empty, 0, 1);
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
        public void GetNumericValue_should_return_a_value_when_return_was_pressed()
        {
            // Arrange
            var display = new Mock<IPinpadDisplay>();
            display.Setup(x => x.ShowMessage("label", string.Empty, DisplayPaddingType.Center)).Returns(true);
            var keyboard = new Mock<IPinpadKeyboard>();
            keyboard.Setup(x => x.GetKey()).Returns(PinpadKeyCode.Return);
            var infos = new Mock<IPinpadInfos>();
            IDataPicker picker = new DataPicker(keyboard.Object, infos.Object, display.Object);
            // Act
            short? value = picker.GetNumericValue("label", 0, 1);
            // Assert
            Assert.IsNotNull(value);
            Assert.IsTrue(value.HasValue);
            Assert.AreEqual(0, value.Value);
        }
        [TestMethod]
        public void GetNumericValue_should_return_a_null_when_cancel_was_pressed()
        {
            // Arrange
            var display = new Mock<IPinpadDisplay>();
            display.Setup(x => x.ShowMessage("label", string.Empty, DisplayPaddingType.Center)).Returns(true);
            var keyboard = new Mock<IPinpadKeyboard>();
            keyboard.Setup(x => x.GetKey()).Returns(PinpadKeyCode.Cancel);
            var infos = new Mock<IPinpadInfos>();
            IDataPicker picker = new DataPicker(keyboard.Object, infos.Object, display.Object);
            // Act
            short? value = picker.GetNumericValue("label", 0, 1);
            // Assert
            Assert.IsNull(value);
            Assert.IsFalse(value.HasValue);
        }
        [TestMethod]
        public void GetNumericValue_should_return_a_value_when_return_was_pressed_and_negative_number_was_passed()
        {
            // Arrange
            var display = new Mock<IPinpadDisplay>();
            display.Setup(x => x.ShowMessage("label", string.Empty, DisplayPaddingType.Center)).Returns(true);
            var keyboard = new Mock<IPinpadKeyboard>();
            keyboard.Setup(x => x.GetKey()).Returns(PinpadKeyCode.Return);
            var infos = new Mock<IPinpadInfos>();
            IDataPicker picker = new DataPicker(keyboard.Object, infos.Object, display.Object);
            // Act
            short? value = picker.GetNumericValue("label", -1, 1);
            // Assert
            Assert.IsNotNull(value);
            Assert.IsTrue(value.HasValue);
            Assert.AreEqual(-1, value.Value);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetValueInOptions_short_should_throws_exception_when_label_is_empty()
        {
            // Arrange
            var display = new Mock<IPinpadDisplay>();
            display.Setup(x => x.ShowMessage("label", string.Empty, DisplayPaddingType.Center)).Returns(true);
            var keyboard = new Mock<IPinpadKeyboard>();
            keyboard.Setup(x => x.GetKey()).Returns(PinpadKeyCode.Cancel);
            var infos = new Mock<IPinpadInfos>();
            IDataPicker picker = new DataPicker(keyboard.Object, infos.Object, display.Object);
            // Act
            short? value = picker.GetValueInOptions(string.Empty, 1, 2, 3, 4, 5);
            // Assert
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetValueInOptions_string_should_throws_exception_when_label_is_empty()
        {
            // Arrange
            var display = new Mock<IPinpadDisplay>();
            display.Setup(x => x.ShowMessage("label", string.Empty, DisplayPaddingType.Center)).Returns(true);
            var keyboard = new Mock<IPinpadKeyboard>();
            keyboard.Setup(x => x.GetKey()).Returns(PinpadKeyCode.Cancel);
            var infos = new Mock<IPinpadInfos>();
            IDataPicker picker = new DataPicker(keyboard.Object, infos.Object, display.Object);
            // Act
            string value = picker.GetValueInOptions(string.Empty, string.Empty, string.Empty);
            // Assert
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetValueInOptions_short_should_throws_exception_when_label_is_null()
        {
            // Arrange
            var display = new Mock<IPinpadDisplay>();
            display.Setup(x => x.ShowMessage("label", string.Empty, DisplayPaddingType.Center)).Returns(true);
            var keyboard = new Mock<IPinpadKeyboard>();
            keyboard.Setup(x => x.GetKey()).Returns(PinpadKeyCode.Cancel);
            var infos = new Mock<IPinpadInfos>();
            IDataPicker picker = new DataPicker(keyboard.Object, infos.Object, display.Object);
            // Act
            short? value = picker.GetValueInOptions(null, 1, 2, 3, 4, 5);
            // Assert
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetValueInOptions_string_should_throws_exception_when_label_is_null()
        {
            // Arrange
            var display = new Mock<IPinpadDisplay>();
            display.Setup(x => x.ShowMessage("label", string.Empty, DisplayPaddingType.Center)).Returns(true);
            var keyboard = new Mock<IPinpadKeyboard>();
            keyboard.Setup(x => x.GetKey()).Returns(PinpadKeyCode.Cancel);
            var infos = new Mock<IPinpadInfos>();
            IDataPicker picker = new DataPicker(keyboard.Object, infos.Object, display.Object);
            // Act
            string value = picker.GetValueInOptions(string.Empty, string.Empty, string.Empty);
            // Assert
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetValueInOptions_short_should_throws_exception_when_option_is_empty()
        {
            // Arrange
            var display = new Mock<IPinpadDisplay>();
            display.Setup(x => x.ShowMessage("label", string.Empty, DisplayPaddingType.Center)).Returns(true);
            var keyboard = new Mock<IPinpadKeyboard>();
            keyboard.Setup(x => x.GetKey()).Returns(PinpadKeyCode.Cancel);
            var infos = new Mock<IPinpadInfos>();
            IDataPicker picker = new DataPicker(keyboard.Object, infos.Object, display.Object);
            // Act
            short? value = picker.GetValueInOptions("label", new short[]{ });
            // Assert
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetValueInOptions_string_should_throws_exception_when_option_is_empty()
        {
            // Arrange
            var display = new Mock<IPinpadDisplay>();
            display.Setup(x => x.ShowMessage("label", string.Empty, DisplayPaddingType.Center)).Returns(true);
            var keyboard = new Mock<IPinpadKeyboard>();
            keyboard.Setup(x => x.GetKey()).Returns(PinpadKeyCode.Cancel);
            var infos = new Mock<IPinpadInfos>();
            IDataPicker picker = new DataPicker(keyboard.Object, infos.Object, display.Object);
            // Act
            string value = picker.GetValueInOptions("label", new string[] { });
            // Assert
        }
        [TestMethod]
        public void GetValueInOptions_short_should_return_a_null_when_cancel_was_pressed()
        {
            // Arrange
            var display = new Mock<IPinpadDisplay>();
            display.Setup(x => x.ShowMessage("label", string.Empty, DisplayPaddingType.Center)).Returns(true);
            var keyboard = new Mock<IPinpadKeyboard>();
            keyboard.Setup(x => x.GetKey()).Returns(PinpadKeyCode.Cancel);
            var infos = new Mock<IPinpadInfos>();
            IDataPicker picker = new DataPicker(keyboard.Object, infos.Object, display.Object);
            // Act
            short? value = picker.GetValueInOptions("label", 1, 2, 3, 4, 5);
            // Assert
            Assert.IsNull(value);
            Assert.IsFalse(value.HasValue);
        }
        [TestMethod]
        public void GetValueInOptions_string_should_return_a_null_when_cancel_was_pressed()
        {
            // Arrange
            var display = new Mock<IPinpadDisplay>();
            display.Setup(x => x.ShowMessage("label", string.Empty, DisplayPaddingType.Center)).Returns(true);
            var keyboard = new Mock<IPinpadKeyboard>();
            keyboard.Setup(x => x.GetKey()).Returns(PinpadKeyCode.Cancel);
            var infos = new Mock<IPinpadInfos>();
            IDataPicker picker = new DataPicker(keyboard.Object, infos.Object, display.Object);
            // Act
            string value = picker.GetValueInOptions("label", "um", "dois", "tres", "quatro", "cinco");
            // Assert
            Assert.IsNull(value);
        }
        [TestMethod]
        public void GetValueInOptions_short_should_return_a_value_when_return_was_pressed()
        {
            // Arrange
            var display = new Mock<IPinpadDisplay>();
            display.Setup(x => x.ShowMessage("label", string.Empty, DisplayPaddingType.Center)).Returns(true);
            var keyboard = new Mock<IPinpadKeyboard>();
            keyboard.Setup(x => x.GetKey()).Returns(PinpadKeyCode.Return);
            var infos = new Mock<IPinpadInfos>();
            IDataPicker picker = new DataPicker(keyboard.Object, infos.Object, display.Object);
            // Act
            short? value = picker.GetValueInOptions("label", 1, 2, 3, 4, 5);
            // Assert
            Assert.IsNotNull(value);
            Assert.IsTrue(value.HasValue);
            Assert.AreEqual(1, value.Value);
        }
        [TestMethod]
        public void GetValueInOptions_string_should_return_a_value_when_return_was_pressed()
        {
            // Arrange
            var display = new Mock<IPinpadDisplay>();
            display.Setup(x => x.ShowMessage("label", string.Empty, DisplayPaddingType.Center)).Returns(true);
            var keyboard = new Mock<IPinpadKeyboard>();
            keyboard.Setup(x => x.GetKey()).Returns(PinpadKeyCode.Return);
            var infos = new Mock<IPinpadInfos>();
            IDataPicker picker = new DataPicker(keyboard.Object, infos.Object, display.Object);
            // Act
            string value = picker.GetValueInOptions("label", "um", "dois", "tres", "quatro", "cinco");
            // Assert
            Assert.IsNotNull(value);
            Assert.AreEqual("um", value);
        }
    }
}
