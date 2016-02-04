﻿using PinPadSDK.Property;
using PinPadSDK.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StonePortableUtils;

namespace PinPadSDK.Controllers.Tables {
    /// <summary>
    /// Controller for CAPK revoked certificates
    /// </summary>
    public class RevCerTable : BaseTable {
        /// <summary>
        /// Constructor
        /// </summary>
        public RevCerTable() {
            this.T3_RID = new PinPadFixedLengthPropertyController<HexadecimalData>("T3_RID", 10, false, DefaultStringFormatter.HexadecimalStringFormatter, DefaultStringParser.HexadecimalStringParser);
            this.T3_CAPKIDX = new PinPadFixedLengthPropertyController<HexadecimalData>("T3_CAPKIDX", 2, false, DefaultStringFormatter.HexadecimalStringFormatter, DefaultStringParser.HexadecimalStringParser);
            this.T3_CERTSN = new PinPadFixedLengthPropertyController<HexadecimalData>("T3_CERTSN", 6, false, DefaultStringFormatter.HexadecimalStringFormatter, DefaultStringParser.HexadecimalStringParser);

            {//PinPadBaseTableController starts the region and doesn't close
                this.AddProperty(this.T3_RID);
                this.AddProperty(this.T3_CAPKIDX);
                this.AddProperty(this.T3_CERTSN);
            }//PinPadBaseTableController starts the region and doesn't close
            this.EndLastRegion();
        }

        /// <summary>
        /// Table Identifier
        /// </summary>
        public override EmvTableType TAB_ID {
            get {
                return EmvTableType.RevokedCertificate;
            }
        }

        /// <summary>
        /// Registered Application provider Identifier
        /// </summary>
        public PinPadFixedLengthPropertyController<HexadecimalData> T3_RID { get; private set; }

        /// <summary>
        /// Certification Authority Public Key Index
        /// </summary>
        public PinPadFixedLengthPropertyController<HexadecimalData> T3_CAPKIDX { get; private set; }

        /// <summary>
        /// Certificate Serial Number
        /// </summary>
        public PinPadFixedLengthPropertyController<HexadecimalData> T3_CERTSN { get; private set; }
    }
}
