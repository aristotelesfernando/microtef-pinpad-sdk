using Pinpad.Core.Commands;
using Pinpad.Core.Pinpad;
using Pinpad.Sdk.Model;

/* WARNING!
 * 
 * DEPRECATED.
 * MUST BE REFACTORED.
 * 
 */

namespace Pinpad.Core.Pinpad 
{
	/// <summary>
	/// PinPad infos tool
	/// </summary>
	public class PinpadInfos : IPinpadInfos
	{
        // Constants
        public const short ACQUIRER_APP_ID = 8;

		// Members
		/// <summary>
		/// Pinpad communication.
		/// </summary>
		private PinpadCommunication communication;
		/// <summary>
		/// Response from GIN command.
		/// </summary>
		private GinResponse _ginResponse { get; set; }
		/// <summary>
		/// Response from GIN command.
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
		/// Manufacturer Name
		/// </summary>
		public string ManufacturerName {
			get {
				if (ginResponse == null) 
				{
					return null;
				}
				else 
				{
					return ginResponse.GIN_MNAME.Value;
				}
			}
		}
		/// <summary>
		/// Model and Hardware version
		/// </summary>
		public string Model 
		{
			get 
			{
				if (ginResponse == null) 
				{
					return null;
				}
				else 
				{
					return ginResponse.GIN_MODEL.Value;
				}
			}
		}
		/// <summary>
		/// Is Contactless supported?
		/// </summary>
		public bool IsContactless 
		{
			get 
			{
				if (ginResponse == null) 
				{
					return false;
				}
				else 
				{
					return ginResponse.GIN_CTLSUP.Value == "C";
				}
			}
		}
		/// <summary>
		/// Basic software or Operational System versions, without a defined format
		/// </summary>
		public string OperatingSystemVersion 
		{
			get 
			{
				if (ginResponse == null) 
				{
					return null;
				}
				else 
				{
					return ginResponse.GIN_SOVER.Value;
				}
			}
		}
		/// <summary>
		/// Specification version at format "V.VV" or "VVVA" where A is the alphanumeric identifier, example: "108a"
		/// </summary>
		public string Specifications 
		{
			get 
			{
				if (ginResponse == null) 
				{
					return null;
				}
				else 
				{
					return ginResponse.GIN_SPECVER.Value;
				}
			}
		}
		/// <summary>
		/// Manufactured Version at format "VVV.VV YYMMDD"
		/// </summary>
		public string ManufacturerVersion 
		{
			get 
			{
				if (ginResponse == null) 
				{
					return null;
				}
				else 
				{
					return ginResponse.GIN_MANVER.Value;
				}
			}
		}
		/// <summary>
		/// Serial number
		/// </summary>
		public string SerialNumber 
		{
			get 
			{
				if (ginResponse == null) 
				{
					return null;
				}
				else 
				{
					return ginResponse.GIN_SERNUM.Value;
				}
			}
		}

		// Constructor
		public PinpadInfos(PinpadCommunication communication)
			: base()
		{
			this.communication = communication;
		}

		// Method
		/// <summary>
		/// Sends a GIN command to the pinpad.
		/// </summary>
		/// <returns>GIN response.</returns>
		private GinResponse GetGin () 
		{
			GinRequest request = new GinRequest();
			request.GIN_ACQIDX.Value = 00;

			GinResponse response = this.communication.SendRequestAndReceiveResponse<GinResponse>(request);
			return response;
		}
	}
}
