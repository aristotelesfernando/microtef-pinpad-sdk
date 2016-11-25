using Pinpad.Sdk.Commands;
using Pinpad.Sdk.Model;

namespace Pinpad.Sdk.Transaction.Mapper.MagneticStripe
{
    /// <summary>
    /// Provide methods to read a magnetic stripe track.
    /// </summary>
    internal interface IMagneticStripeTrackReader
    {
        /// <summary>
        /// Start sentinel for track 2.
        /// </summary>
        char StartSentinel { get; }
        /// <summary>
        /// Track1, separator between different information. Based on ISO 7810/7811.
        /// </summary>
        char Track1FieldSeparator { get; }
        /// <summary>
        /// Track2, separator between different information. Based on ISO 7810/7811.
        /// </summary>
        char Track2FieldSeparator { get; }
        /// <summary>
        /// Track 1 is made of several information divided by a separator. 
        /// This is the index of card expiration date on track 1.
        /// </summary>
        short ExpirationDateIndexOnTrack1 { get; }
        /// <summary>
        /// Track 2 is made of several information divided by a separator. 
        /// This is the index of card expiration date on track 2.
        /// </summary>
        short ExpirationDateIndexOnTrack2 { get; }
        /// <summary>
        /// Card expiration date string length.
        /// </summary>
        short ExpirationDateLength { get; }
        /// <summary>
        /// Card service code string length.
        /// </summary>
        short ServiceCodeLength { get; }
        /// <summary>
        /// Track 2 is made of several information divided by a separator. 
        /// This is the index of the Primary Account Number (PAN) on track 1.
        /// </summary>
        short PanIndex { get; }
        /// <summary>
        /// Track 1 is made of several information divided by a separator. 
        /// This is the index of cardholder name on track 1.
        /// </summary>
        short CardholderNameIndex { get; }

        /// <summary>
        /// Map card from tracks in a specific manner.
        /// </summary>
        /// <param name="rawResponse">Response from GCR.</param>
        /// <returns>The card read.</returns>
        CardEntry Read(GcrResponse rawResponse);
    }
}
