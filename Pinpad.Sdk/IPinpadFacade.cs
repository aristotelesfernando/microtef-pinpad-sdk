using Pinpad.Sdk.Model;

namespace Pinpad.Sdk
{
	/// <summary>
	/// Contains the access to each pinpad component, i. e. keyboard, display, terminal information and so forth.
	/// </summary>
	public interface IPinpadFacade
	{
		/// <summary>
		/// Controller for Stone Connection adapter.
		/// </summary>
		PinpadConnection Connection { get; set; }
		/// <summary>
		/// Gets the default Communication adapter.
		/// </summary>
		PinpadCommunication Communication { get; set; }
		/// <summary>
		/// Responsible for authorization operations.
		/// </summary>
		PinpadTransaction TransactionService { get; set; }
		/// <summary>
		/// Gets the default Keyboard adapter
		/// </summary>
		IPinpadKeyboard Keyboard { get; set; }
		/// <summary>
		/// Gets the default Display adapter
		/// </summary>
		IPinpadDisplay Display { get; set; }
		/// <summary>
		/// Gets the default Infos adapter
		/// </summary>
		IPinpadInfos Infos { get; set; }
	}
}
