using Pinpad.Sdk.Model;
using Pinpad.Sdk.Commands;
using Pinpad.Sdk.Transaction.Mapper.MagneticStripe;
using System.Collections.Generic;
using System;

namespace Pinpad.Sdk.Transaction
{
    // TODO: Documentar.
    internal class MagneticStripeTrackMapper
    {
        internal static CardEntry GetCard(GcrResponse rawResponse, IList<PinpadCardBrand> cardBrands)
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
