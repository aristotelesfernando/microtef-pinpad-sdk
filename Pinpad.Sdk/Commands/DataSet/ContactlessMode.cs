﻿namespace Pinpad.Sdk.Commands
{
	/// <summary>
	/// Enumerator for contactless modes to be used at AID table
	/// Since undefined is 0 every value will be the actual code plus 1
	/// </summary>
	public enum ContactlessMode 
	{
		/// <summary>
		/// Null
		/// </summary>
		Undefined = 0,
		/// <summary>
		/// CTLS mode not supported
		/// </summary>
		Unsupported = 1,
		/// <summary>
		/// Visa MSD
		/// </summary>
		VisaMsd = 2,
		/// <summary>
		/// Visa qVSDC
		/// </summary>
		VisaQvsdc = 3,
		/// <summary>
		/// MasterCard PayPass for Magnetic Stripe
		/// </summary>
		MasterCardPayPassMagneticStripe = 4,
		/// <summary>
		/// MasterCard PayPass for m/Chip
		/// </summary>
		MasterCardPayPassMChip = 5,
		/// <summary>
		/// Amex Expresspay for Magnetic Stripe
		/// </summary>
		AmexExpresspayMagneticStripe = 6,
		/// <summary>
		/// Amex Expresspay for EMV
		/// </summary>
		AmexExpresspayEmv = 7
	}
}
