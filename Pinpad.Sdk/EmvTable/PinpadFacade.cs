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
		public Core.Pinpad.PinpadCommunication Communication { get; set; }
		/// <summary>
		/// Gets the default Keyboard adapter.
		/// </summary>
		public Core.Pinpad.PinpadKeyboard Keyboard { get; set; }
		/// <summary>
		/// Gets the default Display adapter
		/// </summary>
		public Core.Pinpad.PinpadDisplay Display { get; set; }
		/// <summary>
		/// Gets the default Printer adapter
		/// </summary>
		public Core.Pinpad.PinpadPrinter Printer { get; set; }
		/// <summary>
		/// Gets the default Storage adapter
		/// </summary>
		public Core.Pinpad.PinpadStorage Storage { get; set; }
		/// <summary>
		/// Gets the default Table adapter
		/// </summary>
		public PinpadTable Table { get; set; }
		/// <summary>
		/// Gets the default Infos adapter
		/// </summary>
		public Core.Pinpad.PinpadInfos Infos { get; set; }
		/// <summary>
		/// Controller for Stone Secure Command.
		/// </summary>
		public Core.Pinpad.PinpadEncryption Encryption { get; set; }

		/// <summary>
		/// Creates all pinpad adapters.
		/// </summary>
		/// <param name="pinpadConnection">Pinpad connection.</param>
		public PinpadFacade(CrossPlatformBase.IPinPadConnection pinpadConnection)
		{
			this.PinpadConnection = new PinpadConnection();
			this.PinpadConnection.PlatformPinpadConnection = pinpadConnection;

			this.Communication = new PinpadCommunication(this.PinpadConnection.PlatformPinpadConnection);
			this.Keyboard = new PinpadKeyboard(this.Communication);
			this.Display = new PinpadDisplay(this.Communication);
			this.Storage = new PinpadStorage(this.Communication);
			this.Table = PinpadTable.GetInstance(this.PinpadConnection);
			this.Infos = new PinpadInfos(this.Communication);
			this.Printer = new PinpadPrinter(this.Communication, this.Infos.Model);
		}
	}
}
