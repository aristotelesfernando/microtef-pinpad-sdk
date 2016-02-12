namespace Pinpad.Sdk.Model
{
	public interface IPinpadInfos
	{
		string ManufacturerName { get; }
		string Model { get; }
		string Specifications { get; }
		string SerialNumber { get; }
		bool IsContactless { get; }
		string OperatingSystemVersion { get; }
		string ManufacturerVersion { get; }
	}
}
