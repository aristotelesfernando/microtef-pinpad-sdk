using MicroPos.CrossPlatform;
using System;

namespace Pinpad.Sdk.Connection
{
    public abstract class BasePinpadConnection
    {
        /// <summary>
        /// Whether the connection is opened or not.
        /// This property determines wheter something can be sent to the device or not. Should be verified after every attempting to send data to the pinpad.
        /// </summary>
        public abstract bool IsOpen { get; }

        /// <summary>
        /// Legacy Pinpad.Core pinpad connection.
        /// </summary>
        internal IPinpadConnection PlatformPinpadConnection { get; set; }
        /// <summary>
        /// Try connect to pinpad in the specified serial port.
        /// </summary>
        /// <param name="portName">The serial port name.</param>
        /// <exception cref="System.IO.IOException">The port is in an invalid state. - or - An attempt to set the state of the underlying port failed. For example, the parameters passed from this System.IO.Ports.SerialPort object were invalid.</exception>
        public abstract void Open(string portName);
        /// <summary>
        /// Disconnect from the attached serial port.
        /// </summary>
        /// <param name="portName">The serial port name.</param>
        public abstract void Close(string portName);
        /// <summary>
        /// Sends a symbolic stream of bytes, to verify if the connection is OK.
        /// </summary>
        /// <returns>Whether the connection is alive or not.</returns>
        public abstract bool Ping();
    }
}
