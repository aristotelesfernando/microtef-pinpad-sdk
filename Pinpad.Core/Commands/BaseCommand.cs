using Pinpad.Core.Commands.Context;
using Pinpad.Core.Properties;
using Pinpad.Sdk.Model.Exceptions;
using Pinpad.Core.Pinpad;

/* WARNING!
 * 
 * DEPRECATED.
 * MUST BE REFACTORED.
 * 
 */

namespace Pinpad.Core.Commands 
{
	/// <summary>
	/// PinPad command
	/// </summary>
	public abstract class BaseCommand : BaseProperty 
	{
		// Members
		/// <summary>
		/// Name of this command
		/// </summary>
		public abstract string CommandName { get; }
		/// <summary>
		/// Command Id
		/// </summary>
		protected PinpadFixedLengthProperty<string> CMD_ID { get; set; }
		/// <summary>
		/// Context of the command.
		/// </summary>
		internal IContext CommandContext { get; set; }

		// Constructor
		/// <summary>
		/// Creates all properties for a basic command.
		/// </summary>
		/// <param name="commandNameLength">CommandName property length. The default value is 3.</param>
		/// <param name="context"></param>
		public BaseCommand (IContext context = null)
		{
			if (context == null) { context = new AbecsContext(); }
			this.CommandContext = context;

			this.CMD_ID = new PinpadFixedLengthProperty<string>("CMD_ID", this.CommandContext.CommandNameLength, false, this.CommandNameStringFormatter, this.CommandNameStringParser, null, this.CommandName);

			this.AddProperty(this.CMD_ID);
		}
		
		// Methods
		/// <summary>
		/// StringFormatter to limit the response to the expected command
		/// </summary>
		/// <param name="obj">object to convert</param>
		/// <param name="length">length for the value as string, ignored</param>
		/// <returns>Value of the property as string</returns>
		protected virtual string CommandNameStringFormatter(string obj, int length) 
		{
			string value = DefaultStringFormatter.StringStringFormatter(obj, length);
			
			if (CommandName.Equals(value) == false) 
			{
				throw new CommandNameMismatchException("\"" + value + "\" not supported for " + CommandName);
			}
			return value;
		}
		/// <summary>
		/// StringParser to limit the response to the expected command
		/// </summary>
		/// <param name="reader">string reader</param>
		/// <param name="length">string length</param>
		/// <returns>string</returns>
		protected virtual string CommandNameStringParser(StringReader reader, int length)
		{
			string value = DefaultStringParser.StringStringParser(reader, length);

			if (CommandName.Equals(value) == false)
			{
				throw new CommandNameMismatchException("\"" + value + "\" not supported for " + CommandName);
			}
			return value;
		}
	}
}
