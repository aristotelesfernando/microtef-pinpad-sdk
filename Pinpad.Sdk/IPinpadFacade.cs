using MicroPos.CrossPlatform;
using Pinpad.Sdk.Pinpad;

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
		/// Gets the default Keyboard adapter
		/// </summary>
		PinpadKeyboard Keyboard { get; set; }
		/// <summary>
		/// Gets the default Display adapter
		/// </summary>
		PinpadDisplay Display { get; set; }
		/// <summary>
		/// Gets the default Infos adapter
		/// </summary>
		PinpadInfos Infos { get; set; }
	}
}
