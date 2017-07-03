using Pinpad.Sdk.PinpadProperties.Refactor.Command;
using Pinpad.Sdk.PinpadProperties.Refactor.Property;

namespace Pinpad.Sdk.Commands.Request
{
    /// <summary>
    /// Update Record request.
    /// Sends a piece of the new application to be installed in the device.
    /// </summary>
    public sealed class UprRequest : BaseCommand
    {
        /// <summary>
        /// Serction size, determines the size of a package to be send in each <see cref="UprRequest"/>.
        /// </summary>
        public const int PackageSectionSize = 900;

        /// <summary>
        /// Name of the command.
        /// </summary>
        public override string CommandName { get { return "UPR"; } }
        ///// <summary>
        ///// Length of the first region of the command
        ///// </summary>
        //public RegionProperty CMD_LEN1 { get; private set; }
        /// <summary>
        /// Table records
        /// </summary>
        public BinaryVariableLengthProperty<byte[]> UPR_REC { get; private set; }

        /// <summary>
        /// Creates the request with all it's properties.
        /// </summary>
        public UprRequest()
        {
            this.UPR_REC = new BinaryVariableLengthProperty<byte[]>("TLR_REC", 3, UprRequest.PackageSectionSize, 
                false, false);

            this.AddProperty(this.UPR_REC);
        }   
    }
}
