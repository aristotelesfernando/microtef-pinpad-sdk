using Pinpad.Sdk.Model;
using Pinpad.Sdk.Model.Utilities;
using System;

namespace Pinpad.Sdk.Utilities
{
    /// <summary>
    /// It contains methods to select data through the pinpad.
    /// </summary>
    internal class DataPicker : IDataPicker
    {
        // Members
        /// <summary>
        /// Reference to display operations.
        /// </summary>
        private IPinpadDisplay _display = null;
        /// <summary>
        /// Reference to keyboard operations.
        /// </summary>
        private IPinpadKeyboard _keyboard = null;

        // Properties
        /// <summary>
        /// Keys of Down and Up in pinpad.
        /// </summary>
        public DataPickerKeys DataPickerKeys { get; set; }

        // Constructor
        /// <summary>
        /// Build a data picker with a reference to keyboard and display.
        /// </summary>
        /// <param name="keyboard"><seealso cref="IPinpadKeyboard"/> implementation.</param>
        /// <param name="infos"><seealso cref="IPinpadInfos"/> implementation.</param>
        /// <param name="display"><seealso cref="IPinpadDisplay"/> implementation.</param>
        public DataPicker(IPinpadKeyboard keyboard, IPinpadInfos infos, IPinpadDisplay display)
        {
            this._keyboard = keyboard;
            this._display = display;
            this.DataPickerKeys = DataPickerKeysFactory.Create(infos);
        }

        /// <summary>
        /// Get numeric value in range informed.
        /// </summary>
        /// <param name="label">Text to display on the first line of pinpad display.</param>
        /// <param name="minimunLimit">Minimum numeric value for pick. Limit: -32.768.</param>
        /// <param name="maximumLimit">Maximum numeric value for pick. Limit: 32.767.</param>
        /// <returns>Number picked or null if no one was picked.</returns>
        public Nullable<short> GetNumericValue(string label, short minimunLimit, short maximumLimit)
        {
            if (minimunLimit > maximumLimit)
            {
                throw new ArgumentException("minimunValue");
            }
            if (string.IsNullOrEmpty(label) == true)
            {
                throw new ArgumentException("label");
            }

            PinpadKeyCode code = PinpadKeyCode.Undefined;
            short index = minimunLimit;

            do
            {
                this._display.ShowMessage(label + ":", index.ToString(), DisplayPaddingType.Left);
                code = this._keyboard.GetKey();

                if (code == PinpadKeyCode.Backspace)
                {
                    // Restart counter
                    index = minimunLimit;
                }
                else if (code == this.DataPickerKeys.DownKey && index > minimunLimit)
                {
                    // Down key
                    index--;
                }
                else if (code == this.DataPickerKeys.UpKey && index < maximumLimit)
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
        public Nullable<short> GetValueInOptions(string label, params short[] options)
        {
            if (string.IsNullOrEmpty(label) == true)
            {
                throw new ArgumentException("label");
            }
            if (options.Length == 0)
            {
                throw new ArgumentException("options");
            }

            return this.PickObjectInArray<Nullable<short>, short>(label, options);
        }
        /// <summary>
        /// Get text value in array options.
        /// </summary>
        /// <param name="label">Text to display on the first line of pinpad display.</param>
        /// <param name="options">Array with options.</param>
        /// <returns>Option picked or null/empty if no one was picked.</returns>
        public string GetValueInOptions(string label, params string[] options)
        {
            if (string.IsNullOrEmpty(label) == true)
            {
                throw new ArgumentException("label");
            }
            if (options.Length == 0)
            {
                throw new ArgumentException("options");
            }

            return this.PickObjectInArray<string,string>(label, options);
        }

        // Private methods
        /// <summary>
        /// Base method to picked options.
        /// </summary>
        /// <typeparam name="T">Return type.</typeparam>
        /// <typeparam name="V">Options type.</typeparam>
        /// <param name="label">Text to display on the first line of pinpad display.</param>
        /// <param name="options">Array with options.</param>
        /// <returns>Option picker or null if no one was picked.</returns>
        private T PickObjectInArray<T, V>(string label, params V[] options)
        {
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
                else if (code == this.DataPickerKeys.DownKey && index > 0)
                {
                    // Down key
                    index--;
                }
                else if (code == this.DataPickerKeys.UpKey && index < options.Length - 1)
                {
                    // Up key
                    index++;
                }

            } while (code != PinpadKeyCode.Return && code != PinpadKeyCode.Cancel);

            if (code == PinpadKeyCode.Return)
            {
                return (T)(object)options[index];
            }
            return default(T);
        }
    }
}
