using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PinPadSDK.Events {
    public class ConnectionStateChangedEventArgs : EventArgs {
        public ConnectionStateChangedEventArgs(bool Connected)
            : base() {
            this.Connected = Connected;
        }
        public bool Connected { get; protected set; }
    }
}
