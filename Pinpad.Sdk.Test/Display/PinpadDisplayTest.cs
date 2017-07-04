using System;
using NUnit.Framework;
using Moq;
using Microtef.CrossPlatform;

namespace Pinpad.Sdk.Test.Display
{
    [TestFixture]
	public class PinpadDisplayTest
	{
		[Test]
		public void PinpadDisplay_Construction_ShouldNotReturnNull()
		{
            // Arrange
			IPinpadConnection connectionStub = Mock.Of<IPinpadConnection>();
            PinpadCommunication comm = new PinpadCommunication(connectionStub);
			
            // Act
            PinpadDisplay display = new PinpadDisplay(comm);
			
            // Assert
            Assert.IsNotNull(display);
		}

        [Test]
        public void PinpadDisplay_Construction_ShouldThrowException_IfPinpadCommunicationIsNull ()
		{
            // Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                // Arrange
                PinpadCommunication nullPinpadConnection = null;

                // Act
                PinpadDisplay display = new PinpadDisplay(nullPinpadConnection);
            });
		}
	}
}
