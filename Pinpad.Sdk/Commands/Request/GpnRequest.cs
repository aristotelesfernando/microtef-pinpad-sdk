using Pinpad.Sdk.Properties;
using System;

namespace Pinpad.Sdk.Commands
{
	/// <summary>
	/// GPN request
	/// </summary>
	public class GpnRequest : BaseCommand {
		/// <summary>
		/// Contructor
		/// </summary>
		public GpnRequest() {
			this.CMD_LEN1 = new RegionProperty("CMD_LEN1", 3);
			this.GPN_METHOD = new SimpleProperty<CryptographyMethod>("GPN_METHOD", false, CryptographyMethod.StringFormatter, CryptographyMethod.StringParser);
			this.GPN_KEYIDX = new PinpadFixedLengthProperty<Nullable<int>>("GPN_KEYIDX", 2, false, DefaultStringFormatter.IntegerStringFormatter, DefaultStringParser.IntegerStringParser);
			this.GPN_WKENC = new PinpadFixedLengthProperty<HexadecimalData>("GPN_WKENC", 32, false, DefaultStringFormatter.HexadecimalRightPaddingStringFormatter, DefaultStringParser.HexadecimalRightPaddingStringParser);
			this.GPN_PAN = new VariableLengthProperty<string>("GPN_PAN", 2, 19, 1.0f, true, false, DefaultStringFormatter.StringStringFormatter, DefaultStringParser.StringStringParser);
			this.GPN_ENTRIES = new PinpadCollectionProperty<GpnPinEntryRequest>("GPN_ENTRIES", 1, 1, 36, DefaultStringFormatter.PropertyControllerStringFormatter, DefaultStringParser.PropertyControllerStringParser<GpnPinEntryRequest>);

			this.StartRegion(this.CMD_LEN1);
			{
				this.AddProperty(this.GPN_METHOD);
				this.AddProperty(this.GPN_KEYIDX);
				this.AddProperty(this.GPN_WKENC);
				this.AddProperty(this.GPN_PAN);
				this.AddProperty(this.GPN_ENTRIES);
			}
			this.EndLastRegion();
		}

		/// <summary>
		/// Name of the command
		/// </summary>
		public override string CommandName { get { return "GPN"; } }

		/// <summary>
		/// Length of the first region of the command
		/// </summary>
		public RegionProperty CMD_LEN1 { get; private set; }

		/// <summary>
		/// Online pin encryption method
		/// </summary>
		public SimpleProperty<CryptographyMethod> GPN_METHOD { get; private set; }

		/// <summary>
		/// MK/WK or DUKPT index
		/// </summary>
		public PinpadFixedLengthProperty<Nullable<int>> GPN_KEYIDX { get; private set; }

		/// <summary>
		/// Working key encrypted by MK if using MK as encryption
		/// </summary>
		public PinpadFixedLengthProperty<HexadecimalData> GPN_WKENC { get; private set; }

		/// <summary>
		/// Card Primary Account Number
		/// </summary>
		public VariableLengthProperty<string> GPN_PAN { get; private set; }

		/// <summary>
		/// Data to be captured
		/// Each entry is a Pin Capture and data will be returned concatenated
		/// By restriction of PCI the sum of GPN_MIN values cannot be lower than 4
		/// </summary>
		public PinpadCollectionProperty<GpnPinEntryRequest> GPN_ENTRIES { get; private set; }
	}
}
