using Pinpad.Sdk.Model;

namespace Pinpad.Sdk.Utilities
{
    /// <summary>
    /// Class containing up and down keys of pinpad.
    /// </summary>
    internal class DataPickerKeys
    {
        // Properties
        /// <summary>
        /// Up key.
        /// </summary>
        internal PinpadKeyCode Up { get; private set; }
        /// <summary>
        /// Down key.
        /// </summary>
        internal PinpadKeyCode Down { get; private set; }

        // Constructor
        /// <summary>
        /// Class constructor containing keys up and down.
        /// </summary>
        /// <param name="up">Up key. Function3 as default.</param>
        /// <param name="down">Down key. Functuion2 as default.</param>
        internal DataPickerKeys(PinpadKeyCode up = PinpadKeyCode.Function3, PinpadKeyCode down = PinpadKeyCode.Function2)
        {
            this.Up = up;
            this.Down = down;
        }
    }
}
