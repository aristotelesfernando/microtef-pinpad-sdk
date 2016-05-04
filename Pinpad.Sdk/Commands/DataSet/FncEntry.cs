namespace Pinpad.Sdk.Commands
{
	public class IssuerEmvDataEntry
	{
		public AcquirerCommunicationStatus AuthorizationStatus { get; set; }
		public string AuthorizationResponseCode { get; set; }
		public string IssuerRelatedData { get; set; }
	}
}
