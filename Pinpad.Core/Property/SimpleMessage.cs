using PinPadSDK.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PinPadSDK.Property {
    /// <summary>
    /// simple pin pad message (32 characters long with 2 lines of 16 characters)
    /// </summary>
    public class SimpleMessage : BaseProperty {
        /// <summary>
        /// Constructor
        /// </summary>
        public SimpleMessage() {
            this.firstLine = new PinPadFixedLengthPropertyController<string>("FirstLine", 16, false, DefaultStringFormatter.StringStringFormatter, DefaultStringParser.StringStringParser);
            this.secondLine = new PinPadFixedLengthPropertyController<string>("SecondLine", 16, false, DefaultStringFormatter.StringStringFormatter, DefaultStringParser.StringStringParser);

            this.AddProperty(this.firstLine);
            this.AddProperty(this.secondLine);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="fullValue">32 characters long message where every 16 characters is a line</param>
        public SimpleMessage(string fullValue, PaddingType padding = PaddingType.Undefined)
            : this() {
            this.Padding = padding;
            this.FullValue = fullValue;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="firstLine">First line with up to 16 characters long message, if using padding</param>
        /// <param name="secondLine">Second line with up to 16 characters long message, if using padding</param>
        /// <param name="padding">Padding to use when requested or Undefined to fire exceptions if requested</param>
        public SimpleMessage(string firstLine, string secondLine, PaddingType padding = PaddingType.Center)
            : this( ) {
                this.Padding = padding;
                this.FirstLine = firstLine;
                this.SecondLine = secondLine;
        }

        private PinPadFixedLengthPropertyController<string> firstLine { get; set; }
        private PinPadFixedLengthPropertyController<string> secondLine { get; set; }
        private PaddingType padding { get; set; }

        /// <summary>
        /// Gets or sets the Padding type of this message
        /// </summary>
        public PaddingType Padding {
            get {
                return padding;
            }
            set {
                padding = value;
                if (padding != PaddingType.Undefined) {
                    if (this.FirstLine != null) {
                        this.FirstLine = this.FirstLine.Trim(' ');
                    }
                    else {
                        this.FirstLine = String.Empty;
                    }
                    if (this.SecondLine != null) {
                        this.SecondLine = this.SecondLine.Trim(' ');
                    }
                    else {
                        this.SecondLine = String.Empty;
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets the first line of this message
        /// Padding will be automatically applied if set
        /// Maximum length is 16
        /// </summary>
        public string FirstLine {
            get {
                return this.firstLine.Value;
            }
            set {
                if (value == null) {
                    value = String.Empty;
                }
                if (value.Length > 16) {
                    throw new ArgumentOutOfRangeException("Maximum length of a line is 16 characters.");
                }
                else if (value.Length < 16) {
                    if (this.Padding == PaddingType.Undefined) {
                        throw new ArgumentOutOfRangeException("Settings lines with dynamic sizes is only supported with a defined Padding");
                    }
                    else {
                        this.firstLine.Value = this.PadLine(value);
                    }
                }
                else {
                    this.firstLine.Value = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the second line of this message
        /// Padding will be automatically applied if set
        /// Maximum length is 16
        /// </summary>
        public string SecondLine {
            get {
                return this.secondLine.Value;
            }
            set {
                if (value == null) {
                    value = String.Empty;
                }
                if (value.Length > 16) {
                    throw new ArgumentOutOfRangeException("Maximum length of a line is 16 characters.");
                }
                else if (value.Length < 16) {
                    if (this.Padding == PaddingType.Undefined) {
                        throw new ArgumentOutOfRangeException("Settings lines with dynamic sizes is only supported with a defined Padding");
                    }
                    else {
                        this.secondLine.Value = this.PadLine(value);
                    }
                }
                else {
                    this.secondLine.Value = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the full string of the message
        /// 32 characters long where every 16 characters is a line
        /// </summary>
        public string FullValue {
            get {
                return this.FirstLine + this.SecondLine;
            }
            set {
                if (value == null) {
                    value = String.Empty;
                }
                if (value.Length != 32) {
                    if (value.Length < 32 && this.Padding != PaddingType.Undefined) {
                        value = this.PadLine(value, 32);
                    }
                    else {
                        throw new ArgumentOutOfRangeException("FullValue must be a string with 32 characters or below with padding.");
                    }
                }
                this.FirstLine = value.Substring(0, 16);
                this.SecondLine = value.Substring(16, 16);
            }
        }

        private string PadLine(string value, int length = 16) {
            string result;
            switch (this.Padding) {
                case PaddingType.Left:
                    result = value.PadRight(length, ' ');
                    break;

                case PaddingType.Center:
                    StringBuilder stringBuilder = new StringBuilder(value);
                    while (stringBuilder.Length < length) {
                        if (stringBuilder.Length % 2 == 0) {
                            stringBuilder.Insert(0, " ");
                        }
                        else {
                            stringBuilder.Append(" ");
                        }
                    }
                    result = stringBuilder.ToString();
                    break;

                case PaddingType.Right:
                    result = value.PadLeft(length, ' ');
                    break;

                default:
                    throw new InvalidOperationException("Can not pad a string without a defined Padding");
            }
            return result;
        }

        /// <summary>
        /// Default string parser
        /// </summary>
        /// <param name="reader">string reader</param>
        /// <returns>PinPadSimpleMessageController</returns>
        public static SimpleMessage StringParser(StringReader reader)
        {
            //TODO: refatorar logica de calcular length

            if (reader.Remaining < 32)
            {
                int gap = (32 - reader.Remaining);
                string shim = new string(' ', gap);
                reader.Value += shim;
            }

            string data = DefaultStringParser.StringStringParser(reader, 32);



            SimpleMessage value = new SimpleMessage();
            value.CommandString = data;
            return value;
        }
    }
}
