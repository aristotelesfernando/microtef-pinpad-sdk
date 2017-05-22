using System;
using Pinpad.Sdk.Commands;
using Pinpad.Sdk.Model;
using Pinpad.Sdk.Model.TypeCode;
using Pinpad.Sdk.Properties;

namespace Pinpad.Sdk 
{
	/// <summary>
	/// Pinpad informations.
	/// </summary>
	public sealed class PinpadInfos : IPinpadInfos
	{
		// Members
		/// <summary>
		/// Pinpad communication.
		/// </summary>
		private PinpadCommunication communication;
		/// <summary>
		/// Response from GIN command. GIN command returns pinpad information.
		/// </summary>
		private GinResponse _ginResponse;
		/// <summary>
		/// Response from GIN command. GIN command returns pinpad information.
		/// </summary>
		private GinResponse ginResponse 
		{
			get 
			{
				if (_ginResponse == null) 
				{
					_ginResponse = this.GetGin();
				}
				return _ginResponse;
			}
		}
		/// <summary>
		/// Response from GDU command. GDU command searches for a specified acquirer encryption key, and can be used to verify if Stone is supported in this pinpad.
		/// </summary>
		private GduResponse _gduResponse;
		/// <summary>
		/// Response from GDU command. GDU command searches for a specified acquirer encryption key, and can be used to verify if Stone is supported in this pinpad.
		/// </summary>
		private GduResponse gduResponse
		{
			get
			{
				if (this._gduResponse == null)
				{
					this._gduResponse = this.GetGdu();
				}
				return this._gduResponse;
			}
		}

		// Pinpad information members:
		/// <summary>
		/// Manufacturer Name
		/// </summary>
		public string ManufacturerName
		{
			get
			{
				if (ginResponse == null) { return null; }
				else { return ginResponse.GIN_MNAME.Value; }
			}
		}
		/// <summary>
		/// Model and Hardware version
		/// </summary>
		public string Model 
		{
			get 
			{
				if (ginResponse == null) { return null; }
				else { return ginResponse.GIN_MODEL.Value; }
			}
		}
		/// <summary>
		/// Is Contactless supported?
		/// </summary>
		public bool IsContactless 
		{
			get 
			{
				if (ginResponse == null) { return false; }
				else { return ginResponse.GIN_CTLSUP.Value == "C"; }
			}
		}
		/// <summary>
		/// Basic software or Operational System versions, without a defined format
		/// </summary>
		public string OperatingSystemVersion 
		{
			get 
			{
				if (ginResponse == null) { return null; }
				else { return ginResponse.GIN_SOVER.Value; }
			}
		}
		/// <summary>
		/// Specification version at format "V.VV" or "VVVA" where A is the alphanumeric identifier, example: "108a"
		/// </summary>
		public string Specifications 
		{
			get 
			{
				if (ginResponse == null) { return null; }
				else { return ginResponse.GIN_SPECVER.Value; }
			}
		}
		/// <summary>
		/// Manufactured Version at format "VVV.VV YYMMDD"
		/// </summary>
		public string ManufacturerVersion 
		{
			get 
			{	
				if (ginResponse == null) { return null; }
				else { return ginResponse.GIN_MANVER.Value; }
			}
		}
		/// <summary>
		/// Serial number
		/// </summary>
		public string SerialNumber 
		{
			get 
			{
				if (this.ginResponse == null) { return null; }
				else { return ginResponse.GIN_SERNUM.Value; }
			}
		}
		/// <summary>
		/// If stone is supported on this pinpad.
		/// </summary>
		public bool IsStoneSupported
		{
			get
			{
				if (this.gduResponse == null) { return false; }
				else { return this.gduResponse.IsStoneSupported; }
			}
		}
        // TODO: Doc
        public bool IsStoneProprietaryDevice
        {
            get
            {
                if (this.ginResponse == null)
                {
                    return false;
                }
                else
                {
                    return ginResponse.GIN_ISSTONE.Value == 1;
                }
            }
        }

        // Constructor
        /// <summary>
        /// Pinpad information.
        /// </summary>
        /// <param name="communication">Pinpad communication.</param>
        public PinpadInfos(PinpadCommunication communication)
		{
			this.communication = communication;
		}

        // Public Methods
        /// <summary>
        /// Obtains the current KSN (Key Serial Number) of an index in the table.
        /// </summary>
        /// <param name="indexToSearch">KSN index.</param>
        /// <param name="cryptographyMode">Cryptography method.</param>
        /// <returns>The obtained KSN, or null if the KSN was not found.</returns>
        public string GetDukptSerialNumber(int indexToSearch, CryptographyMode cryptographyMode)
        {
            // Setup request:
            GduRequest request = new GduRequest();
            request.GDU_IDX.Value = indexToSearch;
            request.GDU_METHOD.Value = new CryptographyMethod(KeyManagementMode.DerivedUniqueKeyPerTransaction,
                cryptographyMode);

            // Send request to the pinpad:
            GduResponse response = this.communication
                .SendRequestAndReceiveResponse<GduResponse>(request);

            // Verify if the command was successful:
            if (response != null && response.RSP_STAT.Value == AbecsResponseStatus.ST_OK)
            {
                // Return KSN (if any):
                return response.GDU_KSN.Value;
            }

            // Failure:
            return null;
        }
        /// <summary>
        /// Update pinpad informations.
        /// </summary>
        public void Update ()
        {
            this._ginResponse = null;
			this._gduResponse = null;
        }

		// Private Methods
		/// <summary>
		/// Sends a GIN command to the pinpad.
		/// </summary>
		/// <returns>GIN response.</returns>
		private GinResponse GetGin () 
		{
			// Creates the GIN request:
			GinRequest request = new GinRequest();

			// Sets it to refer to all acquirers:
			// flag de acquirer
			request.GIN_ACQIDX.Value = (int) StoneIndexCode.Generic;


			// Sends the request and gets the response:
			GinResponse response = this.communication.SendRequestAndReceiveResponse<GinResponse>(request);

			// Returns the response:
			return response;
		}
		/// <summary>
		/// Searches for Stone encryption DUKPT:TDES key, on pinpad index 16.
		/// </summary>
		/// <returns>GDU response.</returns>
		private GduResponse GetGdu ()
		{
			// Creates GDU request:
			GduRequest request = new GduRequest();

			// Sets GDU to search for Stone encryption index (16):
			request.GDU_IDX.Value = (int)StoneIndexCode.EncryptionKey;

			// Sets GDU to search for a DUKPT:TDES key:
			request.GDU_METHOD.Value = new CryptographyMethod(KeyManagementMode.DerivedUniqueKeyPerTransaction, CryptographyMode.TripleDataEncryptionStandard);

			// Sends the request to pinpad, gets and returns it's response:
			return this.communication.SendRequestAndReceiveResponse<GduResponse>(request);
		}
    }
}
