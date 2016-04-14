using Pinpad.Sdk.Pinpad;

namespace Pinpad.Sdk
{
	public interface IPinpadFacade
	{
		/// <summary>
		/// Gets the default Communication adapter.
		/// </summary>
		PinpadCommunication Communication { get; set; }
		/// <summary>
		/// Gets the default Keyboard adapter
		/// </summary>
		PinpadKeyboard Keyboard { get; set; }
		/// <summary>
		/// Gets the default Display adapter
		/// </summary>
		PinpadDisplay Display { get; set; }
		/// <summary>
		/// Gets the default Printer adapter
		/// </summary>
		PinpadPrinter Printer { get; set; }
		/// <summary>
		/// Gets the default Storage adapter
		/// </summary>
		PinpadStorage Storage { get; set; }
		/// <summary>
		/// Gets the default Table adapter
		/// </summary>
		PinpadTable Table { get; set; }
		/// <summary>
		/// Gets the default Infos adapter
		/// </summary>
		PinpadInfos Infos { get; set; }
		/// <summary>
		/// Responsible for transaction operations.
		/// </summary>
		PinpadTransaction TransactionService { get; set; }
	}
}
