using Pinpad.Sdk.Commands;
using Pinpad.Sdk.Model;
using System;
using System.Collections.Generic;

namespace Pinpad.Sdk.Transaction.Mapper.MagneticStripe
{
    /// <summary>
    /// Provide methods to read a magnetic stripe track.
    /// </summary>
    internal abstract class AbstractMagneticStripeTrackReader
    {
        /// <summary>
        /// Start sentinel for track 2.
        /// </summary>
        abstract protected char Track2StartSentinel { get; }
        /// <summary>
        /// Track1, separator between different information. Based on ISO 7810/7811.
        /// </summary>
        abstract protected char Track1FieldSeparator { get; }
        /// <summary>
        /// Track2, separator between different information. Based on ISO 7810/7811.
        /// </summary>
        abstract protected char Track2FieldSeparator { get; }
        /// <summary>
        /// Track 1 is made of several information divided by a separator. 
        /// This is the index of card expiration date on track 1.
        /// </summary>
        abstract protected short ExpirationDateIndexOnTrack1 { get; }
        /// <summary>
        /// Track 2 is made of several information divided by a separator. 
        /// This is the index of card expiration date on track 2.
        /// </summary>
        abstract protected short ExpirationDateIndexOnTrack2 { get; }
        /// <summary>
        /// Card expiration date string length.
        /// </summary>
        abstract protected short ExpirationDateLength { get; }
        /// <summary>
        /// Card service code string length.
        /// </summary>
        abstract protected short ServiceCodeLength { get; }
        /// <summary>
        /// Track 2 is made of several information divided by a separator. 
        /// This is the index of the Primary Account Number (PAN) on track 1.
        /// </summary>
        abstract protected short PanIndex { get; }
        /// <summary>
        /// Track 1 is made of several information divided by a separator. 
        /// This is the index of cardholder name on track 1.
        /// </summary>
        abstract protected short CardholderNameIndex { get; }

        public CardEntry GetCard (GcrResponse rawResponse, IList<PinpadCardBrand> cardBrands)
        {
            CardEntry card = this.MapCardFromTrack(rawResponse);
            card.BrandName = this.GetBrandName(card.PrimaryAccountNumber, cardBrands);
            return card;
        }
        protected virtual string GetBrandName (string pan, IList<PinpadCardBrand> cardBrands)
        {
            if (cardBrands != null)
            {
                // Get PAN as decimal:
                decimal decimalPan = this.PanToDecimal(pan);

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
        private decimal PanToDecimal (string panString)
        {
            // TODO: Melhorar valores mágicos.
            // Does not allow string if different length from 19 chars:
            panString = panString.PadRight(19, '0');

            // Get decimal PAN:
            decimal decimalPan;
            Decimal.TryParse(panString, out decimalPan);

            return decimalPan;
        }

        // TODO: Documentar.
        protected abstract CardEntry MapCardFromTrack(GcrResponse rawResponse);
    }
}
