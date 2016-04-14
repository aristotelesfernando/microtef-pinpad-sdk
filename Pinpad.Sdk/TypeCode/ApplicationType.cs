namespace Pinpad.Sdk.TypeCode 
{
    /// <summary>
    /// Enumerator for the card applications, values are defined from GCR_CARDTYPE and AID's T1_ICCSTD values
    /// Since undefined is 0 every value will be the actual code plus 1
    /// </summary>
    public enum ApplicationType 
	{
        /// <summary>
        /// Null
        /// </summary>
        Undefined = 0,
        /// <summary>
        /// Magnetic Stripe card
        /// </summary>
        MagneticStripe = 1,
        /// <summary>
        /// CIELO's proprietary pattern
        /// </summary>
        VisaCashOverTIBCv1 = 2,
        /// <summary>
        /// CIELO's proprietary pattern
        /// </summary>
        VisaCashOverTIBCv3 = 3,
        /// <summary>
        /// Chip and pin card
        /// </summary>
        IccEmv = 4,
        /// <summary>
        /// CIELO's proprietary pattern
        /// </summary>
        EasyEntryOverTIBCv1 = 5,
        /// <summary>
        /// Contactless simulating Magnetic Stripe card
        /// </summary>
        ContactlessMagneticStripe = 6,
        /// <summary>
        /// Contactless simulating EMV card
        /// </summary>
        ContactlessEmv = 7,
    }
}
