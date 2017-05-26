using Pinpad.Sdk.PinpadProperties.Refactor.Formatter;
using Pinpad.Sdk.PinpadProperties.Refactor.Parser;
using Pinpad.Sdk.PinpadProperties.Refactor.Property;

namespace Pinpad.Sdk.Commands.Response
{
    /// <summary>
    /// Response from a Get Application Version. It gets the version of the current application running in the 
    /// device.
    /// This is a command made by Stone Payments, and it's only available for Stone Wi-Fi Pinpads.
    /// </summary>
    internal sealed class GavResponse : BaseResponse
    {
        /// <summary>
        /// GAV command name as string.
        /// </summary>
        public override string CommandName { get { return "GAV"; } }
        /// <summary>
        /// Whether the command needs user interaction to return the response.
        /// </summary>
        public override bool IsBlockingCommand { get { return false; } }
        /// <summary>
        /// Length of the first region of the command.
        /// </summary>
        public RegionProperty RSP_LEN1 { get; private set; }
        /// <summary>
        /// Pinpad Wi-Fi application version under the format x.y.z.
        /// </summary>
        public VariableLengthProperty<string> GAV_APPVER { get; private set; }

        /// <summary>
        /// Creates the response with it's properties.
        /// </summary>
        public GavResponse()
        {
            this.GAV_APPVER = new VariableLengthProperty<string>("GAV_APPVER", 3, 
                16, 1, false, false, 
                StringFormatter.StringStringFormatter, 
                StringParser.StringStringParser);
            
            this.AddProperty(this.GAV_APPVER);
        }
    }
}
