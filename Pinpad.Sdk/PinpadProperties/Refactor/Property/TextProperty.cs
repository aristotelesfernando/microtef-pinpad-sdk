using System;
using Pinpad.Sdk.Model.Exceptions;

namespace Pinpad.Sdk.PinpadProperties.Refactor.Property
{
    public class TextProperty<T> : ITextProperty
    {
        // Members
        /// <summary>
        /// Indicates if the property is null or not
        /// </summary>
        public bool HasValue
        {
            get { return this.Value != null && this.Value.Equals(default(T)) == false; }
        }
        /// <summary>
        /// Indicates if this property must exist in the command string or not
        /// </summary>
        public bool IsOptional { get; private set; }
        /// <summary>
        /// Name of this property
        /// </summary>
        public string Name { get; private set; }
        /// <summary>
        /// Default string value for when the actual value is the default of it's type
        /// </summary>
        protected string DefaultStringValue { get; set; }
        /// <summary>
        /// Property value.
        /// </summary>
        private T value { get; set; }
        /// <summary>
        /// Property value (controller).
        /// </summary>
        public virtual T Value
        {
            get { return this.value; }
            set
            {
                T oldValue = this.value;
                this.value = value;

                try { GetString(); }
                catch (Exception ex)
                {
                    this.value = oldValue;

                    if (value == null)
                    {
                        throw new InvalidValueException(this.Name + " : Null value was not supported.", ex);
                    }
                    else
                    {
                        throw new InvalidValueException(this.Name + " : Value \"" + value.ToString() + "\" was not supported.", ex);
                    }
                }
            }
        }
        /// <summary>
        /// Responsible for formatting strings.
        /// </summary>
        private Func<T, string> stringFormatter;
        /// <summary>
        /// Responsible for parsing strings.
        /// </summary>
        private Func<StringReader, T> stringParser;

        // Constructor
        /// <summary>
        /// Constructor that sets default values.
        /// </summary>
        /// <param name="name">Name of the property</param>
        /// <param name="isOptional">Indicates if this property must exist in the command string or not</param>
        /// <param name="stringFormatter">String formatter to use</param>
        /// <param name="stringParser">String parser to use</param>
        /// <param name="defaultStringValue">Default string value for Optional properties with default value</param>
        /// <param name="value">Initial Value for the property</param>
        public TextProperty(string name, bool isOptional = false, Func<T, string> stringFormatter = null, 
            Func<StringReader, T> stringParser = null, string defaultStringValue = null, 
            T value = default(T))
        {
            this.Name = name;
            this.IsOptional = isOptional;
            this.stringFormatter = stringFormatter;
            this.stringParser = stringParser;
            this.DefaultStringValue = defaultStringValue;
            this.value = value;
        }

        // Methods
        /// <summary>
        /// Gets the value of the property, throws UnsetPropertyException if the value is null
        /// </summary>
        /// <returns>Value of the property</returns>
        public T GetValue()
        {
            if (this.HasValue == false && this.IsOptional == false)
            {
                throw new UnsetPropertyException(this.Name);
            }

            return Value;
        }
        /// <summary>
        /// Gets the value of the property, throws UnsetPropertyException if the value is null
        /// </summary>
        /// <returns>Value of the property</returns>
        public propertyType GetValueAs<propertyType>()
            where propertyType : class
        {
            return this.GetValue() as propertyType;
        }
        /// <summary>
        /// Gets the value of the property as a String, throws UnsetPropertyException if the value is null
        /// </summary>
        /// <returns>Value of the property as string</returns>
        public virtual string GetString()
        {
            T obj = this.GetValue();

            if (obj == null || obj.Equals(default(T)))
            {
                return this.DefaultStringValue;
            }

            return this.stringFormatter(obj);
        }
        /// <summary>
        /// Parses a string into the property Value
        /// </summary>
        /// <param name="reader">string reader</param>
        public virtual void ParseString(StringReader reader)
        {
            if (reader.IsOver == true && this.IsOptional == true)
            {
                this.Value = default(T);
            }
            else
            {
                T value = this.stringParser(reader);
                this.Value = value;
            }
        }
    }
}
