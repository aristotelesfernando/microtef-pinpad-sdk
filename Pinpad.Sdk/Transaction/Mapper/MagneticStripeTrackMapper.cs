using Pinpad.Sdk.Model;
using Pinpad.Sdk.Commands;
using System;

namespace Pinpad.Sdk.Transaction
{
    internal class MagneticStripeTrackMapper
    {
        // Constants
        /// <summary>
        /// Track1, separator between different information. Based on ISO 7810/7811.
        /// </summary>
        internal const char TRACK1_FIELD_SEPARATOR = '^';
        /// <summary>
        /// Track2, separator between different information. Based on ISO 7810/7811.
        /// </summary>
        internal const char TRACK2_FIELD_SEPARATOR = '=';
        /// <summary>
        /// Track 1 is made of several information divided by a <see cref="TRACK1_FIELD_SEPARATOR">separator</see>. This is the index of card expiration date on track 1.
        /// </summary>
        internal const short EXPIRATIONDATE_INDEX_ON_TRACK1 = 2;
        /// <summary>
        /// Track 2 is made of several information divided by a <see cref="TRACK2_FIELD_SEPARATOR">separator</see>. This is the index of card expiration date on track 2.
        /// </summary>
        internal const short EXPIRATIONDATE_INDEX_ON_TRACK2 = 1;
        /// <summary>
        /// Card expiration date string length.
        /// </summary>
        internal const short EXPIRATIONDATE_LENGTH = 4;
		/// <summary>
		/// Track 2 is made of several information divided by a <see cref="TRACK2_FIELD_SEPARATOR">separator</see>. This is the index of the Primary Account Number (PAN) on track 1.
		/// </summary>
		internal const short PAN_INDEX = 0;
        /// <summary>
        /// Track 1 is made of several information divided by a <see cref="TRACK1_FIELD_SEPARATOR">separator</see>. This is the index of cardholder name on track 1.
        /// </summary>
        internal const short CARDHOLDERNAME_INDEX = 1;

