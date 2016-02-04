using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pinpad.Sdk.Exceptions
{
	public class InvalidTableException : Exception
	{
		public InvalidTableException(string message = null, Exception innerException = null) 
			: base (message, innerException) {  }
	}
}
