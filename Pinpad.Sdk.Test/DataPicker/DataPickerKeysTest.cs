using NUnit.Framework;
using Pinpad.Sdk.Model;
using Pinpad.Sdk.Model.Utilities;
using System;

namespace Pinpad.Sdk.Test.Utilities
{
    [TestFixture]
    public class DataPickerKeysTest
    {
        [Test]
        public void DataPickerKeys_Construction_ShouldNotReturnNullAndReturnDefaultDataPickerKeys ()
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
        [Test]
        public void DataPickerKeys_Construction_ShouldNotReturnNullAndParametersShouldMatch ()
        {
            // Arrange
            DataPickerKeys keys = null;

            // Act
            keys = new DataPickerKeys 
            { 
                UpKey = PinpadKeyCode.Function3, 
                DownKey = PinpadKeyCode.Function2 
            };

            // Assert
            Assert.IsNotNull(keys);
            Assert.AreEqual(PinpadKeyCode.Function3, keys.UpKey);
            Assert.AreEqual(PinpadKeyCode.Function2, keys.DownKey);
        }
        [Test]
        public void DataPickerKeys_Construction_ShouldThrowException_IfUpAndDownKeysAreTheSame ()
        {
            // Assert
            Assert.Throws<ArgumentException>(() =>
            {
                // Arrange
                DataPickerKeys keys = null;

                // Act
                keys = new DataPickerKeys 
                { 
                    UpKey = PinpadKeyCode.Function1, 
                    DownKey = PinpadKeyCode.Function1 
                };
            });
        }
        [Test]
        public void DataPickerKeys_Construction_ShouldThrowException_IfUpKeyIsUndefined()
        {
            // Assert
            Assert.Throws<ArgumentException>(() =>
            {
                // Arrange
                DataPickerKeys keys = null;

                // Act
                keys = new DataPickerKeys { UpKey = PinpadKeyCode.Undefined };
            });
        }
        [Test]
        public void DataPickerKeys_Construction_ShouldThrowException_IfDownKeyIsUndefined()
        {
            // Assert
            Assert.Throws<ArgumentException>(() =>
            {
                // Arrange
                DataPickerKeys keys = null;

                // Act
                keys = new DataPickerKeys { DownKey = PinpadKeyCode.Undefined };
            });
        }
        [Test]
        public void DataPickerKeys_Construction_ShouldThrowException_IfUpKeyIsCancelKey()
        {
            // Assert
            Assert.Throws<ArgumentException>(() =>
            {
                // Arrange
                DataPickerKeys keys = null;

                // Act
                keys = new DataPickerKeys { UpKey = PinpadKeyCode.Cancel };
            });
        }
        [Test]
        public void DataPickerKeys_Construction_ShouldThrowException_IfDownKeyIsCancelKey()
        {
            // Assert
            Assert.Throws<ArgumentException>(() =>
            {
                // Arrange
                DataPickerKeys keys = null;

                // Act
                keys = new DataPickerKeys { DownKey = PinpadKeyCode.Cancel };
            });
        }
        [Test]
        public void DataPickerKeys_Construction_ShouldThrowException_IfUpKeyIsReturnKey ()
        {
            // Assert
            Assert.Throws<ArgumentException>(() =>
            {
                // Arrange
                DataPickerKeys keys = null;

                // Act
                keys = new DataPickerKeys { UpKey = PinpadKeyCode.Return };
            });
        }
        [Test]
        public void DataPickerKeys_Construction_ShouldThrowException_IfDownKeyIsReturnKey()
        {
            // Assert
            Assert.Throws<ArgumentException>(() =>
            {
                // Arrange
                DataPickerKeys keys = null;

                // Act
                keys = new DataPickerKeys { DownKey = PinpadKeyCode.Return };
            });
        }
        [Test]
        public void DataPickerKeys_Construction_ShouldThrowException_IfUpKeyIsBackspaceKey()
        {
            // Assert
            Assert.Throws<ArgumentException>(() =>
            {
                // Arrange
                DataPickerKeys keys = null;

                // Act
                keys = new DataPickerKeys { UpKey = PinpadKeyCode.Backspace };
            });
        }
        [Test]
        public void DataPickerKeys_Construction_ShouldThrowException_IfDownKeyIsBackspaceKey()
        {
            // Assert
            Assert.Throws<ArgumentException>(() =>
            {
                // Arrange
                DataPickerKeys keys = null;

                // Act
                keys = new DataPickerKeys { DownKey = PinpadKeyCode.Backspace };
            });
        }
    }
}
