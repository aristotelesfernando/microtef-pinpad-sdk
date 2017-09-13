using NUnit.Framework;
using Moq;
using Pinpad.Sdk.Model;
using Pinpad.Sdk.Model.Utilities;
using Pinpad.Sdk.Utilities;
using System;

namespace Pinpad.Sdk.Test.Utilities
{
    [TestFixture]
    public class DataPickerTest
    {
        [Test]
        public void DataPicker_Construction_ShouldNotThrowException_IfParametersAreCorrect ()
        {
            // Arrange
            var dummyKeyboard = Mock.Of<IPinpadKeyboard>();
            var dummyInfos = Mock.Of<IPinpadInfos>();
            var dummyDisplay = Mock.Of<IPinpadDisplay>();

            // Act
            IDataPicker picker = new DataPicker(
                dummyKeyboard, 
                dummyInfos, 
                dummyDisplay);

            // Assert
            Assert.IsNotNull(picker);
        }
        [Test]
        public void DataPicker_GetNumericValue_ShouldThrowException_IfLabelIsEmpty ()
        {
            // Assert
            Assert.Throws<ArgumentException>(() =>
            {
				// Arrange
				var dummyKeyboard = Mock.Of<IPinpadKeyboard>();
				var dummyInfos = Mock.Of<IPinpadInfos>();
				var dummyDisplay = Mock.Of<IPinpadDisplay>();

                IDataPicker picker = new DataPicker(
                    dummyKeyboard, 
                    dummyInfos, 
                    dummyDisplay);

                string emptyLabel = string.Empty;

                // Act
                picker.GetNumericValue(
                    label: emptyLabel, 
                    circularBehavior: false, 
                    minimunLimit: 0, 
                    maximumLimit: 1);
            });
        }
        [Test]
        public void DataPicker_GetNumericValue_ShouldThrowException_IfLabelIsNull()
        {
            // Assert
            Assert.Throws<ArgumentException>(() =>
            {
				// Arrange
				var dummyKeyboard = Mock.Of<IPinpadKeyboard>();
				var dummyInfos = Mock.Of<IPinpadInfos>();
				var dummyDisplay = Mock.Of<IPinpadDisplay>();

				IDataPicker picker = new DataPicker(
					dummyKeyboard,
					dummyInfos,
					dummyDisplay);

				string nullLabel = null;

				// Act
				picker.GetNumericValue(
					label: nullLabel,
					circularBehavior: false,
					minimunLimit: 0,
					maximumLimit: 1);
            });
        }
        [Test]
        public void DataPicker_GetNumericValue_ShouldThrowException_IfMinimumLimitIsGreaterThanMaximumLimit ()
        {
			// Assert
			Assert.Throws<ArgumentException>(() =>
			{
				// Arrange
				var dummyKeyboard = Mock.Of<IPinpadKeyboard>();
				var dummyInfos = Mock.Of<IPinpadInfos>();
				var dummyDisplay = Mock.Of<IPinpadDisplay>();

				IDataPicker picker = new DataPicker(
					dummyKeyboard,
					dummyInfos,
					dummyDisplay);
                
                short invalidMaxLimit = 1;
                short invalidMinLimit = (short)(invalidMaxLimit + 1);

				// Act
				picker.GetNumericValue(
					label: "testLabel",
					circularBehavior: false,
                    minimunLimit: invalidMinLimit,
                    maximumLimit: invalidMaxLimit);
			});
        }
        [Test]
        public void DataPicker_GetNumericValue_ShouldReturnAValue_WhenReturnKeyIsPressed()
        {
            // Arrange
            var displayStub = new Mock<IPinpadDisplay>();
            displayStub.Setup(x => x.ShowMessage("label", 
                                                 string.Empty, 
                                                 DisplayPaddingType.Center))
                       .Returns(true);
            
            var keyboardStub = new Mock<IPinpadKeyboard>();
            keyboardStub.Setup(x => x.GetKey())
                        .Returns(PinpadKeyCode.Return);
            
            var dummyInfos = Mock.Of<IPinpadInfos>();

            IDataPicker picker = new DataPicker(
                keyboardStub.Object, 
                dummyInfos,
                displayStub.Object);

            Random itemsProvider = new Random();
            short expectedMinLimit = (short)itemsProvider.Next(400);
            short expectedMaxLimit = (short)itemsProvider.Next(
                expectedMinLimit,
                expectedMinLimit + 400);

            // Act
            Nullable<short> readValue = picker.GetNumericValue(
                label: "testLabel", 
                circularBehavior: false, 
                minimunLimit: expectedMinLimit, 
                maximumLimit: expectedMaxLimit);

            // Assert
            Assert.IsNotNull(readValue);
            Assert.IsTrue(readValue.HasValue);
            Assert.AreEqual(expectedMinLimit, readValue.Value);
        }
        [Test]
        public void DataPicker_GetNumericValue_ShouldReturnNull_IfCancelIsPressed ()
        {
			// Arrange
			var displayStub = new Mock<IPinpadDisplay>();
			displayStub.Setup(x => x.ShowMessage("label",
												 string.Empty,
												 DisplayPaddingType.Center))
					   .Returns(true);

			var keyboardStub = new Mock<IPinpadKeyboard>();
			keyboardStub.Setup(x => x.GetKey())
                        .Returns(PinpadKeyCode.Cancel);

			var dummyInfos = Mock.Of<IPinpadInfos>();

			IDataPicker picker = new DataPicker(
				keyboardStub.Object,
				dummyInfos,
				displayStub.Object);
            
            // Act
            Nullable<short> readValue = picker.GetNumericValue(
                label: "label", 
                circularBehavior: false, 
                minimunLimit: 0, 
                maximumLimit: 1);

            // Assert
            Assert.IsNull(readValue);
            Assert.IsFalse(readValue.HasValue);
        }
        [Test]
        public void DataPicker_GetValueInOptions_ShouldThrowException_IfLabelIsEmptyAndPassingShortOptionsAsParameters ()
        {
			// Assert
			Assert.Throws<ArgumentException>(() =>
			{
				// Arrange
				var dummyKeyboard = Mock.Of<IPinpadKeyboard>();
				var dummyInfos = Mock.Of<IPinpadInfos>();
				var dummyDisplay = Mock.Of<IPinpadDisplay>();

				IDataPicker picker = new DataPicker(
					dummyKeyboard,
					dummyInfos,
					dummyDisplay);

                short[] options = { 1, 2, 3, 4, 5, 6, 666 };
				string emptyLabel = string.Empty;

				// Act
                picker.GetValueInOptions(
					label: emptyLabel,
					circularBehavior: false,
                    options: options);
			});
        }        
        [Test]
        public void DataPicker_GetValueInOptions_ShouldThrowException_IfLabelIsNullAndPassingShortOptionsAsParameters ()
        {
			// Assert
			Assert.Throws<ArgumentException>(() =>
			{
				// Arrange
				var dummyKeyboard = Mock.Of<IPinpadKeyboard>();
				var dummyInfos = Mock.Of<IPinpadInfos>();
				var dummyDisplay = Mock.Of<IPinpadDisplay>();

				IDataPicker picker = new DataPicker(
					dummyKeyboard,
					dummyInfos,
					dummyDisplay);

				short[] options = { 1, 2, 3, 4, 5, 6, 666 };
				string emptyLabel = null;

				// Act
				picker.GetValueInOptions(
					label: emptyLabel,
					circularBehavior: false,
					options: options);
			});
        }
		[Test]
		public void DataPicker_GetValueInOptions_ShouldThrowException_IfLabelIsEmptyAndPassingStringOptionsAsParameters()
		{
			// Assert
			Assert.Throws<ArgumentException>(() =>
			{
				// Arrange
				var dummyKeyboard = Mock.Of<IPinpadKeyboard>();
				var dummyInfos = Mock.Of<IPinpadInfos>();
				var dummyDisplay = Mock.Of<IPinpadDisplay>();

				IDataPicker picker = new DataPicker(
					dummyKeyboard,
					dummyInfos,
					dummyDisplay);

				string[] options = { "Um", "Dois", "Tres", "Quatro", "Cinco" };
				string emptyLabel = string.Empty;

				// Act
				picker.GetValueInOptions(
					label: emptyLabel,
					circularBehavior: false,
					options: options);
			});
		}
		[Test]
		public void DataPicker_GetValueInOptions_ShouldThrowException_IfLabelIsNullAndPassingStringOptionsAsParameters()
		{
			// Assert
			Assert.Throws<ArgumentException>(() =>
			{
				// Arrange
				var dummyKeyboard = Mock.Of<IPinpadKeyboard>();
				var dummyInfos = Mock.Of<IPinpadInfos>();
				var dummyDisplay = Mock.Of<IPinpadDisplay>();

				IDataPicker picker = new DataPicker(
					dummyKeyboard,
					dummyInfos,
					dummyDisplay);

				string[] options = { "Um", "Dois", "Tres", "Quatro", "Cinco" };
				string emptyLabel = null;

				// Act
				picker.GetValueInOptions(
					label: emptyLabel,
					circularBehavior: false,
					options: options);
			});
		}
        [Test]
        public void DataPicker_GetValueInOptions_ShouldThrowException_IfOptionsAsStringAreNull ()
        {
            // Assert
            Assert.Throws<ArgumentException>(() =>
            {
                // Arrange
                var displayStub = new Mock<IPinpadDisplay>();
                displayStub.Setup(x => x.ShowMessage("label", 
                                                     string.Empty, 
                                                     DisplayPaddingType.Center))
                           .Returns(true);
                
                var keyboardStub = new Mock<IPinpadKeyboard>();
                keyboardStub.Setup(x => x.GetKey())
                            .Returns(PinpadKeyCode.Cancel);
                
                var dummyInfos = Mock.Of<IPinpadInfos>();
                IDataPicker picker = new DataPicker(keyboardStub.Object, 
                                                    dummyInfos, 
                                                    displayStub.Object);

                // Act
                string value = picker.GetValueInOptions(
                    label: "label", 
                    circularBehavior: false, 
                    options: new string[] { });
            });
        }
		[Test]
		public void DataPicker_GetValueInOptions_ShouldThrowException_IfOptionsAsShortAreNull()
		{
			// Assert
			Assert.Throws<ArgumentException>(() =>
			{
				// Arrange
				var displayStub = new Mock<IPinpadDisplay>();
				displayStub.Setup(x => x.ShowMessage("label",
													 string.Empty,
													 DisplayPaddingType.Center))
						   .Returns(true);

				var keyboardStub = new Mock<IPinpadKeyboard>();
				keyboardStub.Setup(x => x.GetKey())
							.Returns(PinpadKeyCode.Cancel);

				var dummyInfos = Mock.Of<IPinpadInfos>();
				IDataPicker picker = new DataPicker(keyboardStub.Object,
													dummyInfos,
													displayStub.Object);

				// Act
				Nullable<short> value = picker.GetValueInOptions(
					label: "label",
					circularBehavior: false,
					options: new short[] { });
			});
		}
        [Test]
		public void DataPicker_GetValueInOptionsWithShortOptions_ShouldReturnNull_IfCancelIsPressed()
        {
			// Arrange
			var displayStub = new Mock<IPinpadDisplay>();
			displayStub.Setup(x => x.ShowMessage("label",
												 string.Empty,
												 DisplayPaddingType.Center))
					   .Returns(true);

			var keyboardStub = new Mock<IPinpadKeyboard>();
			keyboardStub.Setup(x => x.GetKey())
						.Returns(PinpadKeyCode.Cancel);

			var dummyInfos = Mock.Of<IPinpadInfos>();

			IDataPicker picker = new DataPicker(
				keyboardStub.Object,
				dummyInfos,
				displayStub.Object);

            short[] options = { 1, 2, 3, 4, 5 };

			// Act
            Nullable<short> readValue = picker.GetValueInOptions(
				label: "label",
				circularBehavior: false,
				options: options);

			// Assert
			Assert.IsNull(readValue);
			Assert.IsFalse(readValue.HasValue);
        }
		[Test]
		public void DataPicker_GetValueInOptionsWithStringOptions_ShouldReturnNull_IfCancelIsPressed()
		{
			// Arrange
			var displayStub = new Mock<IPinpadDisplay>();
			displayStub.Setup(x => x.ShowMessage("label",
												 string.Empty,
												 DisplayPaddingType.Center))
					   .Returns(true);

			var keyboardStub = new Mock<IPinpadKeyboard>();
			keyboardStub.Setup(x => x.GetKey())
						.Returns(PinpadKeyCode.Cancel);

			var dummyInfos = Mock.Of<IPinpadInfos>();

			IDataPicker picker = new DataPicker(
				keyboardStub.Object,
				dummyInfos,
				displayStub.Object);

			string[] options = { "Um", "Dois", "Tres", "Quatro", "Cinco" };

			// Act
			string readValue = picker.GetValueInOptions(
				label: "label",
				circularBehavior: false,
				options: options);

			// Assert
            Assert.IsTrue(string.IsNullOrEmpty(readValue));
		}
		[Test]
		public void DataPicker_GetValueInOptionsWithShortOptions_ShouldReturnAValue_WhenReturnKeyIsPressed()
		{
			// Arrange
			var displayStub = new Mock<IPinpadDisplay>();
			displayStub.Setup(x => x.ShowMessage("label",
												 string.Empty,
												 DisplayPaddingType.Center))
					   .Returns(true);

			var keyboardStub = new Mock<IPinpadKeyboard>();
			keyboardStub.Setup(x => x.GetKey())
						.Returns(PinpadKeyCode.Return);

			var dummyInfos = Mock.Of<IPinpadInfos>();

			IDataPicker picker = new DataPicker(
				keyboardStub.Object,
				dummyInfos,
				displayStub.Object);

            short[] options = { 1, 2, 3, 4, 5 };

			// Act
            Nullable<short> readValue = picker.GetValueInOptions(
				label: "testLabel",
				circularBehavior: false,
				options: options);

			// Assert
			Assert.IsNotNull(readValue);
			Assert.IsTrue(readValue.HasValue);
			Assert.AreEqual(1, readValue.Value);
		}
		[Test]
		public void DataPicker_GetValueInOptionsWithStringOptions_ShouldReturnAValue_WhenReturnKeyIsPressed()
		{
			// Arrange
			var displayStub = new Mock<IPinpadDisplay>();
			displayStub.Setup(x => x.ShowMessage("label",
												 string.Empty,
												 DisplayPaddingType.Center))
					   .Returns(true);

			var keyboardStub = new Mock<IPinpadKeyboard>();
			keyboardStub.Setup(x => x.GetKey())
						.Returns(PinpadKeyCode.Return);

			var dummyInfos = Mock.Of<IPinpadInfos>();

			IDataPicker picker = new DataPicker(
				keyboardStub.Object,
				dummyInfos,
				displayStub.Object);

			string[] options = { "Um", "Dois", "Tres", "Quatro", "Cinco" };

			// Act
			string readValue = picker.GetValueInOptions(
				label: "testLabel",
				circularBehavior: false,
				options: options);

			// Assert
            Assert.IsFalse(string.IsNullOrEmpty(readValue));
			Assert.AreEqual("Um", readValue);
		}
    }
}
