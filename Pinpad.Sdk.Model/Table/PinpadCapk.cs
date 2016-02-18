using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pinpad.Sdk.Model
{
    /// <summary>
    /// Certification Authority Public Key. 
    /// Embraces all Certificated Authority public keys, used by EMV cards at an authentication process and PIN encryption during an offline transaction.
    /// </summary>
    public class PinpadCapk : BaseTableEntry
    {
        /// <summary>
        /// Acquirer identifier. Responsible for the AID table (in ABECS, Stone is represented by "8").
        /// </summary>
        public int AcquirerNumber { get; set; }
        /// <summary>
        /// Certification Authority Public Key Index.
        /// (TAG 9f22)
        /// </summary>
        public string CapkIndex { get; set; }
        /// <summary>
        /// Table record index (from "01" to "99").
        /// </summary>
        public string CapkIndexInTable { get; set; }
        /// <summary>
        /// Result of an algorithm that verifies the integrity of CAPK data. If this CheckSum doesn't match with it's correspondent, it means data are corrupted.
        /// </summary>
        public string CheckSum { get; set; }
        /// <summary>
        /// Whether checksum exists or not.
        /// - false: unused;
        /// - true: used.
        /// </summary>
        public bool CheckSumStatus { get; set; }
        /// <summary>
        /// DataSet version.
        /// </summary>
        public decimal DataSetVersion { get; set; }
        /// <summary>
        /// Certification Authority Public Key Exponent.
        /// </summary>
        public string Exponent { get; set; }
        /// <summary>
        /// <see cref="Exponent">Exponent</see> field length.
        /// </summary>
        public Nullable<int> ExponentLength { get; set; }
        /// <summary>
        /// Certification Authority Public Key Modulus.
        /// </summary>
        public string Modulus { get; set; }
        /// <summary>
        /// <see cref="Modulus">Modulus</see> field length.
        /// </summary>
        public Nullable<int> ModulusLength { get; set; }
        /// <summary>
        /// RID - Recorded Application Provider Identifier.
        /// </summary>
        public string RecordedIdentification { get; set; }
    }
}
