using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PinPadSDK.Events {
    /// <summary>
    /// EventArgs for requesting access on a lock
    /// </summary>
    public class AccessRequestedEventArgs : EventArgs {
        /// <summary>
        /// The object that is locked
        /// </summary>
        public readonly object lockedObject;

        /// <summary>
        /// The object that is requesting access over the locked object
        /// </summary>
        public readonly object requestingObject;

        public AccessRequestedEventArgs(object lockedObject, object requestingObject) {
            this.lockedObject = lockedObject;
            this.requestingObject = requestingObject;
        }

        public override int GetHashCode() {
            return lockedObject.GetHashCode();
        }
    }
}
