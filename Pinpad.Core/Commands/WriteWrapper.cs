using PinPadSDK.Exceptions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;

namespace PinPadSDK.Commands
{
	/// <summary>
	/// Joga uma exceção caso a variavel tenha sido esquecida
	/// </summary>
	/// <typeparam name="T">Tipo de dado a ser armazenado</typeparam>
	[DebuggerDisplay("{SafeGet()}")]
	public struct WriteWrapper
	{
		public static implicit operator WriteWrapper(string Value)
		{
			return new WriteWrapper(Value);
		}
		public static implicit operator string(WriteWrapper Value)
		{
			return Value.Get();
		}

		public static implicit operator WriteWrapper(int Value)
		{
			return new WriteWrapper(Value.ToString());
		}
		public static implicit operator int(WriteWrapper Value)
		{
			return Convert.ToInt32(Value.Get());
		}

		public static implicit operator WriteWrapper(long Value)
		{
			return new WriteWrapper(Value.ToString());
		}
		public static implicit operator long(WriteWrapper Value)
		{
			return Convert.ToInt64(Value.Get());
		}

		public static implicit operator WriteWrapper(bool Value)
		{
			return new WriteWrapper(Value.ToString());
		}
		public static implicit operator bool(WriteWrapper Value)
		{
			try
			{
				return Convert.ToBoolean(Value.Get());
			}
			catch
			{
				return Value.Get() == "1" ? true : false;
			}
		}

		public static implicit operator WriteWrapper(DateTime Value)
		{
			return new WriteWrapper(Value.ToString("yyMMddHHmmss"));
		}
		public static implicit operator DateTime(WriteWrapper Value)
		{
			DateTime Parse;
			if (DateTime.TryParseExact(Value.Get(), "yyMMddHHmmss", System.Globalization.CultureInfo.InvariantCulture, DateTimeStyles.AssumeLocal, out Parse))
				return Parse;
			else
				return DateTime.Parse(Value.Get());
		}

		public WriteWrapper(string initialvalue)
		{
			value = null;

			Set(initialvalue);
		}
		public bool HasValue
		{
			get
			{
				return value != null;
			}
		}
		public string Get()
		{
			if (!this.HasValue)
			{
				throw new UnsetPropertyException();
			}
			return value.ToString();
		}
		public string AlwaysGet()
		{
			if (value == null)
				return "";
			else
				return value;
		}
		public string SafeGet()
		{
			return value;
		}
		public void Set(string newvalue)
		{
			if (newvalue == "null")
				value = null;
			else
				value = newvalue;
		}
		public override string ToString()
		{
			if (!this.HasValue)
				return "null";
			else
			{
				DateTime datetime;
				if (DateTime.TryParseExact(value, "yyMMddHHmmss", System.Globalization.CultureInfo.InvariantCulture, DateTimeStyles.AssumeLocal, out datetime))
					return datetime.ToString();
				else
					return value;
			}
		}
		public int Length
		{
			get { return Get().Length; }
		}
		private string value;
	}
}
