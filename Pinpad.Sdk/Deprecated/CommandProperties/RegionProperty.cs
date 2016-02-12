using PinPadSDK.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PinPadSDK.Exceptions;

namespace PinPadSDK.Property {
    /// <summary>
    /// Property that represents a region in the command
    /// </summary>
    internal class RegionProperty : SimpleProperty<Nullable<int>> {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">Name of the property</param>
        /// <param name="length">Length of the property</param>
        /// <param name="lengthIncludedOnValue">Whether or not the length of this property included on the region length calculation</param>
        /// <param name="isOptional">Indicates if this property must exist in the command string or not</param>
        internal RegionProperty(string name, int length, bool lengthIncludedOnValue = false, bool isOptional = false) :
            base(name, isOptional) {
            this.Length = length;
            this.LengthIncludedOnValue = lengthIncludedOnValue;
            PropertyCollection = new List<IProperty>();
        }

        private List<IProperty> PropertyCollection {
            get;
            set;
        }

        /// <summary>
        /// Adds a Property to this Region
        /// </summary>
        /// <param name="property"></param>
        internal void AddProperty(IProperty property) {
            PropertyCollection.Add(property);
        }

        /// <summary>
        /// Gets the value of the property as a String, throws UnsetPropertyException if the value is null
        /// </summary>
        /// <returns>Value of the property as string</returns>
        internal override string GetString() {
            Nullable<int> obj = this.GetValue();
            if (obj == null) {
                return String.Empty;
            }

            string value = DefaultStringFormatter.IntegerStringFormatter(obj, this.Length);
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
                this.Value = default(Nullable<int>);
            }
            else {
                this.Value = DefaultStringParser.IntegerStringParser(reader, Length);
            }
        }

        /// <summary>
        /// Length of the property as string
        /// </summary>
        internal int Length {
            get;
            private set;
        }

        /// <summary>
        /// Whether or not the length of this property included on the region length calculation
        /// </summary>
        internal bool LengthIncludedOnValue {
            get;
            private set;
        }

        /// <summary>
        /// Calculates when called, 
        /// Start of the region cannot be set
        /// </summary>
        internal override Nullable<int> Value {
            get {
                int value = 0;
                if (this.LengthIncludedOnValue == true) {
                    value += this.Length;
                }
                foreach (IProperty property in PropertyCollection) {

                    PinPadFixedLengthPropertyController<object> propertyWithLength = property as PinPadFixedLengthPropertyController<object>;
                    if (propertyWithLength != null) {
                        value += propertyWithLength.Length;
                    }
                    else {
                        string propertyValue = property.GetString( );
                        if (propertyValue != null) {
                            value += propertyValue.Length;
                        }
                    }
                }
                if (this.IsOptional == true && value == 0) {
                    return null;
                }
                else {
                    return value;
                }
            }
            set { }
        }
    }
}
