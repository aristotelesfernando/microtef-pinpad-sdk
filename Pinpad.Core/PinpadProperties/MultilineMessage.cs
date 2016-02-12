﻿using System;
using System.Collections.Generic;

/* WARNING!
 * 
 * DEPRECATED.
 * MUST BE REFACTORED.
 * 
 */

namespace Pinpad.Core.Properties 
{
	/// <summary>
	/// multiline PinPad message
	/// </summary>
	public class MultilineMessage : BaseProperty 
	{
		// Members
		private SimpleProperty<string> message { get; set; }
		private List<string> _LineCollection { get; set; }
		private bool lineCollectionChanged { get; set; }
		/// <summary>
		/// Collection of lines in the message
		/// </summary>
		public List<string> LineCollection {
			get {
				this.lineCollectionChanged = true;
				return this._LineCollection;
			}
		}
		/// <summary>
		/// Command string of this command
		/// </summary>
		public override string CommandString {
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

		// Constructor
		/// <summary>
		/// Constructor
		/// </summary>
		public MultilineMessage() 
		{
			this._LineCollection = new List<string>();
			this.lineCollectionChanged = true;
			this.message = new SimpleProperty<string>("Message", true, DefaultStringFormatter.StringStringFormatter, DefaultStringParser.StringStringParser);

			this.AddProperty(this.message);
		}
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="lines">message lines</param>
		public MultilineMessage(params string[] lines)
			: this() {
				this.LineCollection.AddRange(lines);
		}
	}
}
