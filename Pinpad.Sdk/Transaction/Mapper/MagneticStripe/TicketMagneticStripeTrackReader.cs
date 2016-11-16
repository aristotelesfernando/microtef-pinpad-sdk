using System;
using Pinpad.Sdk.Commands;
using Pinpad.Sdk.Model;
using Pinpad.Sdk.Properties;

namespace Pinpad.Sdk.Transaction.Mapper.MagneticStripe
{
    internal class TicketMagneticStripeTrackReader : AbstractMagneticStripeTrackReader
    {
        // TODO: Trocar o nome desse cara.
        /// <summary>
        /// Start sentinel for track 2.
        /// </summary>
        protected override char Track2StartSentinel { get { return 'A'; } }
        /// <summary>
        /// Track1, separator between different information. Based on ISO 7810/7811.
        /// </summary>
        protected override char Track1FieldSeparator { get { return '='; } }
        /// <summary>
        /// Track2, separator between different information. Based on ISO 7810/7811.
        /// </summary>
        protected override char Track2FieldSeparator { get { return '='; } }
        /// <summary>
        /// Track 1 is made of several information divided by a <see cref="Track1FieldSeparator">separator</see>. This is the index of card expiration date on track 1.
        /// </summary>
        protected override short ExpirationDateIndexOnTrack1 { get { return -1; } }
        /// <summary>
        /// Track 2 is made of several information divided by a <see cref="Track2FieldSeparator">separator</see>. This is the index of card expiration date on track 2.
        /// </summary>
        protected override short ExpirationDateIndexOnTrack2 { get { return 1; } }
        /// <summary>
        /// Card expiration date string length.
        /// </summary>
        protected override short ExpirationDateLength { get { return 4; } }
        /// <summary>
        /// Card service code string length.
        /// </summary>
        protected override short ServiceCodeLength { get { return 3; } }
        /// <summary>
        /// Track 2 is made of several information divided by a <see cref="Track2FieldSeparator">separator</see>. This is the index of the Primary Account Number (PAN) on track 1.
        /// </summary>
        protected override short PanIndex { get { return 0; } }
        /// <summary>
        /// Track 1 is made of several information divided by a <see cref="Track1FieldSeparator">separator</see>. This is the index of cardholder name on track 1.
        /// </summary>
        protected override short CardholderNameIndex { get { return 0; } }

        protected override CardEntry MapCardFromTrack(GcrResponse rawResponse)
        {
            // Save tracks:
            CardEntry mappedCard = new CardEntry();
            mappedCard.Track1 = rawResponse.GCR_TRK1.Value;
            mappedCard.Track2 = rawResponse.GCR_TRK2.Value;

            // Get service code:
            ServiceCode serviceCode = this.MapServiceCode(mappedCard.Track2);

            // Values that don't need to be mapped:
            mappedCard.BrandId = rawResponse.GCR_RECIDX.Value.Value;

            // Mapping PAN, cardholder name, card expiration date and Service Code:
            mappedCard.PrimaryAccountNumber = this.MapPan(mappedCard.Track2);
            mappedCard.CardholderName = this.MapCardholderName(mappedCard.Track1);
            mappedCard.ExpirationDate = this.MapExpirationDate(mappedCard.Track2);
            mappedCard.NeedsPassword = serviceCode.IsPinMandatory;
            mappedCard.Type = (serviceCode.IsEmv == true) ? CardType.Emv : CardType.MagneticStripe;

            return mappedCard;
        }

        // TODO: Documentar.
        internal string MapPan (string track2)
        {
            string pan = track2.Split(this.Track2FieldSeparator)[PanIndex];
            
            return pan;
        }
        // TODO: Documentar.
        internal string MapCardholderName(string track1)
        {
            string cardholderName = track1.Split(this.Track1FieldSeparator)[this.CardholderNameIndex];

            if (cardholderName[0] == this.Track2StartSentinel)
            {
                cardholderName = cardholderName.Substring(1, cardholderName.Length - 1);
            }

            return cardholderName;
        }
        // TODO: Documentar.
        internal DateTime MapExpirationDate(string track2)
        {
            string date = string.Empty;
            int month, year;

            date = track2.Split(this.Track2FieldSeparator)[this.ExpirationDateIndexOnTrack2]
                         .Substring(0, this.ExpirationDateLength);

            // Getting year. Sum 2000 with year in track date, because this date does not contain current century.
            year = Convert.ToInt32(date.Substring(0, 2)) + 2000;

            // Getting month.
            month = Convert.ToInt32(date.Substring(2));

            // Creating card expiration date:
            DateTime expirationDate = new DateTime(year, month, day: 1);
            return expirationDate;
        }
        // TODO: Documentar.
        internal ServiceCode MapServiceCode(string track2)
        {
            string serviceCode;

            serviceCode = track2.Split(this.Track2FieldSeparator)[this.ExpirationDateIndexOnTrack2]
                                .Substring(this.ExpirationDateLength, this.ServiceCodeLength);

            return new ServiceCode(serviceCode);
        }
    }
}
