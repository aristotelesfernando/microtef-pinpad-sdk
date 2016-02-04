using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pinpad.Sdk.Model
{
    /// <summary>
    /// Security information about the card.
    /// </summary>
    public class Pin
    {
        /// <summary>
        /// Pin block, encrypted cardholder password.
        /// </summary>
        public string PinBlock { get; set; }
        /// <summary>
        /// Serial number of DUKPT key used on PIN encryption.
        /// </summary>
        public string KeySerialNumber { get; set; }
        /// <summary>
        /// EMV data, if exists. It's a group of information returned by EMV Kernel, representing several information about the transaction and the terminal itself.
        /// </summary>
        public string EmvData { get; set; }
        /// <summary>
        /// Defines if password authentication is online or offline (using pinpad security internal methods).
        /// </summary>
        public bool IsOnline { get; set; }
		/// <summary>
		/// ARQC.
		/// </summary>
		public string ApplicationCryptogram { get; set; }
    }
}
