using Pinpad.Sdk.Model.Exceptions;
using System;
using System.Text;

namespace Pinpad.Sdk.PinpadProperties.Refactor.Property
{
    /// <summary>
    /// Controller for PinPad command properties with the length variable
    /// </summary>
    public sealed class BinaryVariableLengthProperty<T> : BinaryProperty<T>
    {
        /// <summary>
        /// Is the binary property padded to fill the Max Length?
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

        // Constructor
        /// <summary>
        /// Constructor with values.
        /// </summary>
        /// <param name="name">Name of the property</param>
        /// <param name="headerLength">Length of the property header</param>
        /// <param name="maxLength">Maximum Length of the property</param>
        /// <param name="isPadded">Indicates if this property should pad to fill the max length</param>
        /// <param name="isOptional">Indicates if this property must exist in the command string or not</param>
        /// <param name="value">Initial Value for the property</param>
        public BinaryVariableLengthProperty(string name, int headerLength, int maxLength,
            bool isPadded, bool isOptional, T value = default(T)) : base(name, isOptional, value)
        {
            this.HeaderLength = headerLength;
            this.MaxLength = maxLength;
            this.IsPadded = isPadded;
        }

        // Methods
        /// <summary>
        /// Gets the value of the property as array of bytes, throws UnsetPropertyException if the value is null
        /// </summary>
        /// <returns>Value of the property as string</returns>
        public override byte[] GetBytes()
        {
            byte[] objBytes = base.GetBytes();

            if (objBytes == null)
            {
                if (this.IsPadded) { objBytes = null; }

                else
                {
                    if (this.IsOptional == true)
                    {
                        return BitConverter.GetBytes(this.HeaderLength);
                    }
                    else
                    {
                        throw new UnsetPropertyException(this.Name);
                    }
                }
            }
            else if (objBytes.Length > this.MaxLength)
            {
                throw new LenghtMismatchException(this.Name + " : \"" + objBytes + "\" is " + objBytes.Length + " long while it should be under " + this.MaxLength + " long.");
            }

            byte[] header;

            header = Encoding.UTF8.GetBytes(objBytes.Length.ToString());

            if (this.IsPadded)
            {
                for (int i = this.MaxLength ; i < objBytes.Length; i++)
                {
                    objBytes[i] = 0;
                }
            }

            //Concatenate the two arrays.
            byte[] totalBytes = new byte[header.Length + objBytes.Length];
            header.CopyTo(totalBytes, 0);
            objBytes.CopyTo(totalBytes, header.Length);

            return totalBytes;
        }
    }
}
