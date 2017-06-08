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
            this.DataPickerKeys = infos.GetUpAndDownKeys();
        }

        /// <summary>
        /// Get numeric value in range informed.
        /// </summary>
        /// <param name="label">Text to display on the first line of pinpad display.</param>
        /// <param name="circularBehavior">Behavior of the list.</param>
        /// <param name="minimunLimit">Minimum numeric value for pick. Limit: -32.768.</param>
        /// <param name="maximumLimit">Maximum numeric value for pick. Limit: 32.767.</param>
        /// <returns>Number picked or null if no one was picked.</returns>
        public Nullable<short> GetNumericValue(string label, bool circularBehavior, short minimunLimit, short maximumLimit)
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

           if(circularBehavior == true)
            {
                code = this.AddCircularBehaviorNumericValue(label,minimunLimit,maximumLimit);
            }
           else
            {
                code = this.AddLinearBehaviorNumericValue(label, minimunLimit, maximumLimit);
            }

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
        /// <param name="circularBehavior">Behavior of the list.</param>
        /// <returns>Option picked or null if no one was picked.</returns>
        public Nullable<short> GetValueInOptions(string label, bool circularBehavior, params short[] options)
        {
            if (string.IsNullOrEmpty(label) == true)
            {
                throw new ArgumentException("label");
            }
            if (options.Length == 0)
            {
                throw new ArgumentException("options");
            }

            return this.PickObjectInArray<Nullable<short>, short>(label, circularBehavior, options);
        }
        /// <summary>
        /// Get text value in array options.
        /// </summary>
        /// <param name="label">Text to display on the first line of pinpad display.</param>
        /// <param name="options">Array with options.</param>
        /// <param name="circularBehavior">Behavior of the list.</param>
        /// <returns>Option picked or null/empty if no one was picked.</returns>
        public string GetValueInOptions(string label, bool circularBehavior, params string[] options )
        {
            if (string.IsNullOrEmpty(label) == true)
            {
                throw new ArgumentException("label");
            }
            if (options.Length == 0)
            {
                throw new ArgumentException("options");
            }

            return this.PickObjectInArray<string,string>(label, circularBehavior, options );
        }

        // Private methods
        /// <summary>
        /// Base method to picked options.
        /// </summary>
        /// <typeparam name="T">Return type.</typeparam>
        /// <typeparam name="V">Options type.</typeparam>
        /// <param name="label">Text to display on the first line of pinpad display.</param>
        /// <param name="options">Array with options.</param>
        /// <param name="circularBehavior">Behavior of the list.</param>
        /// <returns>Option picker or null if no one was picked.</returns>
        private T PickObjectInArray<T, V>(string label, bool circularBehavior,params V[] options)
        {
            PinpadKeyCode code = PinpadKeyCode.Undefined;
            int index;

            if (circularBehavior == true)
            {
                code = this.AddCircularBehavior<V>(label, out index, options);               
            }
            else
            {
                code = this.AddLinearBehavior<V>(label, out index, options);              
            }

            if (code == PinpadKeyCode.Return)
            {
                return (T)(object)options[index];
            }

            return default(T);
        }
        /// <summary>
        /// Modify the behavior of the list to linear.
        /// </summary>
        /// <typeparam name="V"></typeparam>
        /// <param name="label">Text to display on the first line of pinpad display.</param>
        /// <param name="index">Index of the option selected.</param>
        /// <param name="options">Array with options.</param>
        private PinpadKeyCode AddLinearBehavior<V>(string label, out int index, params V[] options)
        {
            PinpadKeyCode code = PinpadKeyCode.Undefined;
            index = 0;

            do
            {
                this._display.ShowMessage(label + ":", options[index].ToString(), DisplayPaddingType.Left);
                code = this._keyboard.GetKey();

                if (code == PinpadKeyCode.Backspace)
                {
                    // Restart counter
                    index = 0;
                }
                else if (code == this.DataPickerKeys.DownKey && index < options.Length - 1)
                {
                    // Down key
                    index++;
                }
                else if (code == this.DataPickerKeys.UpKey && index > 0)
                {
                    // Up key
                    index--;
                }
            }
            while (code != PinpadKeyCode.Return && code != PinpadKeyCode.Cancel && code != PinpadKeyCode.Undefined);

            return code;
        }
        /// <summary>
        /// Modify the behavior of the list to circular.
        /// </summary>
        /// <typeparam name="V"></typeparam>
        /// <param name="label">Text to display on the first line of pinpad display.</param>
        /// <param name="index">Index of the option selected.</param>
        /// <param name="options">Array with options.</param>
        private PinpadKeyCode AddCircularBehavior<V>(string label, out int index, params V[] options)
        {
            PinpadKeyCode code = PinpadKeyCode.Undefined;
            index = 0;

            do
            {
                this._display.ShowMessage(label + ":", options[index].ToString(), DisplayPaddingType.Left);
                code = this._keyboard.GetKey();

                if (code == PinpadKeyCode.Backspace)
                {
                    // Restart counter
                    index = 0;
                }
                else if (code == this.DataPickerKeys.DownKey)
                {
                    if (index == options.Length - 1)
                    {
                        index = 0;
                    }
                    else
                    {
                        // Down key
                        index++;
                    }
                }
                else if (code == this.DataPickerKeys.UpKey)
                {
                    if (index == 0)
                    {
                        index = options.Length - 1;
                    }
                    else
                    {
                        // Up key
                        index--;
                    }
                }
            }
            while (code != PinpadKeyCode.Return && code != PinpadKeyCode.Cancel && code != PinpadKeyCode.Undefined);

            return code;
        }
        ///<summary>
        /// Modify the behavior of the list to linear.
        ///</summary> 
        /// <param name="label">Text to display on the first line of pinpad display.</param>        
        /// <param name="minimunLimit">Minimum numeric value for pick. Limit: -32.768.</param>
        /// <param name="maximumLimit">Maximum numeric value for pick. Limit: 32.767.</param>
        private PinpadKeyCode AddLinearBehaviorNumericValue(string label, short minimunLimit, short maximumLimit)
        {
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
                else if (code == this.DataPickerKeys.DownKey && index < maximumLimit)
                {
                    // Down key
                    index++;
                }
                else if (code == this.DataPickerKeys.UpKey && index > minimunLimit)
                {
                    // Up key
                    index--;
                }
            } while (code != PinpadKeyCode.Return && code != PinpadKeyCode.Cancel && code != PinpadKeyCode.Undefined);

            return code;
        }
        ///<summary>
        /// Modify the behavior of the list to circular.
        ///</summary> 
        /// <param name="label">Text to display on the first line of pinpad display.</param>       
        /// <param name="minimunLimit">Minimum numeric value for pick. Limit: -32.768.</param>
        /// <param name="maximumLimit">Maximum numeric value for pick. Limit: 32.767.</param>
        private PinpadKeyCode AddCircularBehaviorNumericValue(string label, short minimunLimit, short maximumLimit)
        {
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
                else if (code == this.DataPickerKeys.DownKey)
                {
                    if(index == maximumLimit)
                    {
                        index = minimunLimit;
                    }
                    else
                    {
                        // Down key
                        index++;
                    }                   
                }
                else if (code == this.DataPickerKeys.UpKey && index > minimunLimit)
                {
                    if(index == minimunLimit)
                    {
                        index = maximumLimit;
                    }
                    else
                    {
                        // Up key
                        index--;
                    }                    
                }
            } while (code != PinpadKeyCode.Return && code != PinpadKeyCode.Cancel && code != PinpadKeyCode.Undefined);

            return code;
        }
    }
}
