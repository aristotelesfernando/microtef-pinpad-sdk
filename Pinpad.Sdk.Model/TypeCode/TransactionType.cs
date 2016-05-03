namespace Pinpad.Sdk.Model
{
    /// <summary>
    /// Specifies each transaction type supported.
    /// </summary>
    public enum TransactionType
    {
        /// <summary>
        /// Undefined transaction type.
        /// </summary>
        Undefined = 0,
        /// <summary>
        /// Credit.
        /// </summary>
        Credit = 1,
        /// <summary>
        /// Debit, at cash.
        /// </summary>
        Debit = 2
    }
}
