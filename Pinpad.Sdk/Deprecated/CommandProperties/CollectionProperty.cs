﻿using PinPadSDK.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PinPadSDK.Exceptions;

namespace PinPadSDK.Property {
    /// <summary>
    /// Controller for PinPad properties with a list of items
    /// </summary>
    /// <typeparam name="type">data type to hold</typeparam>
    internal class PinPadCollectionPropertyController<type> : SimpleProperty<List<type>> {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">Name of the property</param>
        /// <param name="headerLength">Length of the list header</param>
        /// <param name="minElementCount">Minimum amount of elements</param>
        /// <param name="elementLength">Length of each list element</param>
        /// <param name="stringFormatter">Element string formatter</param>
        /// <param name="stringParser">Element string parser</param>
        internal PinPadCollectionPropertyController(string name,
            int headerLength, 
            int minElementCount,
            int elementLength,
            Func<type, int, string> stringFormatter = null,
            Func<StringReader, int, type> stringParser = null)
            : base(name) {
            this.HeaderLength = headerLength;
            this.MinElementCount = minElementCount;
            this.ElementLength = elementLength;
            this.stringFormatter = stringFormatter;
            this.stringParser = stringParser;

            this.Collection = new List<type>();
        }

        /// <summary>
        /// Gets the value of the property as a String, throws UnsetPropertyException if the value is null
        /// </summary>
        /// <returns>Value of the property as string</returns>
        internal override string GetString() {
            if (this.Collection.Count < this.MinElementCount) {
                throw new UnsetPropertyException(this.Name + " : Minimum of " + this.MinElementCount + " elements required. Currently have " + this.Collection.Count);
            }

            string headerString = DefaultStringFormatter.IntegerStringFormatter(this.Collection.Count, this.HeaderLength);
            if (headerString.Length != this.HeaderLength) {
                throw new LenghtMismatchException(this.Name + "CNT : \"" + headerString + "\" is " + headerString.Length + " long while it should be " + this.HeaderLength + " long.");
            }

            StringBuilder stringBuilder = new StringBuilder(headerString);
            foreach (type element in this.Collection) {
                string elementString = this.stringFormatter(element, this.ElementLength);
                if (elementString.Length != this.ElementLength) {
                    throw new LenghtMismatchException(this.Name + " : \"" + elementString + "\" is " + elementString.Length + " long while it should be " + this.ElementLength + " long.");
                }
                stringBuilder.Append(elementString);
            }
            return stringBuilder.ToString();
        }

        /// <summary>
        /// Parses a string into the property Value
        /// </summary>
        /// <param name="reader">string reader</param>
        internal override void ParseString(StringReader reader) {
            this.Collection.Clear();

            int elementCount = reader.ReadInt(this.HeaderLength);
            for (int i = 0; i < elementCount; i++) {
                type element = this.stringParser(reader, this.ElementLength);
                this.Collection.Add(element);
            }
        }

        private List<type> Collection { get; set; }

        /// <summary>
        /// Length of the property header
        /// </summary>
        internal int HeaderLength { get; private set; }

        /// <summary>
        /// Minimum amount of elements
        /// </summary>
        internal int MinElementCount { get; private set; }

        /// <summary>
        /// Length of each property element
        /// </summary>
        internal int ElementLength { get; private set; }

        /// <summary>
        /// Override of the value of the property
        /// Prevents the collection from becoming null
        /// </summary>
        internal new List<type> Value {
            get { return this.Collection; }
            set {
                if (value != null) {
                    this.Collection = value;
                }
                else {
                    this.Collection.Clear();
                }
            }
        }

        private Func<type, int, string> stringFormatter;

        private Func<StringReader, int, type> stringParser;
    }
}
