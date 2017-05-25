using Pinpad.Sdk.Model.Exceptions;
using Pinpad.Sdk.Properties;
using System;

namespace Pinpad.Sdk.PinpadProperties.Refactor
{
    /// <summary>
	/// Controller for pinpad command properties with a fixed length.
	/// </summary>
    public sealed class FixedLengthProperty<T> : TextProperty<T>
    {
        // Members
        /// <summary>
        /// Length of the property as string.
        /// </summary>
        public int Length { get; private set; }
        /// <summary>
        /// Responsible for string formatting.
        /// </summary>
        private Func<T, int, string> stringFormatter;
        /// <summary>
        /// Responsible for string reading.
        /// </summary>
        private Func<StringReader, int, T> stringParser;

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
        public FixedLengthProperty(string name, int length, bool isOptional, 
            Func<T, int, string> stringFormatter = null, Func<StringReader, int, T> stringParser = null, 
            string defaultStringValue = null, T value = default(T))
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
            T obj = this.GetValue();

            if (obj == null || obj.Equals(default(T))) { return String.Empty; }

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
                this.Value = default(T);
            }
            else
            {
                T obj;

                try
                {
                    obj = this.stringParser(reader, this.Length);
                }
                catch (Exception)
                {
                    if (this.IsOptional == true)
                    {
                        obj = default(T);
                    }
                    else
                    {
                        throw;
                    }
                }

                this.Value = obj;
            }
        }
    }
}
