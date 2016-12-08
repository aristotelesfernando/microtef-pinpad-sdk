using System;
using MicroPos.CrossPlatform;
using Pinpad.Sdk.Model;
using Pinpad.Sdk.Pinpad;

namespace Pinpad.Sdk
{
	/// <summary>
	/// Contains the access to each pinpad component, i. e. keyboard, display, terminal information and so forth.
	/// </summary>
	public class PinpadFacade : IPinpadFacade
	{
		/// <summary>
		/// Controller for Stone Connection adapter.
		/// </summary>
		private PinpadConnection pinpadConnection;
		/// <summary>
		/// Controller for Stone Connection adapter.
		/// It's set method updates the pinpad facade properties based on the new connection.
		/// </summary>
		internal PinpadConnection Connection
		{
			get { return this.pinpadConnection; }
			set
			{
				this.pinpadConnection = value;

				this.Communication = new PinpadCommunication(this.Connection);
				this.Infos = new PinpadInfos(this.Communication);
				this.Display = new PinpadDisplay(this.Communication);
                this.Keyboard = new PinpadKeyboard(this.Communication, this.Infos, this.Display);
                this.TransactionService = new PinpadTransaction(this.Communication);
                this.Printer = new PinpadPrinter(this.Communication);
			}
		}
		/// <summary>
		/// Gets the default Communication adapter.
		/// </summary>
		public PinpadCommunication Communication { get; internal set; }
		/// <summary>
		/// Responsible for authorization operations.
		/// </summary>
		public PinpadTransaction TransactionService { get; internal set; }
		/// <summary>
		/// Gets the default Keyboard adapter.
		/// </summary>
		public IPinpadKeyboard Keyboard { get; internal set; }
		/// <summary>
		/// Gets the default Display adapter
		/// </summary>
		public IPinpadDisplay Display { get; internal set; }
		/// <summary>
		/// Gets the default Infos adapter
		/// </summary>
		public IPinpadInfos Infos { get; internal set; }
        // TODO: Documentar.
        public IPinpadPrinter Printer { get; internal set; }


        /// <summary>
        /// Creates all pinpad adapters.
        /// </summary>
        /// <param name="pinpadConnection">Pinpad connection.</param>
        public PinpadFacade(IPinpadConnection pinpadConnection)
		{
			this.Connection = new PinpadConnection(pinpadConnection);
		}
		/// <summary>
		/// Creates all pinpad adapters.
		/// </summary>
		/// <param name="pinpadConnection">Pinpad connection.</param>
		public PinpadFacade (PinpadConnection pinpadConnection)
		{
			this.Connection = pinpadConnection;
		}
	}
}
