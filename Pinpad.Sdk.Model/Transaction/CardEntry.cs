using System;

namespace Pinpad.Sdk.Model
{
    /// <summary>
    /// Represents a physical card used to perform a transaction.
    /// </summary>
    public class CardEntry
    {
        /// <summary>
        /// Card type, defining the reading method (by EMV chip or magnetic stripe) to be used by the application.
        /// </summary>
        public CardType Type { get; set; }
        /// <summary>
        /// Card brand (for example Visa, MasterCard, Amex, etc).
        /// </summary>
        public int BrandId { get; set; }
		/// <summary>
		/// Brand name corresponding to BrandId.
		/// </summary>
		public string BrandName { get; set; }
		/// <summary>
        /// Unmasked pan, all characters shown.
        /// </summary>
        public string PrimaryAccountNumber { get; set; }
        /// <summary>
        /// Application PAN Sequencial number for joint bank account.
        /// </summary>
        public Nullable<int> PrimaryAccountNumberSequenceNumber { get; set; }
        /// <summary>
        /// Card expiration date, printed in the original physical card.
        /// </summary>
        public DateTime ExpirationDate { get; set; }
        /// <summary>
        /// Cardholder/card owner name, printed in the original physical card.
        /// </summary>
        public string CardholderName { get; set; }
        /// <summary>
        /// First track of card, if exists.
        /// </summary>
        public string Track1 { get; set; }
        /// <summary>
        /// Second track of card, if exists.
        /// </summary>
        public string Track2 { get; set; }
        /// <summary>
        /// Third track of card, if exists.
        /// </summary>
        public string Track3 { get; set; }
		/// <summary>
		/// Defines whether the card needs password or not.
		/// </summary>
		public bool NeedsPassword { get; set; }
		/// <summary>
		/// AID.
		/// </summary>
		public string ApplicationId { get; set; }
		/// <summary>
		/// ARQC.
		/// </summary>
		public string ApplicationCryptogram { get; set; }
    }
}
