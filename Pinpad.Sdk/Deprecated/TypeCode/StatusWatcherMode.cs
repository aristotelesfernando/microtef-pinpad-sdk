using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PinPadSDK.Enums {
    /// <summary>
    /// Enumerator for status watcher states
    /// Since undefined is 0 every value will be the actual code plus 1
    /// </summary>
    public enum StatusWatcherMode {
        /// <summary>
        /// Null
        /// </summary>
        Undefined = 0,

        /// <summary>
        /// Ignore the state
        /// </summary>
        Ignore = 1,

        /// <summary>
        /// Watch for insert
        /// </summary>
        WatchInsert = 2,

        /// <summary>
        /// Watch for removal
        /// </summary>
        WatchRemoval = 3,
    }
}
