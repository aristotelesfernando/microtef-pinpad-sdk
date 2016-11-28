using System.Collections.Generic;

namespace Pinpad.Sdk.Model
{
    /// <summary>
    /// Represents a card brand.
    /// </summary>
    public class PinpadCardBrand
    {
        /// <summary>
        /// Card brand identifier in Card Brand Table from TMS.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Card brand brief description.
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Card brand type:
        /// - "1": debit;
        /// - "2": credit.
        /// </summary>
        public TransactionType BrandType { get; set; }
        /// <summary>
        /// BIN ranges for this brand.
        /// </summary>
        public ICollection<PinpadBinRange> Ranges { get; set; }
    }
}
