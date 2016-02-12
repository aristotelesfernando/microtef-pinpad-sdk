using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Pinpad.Sdk.Transaction;
using Pinpad.Sdk.Model.TypeCode;
using Pinpad.Sdk.EmvTable;
using Pinpad.Core.Pinpad;
using Pinpad.Core.Commands;
using MicroPos.CrossPlatform;

namespace Pinpad.Sdk.Test.Transaction
{
    [TestClass]
    public class PinReaderTest
    {
        //PinpadFacade pinpadFacade;

        [TestInitialize]
        public void Setup()
        {
            Mock<IPinpadConnection> mockedConn = new Mock<IPinpadConnection>();
            //this.pinpadFacade = new PinpadFacade(mockedConn.Object);
        }

        [TestMethod]
        public void PinReader_should_not_return_null()
        {
            PinReader auth = new PinReader(new MockedPinpadFacade(), CardType.Emv);
            Assert.IsNotNull(auth);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void PinReader_should_throw_exception_if_invalid_CardType()
        {
			PinReader auth = new PinReader(new MockedPinpadFacade(), CardType.Undefined);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void PinReader_should_throw_exception_if_null_PinpadFacade()
        {
			PinReader auth = new PinReader(null, CardType.MagneticStripe);
        }

		// Mocked class for PinpadFacade.
		internal class MockedPinpadFacade : IPinpadFacade 
		{
			public Core.Pinpad.PinpadCommunication Communication
			{
				get
				{
					return null;
				}
				set
				{
					
				}
			}

			public Core.Pinpad.PinpadKeyboard Keyboard
			{
				get
				{
					return null;
				}
				set
				{
					
				}
			}

			public Core.Pinpad.PinpadDisplay Display
			{
				get
				{
					return null;
				}
				set
				{
					
				}
			}

			public Core.Pinpad.PinpadPrinter Printer
			{
				get
				{
					return null;
				}
				set
				{
					
				}
			}

			public Core.Pinpad.PinpadStorage Storage
			{
				get
				{
					return null;
				}
				set
				{
					
				}
			}

			public PinpadTable Table
			{
				get
				{
					return null;
				}
				set
				{
					
				}
			}

			public Core.Pinpad.PinpadInfos Infos
			{
				get
				{
					return null;
				}
				set
				{
					
				}
			}

			public Core.Pinpad.PinpadEncryption Encryption
			{
				get
				{
					return null;
				}
				set
				{
					
				}
			}
		}
    }
}
