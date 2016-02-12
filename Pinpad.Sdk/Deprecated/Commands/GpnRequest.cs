using PinPadSDK.Property;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PinPadSDK.Commands.Gpn;
using StonePortableUtils;

namespace PinPadSDK.Commands {
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
            this.GPN_KEYIDX = new PinPadFixedLengthPropertyController<int?>("GPN_KEYIDX", 2, false, DefaultStringFormatter.IntegerStringFormatter, DefaultStringParser.IntegerStringParser);
            this.GPN_WKENC = new PinPadFixedLengthPropertyController<HexadecimalData>("GPN_WKENC", 32, false, DefaultStringFormatter.HexadecimalRightPaddingStringFormatter, DefaultStringParser.HexadecimalRightPaddingStringParser);
            this.GPN_PAN = new VariableLengthProperty<string>("GPN_PAN", 2, 19, 1.0f, true, false, DefaultStringFormatter.StringStringFormatter, DefaultStringParser.StringStringParser);
            this.GPN_ENTRIES = new PinPadCollectionPropertyController<GpnPinEntryRequest>("GPN_ENTRIES", 1, 1, 36, DefaultStringFormatter.PropertyControllerStringFormatter, DefaultStringParser.PropertyControllerStringParser<GpnPinEntryRequest>);

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
        public PinPadFixedLengthPropertyController<Nullable<int>> GPN_KEYIDX { get; private set; }

        /// <summary>
        /// Working key encrypted by MK if using MK as encryption
        /// </summary>
        public PinPadFixedLengthPropertyController<HexadecimalData> GPN_WKENC { get; private set; }

        /// <summary>
        /// Card Primary Account Number
        /// </summary>
        public VariableLengthProperty<string> GPN_PAN { get; private set; }

        /// <summary>
        /// Data to be captured
        /// Each entry is a Pin Capture and data will be returned concatenated
        /// By restriction of PCI the sum of GPN_MIN values cannot be lower than 4
        /// </summary>
        public PinPadCollectionPropertyController<GpnPinEntryRequest> GPN_ENTRIES { get; private set; }
    }
}
