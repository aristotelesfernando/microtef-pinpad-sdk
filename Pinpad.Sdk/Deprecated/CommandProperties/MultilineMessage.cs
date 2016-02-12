using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PinPadSDK.Property {
    /// <summary>
    /// multiline PinPad message
    /// </summary>
    internal class MultilineMessage : BaseProperty {
        /// <summary>
        /// Constructor
        /// </summary>
        internal MultilineMessage() {
            this._LineCollection = new List<string>();
            this.lineCollectionChanged = true;
            this.message = new SimpleProperty<string>("Message", true, DefaultStringFormatter.StringStringFormatter, DefaultStringParser.StringStringParser);

            this.AddProperty(this.message);
        }

        private SimpleProperty<string> message { get; set; }

        private List<string> _LineCollection { get; set; }

        private bool lineCollectionChanged { get; set; }

        /// <summary>
        /// Collection of lines in the message
        /// </summary>
        internal List<string> LineCollection {
            get {
                this.lineCollectionChanged = true;
                return this._LineCollection;
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="lines">message lines</param>
        internal MultilineMessage(params string[] lines)
            : this( ) {
                this.LineCollection.AddRange(lines);
        }

        /// <summary>
        /// Command string of this command
        /// </summary>
        internal override string CommandString {
            get {
                if (this.lineCollectionChanged == true) {
                    if (this.LineCollection.Count == 0) {
                        this.message.Value = null;
                    }
                    else {
                        this.message.Value = String.Join("\r", this.LineCollection);
                    }
                    this.lineCollectionChanged = false;
                }
                return base.CommandString;
            }
            set {
                base.CommandString = value;
                this.LineCollection.Clear();
                this.LineCollection.AddRange(value.Split('\r'));
            }
        }
    }
}
