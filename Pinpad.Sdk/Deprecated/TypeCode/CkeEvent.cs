namespace PinPadSDK.Enums {
    /// <summary>
    /// Enumerator for CKE response events
    /// Since undefined is 0 every value will be the actual code plus 1
    /// </summary>
    public enum CkeEvent {
        /// <summary>
        /// Null
        /// </summary>
        Undefined = 0,

        /// <summary>
        /// A Key was pressed
        /// </summary>
        KeyPress = 1,

        /// <summary>
        /// A Magnetic Stripe card was passed
        /// </summary>
        MagneticStripeCardPassed = 2,

        /// <summary>
        /// ICC status was changed
        /// </summary>
        IccStatusChanged = 3,

        /// <summary>
        /// CTLS was updated, either was detected or timeout after 2 minutes
        /// </summary>
        CtlsUpdate = 4,
    }
}
