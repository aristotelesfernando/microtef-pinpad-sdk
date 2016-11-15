using Pinpad.Sdk.Model;
using Pinpad.Sdk.Commands;
using System.Diagnostics;

namespace Pinpad.Sdk.Transaction
{
    internal class EmvTrackMapper
    {
        internal static CardEntry MapCardFromEmvTrack(GcrResponse response)
        {
            CardEntry mappedCard = new CardEntry();

			if (response == null) { Debug.WriteLine("GCR response is null."); }

            mappedCard.Type = CardType.Emv;
			if (response.GCR_RECIDX.HasValue == true)
			{
				mappedCard.BrandId = response.GCR_RECIDX.Value.Value;
				Debug.WriteLine(mappedCard.BrandId);
			}
			else { Debug.WriteLine("GCR_RECIDX null."); }

			if (response.GCR_CHNAME.HasValue == true)
			{
				mappedCard.CardholderName = response.GCR_CHNAME.Value;
				Debug.WriteLine(mappedCard.CardholderName);
			}
			else { Debug.WriteLine("GCR_CHNAME null."); }

			if (response.GCR_CARDEXP.HasValue == true)
			{
	            mappedCard.ExpirationDate = response.GCR_CARDEXP.Value.Value;
				Debug.WriteLine(mappedCard.ExpirationDate);
			}
			else { Debug.WriteLine("GCR_CARDEXP null."); }

			if (response.GCR_PAN.HasValue == true)
			{
				mappedCard.PrimaryAccountNumber = response.GCR_PAN.Value;
				Debug.WriteLine(mappedCard.PrimaryAccountNumber);
			}
			else { Debug.WriteLine("GCR_PAN null."); }

            if (response.GCR_PANSEQNO.HasValue ==  true)
            {
                mappedCard.PrimaryAccountNumberSequenceNumber = response.GCR_PANSEQNO.Value;
                Debug.WriteLine(mappedCard.PrimaryAccountNumberSequenceNumber);
            }

            if (response.GCR_TRK1.HasValue == true)
            {
                mappedCard.Track1 = response.GCR_TRK1.Value.CommandString;
            }
            if (response.GCR_TRK2.HasValue == true)
            {
                mappedCard.Track2 = response.GCR_TRK2.Value.CommandString;
            }

			mappedCard.NeedsPassword = true;

            return mappedCard;
        }
    }
}
