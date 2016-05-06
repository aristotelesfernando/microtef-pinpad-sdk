namespace Pinpad.Sdk.Commands
{
	/// <summary>
	/// Entry data to finish an EMV authorization.
	/// </summary>
	public class IssuerEmvDataEntry
	{
		/// <summary>
		/// Communication status from acquirer. On ABECS, corresponds to FNC_COMMST.
		/// </summary>
		public AcquirerCommunicationStatus AuthorizationStatus { get; set; }
		/// <summary>
		/// Approval or decline code returned from acquirer authorizer. On ABECS, corresponds to FNC_ARC.
		/// </summary>
		public string AuthorizationResponseCode { get; set; }
		/// <summary>
		/// EMV transaction data received from card issuer, TLV formatted. On ABECS, corresponds to FNC_ISSDAT.
		/// </summary>
		public string IssuerRelatedData { get; set; }
	}
}
