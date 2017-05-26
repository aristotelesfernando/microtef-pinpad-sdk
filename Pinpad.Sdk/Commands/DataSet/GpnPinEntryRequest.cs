using Pinpad.Sdk.PinpadProperties.Refactor.Formatter;
using Pinpad.Sdk.PinpadProperties.Refactor.Parser;
using Pinpad.Sdk.PinpadProperties.Refactor.Property;
using System;

namespace Pinpad.Sdk.Commands
{
	/// <summary>
	/// GPN pin entry record
	/// </summary>
	public sealed class GpnPinEntryRequest : PinpadProperties.Refactor.Property.BaseProperty 
	{
        /// <summary>
		/// Minimum pin digits
		/// </summary>
		public FixedLengthProperty<Nullable<int>> GPN_MIN { get; private set; }
        /// <summary>
        /// Maximum pin digits
        /// </summary>
        public FixedLengthProperty<Nullable<int>> GPN_MAX { get; private set; }
        /// <summary>
        /// Pin Capture Message
        /// </summary>
        public TextProperty<SimpleMessageProperty> GPN_MSG { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public GpnPinEntryRequest()
		{
			this.GPN_MIN = new FixedLengthProperty<Nullable<int>>("GPN_MIN", 2, false,
                StringFormatter.IntegerStringFormatter, StringParser.IntegerStringParser);
			this.GPN_MAX = new FixedLengthProperty<Nullable<int>>("GPN_MAX", 2, false,
                StringFormatter.IntegerStringFormatter, StringParser.IntegerStringParser);
			this.GPN_MSG = new TextProperty<SimpleMessageProperty>("GPN_MSG", false, 
                StringFormatter.PropertyControllerStringFormatter, SimpleMessageProperty.CustomStringParser, 
                null, new SimpleMessageProperty());

			this.AddProperty(this.GPN_MIN);
			this.AddProperty(this.GPN_MAX);
			this.AddProperty(this.GPN_MSG);
		}
	}
}
