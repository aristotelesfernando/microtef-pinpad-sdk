using Pinpad.Sdk.Model.Exceptions;
using System;

namespace Pinpad.Sdk.PinpadProperties.Refactor.Property
{
    /// <summary>
    /// Property which it's value is parseable to byte[].
    /// The data it stores can be represented as byte[].
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BinaryProperty<T> : IBinaryProperty
    {
        /// <summary>
        /// Indicates if the property is null or not
        /// </summary>
        public bool HasValue
        {
            get
            {
               return this.Value != null && this.Value.Equals(default(T)) == false; 
            }
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
        /// Gets the value of the property as a byte[], throws UnsetPropertyException if the value is null
        /// </summary>
        /// <returns>Value of the property as byte[]</returns>
        public byte[] GetBytes()
        {
            T obj = this.GetValue();

            if(obj is byte[])
            {
                return obj as byte[];
            }

            return null;
        }
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

                try { GetBytes(); }
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
    }
}
