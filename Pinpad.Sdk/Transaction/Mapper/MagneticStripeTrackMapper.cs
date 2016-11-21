using Pinpad.Sdk.Model;
using Pinpad.Sdk.Commands;
using Pinpad.Sdk.Transaction.Mapper.MagneticStripe;
using System.Collections.Generic;
using System;

namespace Pinpad.Sdk.Transaction
{
    /// <summary>
    /// Basic reader for magnetic stripe cards.
    /// Creates an instance for the actual card reader, e. g. 
    /// <see cref="DefaultMagneticStripeTrackReader"/> or 
    /// <see cref="TicketMagneticStripeTrackReader"/>, and reads it's information in the 
    /// correct manner.
    /// </summary>
    internal class MagneticStripeTrackMapper
    {
        /// <summary>
        /// Reads a magnetic stripe card.
        /// </summary>
        /// <param name="rawResponse">Response from GCR.</param>
        /// <param name="cardBrands">List of supported brands.</param>
        /// <returns>The card read.</returns>
        internal static CardEntry ReadCard(GcrResponse rawResponse, IList<PinpadCardBrand> cardBrands)
        {
            AbstractMagneticStripeTrackReader reader;
            CardEntry card = null;

            try
            {
                reader = new DefaultMagneticStripeTrackReader();
                card = reader.GetCard(rawResponse, cardBrands);
            }
            catch (Exception)
            {
                reader = new TicketMagneticStripeTrackReader();
                card = reader.GetCard(rawResponse, cardBrands);
            }

            return card;
        }
    }
}
