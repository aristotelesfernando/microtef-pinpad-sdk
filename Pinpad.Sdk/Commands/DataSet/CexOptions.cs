namespace Pinpad.Sdk.Commands.DataSet
{
    /// <summary>
    /// Event to be verified by the pinpad when CEX is called.
    /// </summary>
    static internal class CexOptions
    {
        /// <summary>
        /// Ignore any pressed key.
        /// </summary>
        public const string IgnoreKeys = "011111";
        /// <summary>
        /// Veirify if any key is pressed.
        /// </summary>
        public const string VerifyKeyPressing = "100000";

        /// <summary>
        /// Ignore magnetic card.
        /// </summary>
        public const string IgnoreMagneticCard = "101111";
        /// <summary>
        /// Verify if a magnetic card was passed.
        /// </summary>
        public const string VerifyMagneticCard = "010000";

        /// <summary>
        /// Ignore ICC.
        /// </summary>
        public const string IgnoreIcc = "110111";
        /// <summary>
        /// Verify if an ICC was inserted.
        /// </summary>
        public const string VerifyIccInsertion = "001000";
        /// <summary>
        /// Verify if an ICC was removed.
        /// </summary>
        public const string VerifyIccRemoval = "002000";

        /// <summary>
        /// Ignore contactless card.
        /// </summary>
        public const string IgnoreCtls = "000100";
        /// <summary>
        /// Verify if a contactless card is present.
        /// </summary>
        public const string VerifyCtls = "000100";
    }
}
