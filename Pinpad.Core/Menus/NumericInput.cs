using PinPadSDK.Controllers;
using PinPadSDK.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PinPadSDK.PinPad;
using PinPadSDK.Property;
using StonePortableUtils;

namespace PinPadSDK.Menus
{
    /// <summary>
    /// Uses F1 and F2 keys to input a numeric value
    /// If stone Extended Key is Supported will also use numeric keys to add to the value
    /// </summary>
    public class NumericInput {
        /// <summary>
        /// PinPad used
        /// </summary>
        public PinPadFacade PinPad { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="pinPad">PinPad to use</param>
        public NumericInput(PinPadFacade pinPad) {
            this.PinPad = pinPad;
        }

        /// <summary>
        /// Default Format Handler
        /// </summary>
        /// <param name="value">long</param>
        /// <returns>long string split with max of 18 characters per line and a message to the user</returns>
        public static string[] DefaultIntegerFormatHandler(long value)
        {
            List<string> Lines = new List<string>();
            Lines.AddRange(value.ToString().BreakString(18));
            Lines.Add("(USE F1 e F2)");
            return Lines.ToArray();
        }

        /// <summary>
        /// Default Format Handler
        /// </summary>
        /// <param name="value">long</param>
        /// <returns>long string split with max of 18 characters per line and a message to the user</returns>
        public static string[] DefaultStoneIntegerFormatHandler(long value) {
            List<string> Lines = new List<string>();
            Lines.AddRange(value.ToString().BreakString(18));
            return Lines.ToArray();
        }

        /// <summary>
        /// Prompts the user to input a numeric value using the PinPad resources available
        /// </summary>
        /// <param name="formatHandler">Function to convert long to the Display message</param>
        /// <param name="minValue">Minimum value that can be inputted</param>
        /// <param name="maxValue">Maximum value that can be inputted</param>
        /// <returns>long or null if cancelled by the user</returns>
        public Nullable<long> ReadInput(Func<long, string[]> formatHandler, long minValue = Int64.MinValue, long maxValue = Int64.MaxValue) {
            if (this.PinPad.Keyboard.ExtendedKeySupported == true) {
                return this.ReadExtendedInput(formatHandler, minValue, maxValue);
            }
            else {
                return this.ReadSimpleInput(formatHandler, minValue, maxValue);
            }
        }

        private Nullable<long> ReadSimpleInput(Func<long, string[]> formatHandler, long minValue, long maxValue)
        {
            long value = 0;
            long previousValue = value;
            this.PinPad.Display.DisplayMessage(new MultilineMessage(formatHandler(value)));

            PinPadKey previousKey = PinPadKey.Undefined;
            PinPadKey key;
            while (((key = this.PinPad.Keyboard.GetKey()) != PinPadKey.Return || previousKey != PinPadKey.Return) && key != PinPadKey.Cancel)
            {
                switch (key) {
                    case PinPadKey.Function1:
                        value--;
                        if (value < minValue) {
                            value = minValue;
                        }
                        break;

                    case PinPadKey.Function2:
                        value++;
                        if (value > maxValue) {
                            value = maxValue;
                        }
                        break;

                    case PinPadKey.Backspace:
                        value /= 10;
                        break;

                    case PinPadKey.Return:
                        previousValue = value;
                        value *= 10;
                        if (value > maxValue) {
                            value = maxValue;
                        }
                        break;
                }

                this.PinPad.Display.DisplayMessage(new MultilineMessage(formatHandler(value)));
                previousKey = key;
            }

            if (key == PinPadKey.Cancel) {
                return null;
            }
            else {
                return previousValue;
            }
        }

        private long? ReadExtendedInput(Func<long, string[]> formatHandler, long minValue, long maxValue) {
            long value = 0;
            this.PinPad.Keyboard.ClearKeyBuffer( );
            this.PinPad.Display.DisplayMessage(new MultilineMessage(formatHandler(value)));

            PinPadKey key;
            while (((key = this.PinPad.Keyboard.GetKeyExtended( )) != PinPadKey.Return || value < minValue) && key != PinPadKey.Cancel) {
                switch (key) {
                    case PinPadKey.Function1:
                        value--;
                        if (value < minValue) {
                            value = minValue;
                        }
                        break;

                    case PinPadKey.Function2:
                        value++;
                        if (value > maxValue) {
                            value = maxValue;
                        }
                        break;

                    case PinPadKey.Backspace:
                        value /= 10;
                        break;

                    default:
                        if (key.IsNumeric( ) == true) {
                            long oldValue = value;
                            value *= 10;
                            value += key.GetLong( );
                            if (value > maxValue) {
                                value = oldValue;
                            }
                        }
                        break;
                }

                this.PinPad.Display.DisplayMessage(new MultilineMessage(formatHandler(value)));
            }

            if (key == PinPadKey.Cancel) {
                return null;
            }
            else {
                return value;
            }
        }
    }
}
