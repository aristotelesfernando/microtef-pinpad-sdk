﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pinpad.Core.Commands;
using System.Diagnostics;
using Pinpad.Core.Utilities;
using Pinpad.Core.Pinpad;
using Pinpad.Sdk.Connection;

namespace Pinpad.Sdk.Test.Connection
{
	[TestClass]
	public class PinpadConnectionTest
	{
		MockedPinpadConnection mockedPinpadConnection;

		[TestMethod]
		public void MyTestMethod()
		{
			GcdRequest r = new GcdRequest();
			r.SPE_MSGIDX.Value = Model.TypeCode.KeyboardMessageCode.AskForSecurityCode;
			r.SPE_MINDIG.Value = 1;
			r.SPE_MAXDIG.Value = 2;

			Debug.WriteLine((int)r.SPE_MSGIDX.Value);
			Debug.WriteLine(r.CommandString);
		}

		[TestMethod]
		public void MyTestMethod2 ()
		{
			MicroPos.Platform.Desktop.DesktopInitializer.Initialize();
			this.mockedPinpadConnection = new MockedPinpadConnection();
			PinpadCommunication comm = new PinpadCommunication(this.mockedPinpadConnection);

			GertecEx07Request request = new GertecEx07Request();

			request.NumericInputType.Value = GertecEx07NumberFormat.Decimal;
			request.TextInputType.Value = GertecEx07TextFormat.None;
			request.LabelFirstLine.Value = GertecEx07MessageInFirstLine.Number;
			request.LabelSecondLine.Value = GertecEx07MessageInSecondLine.GasPump;
			request.MaximumCharacterLength.Value = 1;
			request.MinimumCharacterLength.Value = 3;
			request.TimeOut.Value = 45;
			request.TimeIdle.Value = 0;
			
			Debug.WriteLine(request.LabelFirstLine.Value);
			Debug.WriteLine(request.LabelSecondLine.Value);
			Debug.WriteLine(request.MaximumCharacterLength.Value);
			Debug.WriteLine(request.MinimumCharacterLength.Value);
			Debug.WriteLine(request.TimeOut.Value);
			Debug.WriteLine(request.TimeIdle.Value);

			Debug.WriteLine(request.CommandString);

			GertecEx07Response response = comm.SendRequestAndReceiveResponse<GertecEx07Response>(request);

			Debug.WriteLine("Response status: " + response.RSP_STAT.Value);
			Debug.WriteLine("Value typed: " + response.RSP_RESULT.Value);
		}

		[TestMethod]
		public void MyTestMethod3 ()
		{
			MicroPos.Platform.Desktop.DesktopInitializer.Initialize();

			PinpadConnection conn = new PinpadConnection();

			string portName = PinpadConnection.SearchPinpadPort();

			// Opening connection with specifies serial port:
			conn.Open(portName);

			PinpadCommunication comm = new PinpadCommunication(conn.PlatformPinpadConnection);

			OpnRequest opn = new OpnRequest();
			OpnResponse opnResp = comm.SendRequestAndReceiveResponse<OpnResponse>(opn);
		}
	}
}
