using System;

namespace Pinpad.Sdk.Model.Utilities
{
    /// <summary>
    /// Class containing up and down keys of pinpad.
    /// </summary>
    public class DataPickerKeys
    {
        // Properties
        /// <summary>
        /// Up key property.
        /// </summary>
        public PinpadKeyCode UpKey
        {
            get { return _up; }
            set
            {
                if (value == this._down)
                {
                    throw new ArgumentException("Up and Down keys have the same value.");
                }
                if ((value == PinpadKeyCode.Undefined) ||
                    (value == PinpadKeyCode.Return) ||
                    (value == PinpadKeyCode.Cancel) ||
                    (value == PinpadKeyCode.Backspace))
                {
                    throw new ArgumentException("Invalid value to up key.");
                }
                this._up = value;
            }
        }
        /// <summary>
        /// Down key property.
        /// </summary>
        public PinpadKeyCode DownKey
        {
            get { return _down; }
            set
            {
                if (value == this._up)
                {
                    throw new ArgumentException("Up and Down keys have the same value.");
                }
                if ((value == PinpadKeyCode.Undefined) ||
                    (value == PinpadKeyCode.Return) ||
                    (value == PinpadKeyCode.Cancel) ||
                    (value == PinpadKeyCode.Backspace))
                {
                    throw new ArgumentException("Invalid value to down key.");
                }
                this._down = value;
            }
        }

        // Members
        /// <summary>
        /// Up key member.
        /// </summary>
        private PinpadKeyCode _up = PinpadKeyCode.Function3;
        /// <summary>
        /// Down key member.
        /// </summary>
        private PinpadKeyCode _down = PinpadKeyCode.Function4;
    }
}
