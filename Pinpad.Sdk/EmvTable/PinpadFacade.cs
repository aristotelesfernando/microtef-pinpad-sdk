using MicroPos.CrossPlatform;
using Pinpad.Core.Pinpad;
using Pinpad.Sdk.Connection;

namespace Pinpad.Sdk.EmvTable
{
	public class PinpadFacade : IPinpadFacade
	{
		/// <summary>
		/// Controller for Stone Connection adapter.
		/// </summary>
		private PinpadConnection PinpadConnection;
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
		/// Controller for Stone Secure Command.
		/// </summary>
		public PinpadEncryption Encryption { get; set; }

		/// <summary>
		/// Creates all pinpad adapters.
		/// </summary>
		/// <param name="pinpadConnection">Pinpad connection.</param>
		public PinpadFacade(IPinpadConnection pinpadConnection)
		{
			this.PinpadConnection = new PinpadConnection();
			this.PinpadConnection.PlatformPinpadConnection = pinpadConnection;

			this.Communication = new PinpadCommunication(this.PinpadConnection.PlatformPinpadConnection);
			this.Infos = new PinpadInfos(this.Communication);
			this.Keyboard = new PinpadKeyboard(this.Communication, this.Infos);
			this.Display = new PinpadDisplay(this.Communication);
			this.Storage = new PinpadStorage(this.Communication);
			this.Table = new PinpadTable(this.Communication);
			this.Printer = new PinpadPrinter(this.Communication, this.Infos);
		}
	}
}
