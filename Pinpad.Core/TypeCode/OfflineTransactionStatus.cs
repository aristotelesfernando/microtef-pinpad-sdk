namespace Pinpad.Core.TypeCode 
{
    /// <summary>
    /// Enumerator for offline transaction status
    /// Since undefined is 0 every value will be the actual code plus 1
    /// </summary>
    public enum OfflineTransactionStatus 
	{
        /// <summary>
        /// Null
        /// </summary>
        Undefined = 0,
        /// <summary>
        /// Transaction approved offline
        /// Even though the transaction was approved the acquirer still must be notified, this uses a different processing method since the transaction is actually approved
        /// </summary>
        Approved = 1,
        /// <summary>
        /// Transaction denied offline
        /// </summary>
        Denied = 2,
        /// <summary>
        /// Transaction should be authorized online
        /// </summary>
        RequiresAuthorization = 3
    }
}
