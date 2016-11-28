using Pinpad.Sdk.Model;
using Pinpad.Sdk.Commands;
using System;
using System.Collections.Generic;

namespace Pinpad.Sdk.Transaction
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
        /// <param name="cardBrands">List of supported brands.</param>
        /// <returns>CardEntry containing basic information about the card.</returns>
        internal static CardEntry MapCardFromTracks(GcrResponse rawResponse, 
            IList<PinpadCardBrand> cardBrands)
        {
            CardType readingMode = CardMapper.GetCardType(rawResponse.GCR_CARDTYPE.Value);
            CardEntry card = null;

            if (readingMode == CardType.Emv) 
			{ 
				card = EmvTrackMapper.MapCardFromEmvTrack(rawResponse);
			}
            else 
			{ 
				card = MagneticStripeTrackMapper.MapCardFromTrack(rawResponse);
			}

            card.BrandName = CardMapper.GetBrandName(card.PrimaryAccountNumber,
                cardBrands);

            // Unknown card type:
            return card;
        }
        /// <summary>
        /// Translates ApplicationType from a GCR original (raw) response into CardType.
        /// </summary>
        /// <param name="appType">ApplicationType get from Pinpad GCR response.</param>
        /// <returns>Type of the card.</returns>
        internal static CardType GetCardType(ApplicationType appType)
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
					throw new NotImplementedException(
                        "Invalid Card ApplicationType or ApplicationType not implemented yet.");
            }
        }
        /// <summary>
        /// Based on the Primary Account Number and the list of brands supported, 
        /// determines the name of the card brand.
        /// </summary>
        /// <param name="pan">Card Primary Account Number.</param>
        /// <param name="cardBrands">List of supported brands.</param>
        /// <returns>Name of the card brand.</returns>
        internal static string GetBrandName(string pan,
                                               IList<PinpadCardBrand> cardBrands)
        {
            if (cardBrands != null)
            {
                // Get PAN as decimal:
                decimal decimalPan = CardMapper.PanToDecimal(pan);

                // Iterate through each brad to find the corresponding card read brand:
                foreach (PinpadCardBrand currentBrand in cardBrands)
                {
                    foreach (PinpadBinRange currentRange in currentBrand.Ranges)
                    {
                        // If PAN of read card is within the range, return it's brand name:
                        if (currentRange.IsWithin(decimalPan) == true)
                        {
                            return currentBrand.Description;
                        }
                    }
                }
            }
            return null;
        }
        /// <summary>
        /// Parse a <see cref="string"/> into a <see cref="decimal"/>.
        /// </summary>
        /// <param name="panString">PAN as string.</param>
        /// <returns>PAN as decimal.</returns>
        private static decimal PanToDecimal(string panString)
        {
            // Does not allow string if different length from 19 chars:
            panString = panString.PadRight(19, '0');

            // Get decimal PAN:
            decimal decimalPan;
            Decimal.TryParse(panString, out decimalPan);

            return decimalPan;
        }
    }
}
