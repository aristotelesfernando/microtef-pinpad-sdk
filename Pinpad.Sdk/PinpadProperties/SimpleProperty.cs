using Pinpad.Sdk.Model.Exceptions;
using System;


/*
 * 
 * DEPRECATED.
 * MUST BE REFACTORED.
 * 
 */

namespace Pinpad.Sdk.Properties 
{
	/// <summary>
	/// Controller for PinPad command properties
	/// </summary>
	public class SimpleProperty<type> : IProperty 
	{
		// Members
		/// <summary>
		/// Indicates if the property is null or not
		/// </summary>
		public bool HasValue
		{
			get { return this.Value != null && this.Value.Equals(default(type)) == false; }
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
		private type value { get; set; }
		/// <summary>
		/// Property value (controller).
		/// </summary>
		public virtual type Value
		{
			get { return this.value; }
			set
			{
				type oldValue = this.value;
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
		private Func<type, string> stringFormatter;
		/// <summary>
		/// Responsible for parsing strings.
		/// </summary>
		private Func<StringReader, type> stringParser;

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
		public SimpleProperty(string name, bool isOptional = false, Func<type, string> stringFormatter = null, Func<StringReader, type> stringParser = null, string defaultStringValue = null, type value = default(type))
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
		public type GetValue()
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
			type obj = this.GetValue();

			if (obj == null || obj.Equals(default(type)))
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
				this.Value = default(type);
			}
			else
			{
				type value = this.stringParser(reader);
				this.Value = value;
			}
		}
	}
}
