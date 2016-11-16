namespace Pinpad.Sdk.Model
{
    // TODO: Documentar.
    public class PinpadBinRange
    {
        /// <summary>
        /// Initial BIN of the range.
        /// </summary>
        public decimal InitialRange { get; set; }
        /// <summary>
        /// Final BIN of the range.
        /// </summary>
        public decimal FinalRange { get; set; }
        public int TefBrandId { get; set; }
        /// <summary>
        /// DataSet version.
        /// </summary>
        public decimal DataSetVersion { get; set; }

        /// <summary>
        /// Verify if a given number is within this range.
        /// </summary>
        /// <param name="givenNumber">Given number to be verified.</param>
        /// <returns>Whether the given number is within the range or not.</returns>
        public bool IsWithin(decimal givenNumber)
        {
            return givenNumber >= this.InitialRange && givenNumber <= this.FinalRange;
        }
    }
}
