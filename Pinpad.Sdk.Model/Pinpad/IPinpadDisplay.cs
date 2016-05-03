namespace Pinpad.Sdk.Model
{
    /// <summary>
    /// Responsible for presenting messages in a pinpad device.
    /// </summary>
    public interface IPinpadDisplay
    {
        /// <summary>
        /// Show message on pinpad screen.
        /// </summary>
        /// <param name="firstLine">The first line of the message, shown at the first screen line. Must have 16 characters or less.</param>
        /// <param name="secondLine">The second line of the message, shown at the second screen line. Must have 16 characters or less.</param>
        /// <param name="paddingType">At what alignment the message is present. It default value is left alignment.</param>
        /// <returns>Whether the message could be shown with success or not.</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">This exception is thrown only if one (or both) of the messages exceed the limit of 16 characters.</exception>
        bool ShowMessage(string firstLine, string secondLine = null, DisplayPaddingType paddingType = DisplayPaddingType.Left);
    }
}
