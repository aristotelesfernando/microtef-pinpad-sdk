using MicroPos.CrossPlatform;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pinpad.Sdk;
using Pinpad.Sdk.Commands;
using Pinpad.Sdk.Pinpad;
using Pinpad.Sdk.Model.TypeCode;
using System.Collections.Generic;
using System.Diagnostics;
using Pinpad.Sdk.Properties;
using Pinpad.Sdk.Commands.Context;

namespace Pinpad.Sdk.Test
{
	[TestClass]
	public class NotAutomaticTests
	{
		[TestInitialize]
		public void Setup ()
		{
			MicroPos.Platform.Desktop.DesktopInitializer.Initialize();
		}

		//[TestMethod]
		public void GCD_test ()
		{
			GcdRequest r = new GcdRequest();
			r.SPE_MSGIDX.Value = Model.TypeCode.KeyboardMessageCode.AskForSecurityCode;
			r.SPE_MINDIG.Value = 1;
			r.SPE_MAXDIG.Value = 2;

			Debug.WriteLine((int) r.SPE_MSGIDX.Value);
			Debug.WriteLine(r.CommandString);
		}

		[TestMethod]
		public void Gertec_EX07_test ()
		{
			// testes:
			//this.mockedPinpadConnection = new MockedPinpadConnection();

			// prod:
			PinpadConnection conn = PinpadConnection.GetFirst();

			PinpadCommunication comm = new PinpadCommunication(conn);

			GertecEx07Request request = new GertecEx07Request();

			request.NumericInputType.Value = GertecEx07NumberFormat.Decimal;
			request.TextInputType.Value = GertecEx07TextFormat.None;
			request.LabelFirstLine.Value = GertecMessageInFirstLineCode.TypeNumber;
			request.LabelSecondLine.Value = GertecMessageInSecondLineCode.GasPump;
			request.MaximumCharacterLength.Value = 1;
			request.MinimumCharacterLength.Value = 3;
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

		[TestMethod]
		public void Keyboard_getNumber_test ()
		{
			PinpadConnection conn = PinpadConnection.GetFirst();

			PinpadFacade facade = new PinpadFacade(conn);

			facade.Display.ShowMessage("ola", "tudo bom?", DisplayPaddingType.Center);

			string pump = facade.Keyboard.GetNumericInput(GertecMessageInFirstLineCode.TypeNumber, GertecMessageInSecondLineCode.GasPump, 5, 15, 10);

			if (pump == null)
			{
				Debug.WriteLine("Nao foi possivel ler um valor. Time out ou cancelamento.");
			}
			else
			{
				Debug.WriteLine("Valor digitado: " + pump);
			}
		}

		[TestMethod]
		public void MyTestMethod ()
		{
			PinpadConnection conn = PinpadConnection.GetFirst();

			PinpadFacade facade = new PinpadFacade(conn);

			facade.Display.ShowMessage("ola", "tudo bom?", DisplayPaddingType.Center);

			string pump = facade.Keyboard.GetText(GertecMessageInFirstLineCode.TypeNumber, GertecMessageInSecondLineCode.GasPump, 5, 15, 120);

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

			if (connections == null)
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

			Assert.IsTrue(true);
		}

		[TestMethod]
		public void OnePinpadFind_test ()
		{
			PinpadConnection conn = PinpadConnection.GetFirst();
			PinpadFacade facade = new PinpadFacade(conn);
			facade.Display.ShowMessage("", "wow!", DisplayPaddingType.Center);
		}
	}
}
