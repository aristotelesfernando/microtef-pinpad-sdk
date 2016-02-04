using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PinPadSDK.Controllers.Commands {
    /// <summary>
    /// Controller for GCR PinPad command request Id App entry
    /// </summary>
    public class PinPadGcrIdAppController {
        /// <summary>
        /// Constructor
        /// </summary>
        public PinPadGcrIdAppController() {
            this.TAB_ACQ = new PinPadFixedLengthPropertyController<Nullable<int>>("TAB_ACQ", 2, false);
            this.TAB_RECIDX = new PinPadFixedLengthPropertyController<Nullable<int>>("TAB_RECIDX", 2, false);
        }

        /// <summary>
        /// Constructor with initial value
        /// </summary>
        /// <param name="acquirer">Aid Table Acquirer Index</param>
        /// <param name="record">Aid Table Record Index</param>
        public PinPadGcrIdAppController(int acquirer, int record)
            : this() {
            this.TAB_ACQ.Value = acquirer;
            this.TAB_RECIDX.Value = record;
        }

        /// <summary>
        /// PinPadGcrIdAppController string formatter
        /// </summary>
        /// <param name="obj">value to convert</param>
        /// <param name="length">length for the value as string</param>
        /// <returns>Value of the property as string</returns>
        public static string StringFormatter(PinPadGcrIdAppController obj, int length) {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(StringFormatterController.IntegerStringFormatter(obj.TAB_ACQ.Value, obj.TAB_ACQ.Length));
            stringBuilder.Append(StringFormatterController.IntegerStringFormatter(obj.TAB_RECIDX.Value, obj.TAB_RECIDX.Length));
            return stringBuilder.ToString();
        }

        /// <summary>
        /// PinPadGcrIdAppController string parser
        /// </summary>
        /// <param name="reader">string reader</param>
        /// <param name="length">string length</param>
        /// <returns>PinPadGcrIdAppController</returns>
        public static PinPadGcrIdAppController StringParser(StringReadController reader, int length){
            PinPadGcrIdAppController value = new PinPadGcrIdAppController();
            value.TAB_ACQ.Value = StringParserControler.IntegerStringParser(reader, value.TAB_ACQ.Length);
            value.TAB_RECIDX.Value = StringParserControler.IntegerStringParser(reader, value.TAB_RECIDX.Length);
            return value;
        }

        /// <summary>
        /// Aid Table Acquirer Index
        /// </summary>
        public PinPadFixedLengthPropertyController<Nullable<int>> TAB_ACQ { get; private set; }

        /// <summary>
        /// Aid Table Record Index
        /// </summary>
        public PinPadFixedLengthPropertyController<Nullable<int>> TAB_RECIDX { get; private set; }
    }
}
