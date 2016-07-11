using Pinpad.Sdk.Model;
using Pinpad.Sdk.Model.Utilities;
using System;
using System.Diagnostics;

namespace Pinpad.Sdk.Utilities
{
    /// <summary>
    /// It contains methods to select data through the pinpad.
    /// </summary>
    internal class DataPicker : IDataPicker
    {
        // Constant
        /// <summary>
        /// Manufacturer name of pinpad with different down and up keys.
        /// </summary>
        private const string ManufacturerName = "VERIFONE";

        // Members
        /// <summary>
        /// Reference to display operations.
        /// </summary>
        private IPinpadDisplay _display = null;
        /// <summary>
        /// Reference to keyboard operations.
        /// </summary>
        private IPinpadKeyboard _keyboard = null;
        /// <summary>
        /// Keys of Down and Up in pinpad.
        /// </summary>
        private DataPickerKeys _keys = new DataPickerKeys();

        // Constructor
        /// <summary>
        /// Build a data picker with a reference to keyboard and display.
        /// </summary>
        /// <param name="keyboard">IPinpadKeyboard implementation.</param>
        /// <param name="infos">IPinpadInfos implementation.</param>
        /// <param name="display">IPinpadDisplay implementation.</param>
        public DataPicker(IPinpadKeyboard keyboard, IPinpadInfos infos, IPinpadDisplay display)
        {
            this._keyboard = keyboard;
            this._display = display;

            // Verifone utiliza Function1 como Up e Function3 como Down.
            if (infos.ManufacturerName?.Contains(ManufacturerName) == true)
            {
                this._keys = new DataPickerKeys(PinpadKeyCode.Function1, PinpadKeyCode.Function3);
            }
        }

        // Public methods
        /// <summary>
        /// Get numeric value in range informed.
        /// </summary>
        /// <param name="label">Text to display on the first line of pinpad display.</param>
        /// <param name="minimunValue">Minimum numeric value for pick.</param>
        /// <param name="maximumValue">Maximum numeric value for pick.</param>
        /// <returns>Number picked or null if no one was picked.</returns>
        public Nullable<int> GetNumericValue(string label, int minimunValue, int maximumValue)
        {
            if (minimunValue > maximumValue)
            {
                throw new ArgumentException("minimunValue");
            }
            if (string.IsNullOrEmpty(label) == true)
            {
                throw new ArgumentException("label");
            }

            PinpadKeyCode code = PinpadKeyCode.Undefined;
            int index = minimunValue;

            do
            {
                this._display.ShowMessage(label + ":", index.ToString(), DisplayPaddingType.Left);
                code = this._keyboard.GetKey();

                if (code == PinpadKeyCode.Backspace)
                {
                    // Restart counter
                    index = minimunValue;
                }
                else if (code == this._keys.Down && index > minimunValue)
                {
                    // Down key
                    index--;
                }
                else if (code == this._keys.Up && index < maximumValue)
                {
                    // Up key
                    index++;
                }

            } while (code != PinpadKeyCode.Return && code != PinpadKeyCode.Cancel);

            if (code == PinpadKeyCode.Return)
            {
                return index;
            }
            return null;
        }
        /// <summary>
        /// Get numeric value in array options.
        /// </summary>
        /// <param name="label">Text to display on the first line of pinpad display.</param>
        /// <param name="options">Array with options.</param>
        /// <returns>Option picked or null if no one was picked.</returns>
        public Nullable<int> GetNumericValueInArray(string label, params int[] options)
        {
            if (string.IsNullOrEmpty(label) == true)
            {
                throw new ArgumentException("label");
            }
            if (options.Length == 0)
            {
                throw new ArgumentException("options");
            }
            
            PinpadKeyCode code = PinpadKeyCode.Undefined;
            int index = 0;

            do
            {
                this._display.ShowMessage(label + ":", options[index].ToString(), DisplayPaddingType.Left);
                code = this._keyboard.GetKey();

                if (code == PinpadKeyCode.Backspace)
                {
                    // Restart counter
                    index = 0;
                }
                else if (code == this._keys.Down && index > 0)
                {
                    // Down key
                    index--;
                }
                else if (code == this._keys.Up && index < options.Length - 1)
                {
                    // Up key
                    index++;
                }

            } while (code != PinpadKeyCode.Return && code != PinpadKeyCode.Cancel);

            if (code == PinpadKeyCode.Return)
            {
                return options[index];
            }
            return null;
        }
        /// <summary>
        /// Get text value in array options.
        /// </summary>
        /// <param name="label">Text to display on the first line of pinpad display.</param>
        /// <param name="options">Array with options.</param>
        /// <returns>Option picked or null if no one was picked.</returns>
        public string GetTextValueInArray(string label, params string[] options)
        {
            if (string.IsNullOrEmpty(label) == true)
            {
                throw new ArgumentException("label");
            }
            if (options.Length == 0)
            {
                throw new ArgumentException("options");
            }

            PinpadKeyCode code = PinpadKeyCode.Undefined;
            int index = 0;

            do
            {
                this._display.ShowMessage(label + ":", options[index].ToString(), DisplayPaddingType.Left);
                code = this._keyboard.GetKey();

                if (code == PinpadKeyCode.Backspace)
                {
                    // Restart counter
                    index = 0;
                }
                else if (code == this._keys.Down && index > 0)
                {
                    // Down key
                    index--;
                }
                else if (code == this._keys.Up && index < options.Length - 1)
                {
                    // Up key
                    index++;
                }

            } while (code != PinpadKeyCode.Return && code != PinpadKeyCode.Cancel);

            if (code == PinpadKeyCode.Return)
            {
                return options[index];
            }
            return null;
        }
    }
}
