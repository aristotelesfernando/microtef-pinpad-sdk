using PinPadSDK.Property;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PinPadSDK.Commands {
    /// <summary>
    /// Controller for GIN response for GIN request with ACQIDX 00
    /// </summary>
    public class Gin00Response : BaseResponse{
        /// <summary>
        /// Constructor
        /// </summary>
        public Gin00Response() {
            this.RSP_LEN1 = new RegionProperty("RSP_LEN1", 3);
            this.GIN_MNAME = new PinPadFixedLengthPropertyController<string>("GIN_MNAME", 20, false, DefaultStringFormatter.StringStringFormatter, DefaultStringParser.StringStringParser);
            this.GIN_MODEL = new PinPadFixedLengthPropertyController<string>("GIN_MODEL", 19, false, DefaultStringFormatter.StringStringFormatter, DefaultStringParser.StringStringParser);
            this.GIN_CTLSUP = new PinPadFixedLengthPropertyController<string>("GIN_CTLSUP", 1, false, DefaultStringFormatter.StringStringFormatter, DefaultStringParser.StringStringParser);
            this.GIN_SOVER = new PinPadFixedLengthPropertyController<string>("GIN_SOVER", 20, false, DefaultStringFormatter.StringStringFormatter, DefaultStringParser.StringStringParser);
            this.GIN_SPECVER = new PinPadFixedLengthPropertyController<string>("GIN_SPECVER", 4, false, DefaultStringFormatter.StringStringFormatter, DefaultStringParser.StringStringParser);
            this.GIN_MANVER = new PinPadFixedLengthPropertyController<string>("GIN_MANVER", 16, false, DefaultStringFormatter.StringStringFormatter, DefaultStringParser.StringStringParser);
            this.GIN_SERNUM = new PinPadFixedLengthPropertyController<string>("GIN_SERNUM", 20, false, DefaultStringFormatter.StringStringFormatter, DefaultStringParser.StringStringParser);

            this.StartRegion(this.RSP_LEN1);
            {
                this.AddProperty(this.GIN_MNAME);
                this.AddProperty(this.GIN_MODEL);
                this.AddProperty(this.GIN_CTLSUP);
                this.AddProperty(this.GIN_SOVER);
                this.AddProperty(this.GIN_SPECVER);
                this.AddProperty(this.GIN_MANVER);
                this.AddProperty(this.GIN_SERNUM);
            }
            this.EndLastRegion();
        }

        /// <summary>
        /// Is this a blocking command?
        /// </summary>
        public override bool IsBlockingCommand {
            get { return false; }
        }

        /// <summary>
        /// Name of the command
        /// </summary>
        public override string CommandName { get { return "GIN"; } }

        /// <summary>
        /// Length of the first region of the command
        /// </summary>
        public RegionProperty RSP_LEN1 { get; private set; }

        /// <summary>
        /// Manufacturer Name
        /// </summary>
        public PinPadFixedLengthPropertyController<string> GIN_MNAME { get; private set; }

        /// <summary>
        /// Model and hardware version
        /// </summary>
        public PinPadFixedLengthPropertyController<string> GIN_MODEL { get; private set; }

        /// <summary>
        /// Contactless indicator
        /// If 'C' is present contactless is supported
        /// </summary>
        public PinPadFixedLengthPropertyController<string> GIN_CTLSUP { get; private set; }

        /// <summary>
        /// Basic software or Operational system versions, without a defined format
        /// </summary>
        public PinPadFixedLengthPropertyController<string> GIN_SOVER { get; private set; }

        /// <summary>
        /// Specification version at format "V.VV" or "VVVA" where A is the alphanumeric identifier, example: "108a"
        /// </summary>
        public PinPadFixedLengthPropertyController<string> GIN_SPECVER { get; private set; }

        /// <summary>
        /// Manufactured Version at format "VVV.VV YYMMDD"
        /// </summary>
        public PinPadFixedLengthPropertyController<string> GIN_MANVER { get; private set; }

        /// <summary>
        /// Serial number
        /// </summary>
        public PinPadFixedLengthPropertyController<string> GIN_SERNUM { get; private set; }
    }
}
