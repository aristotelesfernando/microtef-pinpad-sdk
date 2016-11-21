using MicroPos.CrossPlatform;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pinpad.Sdk.Commands;
using System.Collections.Generic;
using System.Diagnostics;
using Pinpad.Sdk.Properties;
using Pinpad.Sdk.Model;
using System;
using System.Globalization;

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
   //         PinpadConnection conn = PinpadConnection.GetFirst();
			//PinpadFacade facade = new PinpadFacade(conn);
			//TransactionType trnxType = TransactionType.Undefined;

   //         //facade.TransactionService.EmvTable.StartLoadingTables();
   //         //facade.TransactionService.EmvTable.FinishLoadingTables();

   //         facade.TransactionService.EmvTable.UpdatePinpad(false);
   //         CardEntry card = facade.TransactionService.ReadCard(TransactionType.Debit, 0.1m, 
   //             out trnxType);

   //         facade.TransactionService.ReadPassword(0.1m, card.PrimaryAccountNumber, CardType.Emv);

   //         Assert.IsNotNull(card);
		}
        [TestMethod]
        public void MyTestMethod()
        {
            GcrResponse response = new GcrResponse();
            response.CommandString = @"GCR00034200000000061AITAMIR RODRIGUES        =CARVAJAL INFORMACAO LTDA=00000=0000               376026513670145118000=15120000000530651000                                                                                                        00                   00                000                          00000000                   00000000000000";
            
        }

        [TestMethod]
        public void MyTestMethod2 ()
        {
            // do pos
            string emvData = "8A0200008407A00000038800B0820258009505400004000057136033425818367010000D25036000478080032F9C01009F10200FA5E1A04080058200000000000000010FED82000000000000000000000000009F1A0200769F26088E7891B195A31C0A9F2701809F360202029F3704DAA249999A031611179F02060000000000109F03060000000000005F2A0209865F24032503319F34030203009F150200009F3303E0F0E85F280200769F3501229B02E8009F0D05FCF8FCA8009F0E0500000000009F0F05FC78FCF8008E0C00000000000000000203000050035452459F0B1C303230322F3530353835313736202020202020202020202020202020";
            // do micropos
            string emvData2 = "82025800950540000400009A031611179C01005F2A0209869F02060000000000109F0B1C303230332F35303538353137362020202020202020202020202020209F10200FA5E1A04180058200000000000000010FED82000000000000000000000000009F1A0200769F2608B6F30BAC60861B3B9F2701809F3303E0F0E89F34030203009F360202039F3704DF2402E1";
            // do pos qnd enviado
            string emvData3 = "5F2A02098682025800950500000480009A031611179C01009F02060000000000109F10200FA5E1A24080048200000000000000010FED82000000000000000000000000009F1A0200769F2608CFFB6645DFD140099F2701809F34030203009F360200BE9F3303E0F8C89F3704E57BEE7F9F1E0832333939333433379F0B1C303042452F3631373136383934202020202020202020202020202020";
            this.GetValueFromEmvData(emvData3);

        }
        private void GetValueFromEmvData(string emvData)
        {
            string data = emvData;

            for (int i = 0; i < data.Length; /*nothing here, the increment is dynamic*/)
            {
                try
                {
                    // Gets the key
                    string key = data.Substring(i, 2);
                    i += 2;

                    // Verifies whether all bits from a byte are on or not.
                    if (this.AreAllBitsOn(key) == true)
                    {
                        // Adding an extra byte for the TAG field:
                        key += data.Substring(i, 2);
                        i += 2;
                    }

                    // Gets the value
                    string len = data.Substring(i, 2);
                    i += 2;
                    int length = Int32.Parse(len, NumberStyles.HexNumber);

                    if (length > 127)
                    {
                        // More than 1 byte for lenth
                        int bytesLength = length - 128;

                        len = data.Substring(i, bytesLength * 2);
                        i += bytesLength * 2;
                        length = Int32.Parse(len, NumberStyles.HexNumber);
                    }

                    length *= 2;

                    Debug.WriteLine("{0} = {1}", key, data.Substring(i, length));

                    i += length;
                }
                catch (ArgumentOutOfRangeException e)
                {
                    throw new IndexOutOfRangeException("Error processing field", e);
                }
                catch (IndexOutOfRangeException e)
                {
                    throw new IndexOutOfRangeException("Error processing field", e);
                }
            }
        }
        private bool AreAllBitsOn(string key)
        {
            return (Int32.Parse(key, NumberStyles.HexNumber) & 0x1F) == 0x1F;
        }
    }
}
