using Pinpad.Sdk.PinpadProperties.Refactor;
using Pinpad.Sdk.PinpadProperties.Refactor.Formatter;
using Pinpad.Sdk.PinpadProperties.Refactor.Parser;

namespace Pinpad.Sdk.Commands 
{
	/// <summary>
	/// generic response
	/// </summary>
	internal class GenericResponse : BaseResponse
	{
		// Members
		/// <summary>
		/// Is this a blocking command?
		/// </summary>
		public override bool IsBlockingCommand 
		{
			get { return false; }
		}
		/// <summary>
		/// Name of the command
		/// </summary>
		public override string CommandName 
		{
			get {
				if (this.CMD_ID == null) {
					return null;
				}
				else {
					return this.CMD_ID.Value;
				}
			}
		}

		// Constructor
		public GenericResponse () { }
		/// <summary>
		/// Constructor
		/// </summary>
		public GenericResponse (IContext context = null)
			: base(context) { }

		// Methods
		/// <summary>
		/// StringFormatter to remove limitation of the response expecting a specific command
		/// </summary>
		/// <param name="obj">object to convert</param>
		/// <param name="length">length for the value as string, ignored</param>
		/// <returns>Value of the property as string</returns>
		protected override string CommandNameStringFormatter(string obj, int length)
		{
			string value = StringFormatter.StringStringFormatter(obj, length);
			return value;
		}
		/// <summary>
		/// StringParser to remove limitation of the response expecting a specific command
		/// </summary>
		/// <param name="reader">string reader</param>
		/// <param name="length">string length</param>
		/// <returns>string</returns>
		protected override string CommandNameStringParser(StringReader reader, int length) 
		{
			string value = StringParser.StringStringParser(reader, length);
			return value;
		}
		/// <summary>
		/// Sets the CommandName
		/// </summary>
		/// <param name="value">New CommandName</param>
		public void SetCommandName(string value) 
		{
			this.CMD_ID.Value = value;
		}
	}
}
