using Pinpad.Sdk.PinpadProperties.Refactor;
using Pinpad.Sdk.PinpadProperties.Refactor.Formatter;
using Pinpad.Sdk.PinpadProperties.Refactor.Parser;
using Pinpad.Sdk.PinpadProperties.Refactor.Property;
using System;

namespace Pinpad.Sdk.Commands
{
	/// <summary>
	/// GPN request
	/// </summary>
	internal sealed class GpnRequest : PinpadProperties.Refactor.BaseCommand
    {
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
        public TextProperty<CryptographyMethod> GPN_METHOD { get; private set; }
        /// <summary>
        /// MK/WK or DUKPT index
        /// </summary>
        public FixedLengthProperty<Nullable<int>> GPN_KEYIDX { get; private set; }
        /// <summary>
        /// Working key encrypted by MK if using MK as encryption
        /// </summary>
        public FixedLengthProperty<HexadecimalData> GPN_WKENC { get; private set; }
        /// <summary>
        /// Card Primary Account Number
        /// </summary>
        public VariableLengthProperty<string> GPN_PAN { get; private set; }
        /// <summary>
        /// Data to be captured
        /// Each entry is a Pin Capture and data will be returned concatenated
        /// By restriction of PCI the sum of GPN_MIN values cannot be lower than 4
        /// </summary>
        public DataCollectionProperty<GpnPinEntryRequest> GPN_ENTRIES { get; private set; }

        /// <summary>
        /// Contructor
        /// </summary>
        public GpnRequest() {
			this.CMD_LEN1 = new RegionProperty("CMD_LEN1", 3);
			this.GPN_METHOD = new TextProperty<CryptographyMethod>("GPN_METHOD", false, 
                CryptographyMethod.StringFormatter, CryptographyMethod.CustomStringParser);
			this.GPN_KEYIDX = new FixedLengthProperty<Nullable<int>>("GPN_KEYIDX", 2, false, 
                StringFormatter.IntegerStringFormatter, StringParser.IntegerStringParser);
			this.GPN_WKENC = new FixedLengthProperty<HexadecimalData>("GPN_WKENC", 32, false, 
                StringFormatter.HexadecimalRightPaddingStringFormatter, StringParser.HexadecimalRightPaddingStringParser);
			this.GPN_PAN = new VariableLengthProperty<string>("GPN_PAN", 2, 19, 1.0f, true, false, 
                StringFormatter.StringStringFormatter, StringParser.StringStringParser);
			this.GPN_ENTRIES = new DataCollectionProperty<GpnPinEntryRequest>("GPN_ENTRIES", 1, 1, 36, 
                StringFormatter.PropertyControllerStringFormatter, 
                StringParser.PropertyControllerStringParser<GpnPinEntryRequest>);

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
	}
}
