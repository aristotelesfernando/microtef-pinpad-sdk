using System;

namespace Pinpad.Sdk.Model.Pinpad
{
    /// <summary>
    /// Responsible for providing methods and informations to update the pinpad embedded application.
    /// </summary>
    public interface IPinpadUpdateService
    {
        /// <summary>
        /// Size of the package to be sent to the pinpad.
        /// Each package is a piece of the zipped application.
        /// </summary>
        int SectionSize { get; }
        /// <summary>
        /// Current application version running in the pinpad.
        /// </summary>
        Version CurrentApplicationVersion { get; }

        /// <summary>
        /// Loads the zipped file in the memory.
        /// </summary>
        /// <param name="filePath">Absolute file path of the new application.</param>
        /// <returns>Whether the loading were successful or not.</returns>
        bool Load (string filePath);
        /// <summary>
        /// Updates the pinpad with the application previously loaded. 
        /// Must be called after the <see cref="Load(string)"/>.
        /// </summary>
        /// <returns>Whether the update was successful.</returns>
        bool Update ();
    }
}
