using Pinpad.Sdk.Model;
using Pinpad.Sdk.Commands;
using System;
using System.Diagnostics;
using Pinpad.Sdk.TypeCode;

namespace Pinpad.Sdk.Transaction
{
    /// <summary>
    /// Translates information about card used on transaction.
    /// </summary>
    internal class CardMapper
    {
		internal static string MASTERCARD_LABEL = "MASTERCARD";
		internal static string VISA_LABEL = "VISA";
		internal static string UNKNOWN_LABEL = "UNKNOWN";

        /// <summary>
        /// Takes a GCR Response from Pinpad and translates it into Card information.
        /// </summary>
        /// <param name="rawResponse">GCR original response from Pinpad.</param>
        /// <returns>CardEntry containing basic information about the card.</returns>
        internal static CardEntry MapCardFromTracks(GcrResponse rawResponse)
        {
            CardType readingMode = CardMapper.MapCardType(rawResponse.GCR_CARDTYPE.Value);

			Debug.WriteLine(rawResponse.GCR_CARDTYPE.Value);


            if (readingMode == CardType.Emv) 
			{ 
				return EmvTrackMapper.MapCardFromEmvTrack(rawResponse); 
			}
            if (readingMode == CardType.MagneticStripe) 
			{ 
				return MagneticStripeTrackMapper.MapCardFromTrack(rawResponse); 
			}

            // Unknown card type:
            return null;
        }
        /// <summary>
        /// Translates ApplicationType from a GCR original (raw) response into CardType.
        /// </summary>
        /// <param name="appType">ApplicationType get from Pinpad GCR response.</param>
        /// <returns>Type of the card.</returns>
        internal static CardType MapCardType(ApplicationType appType)
        {
            // Verifies which type of card is being read.
            switch (appType)
            {
                case ApplicationType.IccEmv:
                case ApplicationType.ContactlessEmv:
                    return CardType.Emv;

                case ApplicationType.MagneticStripe:
                case ApplicationType.ContactlessMagneticStripe:
                    return CardType.MagneticStripe;

                default:
					throw new NotImplementedException("Invalid Card ApplicationType or ApplicationType not implemented yet.");
            }
        }
    }
}
