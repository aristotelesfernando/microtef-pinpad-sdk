using System;

namespace Pinpad.Sdk.Model.Utilities
{
    /// <summary>
    /// It contains methods to select data through the pinpad.
    /// </summary>
    public interface IDataPicker
    {
        // Properties
        /// <summary>
        /// Keys of Down and Up in pinpad.
        /// </summary>
        DataPickerKeys DataPickerKeys { get; set; }

        // Public methods
        /// <summary>
        /// Get numeric value in range informed.
        /// </summary>
        /// <param name="label">Text to display on the first line of pinpad display.</param>
        /// <param name="circularBehavior">Behavior of the list.</param>
        /// <param name="minimunLimit">Minimum numeric value for pick. Limit: -32.768.</param>
        /// <param name="maximumLimit">Maximum numeric value for pick. Limit: 32.767.</param>
        /// <returns>Number picked or null if no one was picked.</returns>
        Nullable<short> GetNumericValue(string label, bool circularBehavior, short minimunLimit, short maximumLimit);
        /// <summary>
        /// Get numeric value in array options.
        /// </summary>
        /// <param name="label">Text to display on the first line of pinpad display.</param>
        /// <param name="circularBehavior">Behavior of the list.</param>
        /// <param name="options">Array with options.</param>
        /// <returns>Option picked or null if no one was picked.</returns>
        Nullable<short> GetValueInOptions(string label, bool circularBehavior, params short[] options);
        /// <summary>
        /// Get text value in array options.
        /// </summary>
        /// <param name="label">Text to display on the first line of pinpad display.</param>
        /// <param name="circularBehavior">Behavior of the list.</param>
        /// <param name="options">Array with options.</param>
        /// <returns>Option picked or null/empty if no one was picked.</returns>
        string GetValueInOptions(string label, bool circularBehavior, params string[] options);
    }
}
