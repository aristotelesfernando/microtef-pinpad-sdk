namespace Pinpad.Sdk.Pinpad_commands
{
	internal enum CommandResponseStatus
	{
		/// <summary>
		/// Null value
		/// </summary>
		Undefined = 0,
		/// <summary>
		/// Command successfully executed
		/// </summary>
		ST_OK = 1,
		/// <summary>
		/// Attempt to use safe communication while it was not established
		/// </summary>
		ST_NOSEC = 4,
		/// <summary>
		/// First Function key was pressed
		/// </summary>
		ST_F1 = 5,
		/// <summary>
		/// Second Function key was pressed
		/// </summary>
		ST_F2 = 6,
		/// <summary>
		/// Third Function key was pressed
		/// </summary>
		ST_F3 = 7,
		/// <summary>
		/// Forth Function key was pressed
		/// </summary>
		ST_F4 = 8,
		/// <summary>
		/// Backspace key was pressed
		/// </summary>
		ST_BACKSP = 9,
		/// <summary>
		/// Erro decrypting secure communication packet
		/// </summary>
		ST_ERRPKTSEC = 10,
		/// <summary>
		/// Invalid call to function, previous operations are required, or unknown command
		/// </summary>
		ST_INVCALL = 11,
		/// <summary>
		/// Invalid parameter passed to function
		/// </summary>
		ST_INVPARM = 12,
		/// <summary>
		/// Operation timed out
		/// </summary>
		ST_TIMEOUT = 13,
		/// <summary>
		/// Operation was cancelled by the operator
		/// Cancel key was pressed
		/// </summary>
		ST_CANCEL = 14,
		/// <summary>
		/// PinPad was not open, OPN was not previously called
		/// </summary>
		ST_NOTOPEN = 16,
		/// <summary>
		/// A mandatory parameter was not sent
		/// </summary>
		ST_MANDAT = 20,
		/// <summary>
		/// EMV tables versions different than expected
		/// </summary>
		ST_TABVERDIF = 21,
		/// <summary>
		/// Error writing tables, lack of space for example
		/// </summary>
		ST_TABERR = 22,
		/// <summary>
		/// Internal error, unexpected situation without a specific return code
		/// </summary>
		ST_INTERR = 41,
		/// <summary>
		/// Magnetic Card read error
		/// </summary>
		ST_MCDATAERR = 42,
		/// <summary>
		/// Online pin key index is not present
		/// </summary>
		ST_ERRKEY = 43,
		/// <summary>
		/// No card inserted or CTLS detected
		/// </summary>
		ST_NOCARD = 44,
		/// <summary>
		/// Could not capture PIN temporarily for security reasons, for example: reached capture limit within a time inverval.
		/// </summary>
		ST_PINBUSY = 45,
		/// <summary>
		/// Response data length overflow
		/// </summary>
		ST_RSPOVRFL = 46,
		/// <summary>
		/// Secure Access Module (SAM) missing, mute or with communication error.
		/// </summary>
		ST_NOSAM = 52,
		/// <summary>
		/// Card missing or mute.
		/// </summary>
		ST_DUMBCARD = 61,
		/// <summary>
		/// Card communication error
		/// </summary>
		ST_ERRCARD = 62,
		/// <summary>
		/// Card was invalidated
		/// </summary>
		ST_CARDINVALIDAT = 68,
		/// <summary>
		/// Card not behaving as expected
		/// </summary>
		ST_CARDPROBLEMS = 69,
		/// <summary>
		/// Card contains invalid or inconsistent data
		/// </summary>
		ST_CARDINVDATA = 70,
		/// <summary>
		/// Card without EMV application available for the required conditions
		/// </summary>
		ST_CARDAPPNAV = 71,
		/// <summary>
		/// Selected EMV application may not be used in this situation
		/// </summary>
		ST_CARDAPPNAUT = 72,
		/// <summary>
		/// High level EMC error passive of fallback to magnetic stripe
		/// </summary>
		ST_ERRFALLBACK = 77,
		/// <summary>
		/// Invalid amount for transaction
		/// </summary>
		ST_INVAMOUNT = 78,
		/// <summary>
		/// Multiple CTLS detected simultaneously
		/// </summary>
		ST_CTLSSMULTIPLE = 81,
		/// <summary>
		/// Communication error with CTLS
		/// </summary>
		ST_CTLSSCOMMERR = 82,
		/// <summary>
		/// CTLS was invalidated
		/// </summary>
		ST_CTLSSINVALIDAT = 83,
		/// <summary>
		/// CTLS not behaving as expected
		/// </summary>
		ST_CTLSSPROBLEMS = 84,
		/// <summary>
		/// CTLS without application available for the required conditions
		/// </summary>
		ST_CTLSSAPPNAV = 85,
		/// <summary>
		/// CTLS selected application may not be used in this situation
		/// </summary>
		ST_CTLSSAPPNAUT = 86,
		/// <summary>
		/// Multimedia file not found
		/// </summary>
		ST_MFNFOUND = 101,
		/// <summary>
		/// Error at multimedia file format
		/// </summary>
		ST_MFERRFMT = 102,
		/// <summary>
		/// Error loading multimedia file
		/// </summary>
		ST_MFERR = 103,
	}
}
