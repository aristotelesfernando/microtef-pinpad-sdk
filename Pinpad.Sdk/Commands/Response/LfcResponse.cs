using Pinpad.Sdk.Commands.Context;
using Pinpad.Sdk.Commands.Request;
using Pinpad.Sdk.PinpadProperties.Refactor.Formatter;
using Pinpad.Sdk.PinpadProperties.Refactor.Parser;
using Pinpad.Sdk.PinpadProperties.Refactor.Property;
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
        public TextProperty<Nullable<bool>> LFC_EXISTS { get; private set; }

        /// <summary>
        /// Creates the command and all it's fields.
        /// </summary>
        public LfcResponse()
            : base (new IngenicoContext())
        {
            this.RSP_LEN1 = new RegionProperty("RSP_LEN1", 3);
            this.LFC_EXISTS = new TextProperty<Nullable<bool>>("LFC_EXISTS",
                false, 
                StringFormatter.BooleanStringFormatter,
                StringParser.BooleanStringParser);

            this.StartRegion(this.RSP_LEN1);
            {
                this.AddProperty(this.LFC_EXISTS);
            }
            this.EndLastRegion();
        }
    }
}
