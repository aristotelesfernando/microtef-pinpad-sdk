﻿using Pinpad.Sdk.Properties;
using System;

namespace Pinpad.Sdk
{
    /// <summary>
    /// unknown pattern aid table
    /// </summary>
    public sealed class UnknownAidTable : BaseAidTable {
        /// <summary>
        /// Constructor
        /// </summary>
        public UnknownAidTable() {
            this.T1_UNKDATA = new SimpleProperty<string>("T1_UNKDATA", true, DefaultStringFormatter.StringStringFormatter, DefaultStringParser.StringStringParser, String.Empty);

            {//PinPadBaseTableController starts the region and doesn't close
                this.AddProperty(this.T1_UNKDATA);
            }//PinPadBaseTableController starts the region and doesn't close
            this.EndLastRegion();
        }

        /// <summary>
        /// Contains unknown data from proprietary Aid patterns
        /// </summary>
        public SimpleProperty<string> T1_UNKDATA { get; private set; }
    }
}
