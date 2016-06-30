using System;

namespace Pinpad.Sdk.Model.Exceptions
{
	/// <summary>
	/// Exception thrown when card has expired.
	/// </summary>
	public class ExpiredCardException : Exception
	{
		/// <summary>
		/// Card expirated date.
		/// </summary>
		public DateTime ExpiredDate { get; private set; }
		/// <summary>
		/// Default constructor, creates an exception with the default message "An expired card was read. Impossible to continue.".
		/// </summary>
		public ExpiredCardException (DateTime expiredDate) 
			: base("An expired card was read. Impossible to continue. Expiration at " + expiredDate.ToString())
		{
			this.ExpiredDate = expiredDate;
		}
	}
}
