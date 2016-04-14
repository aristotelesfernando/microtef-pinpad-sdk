using Pinpad.Sdk.Model;
using Pinpad.Sdk.Model.TypeCode;
using Pinpad.Sdk.Commands;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

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

            if (response.GCR_TRK1.HasValue == true)
            {
                mappedCard.Track1 = response.GCR_TRK1.Value.CommandString;
            }
            if (response.GCR_TRK2.HasValue == true)
            {
                mappedCard.Track2 = response.GCR_TRK2.Value.CommandString;
            }

            return mappedCard;
        }
		/// <summary>
		/// Get brand name by Application ID.
		/// </summary>
		/// <param name="aid">Application ID.</param>
		/// <returns>Brand name</returns>
		internal static string GetBrandByAid(string aid)
		{
			// TODO: adicionar RID a algum arquivo de recursos
			// TODO: adicionar RID length ao arquivo de recursos
			if (aid.Substring(0, 10) == "A000000003")
			{
				return CardMapper.VISA_LABEL;
			}
			if (aid.Substring(0, 10) == "A000000004")
			{
				return CardMapper.MASTERCARD_LABEL;
			}

			return CardMapper.UNKNOWN_LABEL;
		}
    }
}
