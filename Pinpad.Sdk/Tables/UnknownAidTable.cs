using Pinpad.Sdk.PinpadProperties.Refactor.Formatter;
using Pinpad.Sdk.PinpadProperties.Refactor.Parser;
using Pinpad.Sdk.PinpadProperties.Refactor.Property;
using System;

namespace Pinpad.Sdk
{
    /// <summary>
    /// unknown pattern aid table
    /// </summary>
    public sealed class UnknownAidTable : BaseAidTable
    {
        /// <summary>
        /// Contains unknown data from proprietary Aid patterns
        /// </summary>
        public TextProperty<string> T1_UNKDATA { get; private set; }
        
        /// <summary>
        /// Constructor
        /// </summary>
        public UnknownAidTable() {
            this.T1_UNKDATA = new TextProperty<string>("T1_UNKDATA", true, 
                StringFormatter.StringStringFormatter, StringParser.StringStringParser, String.Empty);

            {//PinPadBaseTableController starts the region and doesn't close
                this.AddProperty(this.T1_UNKDATA);
            }//PinPadBaseTableController starts the region and doesn't close
            this.EndLastRegion();
        }
    }
}
