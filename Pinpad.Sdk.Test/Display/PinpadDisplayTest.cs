using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pinpad.Sdk.Connection;
using Pinpad.Core.Pinpad;
using Moq;
using CrossPlatformBase;

namespace Pinpad.Sdk.Test.Display
{
	[TestClass]
	public class PinpadDisplayTest
	{
		[TestMethod]
		public void PinpadDisplay_should_not_return_null()
		{
			PinpadConnection conn = new PinpadConnection();
			conn.PlatformPinpadConnection = Mock.Of<IPinPadConnection>();
			PinpadCommunication comm = new PinpadCommunication(conn.PlatformPinpadConnection);
			PinpadDisplay display = new PinpadDisplay(comm);
			Assert.IsNotNull(display);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void PinpadDisplay_should_throw_exception_if_invalid_PinpadConnection()
		{
			PinpadDisplay display = new PinpadDisplay(null);
			Assert.IsNotNull(display);
		}
	}
}
