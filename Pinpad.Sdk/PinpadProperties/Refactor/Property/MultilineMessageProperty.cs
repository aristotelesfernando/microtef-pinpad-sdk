using Pinpad.Sdk.PinpadProperties.Refactor.Formatter;
using Pinpad.Sdk.PinpadProperties.Refactor.Parser;
using System.Collections.Generic;

namespace Pinpad.Sdk.PinpadProperties.Refactor.Property
{
    /// <summary>
	/// multiline PinPad message
	/// </summary>
	public sealed class MultilineMessageProperty : BaseProperty
    {
        // Members
        /// <summary>
        /// Has the value of the current line to be printed.
        /// </summary>
        private TextProperty<string> message { get; set; }
        /// <summary>
        /// Message to be printed. Each item, a line.
        /// </summary>
        private List<string> _LineCollection { get; set; }
        /// <summary>
        /// If the message to be printed has changed.
        /// </summary>
        private bool lineCollectionChanged { get; set; }
        /// <summary>
        /// Collection of lines in the message
        /// </summary>
        public List<string> LineCollection
        {
            get
            {
                this.lineCollectionChanged = true;
                return this._LineCollection;
            }
        }
        /// <summary>
        /// Command string of this command
        /// </summary>
        public override string CommandString
        {
            get
            {
                if (this.lineCollectionChanged == true)
                {
                    if (this.LineCollection.Count == 0)
                    {
                        this.message.Value = null;
                    }
                    else
                    {
                        this.message.Value = string.Join("\r", this.LineCollection);
                    }

                    this.lineCollectionChanged = false;
                }
                return base.CommandString;
            }
        }

        // Constructor
        /// <summary>
        /// Basic constructor.
        /// </summary>
        public MultilineMessageProperty()
        {
            this._LineCollection = new List<string>();
            this.lineCollectionChanged = true;
            this.message = new TextProperty<string>("Message", true, StringFormatter.StringStringFormatter,
                StringParser.StringStringParser);

            this.AddProperty(this.message);
        }
        /// <summary>
        /// Constructor with values.
        /// </summary>
        /// <param name="lines">Lines of message.</param>
        public MultilineMessageProperty(params string[] lines)
            : this()
        {
            this.LineCollection.AddRange(lines);
        }
    }
}
