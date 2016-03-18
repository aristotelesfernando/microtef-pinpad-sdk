namespace Pinpad.Core.Commands
{
	/// <summary>
	/// Operation status of an EX07 command.
	/// </summary>
	public enum GertecResponseCode
	{
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
