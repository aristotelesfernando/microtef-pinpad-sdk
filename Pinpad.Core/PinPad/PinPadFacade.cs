using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CrossPlatformBase;

namespace PinPadSDK.PinPad {
    /// <summary>
    /// Facade for PinPad adapters
    /// </summary>
    public class PinPadFacade {
        /// <summary>
        /// Gets the default Communication adapter
        /// </summary>
        public PinPadCommunication Communication { get; private set; }

        /// <summary>
        /// Gets the default Keyboard adapter
        /// </summary>
        public PinPadKeyboard Keyboard { get; private set; }

        /// <summary>
        /// Gets the default Display adapter
        /// </summary>
        public PinPadDisplay Display { get; private set; }

        /// <summary>
        /// Gets the default Printer adapter
        /// </summary>
        public PinPadPrinter Printer { get; private set; }

        /// <summary>
        /// Gets the default Storage adapter
        /// </summary>
        public PinPadStorage Storage { get; private set; }

        /// <summary>
        /// Gets the default Table adapter
        /// </summary>
        public PinPadTable Table { get; private set; }

        /// <summary>
        /// Gets the default Infos adapter
        /// </summary>
        public PinPadInfos Infos { get; private set; }

        internal PinPadEncryption Encryption { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="pinPadConnection">PinPad Connection</param>
        public PinPadFacade(IPinPadConnection pinPadConnection){
            if (pinPadConnection == null) {
                throw new InvalidOperationException("PinPadConnection cannot be null");
            }

            this.Communication = new PinPadCommunication(this, pinPadConnection);
            this.Keyboard = new PinPadKeyboard(this);
            this.Display = new PinPadDisplay(this);
            this.Printer = new PinPadPrinter(this);
            this.Storage = new PinPadStorage(this);
            this.Table = new PinPadTable(this);
            this.Infos = new PinPadInfos(this);
        }
    }
}
