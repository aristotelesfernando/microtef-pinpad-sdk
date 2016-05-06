namespace Pinpad.Sdk
{
	/// <summary>
	/// Stone indexes.
	/// </summary>
	public enum StoneIndexCode
	{
		/// <summary>
		/// Is not a Stone Index. This option will access data of all acquirer supported by the pinpad.
		/// </summary>
		Generic = 0,
		/// <summary>
		/// Stone application number into the pinpad.
		/// </summary>
		Application = 8,
		/// <summary>
		/// MK index or DUKPT register.
		/// </summary>
		EncryptionKey = 16
	}
}
