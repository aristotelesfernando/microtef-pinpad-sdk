using Pinpad.Sdk.Commands;
using Pinpad.Sdk.Model.Exceptions;
using Pinpad.Sdk.PinpadProperties.Refactor.Formatter;
using Pinpad.Sdk.PinpadProperties.Refactor.Parser;
using Pinpad.Sdk.PinpadProperties.Refactor.Property;

namespace Pinpad.Sdk.PinpadProperties.Refactor.Command
{
    public abstract class BaseCommand : BaseProperty, ICommand
    {
        // Members
        /// <summary>
		/// Name of this command
		/// </summary>
		public abstract string CommandName { get; }
        /// <summary>
        /// Command Id
        /// </summary>
        protected FixedLengthProperty<string> CMD_ID { get; set; }
        /// <summary>
        /// Context of the command.
        /// </summary>
        public IContext Context { get; set; }

        // Constructor
        /// <summary>
        /// Basic constructor.
        /// </summary>
        public BaseCommand(IContext context = default(IContext))
            : base()
        {
            if (context == null)
            {
                context = new AbecsContext();
            }

            this.Context = context;

            this.CMD_ID = new FixedLengthProperty<string>("CMD_ID", this.Context.CommandNameLength, false,
                this.CommandNameStringFormatter, this.CommandNameStringParser, null, this.CommandName);

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
            string value = StringFormatter.StringStringFormatter(obj, length);

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
            string value = StringParser.StringStringParser(reader, length);

            if (CommandName.Equals(value) == false)
            {
                throw new CommandNameMismatchException("\"" + value + "\" not supported for " + CommandName);
            }
            return value;
        }
    }
}
