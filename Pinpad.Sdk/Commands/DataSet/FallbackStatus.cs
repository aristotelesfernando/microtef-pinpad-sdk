namespace Pinpad.Sdk.Commands
{
	/// <summary>
	/// Fallback status enum
	/// </summary>
	public enum FallbackStatus 
	{
		/// <summary>
		/// NULL
		/// </summary>
		Undefined = 0,
		/// <summary>
		/// Fallback not required
		/// </summary>
		NotRequired = 1,
		/// <summary>
		/// Error liable of fallback
		/// </summary>
		ErrorLiableOfFallback = 2,
		/// <summary>
		/// Aplication required not supported, fallback depends on acquirer definitions
		/// </summary>
		FallbackUpToAcquirer = 3
	}
}
