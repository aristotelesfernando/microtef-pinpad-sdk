using Pinpad.Sdk.Properties;
using System;

namespace Pinpad.Sdk.Commands.Request
{
    /// <summary>
	/// UPI request. Initializes the application update flow for a WiFi pinpad.
	/// </summary>
    public sealed class UpiRequest : BaseCommand
    {
        /// <summary>
		/// Name of the command
		/// </summary>
        public override string CommandName { get { return "UPI"; } }
        /// <summary>
		/// Length of the first region of the command
		/// </summary>
		public RegionProperty CMD_LEN1 { get; private set; }
        /// <summary>
        /// Size of the application package.
        /// </summary>
        public PinpadFixedLengthProperty<Nullable<int>> UPI_APPSIZE { get; private set; }

        /// <summary>
        /// Creates the command with it's regions.
        /// </summary>
        public UpiRequest()
        {
            this.CMD_LEN1 = new RegionProperty("CMD_LEN1", 3);
            this.UPI_APPSIZE = new PinpadFixedLengthProperty<Nullable<int>>("UPI_APPSIZE", 24, false, 
                DefaultStringFormatter.IntegerStringFormatter, DefaultStringParser.IntegerStringParser);

            this.StartRegion(this.CMD_LEN1);
            {
                this.AddProperty(this.UPI_APPSIZE);
            }
            this.EndLastRegion();
        }
    }
}
