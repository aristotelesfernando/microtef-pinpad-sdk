using System;
using Pinpad.Sdk.Commands;
using Pinpad.Sdk.Model;
using Pinpad.Sdk.Properties;

namespace Pinpad.Sdk.Transaction.Mapper.MagneticStripe
{
    // TODO: Doc.
    internal class DefaultMagneticStripeTrackReader : AbstractMagneticStripeTrackReader
    {
        /// <summary>
        /// Start sentinel for track 2.
        /// </summary>
        protected override char Track2StartSentinel { get { return 'B'; } }
        /// <summary>
        /// Track1, separator between different information. Based on ISO 7810/7811.
        /// </summary>
        protected override char Track1FieldSeparator { get { return '^'; } }
        /// <summary>
        /// Track2, separator between different information. Based on ISO 7810/7811.
        /// </summary>
        protected override char Track2FieldSeparator { get { return '='; } }
        /// <summary>
        /// Track 1 is made of several information divided by a <see cref="Track1FieldSeparator">separator</see>. This is the index of card expiration date on track 1.
        /// </summary>
        protected override short ExpirationDateIndexOnTrack1 { get { return 2; } }
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
        protected override short CardholderNameIndex { get { return 1; } }

        /// <summary>
        /// Translates card track data into information.
        /// </summary>
        /// <param name="rawResponse">Raw response from pinpad GCR command.</param>
        /// <returns>Card information.</returns>
        /// <exception cref="System.InvalidOperationException">Thrown when womething went wrong internally.</exception>
        protected override CardEntry MapCardFromTrack(GcrResponse rawResponse)
        {
            CardEntry mappedCard = new CardEntry();

            // Save all tracks:
            if (rawResponse.GCR_TRK1.HasValue == true)
            {
                mappedCard.Track1 = rawResponse.GCR_TRK1.Value;
            }
            if (rawResponse.GCR_TRK2.HasValue == true)
            {
                mappedCard.Track2 = rawResponse.GCR_TRK2.Value;
            }

            // Selecting existing track:
            string selectedTrack = this.MapValidTrack(rawResponse);

            // Selecting corresponding track field separator:
            char fieldSeparator = this.GetFieldSeparator(selectedTrack);

            // Get Service code:
            ServiceCode sc = this.MapServiceCode(selectedTrack, fieldSeparator);

            // Values that don't need to be mapped:
            mappedCard.BrandId = rawResponse.GCR_RECIDX.Value.Value;

            // Mapping PAN, cardholder name, card expiration date and Service Code:
            mappedCard.PrimaryAccountNumber = this.MapPan(selectedTrack, fieldSeparator);
            mappedCard.CardholderName = this.MapCardholderName(selectedTrack, fieldSeparator);
            mappedCard.ExpirationDate = this.MapExpirationDate(selectedTrack, fieldSeparator);
            mappedCard.NeedsPassword = sc.IsPinRequired;
            mappedCard.Type = (sc.IsEmv == true) ? CardType.Emv : CardType.MagneticStripe;

            return mappedCard;
        }

        /// <summary>
        /// Selects a valid track (between track 1 and 2), giving priority to Track 1.
        /// </summary>
        /// <param name="rawResponse">Raw response from pinpad GCR command.</param>
        /// <returns>Valid track to be read.</returns>
        internal string MapValidTrack(GcrResponse rawResponse)
        {
            if (rawResponse.GCR_TRK1.HasValue == true)
            {
                return rawResponse.GCR_TRK1.Value;
            }
            if (rawResponse.GCR_TRK2.HasValue == true)
            {
                return rawResponse.GCR_TRK2.Value;
            }

            throw new InvalidOperationException("Invalid track received. Verify if data sent are according to ISO 7810/7811.");
        }
        /// <summary>
        /// Selects the corresponding separator of the selected track.
        /// </summary>
        /// <param name="track">Selected track.</param>
        /// <returns>Corresponding separator os the track received as parameter.</returns>
        internal char GetFieldSeparator(string track)
        {
            if (track.Contains(this.Track1FieldSeparator.ToString()) == true)
            {
                return this.Track1FieldSeparator;
            }
            if (track.Contains(this.Track2FieldSeparator.ToString()) == true)
            {
                return this.Track2FieldSeparator;
            }

            throw new InvalidOperationException("Invalid track received. Verify if data sent are according to ISO 7810/7811.");
        }
        /// <summary>
        /// Translates track data into a string containing PAN.
        /// </summary>
        /// <param name="track">Selected track.</param>
        /// <param name="separator">Corresponding track separator.</param>
        /// <returns>Primary Account Number (PAN).</returns>
        internal string MapPan(string track, char separator)
        {
            string pan = track.Split(separator)[PanIndex];

            if (pan[0] == this.Track2StartSentinel) { pan = pan.Substring(1, pan.Length - 1); }

            return pan;
        }
        /// <summary>
        /// Translates track data into a string containing cardholder name.
        /// </summary>
        /// <param name="track">Selected track.</param>
        /// <param name="separator">Corresponsing track separator.</param>
        /// <returns>Cardholder name.</returns>
        internal string MapCardholderName(string track, char separator)
        {
            string cardholderName = string.Empty;

            if (separator == this.Track1FieldSeparator)
            {
                cardholderName = track.Split(separator)[this.CardholderNameIndex];
            }

            return cardholderName;
        }
        /// <summary>
        /// Translates track data into a string containing card expiration date.
        /// </summary>
        /// <param name="track">Selected track.</param>
        /// <param name="separator">Corresponding track separator.</param>
        /// <returns>Card expiration date.</returns>
        internal DateTime MapExpirationDate(string track, char separator)
        {
            string date = string.Empty;
            int month, year;

            if (separator == this.Track1FieldSeparator)
            {
                date = track.Split(separator)[this.ExpirationDateIndexOnTrack1]
                            .Substring(0, this.ExpirationDateLength);
            }
            else if (separator == this.Track2FieldSeparator)
            {
                date = track.Split(separator)[this.ExpirationDateIndexOnTrack2]
                            .Substring(0, this.ExpirationDateLength);
            }
            else
            {
                throw new ArgumentException(string.Format("Parameter <track> does not have a \"{0}\" separator.", separator));
            }

            // Getting year. Sum 2000 with year in track date, because this date does not contain current century.
            year = Convert.ToInt32(date.Substring(0, 2)) + 2000;

            // Getting month.
            month = Convert.ToInt32(date.Substring(2));

            // Creating card expiration date:
            DateTime expirationDate = new DateTime(year, month, day: 1);
            return expirationDate;
        }
        /// <summary>
        /// Get card service code from a selected track
        /// </summary>
        /// <param name="track">Selected track.</param>
        /// <param name="separator">Track separator.</param>
        /// <returns></returns>
        internal ServiceCode MapServiceCode(string track, char separator)
        {
            string serviceCode;

            if (separator == this.Track1FieldSeparator)
            {
                serviceCode = track.Split(separator)[this.ExpirationDateIndexOnTrack1]
                                   .Substring(this.ExpirationDateLength, this.ServiceCodeLength);
            }
            else if (separator == this.Track2FieldSeparator)
            {
                serviceCode = track.Split(separator)[this.ExpirationDateIndexOnTrack2]
                                   .Substring(this.ExpirationDateLength, this.ServiceCodeLength);
            }
            else
            {
                throw new ArgumentException(string.Format("Parameter <track> does not have a \"{0}\" separator.", separator));
            }

            return new ServiceCode(serviceCode);
        }
    }
}
