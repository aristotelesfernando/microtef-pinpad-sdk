namespace Pinpad.Core.TypeCode
{
    /// <summary>
    /// Enumerator for cryptography methods
    /// </summary>
    public enum CryptographyMode {
        /// <summary>
        /// Null
        /// </summary>
        Undefined = 0,
        
        /// <summary>
        /// DES
        /// </summary>
        DataEncryptionStandard,

        /// <summary>
        /// TDES
        /// </summary>
        TripleDataEncryptionStandard
    }
}
