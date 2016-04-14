using Pinpad.Sdk.Model.Exceptions;
using System;

/* WARNING!
 * 
 * DEPRECATED.
 * MUST BE REFACTORED.
 * 
 */

namespace Pinpad.Sdk.Properties 
{
	/// <summary>
	/// Controller for PinPad command properties with a fixed length
	/// </summary>
	public class PinpadFixedLengthProperty<type> : SimpleProperty<type> 
	{
		// Members
		/// <summary>
		/// Length of the property as string.
		/// </summary>
		public int Length { get; private set; }
		/// <summary>
		/// Responsible for string formatting.
		/// </summary>
		private Func<type, int, string> stringFormatter;
		/// <summary>
		/// Responsible for string reading.
		/// </summary>
		private Func<StringReader, int, type> stringParser;

		// Constructor
		/// <summary>
		/// Constructor with values.
		/// </summary>
		/// <param name="name">Property name.</param>
		/// <param name="length">Length of the property.</param>
		/// <param name="isOptional">Indicates if this property must exist in the command string or not.</param>
		/// <param name="stringFormatter">String formatter to use.</param>
		/// <param name="stringParser">String parser to use.</param>
		/// <param name="defaultStringValue">Default string value for Optional properties with default value.</param>
		/// <param name="value">Initial Value for the property.</param>
		public PinpadFixedLengthProperty(string name, int length, bool isOptional, Func<type, int, string> stringFormatter = null, Func<StringReader, int, type> stringParser = null, string defaultStringValue = null, type value = default(type))
			: base(name, isOptional, null, null, defaultStringValue, value)
		{
			this.Length = length;
			this.stringFormatter = stringFormatter;
			this.stringParser = stringParser;
		}

		// Methods
		/// <summary>
		/// Gets the value of the property as a String, throws UnsetPropertyException if the value is null
		/// </summary>
		/// <returns>Value of the property as string</returns>
		public override string GetString()
		{
			type obj = this.GetValue();
			
			if (obj == null || obj.Equals(default(type))) { return String.Empty; }

			// Formatting obl into string:
			string value = this.stringFormatter(obj, this.Length);

			// Validating length:
			if (value.Length != Length)
			{
				throw new LenghtMismatchException(this.Name + " : \"" + value + "\" is " + value.Length + " long while it should be " + Length + " long.");
			}

			return value;
		}
		/// <summary>
		/// Parses a string into the property Value
		/// </summary>
		/// <param name="reader">string reader</param>
		public override void ParseString(StringReader reader)
		{
			if (reader.Remaining < this.Length && this.IsOptional == true)
			{
				this.Value = default(type);
			}
			else
			{
				type obj = this.stringParser(reader, this.Length);
				this.Value = obj;
			}
		}
	}
}
