namespace Pinpad.Sdk.Model
{
	/// <summary>
	/// Enumerator Emv Tables 
	/// Since undefined is 0 every value will be the actual code plus 1
	/// </summary>
	public enum EmvTableType 
	{
		/// <summary>
		/// Null
		/// </summary>
		Undefined = 0,
		/// <summary>
		/// Application Identifier Table
		/// </summary>
		Aid = 2,
		/// <summary>
		/// Certification Authority Public Keys Table
		/// </summary>
		Capk = 3,
		/// <summary>
		/// CAPK's revoked certificates table
		/// </summary>
		RevokedCertificate = 4,
	}
}
