namespace Pinpad.Sdk.Commands.DataSet
{
    /// <summary>
    /// Event to be verified by the pinpad when CEX is called.
    /// </summary>
    public class CexOptions
    {
        /// <summary>
        /// Ignore any pressed key.
        /// </summary>
        public string IgnoreKeys = "011111";
        /// <summary>
        /// Veirify if any key is pressed.
        /// </summary>
        public string VerifyKeyPressing = "100000";

        /// <summary>
        /// Ignore magnetic card.
        /// </summary>
        public string IgnoreMagneticCard = "101111";
        /// <summary>
        /// Verify if a magnetic card was passed.
        /// </summary>
        public string VerifyMagneticCard = "010000";

        /// <summary>
        /// Ignore ICC.
        /// </summary>
        public string IgnoreIcc = "110111";
        /// <summary>
        /// Verify if an ICC was inserted.
        /// </summary>
        public string VerifyIccInsertion = "001000";
        /// <summary>
        /// Verify if an ICC was removed.
        /// </summary>
        public string VerifyIccRemoval = "002000";

        /// <summary>
        /// Ignore contactless card.
        /// </summary>
        public string IgnoreCtls = "000100";
        /// <summary>
        /// Verify if a contactless card is present.
        /// </summary>
        public string VerifyCtls = "000100";

        /// <summary>
        /// RUF.
        /// </summary>
        public string Ruf = "111100";
    }
}
