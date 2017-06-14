using MicroPos.CrossPlatform;
using Pinpad.Sdk.Model;
using Pinpad.Sdk.Model.Exceptions;
using Pinpad.Sdk.Model.Pinpad;
using Pinpad.Sdk.Pinpad;

namespace Pinpad.Sdk
{
    /// <summary>
    /// Contains the access to each pinpad component, i. e. keyboard, display, terminal information and so forth.
    /// </summary>
    public sealed class PinpadFacade : IPinpadFacade
	{
		/// <summary>
		/// Controller for Stone Connection adapter.
		/// </summary>
		private IPinpadConnection pinpadConnection;
		/// <summary>
		/// Controller for Stone Connection adapter.
		/// It's set method updates the pinpad facade properties based on the new connection.
		/// </summary>
		internal IPinpadConnection Connection
		{
			get { return this.pinpadConnection; }
			set
			{
                if (value == null)
                {
                    throw new PinpadNotFoundException();
                }

				this.pinpadConnection = value;

				this.Communication = new PinpadCommunication(this.Connection);
				this.Infos = new PinpadInfos(this.Communication as PinpadCommunication);
				this.Display = new PinpadDisplay(this.Communication as PinpadCommunication);
                this.Keyboard = new PinpadKeyboard(this.Communication as PinpadCommunication, this.Infos, 
                    this.Display);
                this.TransactionService = new PinpadTransaction(this.Communication as PinpadCommunication);
                this.Printer = new IngenicoPinpadPrinter(this.Communication as PinpadCommunication, this.Infos);
                this.UpdateService = new PinpadUpdateService(this.Infos, this.Communication);
			}
		}
		/// <summary>
		/// Gets the default Communication adapter.
		/// </summary>
		public IPinpadCommunication Communication { get; internal set; }
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
        /// <summary>
        /// Adapter for pinpad thermal printer.
        /// </summary>
        public IPinpadPrinter Printer { get; internal set; }
        /// <summary>
        /// Responsible for to update the pinpad embedded application.
        /// </summary>
        public IPinpadUpdateService UpdateService { get; set; }

        /// <summary>
        /// Creates all pinpad adapters.
        /// </summary>
        /// <param name="pinpadConnection">Pinpad connection.</param>
        public PinpadFacade(IPinpadConnection pinpadConnection)
		{
			this.Connection = pinpadConnection;
		}
	}
}
