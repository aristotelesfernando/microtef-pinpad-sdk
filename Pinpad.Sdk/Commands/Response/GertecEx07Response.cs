using Pinpad.Sdk.PinpadProperties.Refactor.Formatter;
using Pinpad.Sdk.PinpadProperties.Refactor.Parser;
using Pinpad.Sdk.PinpadProperties.Refactor.Property;

namespace Pinpad.Sdk.Commands
{
	internal class GertecEx07Response : BaseResponse
	{
		/// <summary>
		/// Name of this command.
		/// </summary>
		public override string CommandName { get { return "EX07"; } }
		/// <summary>
		/// Is this a blocking command?
		/// Blocking commands depend on external factors and therefore are not garanteed to respond within 10 seconds.
		/// </summary>
		public override bool IsBlockingCommand { get { return true; } }

		public VariableLengthProperty<string> RSP_RESULT { get; set; }

		public GertecEx07Response ()
			: base(new GertecContext())
		{
			this.RSP_RESULT = new VariableLengthProperty<string>("RSP_RESULT", 2, 32, 1f, false, true, 
                StringFormatter.StringStringFormatter, StringParser.StringStringParser);

			// Start region
			{
				this.AddProperty(this.RSP_RESULT);
			}
			// End region
		}
	}
}
