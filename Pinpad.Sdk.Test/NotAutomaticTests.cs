using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pinpad.Core.Commands;
using Pinpad.Core.Pinpad;
using Pinpad.Sdk.Connection;
using Pinpad.Sdk.EmvTable;
using Pinpad.Sdk.Model.TypeCode;
using System.Diagnostics;

namespace Pinpad.Sdk.Test
{
	[TestClass]
	public class NotAutomaticTests
	{
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
			MicroPos.Platform.Desktop.DesktopInitializer.Initialize();

			// testes:
			//this.mockedPinpadConnection = new MockedPinpadConnection();

			// prod:
			PinpadConnection conn = new PinpadConnection();
			string portName = PinpadConnection.SearchPinpadPort();
			conn.Open(portName);

			PinpadCommunication comm = new PinpadCommunication(conn.PlatformPinpadConnection);

			GertecEx07Request request = new GertecEx07Request();

			request.NumericInputType.Value = GertecEx07NumberFormat.Decimal;
			request.TextInputType.Value = GertecEx07TextFormat.None;
			request.LabelFirstLine.Value = GertecMessageInFirstLineCode.TypeNumber;
			request.LabelSecondLine.Value = GertecMessageInSecondLineCode.GasPump;
			request.MaximumCharacterLength.Value = 5;
			request.MinimumCharacterLength.Value = 20;
			request.TimeOut.Value = 30;
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
			MicroPos.Platform.Desktop.DesktopInitializer.Initialize();

			PinpadConnection conn = new PinpadConnection();
			string portName = PinpadConnection.SearchPinpadPort();
			conn.Open(portName);
			
			PinpadFacade facade = new PinpadFacade(conn.PlatformPinpadConnection);

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
		public void OPN_test ()
		{
			MicroPos.Platform.Desktop.DesktopInitializer.Initialize();

			PinpadConnection conn = new PinpadConnection();

			string portName = PinpadConnection.SearchPinpadPort();

			// Opening connection with specifies serial port:
			conn.Open(portName);

			PinpadCommunication comm = new PinpadCommunication(conn.PlatformPinpadConnection);

			OpnRequest opn = new OpnRequest();
			OpnResponse opnResp = comm.SendRequestAndReceiveResponse<OpnResponse>(opn);

			DspRequest dsp = new DspRequest();
			dsp.DSP_MSG.Value = new Core.Properties.SimpleMessage("ola");

			GenericResponse r = comm.SendRequestAndReceiveResponse<GenericResponse>(dsp);
		}
	}
}
