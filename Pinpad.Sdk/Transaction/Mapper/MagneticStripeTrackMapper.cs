using Pinpad.Sdk.Model;
using Pinpad.Sdk.Commands;
using Pinpad.Sdk.Transaction.Mapper.MagneticStripe;

namespace Pinpad.Sdk.Transaction
{
    // TODO: Documentar.
    internal class MagneticStripeTrackMapper
    {
        internal static CardEntry GetCard(GcrResponse rawResponse)
        {
            IMagneticStripeTrackReader reader = MagneticStripeTrackMapper.CreateMapper(rawResponse);
            return reader.MapCardFromTrack(rawResponse);
        }
        internal static IMagneticStripeTrackReader CreateMapper(GcrResponse rawResponse)
        {
            // TODO: Adicionar leitura para ticket aqui!
            return new DefaultMagneticStripeTrackReader();
        }
    }
}
