using Pinpad.Sdk.Commands.Context;
using Pinpad.Sdk.Commands.Request;
using Pinpad.Sdk.Properties;
using System;

namespace Pinpad.Sdk.Commands.Response
{
    /// <summary>
    /// Response for <see cref="LfcRequest"/>.
    /// Currenty supported by Ingenico pinpads only.
    /// </summary>
    internal sealed class LfcResponse : BaseResponse
    {
        /// <summary>
        /// Command name, LFC in this case.
        /// </summary>
        public override string CommandName { get { return "LFC"; } }
        /// <summary>
        /// Indicates if the pinpad needs user interaction before returning
        /// any response.
        /// </summary>
        public override bool IsBlockingCommand { get { return false; } }
        /// <summary>
        /// Command length, excluding itself.
        /// </summary>
        public RegionProperty RSP_LEN1 { get; private set; } 
        /// <summary>
        /// Whether the file exists in pinpad memory or not.
        /// </summary>
        public SimpleProperty<Nullable<bool>> LFC_EXISTS { get; private set; }
        /// <summary>
        /// If <see cref="LFC_EXISTS"/> is True, this property contains the size of the
        /// file. Otherwise, if <see cref="LFC_EXISTS"/> is False, it should be 0.
        /// </summary>
        public PinpadFixedLengthProperty<Nullable<decimal>> LFC_FILESIZE { get; set; }

        /// <summary>
        /// Creates the command and all it's fields.
        /// </summary>
        public LfcResponse()
            : base (new IngenicoContext())
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
