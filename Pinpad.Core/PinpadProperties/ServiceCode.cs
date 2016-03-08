using Pinpad.Core.TypeCode;
using System;
using System.Text.RegularExpressions;

namespace Pinpad.Core.Properties 
{
	/// <summary>
	/// Controller for Service Codes
	/// </summary>
	public class ServiceCode 
	{
		/// <summary>
		/// The service code as string.
		/// </summary>
		private string _code;
		/// <summary>
		/// The service code as string, in the controller.
		/// </summary>
		public string Code 
		{
			get 
			{
				return this._code;
			}
			set 
			{
				if (value == null || value.Length != 3) {
					throw new InvalidOperationException("ServiceCode does not support codes that are not 3 digits long");
				}
				this._code = value;
			}
		}
		/// <summary>
		/// Does the service code contains a EMV chip?
		/// </summary>
		public bool IsEmv 
		{
			get 
			{
				switch (this.GetPositionValue(0)) 
				{
					case 2:
					case 6:
						return true;

					default:
						return false;
				}
			}
		}
		/// <summary>
		/// Is the Pin required for this card?
		/// </summary>
		public bool IsPinRequired 
		{
			get 
			{
				switch (this.GetPositionValue(2)) 
				{
					case 0:
					case 3:
					case 5:
					case 6:
					case 7:
						return true;

					default:
						return false;
				}
			}
		}
		/// Is the pin mandatory?
		/// Some cases are not required to get the Pin if a Pin Entry Device is not present
		/// </summary>
		public bool IsPinMandatory 
		{
			get 
			{
				switch (this.GetPositionValue(2)) 
				{
					case 0:
					case 3:
					case 5:
						return true;

					default:
						return false;
				}
			}
		}

		// Constructor
		/// <summary>
		/// Constructor with values.
		/// </summary>
		/// <param name="code">Service Code to read</param>
		public ServiceCode(string code) 
		{
			this.Code = code;
		}

		// Methods:
		/// <summary>
		/// Interchange
		/// </summary>
		public InterchangeType Interchange 
		{
			get 
			{
				switch (this.GetPositionValue(0)) 
				{
					case 1:
					case 2:
						return InterchangeType.International;

					case 5:
					case 6:
						return InterchangeType.National;

					case 7:
						return InterchangeType.Private;

					case 9:
						return InterchangeType.Test;

					default:
						return InterchangeType.Undefined;
				}
			}
		}
		/// <summary>
		/// String formatter for ServiceCodeController
		/// </summary>
		/// <param name="obj">object to convert</param>
		/// <returns>Value of the property as string</returns>
		public static string StringFormatter(ServiceCode obj) 
		{
			return obj.Code;
		}
		/// <summary>
		/// String parser for ServiceCodeController
		/// </summary>
		/// <param name="reader">string reader</param>
		/// <returns>ServiceCodeController</returns>
		public static ServiceCode StringParser(StringReader reader) 
		{
			string firstChar = reader.PeekString(1);

			//If the first char is not a numeric char then consider it a separator and the value null
			if (Regex.IsMatch(firstChar, @"\d") == false) 
			{ 
				return null;
			}
			else 
			{
				string strValue = reader.ReadString(3);
				ServiceCode value = new ServiceCode(strValue);
				
				return value;
			}
		}

		// Internally used:
		/// <summary>
		/// Get the value in a specific index of the service code.
		/// </summary>
		/// <param name="position">Index.</param>
		/// <returns>The value at a specific index.</returns>
		private int GetPositionValue(int position) 
		{
			string digit = Code.Substring(position, 1);
		
			return Convert.ToInt32(digit);
		}
	}
}
