using Pinpad.Sdk.Model.Exceptions;
using Pinpad.Sdk.PinpadProperties.Refactor.Formatter;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pinpad.Sdk.PinpadProperties.Refactor.Property
{
    /// <summary>
	/// Controller for PinPad properties with a list of items
	/// </summary>
	/// <typeparam name="type">data type to hold</typeparam>
	public sealed class VariableLengthCollectionProperty<T> : TextProperty<List<T>>
    {
        // Members
        private List<T> Collection { get; set; }
        /// <summary>
        /// Length of the property header
        /// </summary>
        public int HeaderLength { get; private set; }
        /// <summary>
        /// Minimum amount of elements
        /// </summary>
        public int MinElementCount { get; private set; }
        /// <summary>
        /// Length of each property element
        /// </summary>
        public int ElementMaxLength { get; private set; }
        /// <summary>
        /// Override of the value of the property
        /// Prevents the collection from becoming null
        /// </summary>
        public override List<T> Value
        {
            get { return this.Collection; }
            set
            {
                if (value != null) { this.Collection = value; }
                else { this.Collection.Clear(); }
            }
        }
        /// <summary>
        /// Responsible for string formatting.
        /// </summary>
        private Func<T, string> stringFormatter;
        /// <summary>
        /// Responsible for parsing strings.
        /// </summary>
        private Func<StringReader, T> stringParser;

        // Constructor
        /// <summary>
        /// Contructor with values.
        /// </summary>
        /// <param name="name">Name of the property</param>
        /// <param name="headerLength">Length of the list header</param>
        /// <param name="minElementCount">Minimum amount of elements</param>
        /// <param name="elementMaxLength">Maximum length of each list element</param>
        /// <param name="stringFormatter">Element string formatter</param>
        /// <param name="stringParser">Element string parser</param>
        public VariableLengthCollectionProperty(string name, int headerLength, int minElementCount, 
            int elementMaxLength, Func<T, string> stringFormatter = null, 
            Func<StringReader, T> stringParser = null)
            : base(name)
        {
            this.HeaderLength = headerLength;
            this.MinElementCount = minElementCount;
            this.ElementMaxLength = elementMaxLength;
            this.stringFormatter = stringFormatter;
            this.stringParser = stringParser;

            this.Collection = new List<T>();
        }

        // Methods
        /// <summary>
        /// Gets the value of the property as a String, throws UnsetPropertyException if the value is null
        /// </summary>
        /// <returns>Value of the property as string</returns>
        public override string GetString()
        {
            if (this.Collection.Count < this.MinElementCount)
            {
                throw new UnsetPropertyException(this.Name + " : Minimum of " + this.MinElementCount + " elements required. Currently have " + this.Collection.Count);
            }

            string headerString = StringFormatter.IntegerStringFormatter(this.Collection.Count, this.HeaderLength);

            if (headerString.Length != this.HeaderLength)
            {
                throw new LenghtMismatchException(this.Name + "CNT : \"" + headerString + "\" is " + headerString.Length + " long while it should be " + this.HeaderLength + " long.");
            }

            StringBuilder stringBuilder = new StringBuilder(headerString);

            foreach (T element in this.Collection)
            {
                string elementString = this.stringFormatter(element);

                if (elementString.Length > this.ElementMaxLength)
                {
                    throw new LenghtMismatchException(this.Name + " : \"" + elementString + "\" is " + 
                        elementString.Length + " long while it should be under " + this.ElementMaxLength + 
                        " long.");
                }

                stringBuilder.Append(elementString);
            }

            return stringBuilder.ToString();
        }
        /// <summary>
        /// Parses a string into the property Value
        /// </summary>
        /// <param name="reader">string reader</param>
        public override void ParseString(StringReader reader)
        {
            this.Collection.Clear();

            int elementCount = reader.ReadInt(this.HeaderLength);
            for (int i = 0; i < elementCount; i++)
            {
                T element = this.stringParser(reader);
                this.Collection.Add(element);
            }
        }
    }
}
