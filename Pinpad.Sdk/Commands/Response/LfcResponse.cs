using Pinpad.Sdk.Properties;
using System;

namespace Pinpad.Sdk.Commands.Response
{
    internal sealed class LfcResponse : BaseResponse
    {
        public override string CommandName { get { return "LFC"; } }
        public override bool IsBlockingCommand { get { return false; } }
        public RegionProperty RSP_LEN1 { get; private set; } 
        public SimpleProperty<Nullable<bool>> LFC_EXISTS { get; private set; }
        public PinpadFixedLengthProperty<Nullable<decimal>> LFC_FILESIZE { get; set; }

        public LfcResponse()
        {
            this.RSP_LEN1 = new RegionProperty("RSP_LEN1", 3);
            this.LFC_EXISTS = new SimpleProperty<Nullable<bool>>("LFC_EXISTS",
                false, 
                DefaultStringFormatter.BooleanStringFormatter,
                DefaultStringParser.BooleanStringParser);

            this.StartRegion(this.RSP_LEN1);
            {
                this.AddProperty(this.LFC_EXISTS);
            }
            this.EndLastRegion();
        }
    }
}
