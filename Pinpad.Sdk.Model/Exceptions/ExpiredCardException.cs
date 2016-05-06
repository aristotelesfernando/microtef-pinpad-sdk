using System;

namespace Pinpad.Sdk.Model.Exceptions
{
	/// <summary>
	/// Exception thrown when card has expired.
	/// </summary>
	public class ExpiredCardException : Exception
	{
		/// <summary>
		/// Default constructor, creates an exception with the default message "An expired card was read. Impossible to continue.".
		/// </summary>
		public ExpiredCardException () 
			: base("An expired card was read. Impossible to continue.")
		{

		}
	}
}
