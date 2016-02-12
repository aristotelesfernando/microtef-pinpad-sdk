using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pinpad.Sdk.Model.TypeCode
{
	/// <summary>
	/// Pinpad command response status return codes enum
	/// Since undefined is 0 every value will be the actual code plus 1, example: ST_OK is "000", 0 + 1 = 1
	/// </summary>
	public enum ResponseStatus
	{
		/// <summary>
		/// Null value
		/// </summary>
		Undefined = 0,
		/// <summary>
		/// Command successfully executed
		/// </summary>
		Ok = 1,
		/// <summary>
		/// Attempt to use safe communication while it was not established
		/// </summary>
		NotSecureCommunication = 4,
		/// <summary>
		/// First Function key was pressed
		/// </summary>
		F1KeyPressed = 5,
		/// <summary>
		/// Second Function key was pressed
		/// </summary>
		F2KeyPressed = 6,
		/// <summary>
		/// Third Function key was pressed
		/// </summary>
		F3KeyPressed = 7,
		/// <summary>
		/// Forth Function key was pressed
		/// </summary>
		F4KeyPressed = 8,
		/// <summary>
		/// Backspace key was pressed
		/// </summary>
		BackspacePressed = 9,
		/// <summary>
		/// Erro decrypting secure communication packet
		/// </summary>
		CancelKeyPressed = 10,
		/// <summary>
		/// Invalid call to function, previous operations are required, or unknown command
		/// </summary>
		InvalidCommand = 11,
		/// <summary>
		/// Invalid parameter passed to function
		/// </summary>
		InvalidParameter = 12,
		/// <summary>
		/// Operation timed out
		/// </summary>
		TimeOut = 13,
		/// <summary>
		/// Operation was cancelled by the operator
		/// Cancel key was pressed
		/// </summary>
		OperationCancelled = 14,
		/// <summary>
		/// PinPad was not open, OPN was not previously called
		/// </summary>
		PinpadIsClosed = 16,
		/// <summary>
		/// A mandatory parameter was not sent
		/// </summary>
		MandatoryParameterNotReceived = 20,
		/// <summary>
		/// EMV tables versions different than expected
		/// </summary>
		InvalidEmvTable = 21,
		/// <summary>
		/// Error writing tables, lack of space for example
		/// </summary>
		CouldNotWriteTable = 22,
		/// <summary>
		/// Internal error, unexpected situation without a specific return code
		/// </summary>
		InternalError = 41,
		/// <summary>
		/// Magnetic Card read error
		/// </summary>
		MagneticStripeError = 42,
		/// <summary>
		/// Online pin key index is not present
		/// </summary>
		PinIndexNotFound = 43,
		/// <summary>
		/// No card inserted or CTLS detected
		/// </summary>
		NoneCardDetected = 44,
		/// <summary>
		/// Could not capture PIN temporarily for security reasons, for example: reached capture limit within a time inverval.
		/// </summary>
		PinBusy = 45,
		/// <summary>
		/// Response data length overflow
		/// </summary>
		ResponseDataOverflow = 46,
		/// <summary>
		/// Secure Access Module (SAM) missing, mute or with communication error.
		/// </summary>
		SamError = 52,
		/// <summary>
		/// Card missing or mute.
		/// </summary>
		DumbCard = 61,
		/// <summary>
		/// Card communication error
		/// </summary>
		CardCommunicationError = 62,
		/// <summary>
		/// Card was invalidated
		/// </summary>
		InvalidCard = 68,
		/// <summary>
		/// Card not behaving as expected
		/// </summary>
		CardProblems = 69,
		/// <summary>
		/// Card contains invalid or inconsistent data
		/// </summary>
		InconsistentCard = 70,
		/// <summary>
		/// Card without EMV application available for the required conditions
		/// </summary>
		InvalidEmvApplication = 71,
		/// <summary>
		/// Selected EMV application may not be used in this situation
		/// </summary>
		UnableToProcessEmvApplication = 72,
		/// <summary>
		/// High level EMC error passive of fallback to magnetic stripe
		/// </summary>
		Fallback = 77,
		/// <summary>
		/// Invalid amount for transaction
		/// </summary>
		InvalidAmount = 78,
		/// <summary>
		/// Multiple CTLS detected simultaneously
		/// </summary>
		MultipleContactlessDetected = 81,
		/// <summary>
		/// Communication error with CTLS
		/// </summary>
		CtlsCommunicationError = 82,
		/// <summary>
		/// CTLS was invalidated
		/// </summary>
		InvalidCtlsCard = 83,
		/// <summary>
		/// CTLS not behaving as expected
		/// </summary>
		CtlsCardProblems = 84,
		/// <summary>
		/// CTLS without application available for the required conditions
		/// </summary>
		InvalidCtlsApplication = 85,
		/// <summary>
		/// CTLS selected application may not be used in this situation
		/// </summary>
		UnableToProcessCtlsApplication = 86,
		/// <summary>
		/// Could not connect with pinpad.
		/// </summary>
		PinpadConnectionError = 87,
		/// <summary>
		/// Multimedia file not found
		/// </summary>
		FileNotFound = 101,
		/// <summary>
		/// Error at multimedia file format
		/// </summary>
		FileFormatError = 102,
		/// <summary>
		/// Error loading multimedia file
		/// </summary>
		FileLoadingError = 103,
	}
}
