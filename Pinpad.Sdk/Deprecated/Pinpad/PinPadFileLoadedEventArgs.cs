using PinPadSDK.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PinPadSDK.PinPad;

namespace PinPadSDK.PinPad {
    /// <summary>
    /// Event Args for when a file is loaded
    /// </summary>
    public class PinPadFileLoadedEventArgs : EventArgs {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="pinPad">PinPad used</param>
        /// <param name="name">File Name</param>
        /// <param name="path">File Path</param>
        /// <param name="sucessfullyLoaded">Was the file successfully loaded?</param>
        /// <param name="totalLoaded">Amount of files loaded</param>
        /// <param name="totalToLoad">Amount of files to load</param>
        public PinPadFileLoadedEventArgs(PinPadFacade pinPad, string name, string path, bool sucessfullyLoaded, int totalLoaded, int totalToLoad) {
            this.PinPad = pinPad;
            this.Name = name;
            this.Path = path;
            this.SucessfullyLoaded = sucessfullyLoaded;
            this.TotalLoaded = totalLoaded;
            this.TotalToLoad = totalToLoad;
        }

        /// <summary>
        /// PinPad used
        /// </summary>
        public PinPadFacade PinPad { get; private set; }

        /// <summary>
        /// Image Name
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// File Path
        /// </summary>
        public string Path { get; private set; }

        /// <summary>
        /// Was the file successfully loaded?
        /// </summary>
        public bool SucessfullyLoaded { get; private set; }

        /// <summary>
        /// Amount of files loaded
        /// </summary>
        public int TotalLoaded { get; private set; }

        /// <summary>
        /// Amount of files to load
        /// </summary>
        public int TotalToLoad { get; private set; }
    }
}
