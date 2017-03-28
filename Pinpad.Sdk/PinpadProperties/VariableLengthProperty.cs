using Pinpad.Sdk.Model.Exceptions;
using System;

namespace Pinpad.Sdk.Properties
{
	/// <summary>
	/// Controller for PinPad command properties with the length variable
	/// </summary>
	public sealed class VariableLengthProperty<type> : SimpleProperty<type>
	{
		// Members
		/// <summary>
		/// Ratio of the length value
		/// Hexadecimal value's length are calculated with the bytes, so every 2 chars will be considered 1 byte and therefore a 16 char long string would have a length of 8, therefore it requires a ratio of 50% or 0.5
		/// </summary>
		public float LengthRatio { get; private set; }
		/// <summary>
		/// Is the string property padded to fill the Max Length?
		/// </summary>
		public bool IsPadded { get; private set; }
		/// <summary>
		/// Length of the property header
		/// </summary>
		public int HeaderLength { get; private set; }
		/// <summary>
		/// Maximum Length of the property as string
		/// </summary>
		public int MaxLength { get; private set; }
		/// <summary>
		/// Responsible for string parsing.
		/// </summary>
		private Func<StringReader, int, type> stringParser;

		// Constructor
		/// <summary>
		/// Constructor with values.
		/// </summary>
		/// <param name="name">Name of the property</param>
		/// <param name="headerLength">Length of the property header</param>
		/// <param name="maxLength">Maximum Length of the property</param>
		/// <param name="lengthRatio">Ratio of the length value</param>
		/// <param name="isPadded">Indicates if this property should pad to fill the max length</param>
		/// <param name="isOptional">Indicates if this property must exist in the command string or not</param>
		/// <param name="stringFormatter">String formatter to use</param>
		/// <param name="stringParser">String parser to use</param>
		/// <param name="defaultStringValue">Default string value for Optional properties with default value</param>
		/// <param name="value">Initial Value for the property</param>
		public VariableLengthProperty(string name, int headerLength, int maxLength, float lengthRatio, bool isPadded, bool isOptional, Func<type, string> stringFormatter = null, Func<StringReader, int, type> stringParser = null, string defaultStringValue = null, type value = default(type))
			: base(name, isOptional, stringFormatter, null, defaultStringValue, value)
		{
			this.HeaderLength = headerLength;
			this.MaxLength = maxLength;
			this.LengthRatio = lengthRatio;
			this.IsPadded = isPadded;
			this.stringParser = stringParser;
		}

		// Methods
		/// <summary>
		/// Gets the value of the property as a String, throws UnsetPropertyException if the value is null
		/// </summary>
		/// <returns>Value of the property as string</returns>
		public override string GetString()
		{
			string objString = base.GetString();

			if (objString == null)
			{
				if (this.IsPadded) { objString = String.Empty; }

				else
				{
					if (this.IsOptional == true)
					{
						return DefaultStringFormatter.IntegerStringFormatter(0, this.HeaderLength);
					}
					else
					{
						throw new UnsetPropertyException(this.Name);
					}
				}
			}
			else if (objString.Length > this.MaxLength)
			{
				throw new LenghtMismatchException(this.Name + " : \"" + objString + "\" is " + objString.Length + " long while it should be under " + this.MaxLength + " long.");
			}

			string header;
			int length;
			if (objString == this.DefaultStringValue)
			{
				length = 0;
			}
			else
			{
				length = Convert.ToInt32(objString.Length * this.LengthRatio);
			}
			header = DefaultStringFormatter.IntegerStringFormatter(length, this.HeaderLength);

			if (header.Length != this.HeaderLength)
			{
				throw new LenghtMismatchException(this.Name + "LEN : \"" + header + "\" is " + header.Length + " long while it should be " + this.HeaderLength + " long.");
			}

			if (this.IsPadded)
			{
				objString = objString.PadRight(this.MaxLength, ' ');
			}

			return header + objString;
		}
		/// <summary>
		/// Parses a string into the property Value
		/// </summary>
		/// <param name="reader">string reader</param>
		public override void ParseString(StringReader reader)
		{
			int realLength = DefaultStringParser.IntegerStringParser(reader, this.HeaderLength).Value;
			int length = Convert.ToInt32(realLength / this.LengthRatio);

			if (length == 0 || (reader.Remaining < length && this.IsOptional == true))
			{
				this.Value = default(type);
			}
			else
			{
				type obj = this.stringParser(reader, length);
				this.Value = obj;
			}

			if (this.IsPadded == true)
			{
				reader.Jump(this.MaxLength - length);
			}
		}

	}
}
