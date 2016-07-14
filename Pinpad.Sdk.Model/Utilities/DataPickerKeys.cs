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
        public PinpadKeyCode Up
        {
            get { return _up; }
            set
            {
                if (value == this._down)
                {
                    throw new ArgumentException("up = down");
                }
                if ((value == PinpadKeyCode.Undefined) ||
                    (value == PinpadKeyCode.Return) ||
                    (value == PinpadKeyCode.Cancel) ||
                    (value == PinpadKeyCode.Backspace))
                {
                    throw new ArgumentException("up");
                }
                this._up = value;
            }
        }
        /// <summary>
        /// Down key property.
        /// </summary>
        public PinpadKeyCode Down
        {
            get { return _down; }
            set
            {
                if (value == this._up)
                {
                    throw new ArgumentException("up = down");
                }
                if ((value == PinpadKeyCode.Undefined) ||
                    (value == PinpadKeyCode.Return) ||
                    (value == PinpadKeyCode.Cancel) ||
                    (value == PinpadKeyCode.Backspace))
                {
                    throw new ArgumentException("down");
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
