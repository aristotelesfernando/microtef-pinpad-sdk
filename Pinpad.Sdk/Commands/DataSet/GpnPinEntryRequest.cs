using Pinpad.Sdk.Properties;
using System;

namespace Pinpad.Sdk.Commands
{
	/// <summary>
	/// GPN pin entry record
	/// </summary>
	public class GpnPinEntryRequest : BaseProperty 
	{
		/// <summary>
		/// Constructor
		/// </summary>
		public GpnPinEntryRequest()
		{
			this.GPN_MIN = new PinpadFixedLengthProperty<Nullable<int>>("GPN_MIN", 2, false, DefaultStringFormatter.IntegerStringFormatter, DefaultStringParser.IntegerStringParser);
			this.GPN_MAX = new PinpadFixedLengthProperty<Nullable<int>>("GPN_MAX", 2, false, DefaultStringFormatter.IntegerStringFormatter, DefaultStringParser.IntegerStringParser);
			this.GPN_MSG = new SimpleProperty<SimpleMessage>("GPN_MSG", false, DefaultStringFormatter.PropertyControllerStringFormatter, SimpleMessage.StringParser, null, new SimpleMessage());

			this.AddProperty(this.GPN_MIN);
			this.AddProperty(this.GPN_MAX);
			this.AddProperty(this.GPN_MSG);
		}

		/// <summary>
		/// Minimum pin digits
		/// </summary>
		public PinpadFixedLengthProperty<Nullable<int>> GPN_MIN { get; private set; }

		/// <summary>
		/// Maximum pin digits
		/// </summary>
		public PinpadFixedLengthProperty<Nullable<int>> GPN_MAX { get; private set; }

		/// <summary>
		/// Pin Capture Message
		/// </summary>
		public SimpleProperty<SimpleMessage> GPN_MSG { get; private set; }
	}
}
