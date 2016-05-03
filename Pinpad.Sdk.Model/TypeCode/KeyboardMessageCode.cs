namespace Pinpad.Sdk.Model
{
	/// <summary>
	/// Represents the message code to be printed when waiting for a numeric input.
	/// </summary>
	public enum KeyboardMessageCode
	{
		/// <summary>
		/// Undefined label to be presented.
		/// </summary>
		Undefined = 0,
		/// <summary>
		/// Ask for DDD.
		/// </summary>
		AskForDdd = 0x0001 + 1,
		/// <summary>
		/// Ask for DDD confirmation.
		/// </summary>
		AskForDddConfirmation = 0x0002 + 1,
		/// <summary>
		/// Ask for phone number.
		/// </summary>
		AskForPhoneNumber = 0x0003 + 1,
		/// <summary>
		/// Ask for phone number confirmation.
		/// </summary>
		AskForPhoneNumberConfirmation = 0x0004 + 1,
		/// <summary>
		/// Ask for DDD and phone number.
		/// </summary>
		AskForDddAndPhone = 0x0005 + 1,
		/// <summary>
		/// Ask for DDD and phone number confirmation.
		/// </summary>
		AskForDddAndPhoneConfirmation = 0x0006 + 1,
		/// <summary>
		/// Ask for CPF.
		/// </summary>
		AskForCpf = 0x0007 + 1,
		/// <summary>
		/// Ask for CPF confirmation.
		/// </summary>
		AskForCpfConfirmation = 0x0008 + 1,
		/// <summary>
		/// Ask for RG.
		/// </summary>
		AskForRg = 0x0009 + 1,
		/// <summary>
		/// Ask for RG confirmation.
		/// </summary>
		AskForRgConfirmation = 0x000A + 1,
		/// <summary>
		/// Ask for the last 4 digits from card.
		/// </summary>
		AskForLast4Digits = 0x000B + 1,
		/// <summary>
		/// Ask for card security code.
		/// </summary>
		AskForSecurityCode = 0x000C + 1
	}
}
