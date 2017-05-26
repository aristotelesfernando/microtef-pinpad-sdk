using Pinpad.Sdk.PinpadProperties.Refactor;
using Pinpad.Sdk.PinpadProperties.Refactor.Formatter;
using Pinpad.Sdk.PinpadProperties.Refactor.Parser;
using Pinpad.Sdk.PinpadProperties.Refactor.Property;

namespace Pinpad.Sdk.Commands 
{
	/// <summary>
	/// PinPad response command
	/// </summary>
	internal abstract class BaseResponse : BaseCommand 
	{
		// Members
		/// <summary>
		/// Is this a blocking command?
		/// Blocking commands depend on external factors and therefore are not garanteed to respond within 10 seconds.
		/// </summary>
		public abstract bool IsBlockingCommand { get; }
		/// <summary>
		/// Command response code
		/// </summary>
		public FixedLengthProperty<AbecsResponseStatus> RSP_STAT { get; private set; }
		
		// Constructor
		/// <summary>
		/// Constructor
		/// </summary>
		public BaseResponse (IContext context = null)
			: base(context)
		{
			if (context == null) { context = new AbecsContext(); }
			this.Context = context;

			this.RSP_STAT = new FixedLengthProperty<AbecsResponseStatus>("RSP_STAT", this.Context.StatusLength,
                false, StringFormatter.EnumStringFormatter<AbecsResponseStatus>, 
                StringParser.EnumStringParser<AbecsResponseStatus>);

			this.AddProperty(this.RSP_STAT);
		}

		// Methods
		/// <summary>
		/// Does the property makes the other properties be ignored?
		/// </summary>
		/// <param name="property">Property</param>
		/// <returns>boolean</returns>
		protected override bool IsPropertyFinal(IProperty property) 
		{
			//If the RSP_STAT is not ST_OK, there is no response data
			if (property == this.RSP_STAT)
			{
				if (this.RSP_STAT.Value != AbecsResponseStatus.ST_OK)
				{
					return true;
				}
				else
				{
					return false;
				}
			}
			return base.IsPropertyFinal(property);
		}
	}
}
