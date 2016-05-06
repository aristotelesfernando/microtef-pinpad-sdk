namespace Pinpad.Sdk.Commands
{
	/// <summary>
	/// Operation status of an EX07 command.
	/// </summary>
	public enum GertecResponseCode
	{
		/// <summary>
		/// Undefined/invalid type code.
		/// </summary>
		Undefined = 0,
		/// <summary>
		/// Operation succeed.
		/// </summary>
		Success = 1,
		/// <summary>
		/// Host cancelled.
		/// </summary>
		HostCancelled = 72,
		/// <summary>
		/// User cancelled.
		/// </summary>
		UserCancelled = 85,
		/// <summary>
		/// Time out.
		/// </summary>
		TimeOut = 84
	}
}
