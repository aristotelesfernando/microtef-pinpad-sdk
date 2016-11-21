using MicroPos.CrossPlatform;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pinpad.Sdk.Commands;
using System.Collections.Generic;
using System.Diagnostics;
using Pinpad.Sdk.Properties;
using Pinpad.Sdk.Model;
using System;
using System.Globalization;
using Pinpad.Sdk.Test.Mockings;

namespace Pinpad.Sdk.Test
{
	[TestClass]
	public class NotAutomaticTests
	{
		[TestInitialize]
		public void Setup ()
		{
			//MicroPos.Platform.Desktop.DesktopInitializer.Initialize();
		}
		//[TestMethod]
		public void GCD_test ()
		{
			GcdRequest r = new GcdRequest();
			r.SPE_MSGIDX.Value = KeyboardMessageCode.AskForSecurityCode;
			r.SPE_MINDIG.Value = 1;
			r.SPE_MAXDIG.Value = 2;

			Debug.WriteLine((int) r.SPE_MSGIDX.Value);
			Debug.WriteLine(r.CommandString);
		}
		//[TestMethod]
		public void Gertec_EX07_test ()
		{
			// testes:
			//this.mockedPinpadConnection = new MockedPinpadConnection();

			// prod:
			PinpadConnection conn = PinpadConnection.GetFirst();

			PinpadCommunication comm = new PinpadCommunication(conn);

			GciGertecRequest request = new GciGertecRequest();

			request.NumericInputType.Value = KeyboardNumberFormat.Decimal;
			request.TextInputType.Value = KeyboardTextFormat.None;
			request.LabelFirstLine.Value = FirstLineLabelCode.TypeNumber;
			request.LabelSecondLine.Value = SecondLineLabelCode.GasPump;
			request.MaximumCharacterLength.Value = 1;
			request.MinimumCharacterLength.Value = 10;
			request.TimeOut.Value = 60;
			request.TimeIdle.Value = 0;

			Debug.WriteLine(request.CommandString);

			GertecEx07Response response = comm.SendRequestAndReceiveResponse<GertecEx07Response>(request);

			if (response != null)
			{
				Debug.WriteLine("Response status: " + response.RSP_STAT.Value);
				Debug.WriteLine("Value typed: " + response.RSP_RESULT.Value);
			}
			else
			{
				Debug.WriteLine("Resposta nula. Cancelamento ou timeout.");
			}
		}
		//[TestMethod]
		public void Keyboard_getNumber_test ()
		{
			PinpadConnection conn = PinpadConnection.GetFirst();

			PinpadFacade facade = new PinpadFacade(conn);

			facade.Display.ShowMessage("ola", "tudo bom?", DisplayPaddingType.Center);

			string pump = facade.Keyboard.GetNumericInput(FirstLineLabelCode.TypeNumber, SecondLineLabelCode.GasPump, 1, 15, 15);

			if (pump == null)
			{
				Debug.WriteLine("Nao foi possivel ler um valor. Time out ou cancelamento.");
			}
			else
			{
				Debug.WriteLine("Valor digitado: " + pump);
			}
		}
		//[TestMethod]
		public void Keyboard_GetAmount_test ()
		{
			PinpadConnection conn = PinpadConnection.GetFirst();

			PinpadFacade pinpad = new PinpadFacade(conn);
			Nullable<decimal> amount = pinpad.Keyboard.GetAmount(AmountCurrencyCode.Yen);

			if (amount.HasValue)
			{
				Debug.WriteLine("Valor digitado: " + amount.Value);
			}
			else
			{
				Debug.WriteLine("Não foi possível ler o valor.");
			}
		}
		//[TestMethod]
		public void OPN_test ()
		{
			PinpadConnection conn = PinpadConnection.GetFirst();

			PinpadCommunication comm = new PinpadCommunication(conn);

			OpnRequest opn = new OpnRequest();
			OpnResponse opnResp = comm.SendRequestAndReceiveResponse<OpnResponse>(opn);

			DspRequest dsp = new DspRequest();
			dsp.DSP_MSG.Value = new SimpleMessage("ola");

			GenericResponse r = comm.SendRequestAndReceiveResponse<GenericResponse>(dsp);
		}
		//[TestMethod]
		public void MultiplePinpads_test ()
		{
			// Gets all connections:
			ICollection<IPinpadConnection> connections = CrossPlatformController.PinpadFinder.FindAllDevices();

			if (connections == null || connections.Count <= 0)
			{
				Assert.IsTrue(false);
			}

			// Gets all pinpads:
			ICollection<IPinpadFacade> pinpads = new List<IPinpadFacade>();

			foreach (IPinpadConnection conn in connections)
			{
				pinpads.Add(new PinpadFacade(conn));
			}

			// Show corresponding COM port on the pinpad display:
			foreach (IPinpadFacade pinpad in pinpads)
			{
				pinpad.Display.ShowMessage("", pinpad.Communication.PinpadConnection.Connection.ConnectionName, DisplayPaddingType.Center);
			}

			// Closing conections...
			foreach (IPinpadFacade pinpad in pinpads)
			{
				pinpad.Communication.ClosePinpadConnection("Close " + pinpad.Communication.PinpadConnection.Connection.ConnectionName);
			}

			Assert.IsTrue(true);
		}
		//[TestMethod]
		public void OnePinpadFind_test ()
		{
			for (int i = 0; i < 5; i++)
			{
				PinpadConnection conn = PinpadConnection.GetFirst();
				PinpadFacade facade = new PinpadFacade(conn);

				facade.Display.ShowMessage("YAY! ^-^", (i + 1).ToString(), DisplayPaddingType.Center);

				facade.Communication.ClosePinpadConnection("Fechando conexao (" + (i + 1).ToString() + ")");
			}
		}
		//[TestMethod]
		public void OnePinpadFind_with_portName_test ()
		{
			for (int i = 0; i < 5; i++)
			{
				PinpadConnection conn = PinpadConnection.GetAt("COM27");
                PinpadFacade facade = new PinpadFacade(conn);

				bool displayStatus = facade.Display.ShowMessage("YAY! ^-^", (i + 1).ToString(), DisplayPaddingType.Center);

				facade.Communication.ClosePinpadConnection("Fechando conexao (" + (i + 1).ToString() + ")");
			}
		}
		//[TestMethod]
		public void Ping_test ()
		{
			PinpadConnection conn = PinpadConnection.GetFirst();
			PinpadFacade facade = new PinpadFacade(conn);

			facade.Display.ShowMessage("yay!", null, DisplayPaddingType.Center);
			facade.Communication.ClosePinpadConnection("adios");

			bool status = facade.Communication.Ping();
		}
		//[TestMethod]
		public void GetNumericValue_test ()
		{
			PinpadConnection conn = PinpadConnection.GetFirst();
			PinpadFacade facade = new PinpadFacade(conn);

			int? value = facade.Keyboard.DataPicker.GetNumericValue("Data Picker", 0, 5);

			Debug.WriteLine(value.Value);
		}
		//[TestMethod]
		public void GetValueInOptionsShort_test ()
		{
			PinpadConnection conn = PinpadConnection.GetFirst();
			PinpadFacade facade = new PinpadFacade(conn);

			short? value = facade.Keyboard.DataPicker.GetValueInOptions("Parcelas", 2, 3, 4, 5, 6);

			Debug.WriteLine(value.Value);
		}
		//[TestMethod]
		public void GetValueInOptionsString_test ()
		{
			PinpadConnection conn = PinpadConnection.GetFirst();
			PinpadFacade facade = new PinpadFacade(conn);

			string value = facade.Keyboard.DataPicker.GetValueInOptions("Pokemon", "Bulbasaur", "Charmander", "Squirtle");

			Debug.WriteLine(value);
		}
		//[TestMethod]
		public void PinpadTransaction_ReadCard_test ()
		{
            PinpadConnection conn = PinpadConnection.GetFirst();
            PinpadFacade facade = new PinpadFacade(conn);
            TransactionType trnxType = TransactionType.Undefined;

            facade.TransactionService.EmvTable.UpdatePinpad(false);
            CardEntry card = facade.TransactionService.ReadCard(TransactionType.Debit, 
                0.1m, out trnxType, CardBrandMocker.GetMock());

            facade.TransactionService.ReadPassword(0.1m, card.PrimaryAccountNumber, CardType.Emv);

            Assert.IsNotNull(card);
        }
    }
}
