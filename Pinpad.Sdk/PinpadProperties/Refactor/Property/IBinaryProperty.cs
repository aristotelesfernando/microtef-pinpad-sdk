namespace Pinpad.Sdk.PinpadProperties.Refactor.Property
{
    /// <summary>
    /// A property whose data can only be represented in bytes.
    /// </summary>
    public interface IBinaryProperty : IProperty
    {
        /// <summary>
        /// Returns it's body.
        /// </summary>
        /// <returns></returns>
        byte[] GetBytes();
    }
}
