using System;

namespace Pinpad.Sdk.Model.Utilities
{
    /// <summary>
    /// It contains methods to select data through the pinpad.
    /// </summary>
    public interface IDataPicker
    {
        /// <summary>
        /// Get numeric value in range informed.
        /// </summary>
        /// <param name="label">Text to display on the first line of pinpad display.</param>
        /// <param name="minimunValue">Minimum numeric value for pick.</param>
        /// <param name="maximumValue">Maximum numeric value for pick.</param>
        /// <returns>Number picked or null if no one was picked.</returns>
        Nullable<short> GetNumericValue(string label, short minimunValue, short maximumValue);
        /// <summary>
        /// Get numeric value in array options.
        /// </summary>
        /// <param name="label">Text to display on the first line of pinpad display.</param>
        /// <param name="options">Array with options.</param>
        /// <returns>Option picked or null if no one was picked.</returns>
        Nullable<short> GetNumericValueInArray(string label, params short?[] options);
        /// <summary>
        /// Get text value in array options.
        /// </summary>
        /// <param name="label">Text to display on the first line of pinpad display.</param>
        /// <param name="options">Array with options.</param>
        /// <returns>Option picked or null if no one was picked.</returns>
        string GetTextValueInArray(string label, params string[] options);
    }
}
