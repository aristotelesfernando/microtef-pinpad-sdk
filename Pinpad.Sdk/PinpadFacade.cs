using System;
using MicroPos.CrossPlatform;
using Pinpad.Sdk.Pinpad;

namespace Pinpad.Sdk
{
	public class PinpadFacade : IPinpadFacade
	{
		/// <summary>
		/// Controller for Stone Connection adapter.
		/// </summary>
		private PinpadConnection pinpadConnection;
		/// <summary>
		/// Controller for Stone Connection adapter.
		/// It's set method updates the pinpad facade properties based on the new connection set.
		/// </summary>
		public PinpadConnection PinpadConnection
		{
			get { return this.pinpadConnection; }
			set
			{
				this.pinpadConnection = value;

				this.Communication = new PinpadCommunication(this.PinpadConnection);
				this.Infos = new PinpadInfos(this.Communication);
				this.Keyboard = new PinpadKeyboard(this.Communication, this.Infos);
				this.Display = new PinpadDisplay(this.Communication);
				this.Storage = new PinpadStorage(this.Communication);
				this.Table = new PinpadTable(this.Communication);
				this.Printer = new PinpadPrinter(this.Communication, this.Infos);
				this.TransactionService = new PinpadTransaction(this.Communication);
			}
		}
		/// <summary>
		/// Gets the default Communication adapter.
		/// </summary>
		public PinpadCommunication Communication { get; set; }
		/// <summary>
		/// Gets the default Keyboard adapter.
		/// </summary>
		public PinpadKeyboard Keyboard { get; set; }
		/// <summary>
		/// Gets the default Display adapter
		/// </summary>
		public PinpadDisplay Display { get; set; }
		/// <summary>
		/// Gets the default Printer adapter
		/// </summary>
		public PinpadPrinter Printer { get; set; }
		/// <summary>
		/// Gets the default Storage adapter
		/// </summary>
		public PinpadStorage Storage { get; set; }
		/// <summary>
		/// Gets the default Table adapter
		/// </summary>
		public PinpadTable Table { get; set; }
		/// <summary>
		/// Gets the default Infos adapter
		/// </summary>
		public PinpadInfos Infos { get; set; }
		/// <summary>
		/// Responsible for transaction operations.
		/// </summary>
		public PinpadTransaction TransactionService { get; set; }

		/// <summary>
		/// Creates all pinpad adapters.
		/// </summary>
		/// <param name="pinpadConnection">Pinpad connection.</param>
		public PinpadFacade(IPinpadConnection pinpadConnection)
		{
			this.PinpadConnection = new PinpadConnection(pinpadConnection);
		}
		/// <summary>
		/// Creates all pinpad adapters.
		/// </summary>
		/// <param name="pinpadConnection">Pinpad connection.</param>
		public PinpadFacade (PinpadConnection pinpadConnection)
		{
			this.PinpadConnection = pinpadConnection;
		}
	}
}
