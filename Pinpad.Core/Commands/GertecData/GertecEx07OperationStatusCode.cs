namespace Pinpad.Core.Commands
{
	/// <summary>
	/// Operation status of an EX07 command.
	/// </summary>
	public enum GertecEx07OperationStatusCode
	{
		/// <summary>
		/// Operation succeed.
		/// </summary>
		Success = '0',
		/// <summary>
		/// Host cancelled.
		/// </summary>
		HostCancelled = 'H',
		/// <summary>
		/// User cancelled.
		/// </summary>
		UserCancelled = 'U',
		/// <summary>
		/// Time out.
		/// </summary>
		TimeOut = 'T'
	}
}
