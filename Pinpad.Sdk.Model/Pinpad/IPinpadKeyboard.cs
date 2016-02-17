using Pinpad.Sdk.Model.TypeCode;

namespace Pinpad.Sdk.Model
{
	/// <summary>
	/// Pinpad keyboard interface tool.
	/// </summary>
	public interface IPinpadKeyboard
	{
		/// <summary>
		/// Gets the next Key pressed at the Pinpad with the default, safe, method.
		/// Does not retrieve numeric keys.
		/// </summary>
		/// <returns>PinpadKey or Undefined on failure.</returns>
		PinpadKeyCode GetKey();
	}
}
