namespace Pinpad.Sdk.Commands.DataSet
{
    /// <summary>
    /// Cex command identifier of the occurred event.
    /// </summary>
    public enum CexEventRead
    {
        /// <summary>
        /// OK or ENTER key pressed. 
        /// </summary>
        OkOrEnterPressed = 0,
        /// <summary>
        /// F1 key pressed.
        /// </summary>
        F1KeyPressed = 4,
        /// <summary>
        /// F2 key pressed.
        /// </summary>
        F2KeyPressed = 5,
        /// <summary>
        /// F3 key pressed.
        /// </summary>
        F3KeyPressed = 6,
        /// <summary>
        /// F4 key pressed.
        /// </summary>
        F4KeyPressed = 7,
        /// <summary>
        /// Clear key pressed.
        /// </summary>
        ClearKeyPressed = 8,
        /// <summary>
        /// Cancel key pressed.
        /// </summary>
        CancelKeyPressed = 13,
        /// <summary>
        /// Magnetic card passed.
        /// </summary>
        MagneticCardPassed = 90,
        /// <summary>
        /// ICC was removed or wasn't present.
        /// </summary>
        IccRemoved = 91,
        /// <summary>
        /// ICC was inserted or was already present.
        /// </summary>
        IccInserted = 92,
        /// <summary>
        /// Contactless wasn't detected in 2 minutes.
        /// </summary>
        CtlsNotDetected = 93,
        /// <summary>
        /// Contactless detected.
        /// </summary>
        CtlsDetected = 94,
    }
}
