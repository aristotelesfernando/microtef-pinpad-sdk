using Pinpad.Sdk.Commands.DataSet;
using Pinpad.Sdk.PinpadProperties.Refactor.Formatter;
using Pinpad.Sdk.PinpadProperties.Refactor.Parser;
using Pinpad.Sdk.PinpadProperties.Refactor.Property;
using System;

namespace Pinpad.Sdk.Commands.Response
{
    /// <summary>
    /// CEX response.
    /// </summary>
    internal class CexResponse : BaseResponse
    {
        /// <summary>
        /// Is this a blocking command?
        /// </summary>
        public override bool IsBlockingCommand { get { return true; } }
        /// <summary>
        /// Name of the command.
        /// </summary>
        public override string CommandName { get { return "CEX"; } }
        /// <summary>
        /// Length of the first region of the command.
        /// </summary>
        public RegionProperty RSP_LEN1 { get; private set; }
        /// <summary>
        /// Identifier of the ocurred event.
        /// Supported events listed <see cref="CexEventRead"/>.
        /// </summary>
        public FixedLengthProperty<Nullable<int>> PP_EVENT { get; private set; }
    
        /// <summary>
        /// Constructor.
        /// </summary>
        public CexResponse()
        {
            this.RSP_LEN1 = new RegionProperty("CMD_LEN1", 3);
            this.PP_EVENT = new FixedLengthProperty<int?>("PP_EVENT", 2, false,
                StringFormatter.IntegerStringFormatter, StringParser.IntegerStringParser);

            this.StartRegion(RSP_LEN1);
            {
                this.AddProperty(this.PP_EVENT);
            }
            this.EndLastRegion();
        }
    }
}
