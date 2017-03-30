using Pinpad.Sdk.Model;
using Pinpad.Sdk.Model.Pinpad;

namespace Pinpad.Sdk
{
	/// <summary>
	/// Contains the access to each pinpad component, i. e. keyboard, display, terminal information and so forth.
	/// </summary>
	public interface IPinpadFacade
	{
		/// <summary>
		/// Gets the default Communication adapter.
		/// </summary>
		IPinpadCommunication Communication { get; }
		/// <summary>
		/// Responsible for authorization operations.
		/// </summary>
		PinpadTransaction TransactionService { get; }
		/// <summary>
		/// Gets the default Keyboard adapter
		/// </summary>
		IPinpadKeyboard Keyboard { get; }
		/// <summary>
		/// Gets the default Display adapter
		/// </summary>
		IPinpadDisplay Display { get; }
		/// <summary>
		/// Gets the default Infos adapter
		/// </summary>
		IPinpadInfos Infos { get; }
        /// <summary>
        /// Gets the default pinpad printer adapter.
        /// </summary>
        IPinpadPrinter Printer { get; }
	}
}
