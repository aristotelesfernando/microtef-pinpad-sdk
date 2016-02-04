using Pinpad.Sdk.Model.TypeCode;
using PinPadSDK.Commands;
using PinPadSDK.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pinpad.Sdk.Model
{
    /// <summary>
    /// Translates information about card used on transaction.
    /// </summary>
    internal class CardMapper
    {
        /// <summary>
        /// Takes a GCR Response from Pinpad and translates it into Card information.
        /// </summary>
        /// <param name="rawResponse">GCR original response from Pinpad.</param>
        /// <returns>CardEntry containing basic information about the card.</returns>
        internal static CardEntry MapCardFromTracks(GcrResponse rawResponse)
        {
            CardType readingMode = CardMapper.MapCardType(rawResponse.GCR_CARDTYPE.Value);

            if (readingMode == CardType.Emv) { return EmvTrackMapper.MapCardFromEmvTrack(rawResponse); }
            if (readingMode == CardType.MagneticStripe) { return MagneticStripeTrackMapper.MapCardFromTrack(rawResponse); }

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

                default: return CardType.Undefined;
            }
        }
    }
}
