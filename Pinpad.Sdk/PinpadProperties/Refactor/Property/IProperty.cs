namespace Pinpad.Sdk.PinpadProperties.Refactor.Property
{
    /// <summary>
    /// Defines basic information every property (that represents data to be sent to the pinpad) must have.
    /// </summary>
    public interface IProperty
    {
        /// <summary>
        /// Indicates if the property is null or not
        /// </summary>
        bool HasValue { get; }
        /// <summary>
        /// Indicates if this property must exist in the command string or not
        /// </summary>
        bool IsOptional { get; }
        /// <summary>
        /// Name of this property
        /// </summary>
        string Name { get; }
    }
}
