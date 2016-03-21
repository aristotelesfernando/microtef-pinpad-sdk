using System;

namespace Pinpad.Sdk.Model.Exceptions
{
	public class ExpiredCardException : Exception
	{
		public ExpiredCardException () 
			: base("An expired card was read. Impossible to continue.")
		{

		}
	}
}
