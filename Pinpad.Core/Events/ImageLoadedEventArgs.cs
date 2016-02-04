using PinPadSDK.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PinPadSDK.Events {
    public class ImageLoadedEventArgs : EventArgs {
        public ImageLoadedEventArgs(PinPadConnectionController writeTool, string name, string path, bool successfullyLoaded, int totalLoaded, int totalToLoad) {
            this.writeTool = writeTool;
            this.name = name;
            this.path = path;
            this.successfullyLoaded = successfullyLoaded;
            this.totalLoaded = totalLoaded;
            this.totalToLoad = totalToLoad;
        }

        public PinPadConnectionController writeTool { get; private set; }
        public string name { get; private set; }
        public string path { get; private set; }
        public bool successfullyLoaded { get; private set; }
        public int totalLoaded { get; private set; }
        public int totalToLoad { get; private set; }
    }
}
