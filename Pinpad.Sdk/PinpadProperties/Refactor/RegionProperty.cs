using Pinpad.Sdk.Model.Exceptions;
using Pinpad.Sdk.Properties;
using System;
using System.Collections.Generic;

namespace Pinpad.Sdk.PinpadProperties.Refactor
{
    /// <summary>
    /// Property that represents a region in the command
    /// </summary>
    public sealed class RegionProperty : TextProperty<Nullable<int>>
    {
        // Members
        /// <summary>
        /// Length of the property as string
        /// </summary>
        public int Length { get; private set; }
        /// <summary>
        /// Whether or not the length of this property included on the region length calculation
        /// </summary>
        public bool LengthIncludedOnValue { get; private set; }
        /// <summary>
        /// Contains all property from a region.
        /// </summary>
        private List<IProperty> PropertyCollection { get; set; }
        /// <summary>
        /// Calculates when called, 
        /// Start of the region cannot be set
        /// </summary>
        public override Nullable<int> Value
        {
            get
            {
                int value = 0;
                if (this.LengthIncludedOnValue == true)
                {
                    value += this.Length;
                }

                foreach (IProperty property in this.PropertyCollection)
                {
                    FixedLengthProperty<object> propertyWithLength = property as FixedLengthProperty<object>;

                    if (propertyWithLength != null)
                    {
                        value += propertyWithLength.Length;
                    }
                    else
                    {
                        int propertyLength = 0;

                        if (property is ITextProperty)
                        {
                            string propertyValue = (property as ITextProperty).GetString();

                            if (propertyValue != null)
                            {
                                propertyLength = propertyValue.Length;
                            }
                        }
                        else
                        {
                            byte[] propertyValue = (property as IBinaryProperty).GetBytes();

                            if (propertyValue != null)
                            {
                                propertyLength = propertyValue.Length;
                            }
                        }

                        value += propertyLength;
                    }
                }
                if (this.IsOptional == true && value == 0) { return null; }

                return value;
            }
            set { }
        }
        

        // Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">Name of the property</param>
        /// <param name="length">Length of the property</param>
        /// <param name="lengthIncludedOnValue">Whether or not the length of this property included on the region length calculation</param>
        /// <param name="isOptional">Indicates if this property must exist in the command string or not</param>
        public RegionProperty(string name, int length, bool lengthIncludedOnValue = false, bool isOptional = false)
            : base(name, isOptional)
        {
            this.Length = length;
            this.LengthIncludedOnValue = lengthIncludedOnValue;
            PropertyCollection = new List<IProperty>();
        }

        // Methods
        /// <summary>
        /// Adds a Property to this Region
        /// </summary>
        /// <param name="property"></param>
        public void AddProperty(IProperty property)
        {
            PropertyCollection.Add(property);
        }
        /// <summary>
        /// Gets the value of the property as a String, throws UnsetPropertyException if the value is null
        /// </summary>
        /// <returns>Value of the property as string</returns>
        public override string GetString()
        {
            Nullable<int> obj = this.GetValue();
            if (obj.HasValue == false) { return String.Empty; }

            // Get integer value as string:
            string value = DefaultStringFormatter.IntegerStringFormatter(obj, this.Length);

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
                this.Value = default(Nullable<int>);
            }
            else
            {
                this.Value = DefaultStringParser.IntegerStringParser(reader, Length);
            }
        }
    }
}