        // Methods
        /// <summary>
        /// Translates card track data into information.
        /// </summary>
        /// <param name="rawResponse">Raw response from pinpad GCR command.</param>
        /// <returns>Card information.</returns>
        /// <exception cref="System.InvalidOperationException">Thrown when womething went wrong internally.</exception>
        internal static CardEntry MapCardFromTrack(GcrResponse rawResponse)
        {
            CardEntry mappedCard = new CardEntry();

            // Save all tracks:
			if (rawResponse.GCR_TRK1.HasValue == true)
			{
				mappedCard.Track1 = rawResponse.GCR_TRK1.Value.CommandString;
			}
			if (rawResponse.GCR_TRK2.HasValue == true)
			{
				mappedCard.Track2 = rawResponse.GCR_TRK2.Value.CommandString;
			}

            // Selecting existing track:
            string selectedTrack = MagneticStripeTrackMapper.MapValidTrack(rawResponse);
            
            // Selecting corresponding track field separator:
            char fieldSeparator = MagneticStripeTrackMapper.GetFieldSeparator(selectedTrack);

            // Values that don't need to be mapped:
            mappedCard.Type = CardType.MagneticStripe;
            mappedCard.BrandId = rawResponse.GCR_RECIDX.Value.Value;

            // Mapping PAN, cardholder name and card expiration date:
            mappedCard.PrimaryAccountNumber = MagneticStripeTrackMapper.MapPan(selectedTrack, fieldSeparator);
			mappedCard.BrandName = MagneticStripeTrackMapper.GetBrandByPan(mappedCard.PrimaryAccountNumber);
            mappedCard.CardholderName = MagneticStripeTrackMapper.MapCardholderName(selectedTrack, fieldSeparator);
            mappedCard.ExpirationDate = MagneticStripeTrackMapper.MapExpirationDate(selectedTrack, fieldSeparator);
            
            return mappedCard;
        }
        /// <summary>
        /// Selects a valid track (between track 1 and 2), giving priority to Track 1.
        /// </summary>
        /// <param name="rawResponse">Raw response from pinpad GCR command.</param>
        /// <returns>Valid track to be read.</returns>
        internal static string MapValidTrack(GcrResponse rawResponse)
        {
            if (rawResponse.GCR_TRK1.HasValue == true)
            {
                return rawResponse.GCR_TRK1.Value.CommandString;
            }
            if (rawResponse.GCR_TRK2.HasValue == true)
            {
                return rawResponse.GCR_TRK2.Value.CommandString;
            }

            throw new InvalidOperationException("Invalid track received. Verify if data sent are according to ISO 7810/7811.");
        }
        /// <summary>
        /// Selects the corresponding separator of the selected track.
        /// </summary>
        /// <param name="track">Selected track.</param>
        /// <returns>Corresponding separator os the track received as parameter.</returns>
        internal static char GetFieldSeparator(string track)
        {
            if (track.Contains(TRACK1_FIELD_SEPARATOR.ToString()) == true)
            {
                return TRACK1_FIELD_SEPARATOR;
            }
            if (track.Contains(TRACK2_FIELD_SEPARATOR.ToString()) == true)
            {
                return TRACK2_FIELD_SEPARATOR;
            }

            throw new InvalidOperationException("Invalid track received. Verify if data sent are according to ISO 7810/7811.");
        }
        /// <summary>
        /// Translates track data into a string containing PAN.
        /// </summary>
        /// <param name="track">Selected track.</param>
        /// <param name="separator">Corresponding track separator.</param>
        /// <returns>Primary Account Number (PAN).</returns>
        internal static string MapPan(string track, char separator)
        {
            string pan = track.Split(separator)[PAN_INDEX];
            
            // TODO: redo! For gods sake!
            if (pan[0] == 'B') { pan = pan.Substring(1, pan.Length - 1); }
            
            return pan;
        }
        /// <summary>
        /// Translates track data into a string containing cardholder name.
        /// </summary>
        /// <param name="track">Selected track.</param>
        /// <param name="separator">Corresponsing track separator.</param>
        /// <returns>Cardholder name.</returns>
        internal static string MapCardholderName(string track, char separator)
        {
            string cardholderName = string.Empty;

            if (separator == TRACK1_FIELD_SEPARATOR)
            {
                cardholderName = track.Split(separator)[CARDHOLDERNAME_INDEX];
            }

            return cardholderName;
        }
        /// <summary>
        /// Translates track data into a string containing card expiration date.
        /// </summary>
        /// <param name="track">Selected track.</param>
        /// <param name="separator">Corresponding track separator.</param>
        /// <returns>Card expiration date.</returns>
        internal static DateTime MapExpirationDate(string track, char separator)
        {
            string date = string.Empty;
            int month, year;

            if (separator == TRACK1_FIELD_SEPARATOR)
            {
                date = track.Split(separator)[EXPIRATIONDATE_INDEX_ON_TRACK1].Substring(0, EXPIRATIONDATE_LENGTH);
            }
            else if (separator == TRACK2_FIELD_SEPARATOR)
            {
                date = track.Split(separator)[EXPIRATIONDATE_INDEX_ON_TRACK2].Substring(0, EXPIRATIONDATE_LENGTH);
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
		/// Get brand name by primary account number first 4 digits.
		/// </summary>
		/// <returns>Brand name.</returns>
		internal static string GetBrandByPan(string pan)
		{
			// Verificar através dos dígitos do PAN
			
			// TODO: adicionar bin length
			int bin = Int32.Parse(pan.Substring(0, 4));

			// TODO: adicionar todos os valores mágicos a um arquivo de recursos, com descrição!!!!!!!!!!!!
			if (bin >= 4000 && bin < 5000)
			{
				return CardMapper.VISA_LABEL;
			}
			if (bin >= 5000 && bin < 5600)
			{
				return CardMapper.MASTERCARD_LABEL;
			}
			
			// Outliers:
			switch (bin)
			{
				case 5018:
				case 5020:
				case 5038:
				case 5612:
				case 5893:
				case 5899:
				case 6304:
				case 6759:
				case 6761:
				case 6762:
				case 6763:
				case 0604:
				case 6036:
				case 6390:
					return CardMapper.MASTERCARD_LABEL;
			}

			return CardMapper.UNKNOWN_LABEL;
		}
    }
}
