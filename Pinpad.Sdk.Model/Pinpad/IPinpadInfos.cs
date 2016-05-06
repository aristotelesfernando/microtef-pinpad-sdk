namespace Pinpad.Sdk.Model
{
	/// <summary>
	/// Pinpad informations.
	/// </summary>
	public interface IPinpadInfos
	{
		/// <summary>
		/// Manufacturer Name
		/// </summary>
		string ManufacturerName { get; }
		/// <summary>
		/// Model and Hardware version
		/// </summary>
		string Model { get; }
		/// <summary>
		/// Specification version at format "V.VV" or "VVVA" where A is the alphanumeric identifier, example: "108a"
		/// </summary>
		string Specifications { get; }
		/// <summary>
		/// Unique pinpad identifier.
		/// </summary>
		string SerialNumber { get; }
		/// <summary>
		/// Is Contactless supported?
		/// </summary>
		bool IsContactless { get; }
		/// <summary>
		/// Basic software or Operational System versions, without a defined format
		/// </summary>
		string OperatingSystemVersion { get; }
		/// <summary>
		/// Manufactured Version at format "VVV.VV YYMMDD"
		/// </summary>
		string ManufacturerVersion { get; }
	}
}
