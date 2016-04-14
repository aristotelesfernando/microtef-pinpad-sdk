/* WARNING!
 * 
 * DEPRECATED.
 * MUST BE REFACTORED.
 * 
 */

namespace Pinpad.Sdk.Properties 
{
	/// <summary>
	/// Interface for PinPad command properties
	/// </summary>
	public interface IProperty 
	{
		// Members
		/// <summary>
		/// Indicates if the property is null or not
		/// </summary>
		bool HasValue { get; }
		/// <summary>
		/// Indicates if this property must exist in the command string or not
		/// </summary>
		bool IsOptional { get; }
		/// <summary>
		/// Name of this property
		/// </summary>
		string Name { get; }
		
		// Methods
		/// <summary>
		/// Gets the value of the property as a String, throws UnsetPropertyException if the value is null
		/// </summary>
		/// <returns>Value of the property as string</returns>
		string GetString();
		/// <summary>
		/// Parses a string into the property Value
		/// </summary>
		/// <param name="reader">string reader</param>
		void ParseString(StringReader reader);
	}
}
