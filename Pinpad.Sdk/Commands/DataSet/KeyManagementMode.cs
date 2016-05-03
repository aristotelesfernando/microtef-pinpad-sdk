namespace Pinpad.Sdk.Commands
{
    /// <summary>
    /// Enumerator for key management methods
    /// </summary>
    public enum KeyManagementMode
	{
        /// <summary>
        /// Null
        /// </summary>
        Undefined = 0,
        /// <summary>
        /// MK/WK
        /// </summary>
        MasterAndWorkingKeys,
        /// <summary>
        /// DUKPT
        /// </summary>
        DerivedUniqueKeyPerTransaction
    }
}
