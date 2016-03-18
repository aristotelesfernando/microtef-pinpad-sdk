using Pinpad.Core.Commands.Context;
using Pinpad.Core.Properties;
using System;

namespace Pinpad.Core.Commands
{
	public class GertecEx07Response : BaseResponse
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

		public new VariableLengthProperty<string> RSP_RESULT { get; set; }

		public GertecEx07Response ()
			: base(new GertecContext())
		{
			this.RSP_RESULT = new VariableLengthProperty<string>("RSP_RESULT", 2, 3, 0.5f, false, false, DefaultStringFormatter.StringStringFormatter, DefaultStringParser.StringStringParser);

			// Start region
			{
				this.AddProperty(this.RSP_RESULT);
			}
			// End region
		}
	}
}
