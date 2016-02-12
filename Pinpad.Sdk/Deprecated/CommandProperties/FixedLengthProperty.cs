using PinPadSDK.Enums;
using StonePortableUtils.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PinPadSDK.Exceptions;

namespace PinPadSDK.Property {
    /// <summary>
    /// Controller for PinPad command properties with a fixed length
    /// </summary>
    internal class PinPadFixedLengthPropertyController<type> : SimpleProperty<type> {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">Name of the property</param>
        /// <param name="length">Length of the property</param>
        /// <param name="isOptional">Indicates if this property must exist in the command string or not</param>
        /// <param name="stringFormatter">String formatter to use</param>
        /// <param name="stringParser">String parser to use</param>
        /// <param name="defaultStringValue">Default string value for Optional properties with default value</param>
        /// <param name="value">Initial Value for the property</param>
        internal PinPadFixedLengthPropertyController(string name,
            int length,
            bool isOptional,
            Func<type, int, string> stringFormatter = null,
            Func<StringReader, int, type> stringParser = null,
            string defaultStringValue = null,
            type value = default(type))
            : base(name, isOptional, null, null, defaultStringValue, value) {

            this.Length = length;
            this.stringFormatter = stringFormatter;
            this.stringParser = stringParser;
        }

        /// <summary>
        /// Gets the value of the property as a String, throws UnsetPropertyException if the value is null
        /// </summary>
        /// <returns>Value of the property as string</returns>
        internal override string GetString() {
            type obj = this.GetValue();
            if (obj == null || obj.Equals(default(type))) {
                return String.Empty;
            }

            string value = this.stringFormatter(obj, this.Length);
            if (value.Length != Length) {
                throw new LenghtMismatchException(this.Name + " : \"" + value + "\" is " + value.Length + " long while it should be " + Length + " long.");
            }
            return value;
        }

        /// <summary>
        /// Parses a string into the property Value
        /// </summary>
        /// <param name="reader">string reader</param>
        internal override void ParseString(StringReader reader) {
            if (reader.Remaining < this.Length && this.IsOptional == true) {
                this.Value = default(type);
            }
            else {
                type obj = this.stringParser(reader, this.Length);
                this.Value = obj;
            }
        }

        /// <summary>
        /// Length of the property as string
        /// </summary>
        internal int Length {
            get;
            private set;
        }

        private Func<type, int, string> stringFormatter;

        private Func<StringReader, int, type> stringParser;
    }
}
